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
        }
        private void ProductsForm_Load(object sender, EventArgs e)
        {
            labelUser.Text = "Пользователь: " + userName + " (" + userRole + ")";
            buttonAddProduct.Visible = userRole == "Администратор";
            LoadProducts();
        }
        void LoadProducts()
        {
            // Загружаем товары
            string sql = @"
            SELECT p.id, p.photo, p.price, p.unit, p.amount_in_stock, p.discount,
                   pn.name AS product_name, c.name AS category, p.description,
                   pr.name AS producer, pv.name AS provider
            FROM products p
            JOIN product_names pn ON p.product_name_id = pn.id
            JOIN categories c ON p.category_id = c.id
            JOIN producers pr ON p.producer_id = pr.id
            JOIN providers pv ON p.provider_id = pv.id
            ORDER BY p.id";
            DataTable dt = new DataTable();
            using (var con = new NpgsqlConnection(cs))
                new NpgsqlDataAdapter(sql, con).Fill(dt);
            flowProducts.Controls.Clear();
            foreach (DataRow row in dt.Rows)
                AddCard(row);
        }
        void AddCard(DataRow row)
        {
            // Данные товара
            int id = Convert.ToInt32(row["id"]);
            decimal price = Convert.ToDecimal(row["price"]);
            decimal sale = Convert.ToDecimal(row["discount"]);
            decimal count = Convert.ToDecimal(row["amount_in_stock"]);
            decimal newPrice = price - price * sale / 100;
            // Карточка
            Panel card = new Panel();
            card.Size = new Size(850, 150);
            card.Margin = new Padding(10);
            card.BorderStyle = BorderStyle.FixedSingle;
            card.BackColor = count <= 0 ? Color.LightBlue : sale > 15 ? Color.FromArgb(46, 139, 87) : Color.White;
            // Фото
            PictureBox pic = new PictureBox();
            pic.Bounds = new Rectangle(10, 20, 110, 90);
            pic.SizeMode = PictureBoxSizeMode.Zoom;
            pic.Image = GetImage(row["photo"].ToString());
            // Информация
            Label info = new Label();
            info.Bounds = new Rectangle(140, 10, 500, 130);
            info.BackColor = Color.Transparent;
            info.Font = new Font("Times New Roman", 11);
            info.Text =
                row["category"] + " | " + row["product_name"] + "\n" +
                row["description"] + "\n" +
                "Производитель: " + row["producer"] + "\n" +
                "Поставщик: " + row["provider"] + "\n" +
                "Цена: " + price.ToString("0.00") + " руб.\n" +
                "Ед. изм.: " + row["unit"] + "   Кол-во: " + count;
            // Скидка
            Label saleText = new Label();
            saleText.Bounds = new Rectangle(660, 35, 140, 70);
            saleText.BackColor = Color.Transparent;
            saleText.TextAlign = ContentAlignment.MiddleCenter;
            saleText.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            saleText.Text = sale > 0 ? "Скидка: " + sale + "%\n" + newPrice.ToString("0.00") + " руб." : "Скидка: 0%";
            // Белый текст на зелёной карточке
            if (sale > 15 && count > 0)
            {
                info.ForeColor = Color.White;
                saleText.ForeColor = Color.White;
            }
            card.Controls.Add(pic);
            card.Controls.Add(info);
            card.Controls.Add(saleText);
            // Функции администратора
            if (userRole == "Администратор")
            {
                Button del = new Button();
                del.Text = "Удалить";
                del.Bounds = new Rectangle(720, 110, 100, 30);
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
            // Фото или заглушка
            string path = Path.Combine(Application.StartupPath, photo.Replace("/", "\\"));
            if (!File.Exists(path))
                path = Path.Combine(Application.StartupPath, "Res\\picture.png");
            return Image.FromFile(path);
        }
        void EditProduct(int id)
        {
            // Открываем редактирование
            new ProductEditForm(id).ShowDialog();
            LoadProducts();
        }
        void DeleteProduct(int id)
        {
            // Удаление товара
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
            // Открываем добавление
            new ProductEditForm().ShowDialog();
            LoadProducts();
        }
        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}