using QuanLyCafeReal.DAO;

namespace QuanLyCafeReal
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btgLogin_Click(object sender, EventArgs e)
        {
            string userName= txtUsername.Text;
            string passWord = txtPassWord.Text;
            
            if(login(userName,passWord))
            {
                DTO.Account accountLogin = AccountDAO.Instance.getAccountbyUserName(userName);              
                FormTable formQl = new FormTable(accountLogin);
                this.Hide();
                formQl.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("incorrect password or username","Alert");
            }
        }
        private bool login( string userName, string passWord)
        {
            return LoginDAO.Instance.login(userName,passWord);
        }
        private void btnLogout_Click(object sender, EventArgs e)
        {
              this.Close();
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("Do you really want to escape?", "Alert",MessageBoxButtons.OKCancel)!=DialogResult.OK)
            {
                e.Cancel = true;
            }
        }
    }
}