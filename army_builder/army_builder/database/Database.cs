using army_builder.model;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;

namespace army_builder.database
{
    public class Database
    {
        readonly string dbPath;

        public Database(string dbPath)
        {
            this.dbPath = dbPath;
        }

        private SqliteConnection CreateConnection()
        {
            SqliteConnectionStringBuilder connStr = new SqliteConnectionStringBuilder
            {
                { "Data Source", dbPath },
                { "Foreign Keys", true }
            };

            return new SqliteConnection(connStr.ToString());
        }

        public IDictionary<string, List<string>> getSoupFactions()
        {
            IDictionary<string, List<string>> factions = new Dictionary<string, List<string>>();

            using (SqliteConnection conn = this.CreateConnection())
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
                            int id = reader.GetInt32(0);
                            factions.Add(reader.GetString(1), getMainFactions(id, conn));
                        }

                        factions.Add("Other Xenos", getMainFactions(-1, conn));
                    }
                    conn.Close();
                }
            }

            return factions;
        }

        private List<string> getMainFactions(int soupId, SqliteConnection conn)
        {
            List<string> factions = new List<string>();

            string query;
            if (soupId > 0) {
                query = "SELECT * FROM main_factions WHERE soup_faction_id = " + soupId;
            } else {
                query = "SELECT * FROM main_factions WHERE soup_faction_id = NULL";
            }

            using (SqliteCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;
                using (SqliteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        factions.Add(reader.GetString(1));
                    }
                }
            }

            return factions;
        }

        public IDictionary<string, List<string>> getMainFactions()
        {
            IDictionary<string, List<string>> factions = new Dictionary<string, List<string>>();

            using (SqliteConnection conn = this.CreateConnection())
            {
                string query = "SELECT * FROM main_factions";

                using (SqliteCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    conn.Open();
                    using (SqliteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            factions.Add(reader.GetString(1), getNamedFactions(id, conn));
                        }
                    }
                    conn.Close();
                }
            }

            return factions;
        }

        private List<string> getNamedFactions(int mainId, SqliteConnection conn)
        {
            List<string> factions = new List<string>();

            string query = "SELECT * FROM named_factions WHERE main_faction_id = " + mainId;

            using (SqliteCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;
                using (SqliteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        factions.Add(reader.GetString(1));
                    }
                }
            }

            return factions;
        }
    }
}
