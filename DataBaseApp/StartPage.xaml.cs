using System.Windows;
using Npgsql;

namespace DataBaseApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        private readonly string nameDb = "farmDb";
        public StartWindow()
        {
            InitializeComponent();
        }

        private void Button_CreateDb(object sender, RoutedEventArgs e)
        {
            string connStr = "Server=localhost;Port=5432;User Id=postgres;Password=test2012!;";
            NpgsqlConnection npgsql = new NpgsqlConnection(connStr);          
            NpgsqlCommand npgsqlCommand = new NpgsqlCommand(
                $@"
                CREATE DATABASE {nameDb}
                WITH OWNER = postgres
                ENCODING = 'UTF8'
                CONNECTION LIMIT = -1;
                ", npgsql);

            npgsql.Open();
            npgsqlCommand.Connection = npgsql;
            try
            {
                npgsqlCommand.ExecuteNonQuery();
                MessageBox.Show($"Database with name {nameDb} is successfully created");
            }
            catch (PostgresException ex)
            {
                MessageBox.Show(ex.MessageText);
            }
            finally
            {
                npgsql.Close();
            }
        }

        private void Button_HaveDb(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
