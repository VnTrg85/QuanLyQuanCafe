using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace QuanLyCafeReal.DAO
{
    internal class DataProvider
    {
        private static DataProvider instance;

        string text = @"Data Source=NGUYNVNTRNGD528\SQLEXPRESS;Initial Catalog=QuanLyQuanCafe;Integrated Security=True";
        internal static DataProvider Instance { get { if (instance == null) instance = new DataProvider();return instance; } private set => instance=value; }
        private DataProvider() { }
        
        public DataTable ExecuteQuery(string query, object[] para = null)
        {
            DataTable data = new DataTable();
            using (SqlConnection con = new SqlConnection(text))
            {
                con.Open();
                SqlCommand sqlCommand = new SqlCommand(query, con);
                if(para !=  null)
                {                   
                    string[] temp = query.Split(' ');
                    int i = 0;
                    foreach (string s in temp)
                    {
                        if(s.Contains("@"))
                        {                           
                            sqlCommand.Parameters.AddWithValue(s, para[i]);
                            i++;
                        }
                    }
                }
                SqlDataAdapter sqladt = new SqlDataAdapter(sqlCommand);
                sqladt.Fill(data);
                con.Close();
            }
            return data;     
        }

        public int ExecuteNonQuery(string query, object[] para = null)
        {
            
            using (SqlConnection con = new SqlConnection(text))
            {
                con.Open();
                SqlCommand sqlCommand = new SqlCommand(query, con);
                if (para !=  null)
                {
                    sqlCommand.Parameters.Clear();
                    string[] temp = query.Split(' ');
                    int i = 0;
                    foreach (string s in temp)
                    {
                        if (s.Contains("@"))
                        {
                            sqlCommand.Parameters.AddWithValue(s,para[i]);
                            i++;
                        }
                    }
                }
                int numberrowaff=sqlCommand.ExecuteNonQuery();
                con.Close();
                return numberrowaff;
            }
        }
        public object ExecuteScalar(string query, object[] para = null)
        {
            using (SqlConnection con = new SqlConnection(text))
            {
                con.Open();
                SqlCommand sqlCommand = new SqlCommand(query, con);
                if (para !=  null)
                {
                    sqlCommand.Parameters.Clear();
                    string[] temp = query.Split(' ');
                    int i = 0;
                    foreach (string s in temp)
                    {
                        if (s.Contains("@"))
                        {
                            sqlCommand.Parameters.AddWithValue(s,para[i]);
                            i++;
                        }
                    }
                }
                object data = sqlCommand.ExecuteScalar();
                con.Close();
                return data;
            }
             
        }
    }
}
