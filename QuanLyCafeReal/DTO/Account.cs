using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCafeReal.DTO
{
    public class Account
    {
        private string userName;
        private string displayName;
        private string passWord;
        private int type;


        public Account() { }
        public Account(string name, string displayName, string passWord, int type)
        {
            this.userName=name;
            this.DisplayName=displayName;
            this.PassWord=passWord;
            this.Type=type;
        }
        public Account(DataRow data)
        {
            this.UserName = (string)data["UserName"];
            this.displayName = (string)data["DisplayedName"];
            this.PassWord = (string)data["PassWord"];
            this.Type =(int)data["Type"];
        }
        public int Type { get => type; set => type=value; }
        public string PassWord { get => passWord; set => passWord=value; }
        public string DisplayName { get => displayName; set => displayName=value; }
        public string UserName { get => userName; set => userName=value; }
    }
}
