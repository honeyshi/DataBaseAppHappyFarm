﻿using System;
using System.ComponentModel;
using System.Data;
using System.Windows;
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
            if (ClearTableOption.IsChecked == true)
            {
                DeleteTable();
            }
            if (SearchByFieldOption.IsChecked == true)
            {
                SearchByField();
            }
            if (AddNewDataOption.IsChecked == true)
            {
                AddNewData();
            }
            if (UpdateTableOption.IsChecked == true)
            {
                UpdateData();
            }
            if (DeleteByIdOption.IsChecked == true)
            {
                DeleteById();
            }
            if (DeleteByTextFieldOption.IsChecked == true)
            {
                DeleteByTextField();
            }
        }

        #region Option Methods
        private void DropDb()
        {
            string connStr = "Server=localhost;Port=5432;User Id=postgres;Password=15postuser*15;";
            NpgsqlConnection npgsql = new NpgsqlConnection(connStr);
            /*NpgsqlCommand npgsqlCommand = new NpgsqlCommand(
                $@"
                REVOKE CONNECT ON DATABASE {nameDb} FROM public;
                SELECT pg_terminate_backend(pg_stat_activity.pid)
                FROM pg_stat_activity
                WHERE pg_stat_activity.datname = '{nameDb}'
                  AND pid <> pg_backend_pid();
                DROP DATABASE {nameDb}
                ", npgsql);*/
            NpgsqlCommand npgsqlCommand = new NpgsqlCommand("DROP_DB", npgsql);
            npgsqlCommand.CommandType = CommandType.StoredProcedure;
            npgsqlCommand.Parameters.AddWithValue("name", nameDb);

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

        private void DeleteTable()
        {
            ClearTableOption clearTableOption = new ClearTableOption();
            clearTableOption.Show();
            this.Close();
        }

        private void SearchByField()
        {
            SearchByFieldWindow searchByFieldWindow = new SearchByFieldWindow();
            searchByFieldWindow.Show();
            this.Close();
        }

        private void AddNewData()
        {
            AddNewDataOption addNewDataOption = new AddNewDataOption();
            addNewDataOption.Show();
            this.Close();
        }

        private void UpdateData()
        {
            UpdateDataOption updateDataOption = new UpdateDataOption();
            updateDataOption.Show();
            this.Close();
        }

        private void DeleteById()
        {
            DeleteByIdOption deleteByIdOption = new DeleteByIdOption();
            deleteByIdOption.Show();
            this.Close();
        }

        private void DeleteByTextField()
        {
            DeleteByTypeOption deleteByTypeOption = new DeleteByTypeOption();
            deleteByTypeOption.Show();
            this.Close();
        }
        #endregion
    }
}
