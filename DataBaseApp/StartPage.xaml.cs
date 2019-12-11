using System;
using System.Data;
using System.IO;
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
        private readonly string createBarn = "create_barn()";
        private readonly string createCorral = "create_corral()";
        private readonly string createAnimal = "create_animal()";
        private readonly string createFood = "create_food()";
        private readonly string createFeeding = "create_feeding()";
        private readonly string createSickAnimal = "create_sick_animals()";

        public StartWindow()
        {
            InitializeComponent();
        }

        private void Button_CreateDb(object sender, RoutedEventArgs e)
        {
            string connStr = "Server=localhost;Port=5432;User Id=postgres;Password=15postuser*15;";
            NpgsqlConnection npgsql = new NpgsqlConnection(connStr);
            NpgsqlCommand npgsqlCommand = new NpgsqlCommand(
                $@"
                CREATE OR REPLACE FUNCTION CREATE_DB(NAME TEXT)
                RETURNS INTEGER
                AS $$
                BEGIN 
                PERFORM DBLINK_EXEC('dbname=postgres user=postgres password=15postuser*15', 'CREATE DATABASE ' || NAME);
                RETURN 1; 
                END; $$ 
                LANGUAGE PLPGSQL;

                CREATE OR REPLACE FUNCTION DROP_DB(NAME TEXT)
                RETURNS INTEGER
                AS $$
                BEGIN PERFORM DBLINK_EXEC('dbname=postgres user=postgres password=15postuser*15', 'DROP DATABASE ' || NAME);
                RETURN 1; 
                END; $$ 
                LANGUAGE PLPGSQL;
                ", npgsql);

            NpgsqlCommand createDb = new NpgsqlCommand("CREATE_DB", npgsql);
            createDb.CommandType = CommandType.StoredProcedure;
            createDb.Parameters.AddWithValue("name", nameDb);

            npgsql.Open();
            npgsqlCommand.Connection = npgsql;
            try
            {
                npgsqlCommand.ExecuteNonQuery();
                createDb.ExecuteNonQuery();
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
            MessageBox.Show("Create procedure for tables creation");
            CreateAllStoredProcedures("dbscript.sql");
            MessageBox.Show("Create tables");
            CreateTable(createBarn);
            CreateTable(createCorral);
            CreateTable(createAnimal);
            CreateTable(createFood);
            CreateTable(createFeeding);
            CreateTable(createSickAnimal);
            MessageBox.Show("Create all procedures");
            CreateAllStoredProcedures("dbscriptAll.sql");
            MessageBox.Show("Add values in database");
            CreateAllStoredProcedures("dbscriptValues.sql");
            MessageBox.Show("Done!");
        }

        private void CreateAllStoredProcedures(string fileName)
        {
            string connStr = "Server=localhost;Port=5432;User Id=postgres;Password=15postuser*15;DataBase=farmdb";
            NpgsqlConnection npgsql = new NpgsqlConnection(connStr);
            string script = File.ReadAllText(fileName);
            NpgsqlCommand npgsqlCommand = new NpgsqlCommand(script, npgsql);

            npgsql.Open();
            npgsqlCommand.Connection = npgsql;
            try
            {
                npgsqlCommand.ExecuteNonQuery();
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


        private void CreateTable(string procedureName)
        {
            FormattableString formattableString = $"Server=localhost;Port=5432;User Id=postgres;Password=15postuser*15;Database=farmdb;";
            string connStr = formattableString.ToString();
            NpgsqlConnection npgsql = new NpgsqlConnection(connStr);
            NpgsqlCommand npgsqlCommand = new NpgsqlCommand(
                $@"
                CALL {procedureName}
                ", npgsql);

            npgsql.Open();
            npgsqlCommand.Connection = npgsql;
            try
            {
                npgsqlCommand.ExecuteNonQuery();
                MessageBox.Show($"Database table with name {procedureName} is successfully created");
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
