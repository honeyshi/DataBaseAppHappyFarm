using Npgsql;
using System;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows;

namespace DataBaseApp
{
    public partial class ShowTableOptions : Window
    {
        private readonly string showAnimal = "output_animal";
        private readonly string showCorral = "output_corral";
        private readonly string showBarn = "output_barn";
        private readonly string showFood = "output_food";
        private readonly string showFeeding = "output_feeding";
        private readonly string showSickAnimals = "output_sick_animals";

        public ShowTableOptions()
        {
            InitializeComponent();
            Closing += new CancelEventHandler(ShowTableOptions_Closing);
        }

        private void ShowTableOptions_Closing(Object sender, CancelEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void ExecuteShowCall(string procedureName)
        {
            string connStr = "Server=localhost;Port=5432;User Id=postgres;Password=15postuser*15;DataBase=farmdb";
            NpgsqlConnection npgsql = new NpgsqlConnection(connStr);
            NpgsqlCommand npgsqlCommand = new NpgsqlCommand(procedureName, npgsql);
            npgsqlCommand.CommandType = CommandType.StoredProcedure;

            npgsql.Open();
            npgsqlCommand.Connection = npgsql;
            try
            {
                NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(npgsqlCommand);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                ShowTableWindow showTableWindow = new ShowTableWindow();
                showTableWindow.DataGrid.DataContext = dataTable.DefaultView;
                showTableWindow.Show();
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

        #region Button Click listeners
        private void ShowAnimal_Click(object sender, RoutedEventArgs e)
        {
            ExecuteShowCall(showAnimal);
        }

        private void ShowCorral_Click(object sender, RoutedEventArgs e)
        {
            ExecuteShowCall(showCorral);
        }

        private void ShowBarn_Click(object sender, RoutedEventArgs e)
        {
            ExecuteShowCall(showBarn);
        }

        private void ShowFood_Click(object sender, RoutedEventArgs e)
        {
            ExecuteShowCall(showFood);
        }

        private void ShowFeeding_Click(object sender, RoutedEventArgs e)
        {
            ExecuteShowCall(showFeeding);
        }

        private void ShowSickAnimals_Click(object sender, RoutedEventArgs e)
        {
            ExecuteShowCall(showSickAnimals);
        }
        #endregion
    }
}
