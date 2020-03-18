using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.Data.Sqlite;

namespace army_builder
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreatePage : ContentPage
    {
        public CreatePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Dictionary<int, string> factions = new Dictionary<int, string>();

            SqliteConnectionStringBuilder connStr = new SqliteConnectionStringBuilder();
            connStr.Add("Data Source", App.dbPath);
            connStr.Add("Foreign Keys", true);

            using(SqliteConnection conn = new SqliteConnection(connStr.ToString()))
            {
                //string query = "CREATE TABLE soup_factions (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, name TEXT NOT NULL)";
                

                //using (SqliteCommand cmd = conn.CreateCommand())
                //{
                  //  cmd.CommandText = query;
                    //cmd.ExecuteNonQuery();
                //}


                string query = "SELECT * FROM soup_factions";
                
                using (SqliteCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    conn.Open();
                    using (SqliteDataReader reader = cmd.ExecuteReader())
                    {
                        //do
                        //{
                        //    factions.Add(reader.GetInt32(0), reader.GetString(1));
                        //} while (reader.Read());

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
            myLabel.Text = label;
        }
    }
}