using Lab3.Entity;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Database
{
    class DatabaseInitialize
    {
        private static SQLiteConnection conn = new SQLiteConnection("lab3.db");
        public static bool CreateTables()
        {
            var sql = @"CREATE TABLE IF NOT EXISTS PersonalTransaction(Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, Name VARCHAR(255), Description TEXT, Money DOUBLE, CreatedDate DATE, Category INTEGER);";
            using (var statement = conn.Prepare(sql))
            {
                statement.Step();
            }
            return true;
        }

        public bool InsertData(PersonalTransaction obj)
        {
            var sql = $"INSERT INTO PersonalTransaction (Name, Description, Money, CreatedDate, Category) VALUES ('{obj.Name}', '{obj.Description}', {obj.Money}, '{obj.CreatedDate}', {obj.Category})";
            using (var statement = conn.Prepare(sql))
            {
                statement.Step();
            }
            return true;
        } 
    }
}
