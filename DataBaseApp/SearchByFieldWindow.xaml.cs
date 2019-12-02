using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DataBaseApp
{
    /// <summary>
    /// Логика взаимодействия для SearchByFieldWindow.xaml
    /// </summary>
    public partial class SearchByFieldWindow : Window
    {
        private readonly string searchAnimal = "search_animal";
        public SearchByFieldWindow()
        {
            InitializeComponent();
            Closing += new CancelEventHandler(SearchByFieldWindow_Closing);
        }

        private void SearchByFieldWindow_Closing(Object sender, CancelEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void ExecuteSearchCall(string searchRequest)
        {
            string connStr = "Server=localhost;Port=5432;User Id=postgres;Password=15postuser*15;DataBase=farmdb";
            NpgsqlConnection npgsql = new NpgsqlConnection(connStr);
            NpgsqlCommand npgsqlCommand = new NpgsqlCommand(searchAnimal, npgsql);
            npgsqlCommand.CommandType = CommandType.StoredProcedure;
            npgsqlCommand.Parameters.AddWithValue("search_type", searchRequest);

            npgsql.Open();
            npgsqlCommand.Connection = npgsql;

            try
            {
                NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(npgsqlCommand);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                SearchResultGrid.DataContext = dataTable.DefaultView;
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

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchInput = SearchTextBox.Text;
            ExecuteSearchCall(searchInput);
        }

        private void SearchTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SearchTextBox.Clear();
        }
    }
}
