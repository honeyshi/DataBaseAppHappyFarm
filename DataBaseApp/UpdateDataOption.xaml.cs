using Npgsql;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace DataBaseApp
{
    /// <summary>
    /// Логика взаимодействия для UpdateDataOption.xaml
    /// </summary>
    public partial class UpdateDataOption : Window
    {

        private readonly string updateAnimal = "update_animal";
        private readonly string updateCorral = "update_corral";
        private readonly string updateBarn = "update_barn";
        private readonly string updateFood = "update_food";
        private readonly string updateFeeding = "update_feeding";
        private readonly string updateSickAnimals = "update_sick_animals";
        private string[] updateRequest;


        public UpdateDataOption()
        {
            InitializeComponent();
            Closing += new CancelEventHandler(UpdateDataOption_Closing);
        }

        private void UpdateDataOption_Closing(Object sender, CancelEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void GetTextBoxText()
        {
            string updateText = UpdateTextBox.Text;
            updateRequest = updateText.Split(' ');
        }

        #region Button click listeners
        private void UpdateTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UpdateTextBox.Clear();
        }

        private void UpdateAnimal_Click(object sender, RoutedEventArgs e)
        {
            GetTextBoxText();

            string connStr = "Server=localhost;Port=5432;User Id=postgres;Password=15postuser*15;DataBase=farmdb";
            NpgsqlConnection npgsql = new NpgsqlConnection(connStr);
            NpgsqlCommand npgsqlCommand = new NpgsqlCommand(
                $"CALL {updateAnimal}({int.Parse(updateRequest[0])}, '{updateRequest[1]}', {int.Parse(updateRequest[2])})", npgsql);

            npgsql.Open();
            npgsqlCommand.Connection = npgsql;

            try
            {
                npgsqlCommand.ExecuteNonQuery();
                MessageBox.Show("Data successfully updated!");
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

        private void UpdateCorral_Click(object sender, RoutedEventArgs e)
        {
            GetTextBoxText();

            string connStr = "Server=localhost;Port=5432;User Id=postgres;Password=15postuser*15;DataBase=farmdb";
            NpgsqlConnection npgsql = new NpgsqlConnection(connStr);
            NpgsqlCommand npgsqlCommand = new NpgsqlCommand(
                $"CALL {updateCorral}({int.Parse(updateRequest[0])}, {int.Parse(updateRequest[1])}, {int.Parse(updateRequest[2])}, {int.Parse(updateRequest[3])})", npgsql);

            npgsql.Open();
            npgsqlCommand.Connection = npgsql;

            try
            {
                npgsqlCommand.ExecuteNonQuery();
                MessageBox.Show("Data successfully updated!");
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

        private void UpdateBarn_Click(object sender, RoutedEventArgs e)
        {
            GetTextBoxText();

            string connStr = "Server=localhost;Port=5432;User Id=postgres;Password=15postuser*15;DataBase=farmdb";
            NpgsqlConnection npgsql = new NpgsqlConnection(connStr);
            NpgsqlCommand npgsqlCommand = new NpgsqlCommand(
                $"CALL {updateBarn}({int.Parse(updateRequest[0])}, '{updateRequest[1]}')", npgsql);

            npgsql.Open();
            npgsqlCommand.Connection = npgsql;

            try
            {
                npgsqlCommand.ExecuteNonQuery();
                MessageBox.Show("Data successfully updated!");
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

        private void UpdateFood_Click(object sender, RoutedEventArgs e)
        {
            GetTextBoxText();

            string connStr = "Server=localhost;Port=5432;User Id=postgres;Password=15postuser*15;DataBase=farmdb";
            NpgsqlConnection npgsql = new NpgsqlConnection(connStr);
            NpgsqlCommand npgsqlCommand = new NpgsqlCommand(
                $"CALL {updateFood}({int.Parse(updateRequest[0])}, '{updateRequest[1]}', {int.Parse(updateRequest[2])})", npgsql);

            npgsql.Open();
            npgsqlCommand.Connection = npgsql;

            try
            {
                npgsqlCommand.ExecuteNonQuery();
                MessageBox.Show("Data successfully updated!");
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

        private void UpdateFeeding_Click(object sender, RoutedEventArgs e)
        {
            GetTextBoxText();

            string connStr = "Server=localhost;Port=5432;User Id=postgres;Password=15postuser*15;DataBase=farmdb";
            NpgsqlConnection npgsql = new NpgsqlConnection(connStr);
            NpgsqlCommand npgsqlCommand = new NpgsqlCommand(
                $"CALL {updateFeeding}({int.Parse(updateRequest[0])}, {int.Parse(updateRequest[1])}, {int.Parse(updateRequest[2])}, {int.Parse(updateRequest[3])}, {int.Parse(updateRequest[4])})", npgsql);

            npgsql.Open();
            npgsqlCommand.Connection = npgsql;

            try
            {
                npgsqlCommand.ExecuteNonQuery();
                MessageBox.Show("Data successfully updated!");
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

        private void UpdateSickAnimals_Click(object sender, RoutedEventArgs e)
        {
            GetTextBoxText();

            string connStr = "Server=localhost;Port=5432;User Id=postgres;Password=15postuser*15;DataBase=farmdb";
            NpgsqlConnection npgsql = new NpgsqlConnection(connStr);
            NpgsqlCommand npgsqlCommand = new NpgsqlCommand(
                $"CALL {updateSickAnimals}({int.Parse(updateRequest[0])}, {int.Parse(updateRequest[1])}, '{updateRequest[2]}', {bool.Parse(updateRequest[3])}, {bool.Parse(updateRequest[4])})", npgsql);

            npgsql.Open();
            npgsqlCommand.Connection = npgsql;

            try
            {
                npgsqlCommand.ExecuteNonQuery();
                MessageBox.Show("Data successfully updated!");
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
        #endregion
    }
}
