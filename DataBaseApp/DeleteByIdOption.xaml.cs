using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для DeleteByIdOption.xaml
    /// </summary>
    public partial class DeleteByIdOption : Window
    {
        private readonly string deleteAnimal = "delete_animal_id";
        private readonly string deleteCorral = "delete_corral_id";
        private readonly string deleteBarn = "delete_barn_id";
        private readonly string deleteFood = "delete_food_id";
        private readonly string deleteFeeding = "delete_feeding_id";
        private readonly string deleteSickAnimals = "delete_sick_animals_id";
        private int Id;
        public DeleteByIdOption()
        {
            InitializeComponent();
            Closing += new CancelEventHandler(DeleteByIdOption_Closing);
        }

        private void DeleteByIdOption_Closing(Object sender, CancelEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
        private void GetTextBoxText()
        {
            Id = int.Parse(DeleteTextBox.Text);
        }

        private void ExecuteDeleteByIdCall(string procedureName)
        {
            GetTextBoxText();

            string connStr = "Server=localhost;Port=5432;User Id=postgres;Password=15postuser*15;DataBase=farmdb";
            NpgsqlConnection npgsql = new NpgsqlConnection(connStr);
            NpgsqlCommand npgsqlCommand = new NpgsqlCommand($"CALL {procedureName}({Id})", npgsql);

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

        #region Button click listeners
        private void DeleteTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DeleteTextBox.Clear();
        }

        private void DeleteAnimal_Click(object sender, RoutedEventArgs e)
        {
            ExecuteDeleteByIdCall(deleteAnimal);
        }

        private void DeleteCorral_Click(object sender, RoutedEventArgs e)
        {
            ExecuteDeleteByIdCall(deleteCorral);
        }

        private void DeleteBarn_Click(object sender, RoutedEventArgs e)
        {
            ExecuteDeleteByIdCall(deleteBarn);
        }

        private void DeleteFood_Click(object sender, RoutedEventArgs e)
        {
            ExecuteDeleteByIdCall(deleteFood);
        }

        private void DeleteFeeding_Click(object sender, RoutedEventArgs e)
        {
            ExecuteDeleteByIdCall(deleteFeeding);
        }

        private void DeleteSickAnimals_Click(object sender, RoutedEventArgs e)
        {
            ExecuteDeleteByIdCall(deleteSickAnimals);
        }
        #endregion
    }
}
