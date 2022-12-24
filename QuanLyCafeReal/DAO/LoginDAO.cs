using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCafeReal.DAO
{
    internal class LoginDAO
    {
        private static LoginDAO instance;


        private LoginDAO(){}
        internal static LoginDAO Instance
        {
            get { if (instance == null) instance = new LoginDAO();  return instance; }
            private set { instance = value; }
        }      
        public bool login(string userName, string passWord)
        {
            byte[] temp = Encoding.ASCII.GetBytes(passWord);
            byte[] hashData = new MD5CryptoServiceProvider().ComputeHash(temp);
            string passCheck = "";
            foreach (var item in hashData)
            {
                passCheck+=item;
            }
            string query = "select * from Account where UserName = @userName and PassWord = @passWord";
            DataTable data = DataProvider.Instance.ExecuteQuery(query,new object[] {userName,passCheck});           
            return data.Rows.Count>0;
        }
    }
}
