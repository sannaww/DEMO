using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Npgsql;

namespace ShoeStoreApp
{
    public partial class ProductsForm : Form
    {
        string cs = "Host=localhost;Port=5432;Database=DEMO;Username=postgres;Password=123123";
        string userName, userRole;

        public ProductsForm(string name, string role)
        {
            InitializeComponent();

            userName = name;
            userRole = role;
            BackColor = Color.White;

            Load += ProductsForm_Load;

            textBoxSearch.TextChanged += FilterChanged;
            comboBoxProviderFilter.SelectedIndexChanged += FilterChanged;
            comboBoxSort.SelectedIndexChanged += FilterChanged;
        }

        private void ProductsForm_Load(object sender, EventArgs e)
        {
            labelUser.Text = "Пользователь: " + userName + " (" + userRole + ")";
            buttonAddProduct.Visible = userRole == "Администратор";
            buttonOrders.Visible = userRole == "Администратор" || userRole == "Менеджер";

            bool filterVisible = userRole == "Администратор" || userRole == "Менеджер";

            labelSearch.Visible = textBoxSearch.Visible = filterVisible;
            labelProvider.Visible = comboBoxProviderFilter.Visible = filterVisible;
            labelSort.Visible = comboBoxSort.Visible = filterVisible;

            comboBoxSort.Items.Clear();
            comboBoxSort.Items.Add("Без сортировки");
            comboBoxSort.Items.Add("Кол-во ↑");
            comboBoxSort.Items.Add("Кол-во ↓");
            comboBoxSort.SelectedIndex = 0;

            LoadProviders();
            LoadProducts();
        }

        void FilterChanged(object sender, EventArgs e)
        {
            LoadProducts();
        }

        void LoadProviders()
        {
            comboBoxProviderFilter.Items.Clear();
            comboBoxProviderFilter.Items.Add("Все поставщики");

            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();

                var cmd = new NpgsqlCommand("SELECT name FROM providers ORDER BY name", con);
                var r = cmd.ExecuteReader();

                while (r.Read())
                    comboBoxProviderFilter.Items.Add(r["name"].ToString());
            }

            comboBoxProviderFilter.SelectedIndex = 0;
        }

        void LoadProducts()
        {
            string search = textBoxSearch.Text.Trim().ToLower();
            string provider = comboBoxProviderFilter.Text;

            string sql = @"
            SELECT p.id, p.photo, p.price, p.unit, p.amount_in_stock, p.discount,
                   pn.name AS product_name, c.name AS category, p.description,
                   pr.name AS producer, pv.name AS provider
            FROM products p
            JOIN product_names pn ON p.product_name_id = pn.id
            JOIN categories c ON p.category_id = c.id
            JOIN producers pr ON p.producer_id = pr.id
            JOIN providers pv ON p.provider_id = pv.id
            WHERE
            (@search = '' OR LOWER(p.article || ' ' || p.unit || ' ' || p.description || ' ' ||
             pn.name || ' ' || c.name || ' ' || pr.name || ' ' || pv.name) LIKE @like)
            AND (@provider = 'Все поставщики' OR pv.name = @provider)";

            if (comboBoxSort.Text == "Кол-во ↑")
                sql += " ORDER BY p.amount_in_stock ASC";
            else if (comboBoxSort.Text == "Кол-во ↓")
                sql += " ORDER BY p.amount_in_stock DESC";
            else
                sql += " ORDER BY p.id";

            DataTable dt = new DataTable();

            using (var con = new NpgsqlConnection(cs))
            {
                var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@search", search);
                cmd.Parameters.AddWithValue("@like", "%" + search + "%");
                cmd.Parameters.AddWithValue("@provider", provider);

                new NpgsqlDataAdapter(cmd).Fill(dt);
            }

            flowProducts.Controls.Clear();

            foreach (DataRow row in dt.Rows)
                AddCard(row);
        }

        void AddCard(DataRow row)
        {
            int id = Convert.ToInt32(row["id"]);
            decimal price = Convert.ToDecimal(row["price"]);
            decimal sale = Convert.ToDecimal(row["discount"]);
            decimal count = Convert.ToDecimal(row["amount_in_stock"]);
            decimal newPrice = price - price * sale / 100;

            Panel card = new Panel();
            card.Size = new Size(900, 150);
            card.Margin = new Padding(10);
            card.BorderStyle = BorderStyle.FixedSingle;
            card.BackColor = count <= 0 ? Color.LightBlue : sale > 15 ? Color.FromArgb(46, 139, 87) : Color.White;

            PictureBox pic = new PictureBox();
            pic.Bounds = new Rectangle(10, 20, 120, 90);
            pic.SizeMode = PictureBoxSizeMode.Zoom;
            pic.Image = GetImage(row["photo"].ToString());

            Label info = new Label();
            info.Bounds = new Rectangle(150, 10, 520, 130);
            info.BackColor = Color.Transparent;
            info.Font = new Font("Times New Roman", 11);
            info.Text =
                row["category"] + " | " + row["product_name"] + "\n" +
                row["description"] + "\n" +
                "Производитель: " + row["producer"] + "\n" +
                "Поставщик: " + row["provider"] + "\n" +
                "Цена: " + price.ToString("0.00") + " руб.\n" +
                "Ед. изм.: " + row["unit"] + "   Кол-во: " + count;

            Label saleText = new Label();
            saleText.Bounds = new Rectangle(700, 35, 160, 70);
            saleText.BackColor = Color.Transparent;
            saleText.TextAlign = ContentAlignment.MiddleCenter;
            saleText.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            saleText.Text = sale > 0
                ? "Скидка: " + sale + "%\n" + newPrice.ToString("0.00") + " руб."
                : "Скидка: 0%";

            if (sale > 15 && count > 0)
            {
                info.ForeColor = Color.White;
                saleText.ForeColor = Color.White;
            }

            card.Controls.Add(pic);
            card.Controls.Add(info);
            card.Controls.Add(saleText);

            if (userRole == "Администратор")
            {
                Button del = new Button();
                del.Text = "Удалить";
                del.Bounds = new Rectangle(760, 110, 100, 30);
                del.BackColor = Color.FromArgb(0, 250, 154);
                del.Click += (s, e) => DeleteProduct(id);

                card.Click += (s, e) => EditProduct(id);
                pic.Click += (s, e) => EditProduct(id);
                info.Click += (s, e) => EditProduct(id);
                saleText.Click += (s, e) => EditProduct(id);

                card.Controls.Add(del);
            }

            flowProducts.Controls.Add(card);
        }

        Image GetImage(string photo)
        {
            string path = Path.Combine(Application.StartupPath, photo.Replace("/", "\\"));

            if (!File.Exists(path))
                path = Path.Combine(Application.StartupPath, "Res\\picture.png");

            return Image.FromFile(path);
        }

        void EditProduct(int id)
        {
            new ProductEditForm(id).ShowDialog();
            LoadProducts();
        }

        void DeleteProduct(int id)
        {
            if (MessageBox.Show("Удалить товар?", "Подтверждение", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();

                var check = new NpgsqlCommand("SELECT COUNT(*) FROM product_in_order WHERE product_id=@id", con);
                check.Parameters.AddWithValue("@id", id);

                if (Convert.ToInt32(check.ExecuteScalar()) > 0)
                {
                    MessageBox.Show("Нельзя удалить товар, который есть в заказе");
                    return;
                }

                var cmd = new NpgsqlCommand("DELETE FROM products WHERE id=@id", con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }

            LoadProducts();
        }

        private void buttonAddProduct_Click_1(object sender, EventArgs e)
        {
            new ProductEditForm().ShowDialog();
            LoadProducts();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonOrders_Click(object sender, EventArgs e)
        {
            new OrdersForm(userName, userRole).ShowDialog();
        }
    }
}