using Npgsql;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace DataBaseApp
{
    /// <summary>
    /// Логика взаимодействия для AddNewDataOption.xaml
    /// </summary>
    public partial class AddNewDataOption : Window
    {
        private readonly string addAnimal = "add_animal";
        private readonly string addCorral = "add_corral";
        private readonly string addBarn = "add_barn";
        private readonly string addFood = "add_food";
        private readonly string addFeeding = "add_feeding";
        private readonly string addSickAnimals = "add_sick_animals";
        private String[] addRequest;

        public AddNewDataOption()
        {
            InitializeComponent();
            Closing += new CancelEventHandler(AddNewDataOption_Closing);
        }

        private void AddNewDataOption_Closing(Object sender, CancelEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void GetTextBoxText()
        {
            string addText = AddTextBox.Text;
            addRequest = addText.Split(' ');
        }

        #region Button Click Listeners
        private void AddTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AddTextBox.Clear();
        }

        private void AddAnimal_Click(object sender, RoutedEventArgs e)
        {
            GetTextBoxText();

            string connStr = "Server=localhost;Port=5432;User Id=postgres;Password=15postuser*15;DataBase=farmdb";
            NpgsqlConnection npgsql = new NpgsqlConnection(connStr);
            NpgsqlCommand npgsqlCommand = new NpgsqlCommand($"CALL {addAnimal}('{addRequest[0]}', {Int32.Parse(addRequest[1])})", npgsql);

            npgsql.Open();
            npgsqlCommand.Connection = npgsql;

            try
            {
                npgsqlCommand.ExecuteNonQuery();
                MessageBox.Show("Data successfully added!");
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

        private void AddCorral_Click(object sender, RoutedEventArgs e)
        {
            GetTextBoxText();

            string connStr = "Server=localhost;Port=5432;User Id=postgres;Password=15postuser*15;DataBase=farmdb";
            NpgsqlConnection npgsql = new NpgsqlConnection(connStr);
            NpgsqlCommand npgsqlCommand = new NpgsqlCommand
                ($"CALL {addCorral}({Int32.Parse(addRequest[0])}, {Int32.Parse(addRequest[1])}, {Int32.Parse(addRequest[2])})", npgsql);

            npgsql.Open();
            npgsqlCommand.Connection = npgsql;

            try
            {
                npgsqlCommand.ExecuteNonQuery();
                MessageBox.Show("Data successfully added!");
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

        private void AddBarn_Click(object sender, RoutedEventArgs e)
        {
            GetTextBoxText();

            string connStr = "Server=localhost;Port=5432;User Id=postgres;Password=15postuser*15;DataBase=farmdb";
            NpgsqlConnection npgsql = new NpgsqlConnection(connStr);
            NpgsqlCommand npgsqlCommand = new NpgsqlCommand
                ($"CALL {addBarn}('{addRequest[0]}')", npgsql);

            npgsql.Open();
            npgsqlCommand.Connection = npgsql;

            try
            {
                npgsqlCommand.ExecuteNonQuery();
                MessageBox.Show("Data successfully added!");
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

        private void AddFood_Click(object sender, RoutedEventArgs e)
        {
            GetTextBoxText();

            string connStr = "Server=localhost;Port=5432;User Id=postgres;Password=15postuser*15;DataBase=farmdb";
            NpgsqlConnection npgsql = new NpgsqlConnection(connStr);
            NpgsqlCommand npgsqlCommand = new NpgsqlCommand
                ($"CALL {addFood}('{addRequest[0]}', {Int32.Parse(addRequest[1])})", npgsql);

            npgsql.Open();
            npgsqlCommand.Connection = npgsql;

            try
            {
                npgsqlCommand.ExecuteNonQuery();
                MessageBox.Show("Data successfully added!");
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

        private void AddFeeding_Click(object sender, RoutedEventArgs e)
        {
            GetTextBoxText();

            string connStr = "Server=localhost;Port=5432;User Id=postgres;Password=15postuser*15;DataBase=farmdb";
            NpgsqlConnection npgsql = new NpgsqlConnection(connStr);
            NpgsqlCommand npgsqlCommand = new NpgsqlCommand
                ($"CALL {addFeeding}({Int32.Parse(addRequest[0])}, {Int32.Parse(addRequest[1])}, {Int32.Parse(addRequest[2])}, {Int32.Parse(addRequest[3])})", npgsql);

            npgsql.Open();
            npgsqlCommand.Connection = npgsql;

            try
            {
                npgsqlCommand.ExecuteNonQuery();
                MessageBox.Show("Data successfully added!");
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

        private void AddSickAnimals_Click(object sender, RoutedEventArgs e)
        {
            GetTextBoxText();

            string connStr = "Server=localhost;Port=5432;User Id=postgres;Password=15postuser*15;DataBase=farmdb";
            NpgsqlConnection npgsql = new NpgsqlConnection(connStr);
            NpgsqlCommand npgsqlCommand = new NpgsqlCommand
                ($"CALL {addSickAnimals}({Int32.Parse(addRequest[0])}, '{addRequest[1]}', {bool.Parse(addRequest[2])}, {bool.Parse(addRequest[3])})", npgsql);

            npgsql.Open();
            npgsqlCommand.Connection = npgsql;

            try
            {
                npgsqlCommand.ExecuteNonQuery();
                MessageBox.Show("Data successfully added!");
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
