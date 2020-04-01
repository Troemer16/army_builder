using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.Data.Sqlite;
using System;

namespace army_builder
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FactionSelectionPage : ContentPage
    {
        public FactionSelectionPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            

            Dictionary<int, string> factions = new Dictionary<int, string>();

            SqliteConnectionStringBuilder connStr = new SqliteConnectionStringBuilder
            {
                { "Data Source", App.dbPath },
                { "Foreign Keys", true }
            };

            using (SqliteConnection conn = new SqliteConnection(connStr.ToString()))
            {
                string query = "SELECT * FROM soup_factions";
                
                using (SqliteCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    conn.Open();
                    using (SqliteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            factions.Add(reader.GetInt32(0), reader.GetString(1));
                        }
                    }
                    conn.Close();
                }
            }

            string label = "";
            foreach(KeyValuePair<int, string> faction in factions)
            {
                label += faction + "\n";
            }
            

        }


    }
}