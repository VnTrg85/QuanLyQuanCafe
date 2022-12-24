using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCafeReal.DAO
{
    internal class AccountDAO
    {
        private static AccountDAO instance;

        internal static AccountDAO Instance { get { if (instance == null) instance = new AccountDAO();return instance; }private set => instance=value; }
        private AccountDAO() { }
        public DTO.Account getAccountbyUserName(string userName)
        { 
            string query = "select * from Account where UserName = @userName ";
            DataTable table =DataProvider.Instance.ExecuteQuery(query, new object[] { userName });
            foreach (DataRow item in table.Rows)
            {
                return new DTO.Account(item);
            }
            return null;
        }
        public bool updateAccount(string userName, string displayName , string passWord , string newPass)
        {
            byte[] temp = Encoding.ASCII.GetBytes(passWord);
            byte[] hashData = new MD5CryptoServiceProvider().ComputeHash(temp);
            string passCheck = "";
            foreach (var item in hashData)
            {
                passCheck+=item;
            }

            byte[] temp1 = Encoding.ASCII.GetBytes(newPass);
            byte[] hashData2 = new MD5CryptoServiceProvider().ComputeHash(temp1);
            string passNew = "";
            foreach (var item in hashData2)
            {
                passNew += item;
            }
            string query = "USP_updateAcocunt @userName , @displayName , @passWord , @newPass";
            int check =DataProvider.Instance.ExecuteNonQuery(query , new object[] {userName,displayName, passCheck, passNew});  
            if(check > 0 )
            {
                return true;
            }
            return false;
        }
        public DataTable getAllAccount()
        {
            string query = " select DisplayedName ,  UserName ,  Type from Account";
            DataTable data = null;
            data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }
        public bool UpdateAccount( string UserName ,string DisplayName , string type)
        {
            string query = "Update Account set DisplayedName = @DisplayName , Type = @type where UserName = @UserName";
            if (DataProvider.Instance.ExecuteNonQuery(query, new object[] { DisplayName, type,UserName }) > 0)
            {
                return true;
            }
            return false;
        }

        public bool AddAccount(string DisplayName, string UserName ,  string Type)
        {
            string query = "Insert into Account(DisplayedName,UserName,Type) values( @DisplayName , @UserName , @Type )";
            if (DataProvider.Instance.ExecuteNonQuery(query, new object[] { DisplayName, UserName, Type })>0)
            {
                return true;
            }
            return false;
        }
        public bool DeleteAccount(string UserName )
        {
            string query = "Delete from Account where UserName = @UserName ";
            if(DataProvider.Instance.ExecuteNonQuery(query, new object[] {UserName})>0)
            {
                return true;
            }
            return false;
        }
        public bool CheckDupAcocunt(string userName)
        {
            string query = "USP_CheckDupUserName @userName";
            int check =(int) DataProvider.Instance.ExecuteScalar(query, new object[] { userName });
            if (check > 0)
                return true;
            return false;
        }
    }
}
