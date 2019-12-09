using Npgsql;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace DataBaseApp
{
    /// <summary>
    /// Логика взаимодействия для DeleteByTypeOption.xaml
    /// </summary>
    public partial class DeleteByTypeOption : Window
    {
        private string animalType;
        public DeleteByTypeOption()
        {
            InitializeComponent();
            Closing += new CancelEventHandler(DeleteByTypeOption_Closing);
        }

        private void DeleteByTypeOption_Closing(Object sender, CancelEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void ExecuteDeleteCall()
        {
            animalType = DeleteTextBox.Text;

            string connStr = "Server=localhost;Port=5432;User Id=postgres;Password=15postuser*15;DataBase=farmdb";
            NpgsqlConnection npgsql = new NpgsqlConnection(connStr);
            NpgsqlCommand npgsqlCommand = new NpgsqlCommand($"CALL delete_feeding_by_animal('{animalType}')", npgsql);

            npgsql.Open();
            npgsqlCommand.Connection = npgsql;

            try
            {
                npgsqlCommand.ExecuteNonQuery();
                MessageBox.Show("Data successfully deleted!");
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

        private void DeleteTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DeleteTextBox.Clear();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            ExecuteDeleteCall();
        }
    }
}
