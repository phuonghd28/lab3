using Lab3.Entity;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Database
{
    class DatabaseInitialize
    {
        private static SQLiteConnection conn = new SQLiteConnection("lab3.db");

        public int totalMoney;
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

        public List<PersonalTransaction> ListData()
        {
            totalMoney = 0;
            var personalTransactions = new List<PersonalTransaction>();
            var sql = "SELECT * FROM PersonalTransaction";
            using (var statement = conn.Prepare(sql))
            {
                while(statement.Step() == SQLiteResult.ROW)
                {
                    var name = (string)statement["Name"];
                    var description = (string)statement["Description"];
                    var money = (double)statement["Money"];
                    var createDate = Convert.ToDateTime(statement["CreatedDate"]);
                    var category  = Convert.ToInt32(statement["Category"]);
                    var obj = new PersonalTransaction(name, description, money, createDate, category);
                    personalTransactions.Add(obj);
                    totalMoney += Convert.ToInt32(statement["Money"]);
                }
            }
            return personalTransactions;
        }

        public List<PersonalTransaction> FindByCategory(int Category)
        {
            totalMoney = 0;
            var list = new List<PersonalTransaction>();
            try
            {
                SQLiteConnection cnn = new SQLiteConnection("transactionsqlite.db");
                using (var stt = cnn.Prepare($"select * from PersonalTransaction where Category = {Category}"))
                {
                    while (stt.Step() == SQLiteResult.ROW)
                    {
                        var personal = new PersonalTransaction()
                        {
                            Name = (string)stt["Name"],
                            Description = (string)stt["Description"],
                            Money = Convert.ToDouble(stt["Money"]),
                            CreatedDate = Convert.ToDateTime(stt["CreatedDate"]),
                            Category = Convert.ToInt32(stt["Category"]),
                        };
                        list.Add(personal);
                        totalMoney += Convert.ToInt32(stt["Money"]);
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Co loi list" + ex);
                return null;
            }
        }

        public List<PersonalTransaction> FindByStartDateAndEndDate(string startDate, string endDate)
        {
            totalMoney = 0;
            var list = new List<PersonalTransaction>();
            try
            {
                SQLiteConnection cnn = new SQLiteConnection("transactionsqlite.db");
                using (var stt = cnn.Prepare($"select * from PersonalTransaction where CreatedDate between '{startDate}' and '{endDate}'"))
                {
                    while (stt.Step() == SQLiteResult.ROW)
                    {
                        var personal = new PersonalTransaction()
                        {
                            Name = (string)stt["Name"],
                            Description = (string)stt["Description"],
                            Money = Convert.ToDouble(stt["Money"]),
                            CreatedDate = Convert.ToDateTime(stt["CreatedDate"]),
                            Category = Convert.ToInt32(stt["Category"]),
                        };
                        list.Add(personal);
                        totalMoney += Convert.ToInt32(stt["Money"]);
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Co loi list" + ex);
                return null;
            }
        }
    }
}
