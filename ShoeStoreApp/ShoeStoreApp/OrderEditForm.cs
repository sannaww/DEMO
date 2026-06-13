using System;
using System.Windows.Forms;
using Npgsql;

namespace ShoeStoreApp
{
    public partial class OrderEditForm : Form
    {
        string cs = "Host=localhost;Port=5432;Database=DEMO;Username=postgres;Password=123123";
        int orderId = 0;

        public OrderEditForm()
        {
            InitializeComponent();
            BackColor = System.Drawing.Color.White;
            Text = "Добавление заказа";
            Load += OrderEditForm_Load;
        }

        public OrderEditForm(int id) : this()
        {
            orderId = id;
            Text = "Редактирование заказа";
        }

        private void OrderEditForm_Load(object sender, EventArgs e)
        {
            LoadStatuses();
            LoadPoints();

            if (orderId != 0)
                LoadOrder();
        }

        void LoadStatuses()
        {
            // Статусы заказа
            comboBoxStatus.Items.Clear();

            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var cmd = new NpgsqlCommand("SELECT name FROM order_statuses ORDER BY name", con);
                var r = cmd.ExecuteReader();

                while (r.Read())
                    comboBoxStatus.Items.Add(r["name"].ToString());
            }
        }

        void LoadPoints()
        {
            // Пункты выдачи
            comboBoxPoint.Items.Clear();

            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();

                var cmd = new NpgsqlCommand("SELECT city || ', ' || street || ', ' || COALESCE(building, '') AS adr FROM pickup_points ORDER BY id", con);
                var r = cmd.ExecuteReader();

                while (r.Read())
                    comboBoxPoint.Items.Add(r["adr"].ToString());
            }
        }

        int GetStatusId()
        {
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var cmd = new NpgsqlCommand("SELECT id FROM order_statuses WHERE name=@n", con);
                cmd.Parameters.AddWithValue("@n", comboBoxStatus.Text);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        int GetPointId()
        {
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var cmd = new NpgsqlCommand("SELECT id FROM pickup_points WHERE city || ', ' || street || ', ' || COALESCE(building, '')=@a", con);
                cmd.Parameters.AddWithValue("@a", comboBoxPoint.Text);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        int GetProductId()
        {
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();
                var cmd = new NpgsqlCommand("SELECT id FROM products WHERE article=@a", con);
                cmd.Parameters.AddWithValue("@a", textBoxArticle.Text);
                object id = cmd.ExecuteScalar();

                if (id == null)
                {
                    MessageBox.Show("Товар с таким артикулом не найден");
                    return 0;
                }

                return Convert.ToInt32(id);
            }
        }

        void LoadOrder()
        {
            // Загрузка заказа
            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();

                string sql = @"
                SELECT o.*, s.name AS status, p.article,
                       pp.city || ', ' || pp.street || ', ' || COALESCE(pp.building, '') AS point
                FROM orders o
                JOIN order_statuses s ON o.status_id = s.id
                JOIN pickup_points pp ON o.pickup_point_id = pp.id
                LEFT JOIN product_in_order po ON po.order_id = o.id
                LEFT JOIN products p ON po.product_id = p.id
                WHERE o.id=@id";

                var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", orderId);

                var r = cmd.ExecuteReader();

                if (r.Read())
                {
                    textBoxArticle.Text = r["article"].ToString();
                    comboBoxStatus.Text = r["status"].ToString();
                    comboBoxPoint.Text = r["point"].ToString();
                    dateTimePickerCreate.Value = Convert.ToDateTime(r["creation_date"]);
                    dateTimePickerDelivery.Value = Convert.ToDateTime(r["delivery_date"]);
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (textBoxArticle.Text == "" || comboBoxStatus.Text == "" || comboBoxPoint.Text == "")
            {
                MessageBox.Show("Заполните все поля");
                return;
            }

            int productId = GetProductId();
            if (productId == 0) return;

            using (var con = new NpgsqlConnection(cs))
            {
                con.Open();

                string sql = orderId == 0
                    ? @"INSERT INTO orders
                    (creation_date, delivery_date, pickup_point_id, user_id, receipt_code, status_id)
                    VALUES (@c,@d,@p,(SELECT id FROM users LIMIT 1),'111',@s) RETURNING id"
                    : @"UPDATE orders SET creation_date=@c, delivery_date=@d,
                    pickup_point_id=@p, status_id=@s WHERE id=@id RETURNING id";

                var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@c", dateTimePickerCreate.Value.Date);
                cmd.Parameters.AddWithValue("@d", dateTimePickerDelivery.Value.Date);
                cmd.Parameters.AddWithValue("@p", GetPointId());
                cmd.Parameters.AddWithValue("@s", GetStatusId());

                if (orderId != 0)
                    cmd.Parameters.AddWithValue("@id", orderId);

                int id = Convert.ToInt32(cmd.ExecuteScalar());

                var del = new NpgsqlCommand("DELETE FROM product_in_order WHERE order_id=@id", con);
                del.Parameters.AddWithValue("@id", id);
                del.ExecuteNonQuery();

                var add = new NpgsqlCommand("INSERT INTO product_in_order(order_id, product_id, amount) VALUES(@o,@p,1)", con);
                add.Parameters.AddWithValue("@o", id);
                add.Parameters.AddWithValue("@p", productId);
                add.ExecuteNonQuery();
            }

            MessageBox.Show("Данные сохранены");
            Close();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}