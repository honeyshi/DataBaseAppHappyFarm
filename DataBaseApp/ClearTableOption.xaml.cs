using Npgsql;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows;

namespace DataBaseApp
{
    public partial class ClearTableOption : Window
    {
        private readonly string deleteAnimal = "delete_animal()";
        private readonly string deleteCorral = "delete_corral()";
        private readonly string deleteBarn = "delete_barn()";
        private readonly string deleteFood = "delete_food()";
        private readonly string deleteFeeding = "delete_feeding()";
        private readonly string deleteSickAnimals = "delete_sick_animals()";
        private readonly string deleteAll = "clear_all()";
        public ClearTableOption()
        {
            InitializeComponent();
            Closing += new CancelEventHandler(ClearTableOption_Closing);
        }

        private void ClearTableOption_Closing(Object sender, CancelEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void ExecuteDeleteCall(string procedureName)
        {
            string connStr = "Server=localhost;Port=5432;User Id=postgres;Password=15postuser*15;DataBase=farmdb";
            NpgsqlConnection npgsql = new NpgsqlConnection(connStr);
            NpgsqlCommand npgsqlCommand = new NpgsqlCommand($"CALL {procedureName}", npgsql);

            npgsql.Open();
            npgsqlCommand.Connection = npgsql;

            try
            {
                npgsqlCommand.ExecuteNonQuery();
                MessageBox.Show("Success!");
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
        private void ClearAnimal_Click(object sender, RoutedEventArgs e)
        {
            ExecuteDeleteCall(deleteAnimal);
        }

        private void ClearCorral_Click(object sender, RoutedEventArgs e)
        {
            ExecuteDeleteCall(deleteCorral);
        }

        private void ClearBarn_Click(object sender, RoutedEventArgs e)
        {
            ExecuteDeleteCall(deleteBarn);
        }

        private void ClearFood_Click(object sender, RoutedEventArgs e)
        {
            ExecuteDeleteCall(deleteFood);
        }

        private void ClearFeeding_Click(object sender, RoutedEventArgs e)
        {
            ExecuteDeleteCall(deleteFeeding);
        }

        private void ClearSickAnimals_Click(object sender, RoutedEventArgs e)
        {
            ExecuteDeleteCall(deleteSickAnimals);
        }

        private void ClearAll_Click(object sender, RoutedEventArgs e)
        {
            ExecuteDeleteCall(deleteAll);
        }
        #endregion
    }
}
