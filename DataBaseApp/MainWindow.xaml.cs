﻿using System.Windows;
using Npgsql;

namespace DataBaseApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string nameDb = "farmDb";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (DropDbOption.IsChecked == true)
            {
                // drop database
                DropDb();
            }
            if (OutputTableOption.IsChecked == true)
            {
                OutputTable();
            }
        }

        private void DropDb()
        {
            string connStr = "Server=localhost;Port=5432;User Id=postgres;Password=15postuser*15;";
            NpgsqlConnection npgsql = new NpgsqlConnection(connStr);
            NpgsqlCommand npgsqlCommand = new NpgsqlCommand(
                $@"
                REVOKE CONNECT ON DATABASE {nameDb} FROM public;
                SELECT pg_terminate_backend(pg_stat_activity.pid)
                FROM pg_stat_activity
                WHERE pg_stat_activity.datname = '{nameDb}'
                  AND pid <> pg_backend_pid();
                DROP DATABASE {nameDb}
                ", npgsql);

            npgsql.Open();
            npgsqlCommand.Connection = npgsql;
            try
            {
                npgsqlCommand.ExecuteNonQuery();
                MessageBox.Show($"Database with name {nameDb} is successfully deleted");
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

        private void OutputTable()
        {
            ShowTableOptions showTableOptions = new ShowTableOptions();
            showTableOptions.Show();
            this.Close();
        }
    }
}
