using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Npgsql;

namespace ShoeStoreApp
{
    public partial class ProductEditForm : Form
    {
        string cs = "Host=localhost; Port=5432; Database=DEMO; Username=postgres; Password=123123";
        int productId = 0; // 0 - добавление, не 0 - редактирование
        string photo = "Res/picture.png"; // фото по умолчанию
        public ProductEditForm()
        {
            InitializeComponent();
            Font = new Font("Times New Roman", 12);
            BackColor = Color.White;
        }
        public ProductEditForm(int id) : this()
        {
            productId = id;
            Text = "Редактирование товара";
        }
        private void ProductEditForm_Load(object sender, EventArgs e)
        {
            // Загружаем данные в выпадающие списки
            LoadCombo(comboBoxName, "product_names");
            LoadCombo(comboBoxCategory, "categories");
            LoadCombo(comboBoxProducer, "producers");
            LoadCombo(comboBoxProvider, "providers");

            // Если редактируем товар, загружаем его данные
            if (productId != 0) LoadProduct();
        }
        void LoadCombo(ComboBox box, string table)
        {
            // Заполняем ComboBox из таблицы
            var dt = new DataTable();
            new NpgsqlDataAdapter($"SELECT id, name FROM {table} ORDER BY name", new NpgsqlConnection(cs)).Fill(dt);

            box.DataSource = dt;
            box.DisplayMember = "name";
            box.ValueMember = "id";
        }
        void LoadProduct()
        {
            using var con = new NpgsqlConnection(cs);
            con.Open();
            using var cmd = new NpgsqlCommand("SELECT * FROM products WHERE id=@id", con);
            cmd.Parameters.AddWithValue("@id", productId);
            using var r = cmd.ExecuteReader();

            if (r.Read())
            {
                // Заполняем поля данными товара
                textBoxArticle.Text = r["article"].ToString();
                comboBoxName.SelectedValue = r["product_name_id"];
                comboBoxCategory.SelectedValue = r["category_id"];
                comboBoxProducer.SelectedValue = r["producer_id"];
                comboBoxProvider.SelectedValue = r["provider_id"];
                textBoxPrice.Text = r["price"].ToString();
                textBoxUnit.Text = r["unit"].ToString();
                textBoxCount.Text = r["amount_in_stock"].ToString();
                textBoxDiscount.Text = r["discount"].ToString();
                textBoxDescription.Text = r["description"].ToString();
                photo = r["photo"].ToString();
            }
        }
        private void buttonSave_Click_1(object sender, EventArgs e)
        {
            // Проверяем обязательные поля
            if (textBoxArticle.Text == "" || textBoxPrice.Text == "" || textBoxCount.Text == "")
            {
                MessageBox.Show("Заполните обязательные поля");
                return;
            }
            // Проверяем числа
            if (!decimal.TryParse(textBoxPrice.Text, out decimal price) ||
                !decimal.TryParse(textBoxCount.Text, out decimal count) ||
                !decimal.TryParse(textBoxDiscount.Text, out decimal discount))
            {
                MessageBox.Show("Цена, количество и скидка должны быть числами");
                return;
            }
            // Проверяем, чтобы значения не были отрицательными
            if (price < 0 || count < 0 || discount < 0)
            {
                MessageBox.Show("Значения не могут быть отрицательными");
                return;
            }
            using var con = new NpgsqlConnection(cs);
            con.Open();
            // Если productId = 0, добавляем товар, иначе редактируем
            string sql = productId == 0 ? @"
                INSERT INTO products
                (article, product_name_id, unit, price, provider_id, producer_id, category_id, discount, amount_in_stock, description, photo)
                VALUES
                (@article, @name, @unit, @price, @provider, @producer, @category, @discount, @count, @description, @photo)"
                            : @"
                UPDATE products SET
                article=@article, product_name_id=@name, unit=@unit, price=@price,
                provider_id=@provider, producer_id=@producer, category_id=@category,
                discount=@discount, amount_in_stock=@count, description=@description, photo=@photo
                WHERE id=@id";

            using var cmd = new NpgsqlCommand(sql, con);
            // Короткая функция для добавления параметров
            void P(string name, object value) => cmd.Parameters.AddWithValue(name, value);

            P("@article", textBoxArticle.Text);
            P("@name", comboBoxName.SelectedValue);
            P("@unit", textBoxUnit.Text);
            P("@price", price);
            P("@provider", comboBoxProvider.SelectedValue);
            P("@producer", comboBoxProducer.SelectedValue);
            P("@category", comboBoxCategory.SelectedValue);
            P("@discount", discount);
            P("@count", count);
            P("@description", textBoxDescription.Text);
            P("@photo", photo);

            if (productId != 0) P("@id", productId);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Данные сохранены");
            Close();
        }

        private void buttonPhoto_Click_1(object sender, EventArgs e)
        {
            // Выбор фото товара
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Картинки|*.jpg;*.png;*.jpeg";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                photo = "Res/" + System.IO.Path.GetFileName(dialog.FileName);
                MessageBox.Show("Фото выбрано. Файл должен быть в папке Res.");
            }
        }
        private void buttonBack_Click_1(object sender, EventArgs e)
        {
            Close();
        }
    }
}