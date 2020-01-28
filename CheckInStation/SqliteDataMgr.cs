using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInStation
{
    public class SqliteDataMgr
    {
        public static bool SettingExists(string settingName)
        {
            bool exists = false;

            using (IDbConnection conn = new SQLiteConnection(LoadConnString()))
            {
                var output = conn.Query<SettingModel>("select * from Settings where SettingName ='" + settingName +"'", new DynamicParameters());

                if (output.ToList().Count!=0)
                {
                    exists = true;
                }

                return exists;
            }
        }

        public static void UpdateSetting(SettingModel setting)
        {
            using (IDbConnection conn = new SQLiteConnection(LoadConnString()))
            {
                conn.Execute("update Settings set SettingValue ='" + setting.SettingValue + "' where SettingName= '" + setting.SettingName +"'");

               
            }
        }

        public static List<SettingModel> LoadSettings()
        {
            using (IDbConnection conn = new SQLiteConnection(LoadConnString()))
            {
                var output = conn.Query<SettingModel>("select * from Settings", new DynamicParameters());

                return output.ToList();
            }
        }

        public static void AddSetting(SettingModel setting)
        {
            using (IDbConnection conn = new SQLiteConnection(LoadConnString()))
            {
                conn.Execute("insert into Settings (SettingName, SettingValue) values (@SettingName, @SettingValue)", setting);
            }
        }

        private static string LoadConnString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }

    public class SettingModel
    {
        public string SettingName { get; set; }
        public string SettingValue { get; set; }

      
    }
}
