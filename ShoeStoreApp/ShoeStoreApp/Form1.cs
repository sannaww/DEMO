using Npgsql;

namespace ShoeStoreApp
{
    public partial class Form1 : Form
    {
        string cs = "Host=localhost;Port=5432;Database=DEMO;Username=postgres;Password=123123";

        public Form1()
        {
            InitializeComponent();
            this.Font = new Font("Times New Roman", 12);
            this.BackColor = Color.White;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            // Создаем подключение к базе данных
            using var con = new NpgsqlConnection(cs);
            // Открываем подключение
            con.Open();
            // SQL-запрос для проверки логина и пароля пользователя. получаем фио пользователя и его роль
            string sql = @"SELECT 
                    users.surname || ' ' || users.name || ' ' || COALESCE(users.patronymic, '') AS full_name,
                    roles.name AS role_name
                FROM users
                JOIN roles ON users.role_id = roles.id
                WHERE login=@login AND password=@password";

            // Создаем команду для выполнения SQL-запроса
            using var cmd = new NpgsqlCommand(sql, con);
            // Передаем логин из текстового поля в параметр запроса
            cmd.Parameters.AddWithValue("@login", textBoxLogin.Text);
            // Передаем пароль из текстового поля в параметр запроса
            cmd.Parameters.AddWithValue("@password", textBoxPassword.Text);
            // Выполняем запрос и получаем результат
            using var reader = cmd.ExecuteReader();
            
            // Если пользователь найден
            if (reader.Read())
            {
                // Получаем ФИО пользователя из результата запроса
                string name = reader["full_name"].ToString() ?? "";
                // Получаем роль пользователя из результата запроса
                string role = reader["role_name"].ToString() ?? "";
                // Скрываем форму авторизации
                this.Hide();
                // Открываем форму со списком товаров и передаем туда имя и роль пользователя
                new ProductsForm(name, role).ShowDialog();
                // После закрытия формы товаров снова показываем форму авторизации
                this.Show();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }
        private void buttonGuest_Click(object sender, EventArgs e)
        {
            // Скрываем форму авторизации
            this.Hide();
            // Открываем форму товаров как гость
            new ProductsForm("Гость", "Гость").ShowDialog();
            // После закрытия формы товаров снова показываем форму авторизации
            this.Show();
        }
    }
}