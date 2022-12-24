using QuanLyCafeReal.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace QuanLyCafeReal
{
    public partial class Account : Form
    {
        private DTO.Account accountLogin;
        public DTO.Account AccountLogin { get => accountLogin; set => accountLogin=value; }
        public Account(DTO.Account account)
        {
            InitializeComponent();
            this.AccountLogin = account;
            load();
        }
        public void load()
        {
            txtUserName.Text = this.AccountLogin.UserName;
            txtDisplayname.Text = this.AccountLogin.DisplayName;
        }

        public void updateAccount()
        {
            string userName = txtUserName.Text;
            string displayName = txtDisplayname.Text;
            string passWord = txtPassword.Text;
            string newPass = txtNPassWord.Text;
            if(AccountDAO.Instance.updateAccount(userName, displayName, passWord, newPass))
            {
                returnAccountEvent(displayName);
                MessageBox.Show("Success updated");
            }else
            {
                MessageBox.Show("Please check password again");
            }

        }
        public bool checkPassWord()
        {
            if(txtNPassWord.Text.Equals(txtRePass.Text))
            {
                return true;
            }
            return false;
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(checkPassWord() != false)
            {
                updateAccount();
            }
            else
            {
                MessageBox.Show("New password and re-password not the same");
            }
        }
        public event EventHandler<MyEventArgs> updateAccountEvent;
        public event EventHandler<MyEventArgs> UpdateAccountEvent
        {
            add { updateAccountEvent += value; }
            remove { updateAccountEvent -= value; }
        }

        public void returnAccountEvent(string name)
        {
            if(updateAccountEvent != null)
            {
                updateAccountEvent(this, new MyEventArgs(name));
            }
        }

        public event EventHandler<MyEventArgs> logOutEvent;
        public event EventHandler<MyEventArgs> LogOutEvent
        {
            add { logOutEvent += value; }
            remove { logOutEvent -= value; }
        }
        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            logOutEvent(this, new MyEventArgs(""));
        }
    }
    public class MyEventArgs : EventArgs
    {
        private string name;

        public string Name { get => name; set => name=value; }
        public MyEventArgs(string name)
        {
            this.Name = name;
        }
    }
}

