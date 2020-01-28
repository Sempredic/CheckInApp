using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckInStation
{
    class SKUDBMgr
    {
        public static string dbLocation;
        public static OleDbConnection conn;

        public enum SKUDBOPTIONS
        {
            [Description("INSERT INTO")]
            INSERT,
            [Description("SELECT")]
            SELECT,
            [Description("VALUES")]
            VALUES,
            [Description("FROM")]
            FROM,
            [Description("WHERE")]
            WHERE,
            [Description("AND")]
            AND,
            [Description("OR")]
            OR,
            NONE

        }

        public static void ExecuteQuery(SKUDBOPTIONS type, string table, List<string> fields, SKUDBOPTIONS option, List<KeyValuePair<string,object>> values,SKUDBOPTIONS clause1,SKUDBOPTIONS clause2)
        {
            //string qry = "INSERT INTO Pics (Data) VALUES(@Data)" + "SELECT F1 FROM Table1 WHERE F2='23456' OR F2='23456'";
            string sql = null;

            if (type == SKUDBOPTIONS.INSERT)
            {
                sql = $"{GetEnumDescription(type)} {table} ({String.Join(",", fields)}) {GetEnumDescription(option)} ({String.Join(",", values.Select(v =>v.Key))})";
                
                if (conn.State == ConnectionState.Open)
                {
                    using (conn)
                    {
                        OleDbCommand cmd = new OleDbCommand(sql, conn);

                        foreach (KeyValuePair<string,object> parameter in values)
                        {
                            if (parameter.Value.GetType() == typeof(byte[]))
                            {

                                cmd.Parameters.AddWithValue(parameter.Key, (byte[])parameter.Value);
                            }
                            
                        }

                        Console.WriteLine(cmd.CommandText);
                        
                       cmd.ExecuteNonQuery();
                    }
                }
            }else if (type == SKUDBOPTIONS.SELECT)
            {

                StringBuilder val = new StringBuilder();

                if (values.Count==1)
                {
                    val.Append($"{values[0].Key} = '{values[0].Value}'");
                }
                else if(values.Count>1 && values.Count<3)
                {
                    val.Append($"{values[0].Key} = '{values[0].Value}'");

                    if (clause2 == SKUDBOPTIONS.OR)
                    {
                        val.Append($" OR {values[1].Key} = '{values[1].Value}'");
                    }
                }

                sql = $"{GetEnumDescription(type)} {String.Join(",", fields)} {GetEnumDescription(option)} {table} {GetEnumDescription(clause1)}".Trim() + (val!=null?" (" +val +")":"");
                Console.WriteLine(sql);

                if (conn.State == ConnectionState.Open)
                {
                    using (conn)
                    {
                        OleDbCommand cmd = new OleDbCommand(sql, conn);


                        Console.WriteLine(cmd.CommandText);
                        
                        cmd.ExecuteNonQuery();

                        GetCommandResults(cmd);
                    }
                }
            }
            
            /*
            using (SKUDBMgr.conn)
            {
                OleDbCommand cmd = new OleDbCommand(qry, SKUDBMgr.conn);
                cmd.Parameters.AddWithValue("@Data", xByte);

                //SKUDBMgr.conn.Open();
                cmd.ExecuteNonQuery();
            }
            */
        }

        public static string GetCommandResults(OleDbCommand command)
        {

            StringBuilder result = new StringBuilder();

            try
            {

                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {


                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.WriteLine(reader.GetValue(i));

                        if (reader.GetValue(i).GetType() == typeof(byte[]))
                        {
                            result.AppendLine(Encoding.Default.GetString((byte[])reader.GetValue(i)));
                        }
                        else
                        {
                            result.AppendLine(reader.GetValue(i).ToString());
                        }


                    }
                    // Insert code to process data.
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: " + e.ToString());
            }

            return result.ToString();
        }

        public static string GetEnumDescription(SKUDBOPTIONS val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val
               .GetType()
               .GetField(val.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }



        public static void ConnectToDatabase(string location)
        {
            conn = new OleDbConnection();

            // TODO: Modify the connection string and include any
            // additional required properties for your database.
            //conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;" + @"Data source="+location;
            if (Path.GetExtension(location) == ".accdb")
            {
                conn.ConnectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={location};Persist Security Info=False;";
            }
            else if (Path.GetExtension(location) == ".mdb")
            {
                conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;" + @"Data source=" + location;
            }

            Console.WriteLine(conn.ConnectionString);
            try
            {
                conn.Open();

                // Insert code to process data.
                MessageBox.Show("Connection Successful");
                dbLocation = location;
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to connect to data source");
            }
            finally
            {
                //conn.Close();
            }
        }

        public static string ExecuteQuery(string sql, DataGridView dgv)
        {
            // Execute Queries
            StringBuilder result = new StringBuilder();
            Console.WriteLine(sql);
            if (conn!=null)
            {

                if (conn.State == ConnectionState.Open)
                {
                    OleDbCommand command = new OleDbCommand(sql, conn);

                    if (dgv!=null)
                    {
                        OleDbDataAdapter adapter = new OleDbDataAdapter(command);

                        DataTable ds = new DataTable();

                        adapter.Fill(ds);

                        dgv.DataSource = ds;
                    }

                    Console.WriteLine(sql);

                    try
                    {

                        OleDbDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {


                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                Console.WriteLine(reader.GetValue(i));

                                if (reader.GetValue(i).GetType() == typeof(byte[]))
                                {
                                    result.AppendLine(Encoding.Default.GetString((byte[])reader.GetValue(i)));
                                }
                                else
                                {
                                    result.AppendLine(reader.GetValue(i).ToString());
                                }
                                

                            }
                            // Insert code to process data.
                        }
                        reader.Close();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("ERROR: " + e.ToString());
                    }

                    
                }
                else
                {
                    Console.WriteLine("Connection Closed");
                }
                
            }

            return result.ToString();
        }
    }
}
