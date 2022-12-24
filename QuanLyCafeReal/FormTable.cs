using QuanLyCafeReal.DAO;
using QuanLyCafeReal.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCafeReal
{
    public partial class FormTable : Form
    {
        private DTO.Account accountLogin;

        public DTO.Account AccountLogin { get => accountLogin; set => accountLogin=value; }

        public FormTable(DTO.Account account)
        {
            InitializeComponent();
            addColumns();
            loadTable();
            loadCategory();
            this.AccountLogin = account;
            changeAdmin(AccountLogin.Type);
            accountToolStripMenuItem.Text += "("+accountLogin.DisplayName+")";
        }
        
        void changeAdmin(int i)
        {
            adminToolStripMenuItem.Enabled = (i==1) ?true:false;
        }
        #region
        void loadTable()
        {
            //Lay danh sach table tu database
            flowTable.Controls.Clear();
            List<Table> listTable = new List<Table>();
            listTable = TableDAO.Instance.loadTableList();
            //Load combo box switch Table
            cmbSwitchTable.DataSource = listTable;
            cmbSwitchTable.Tag = listTable;
            cmbSwitchTable.DisplayMember = "Name";
            //load cac button dai dien cho table len layout
            foreach (Table table in listTable)
            {           
                Button btn = new Button() { Width = TableDAO.width, Height = TableDAO.height };
                btn.Tag = (Table)table;
                btn.Text = table.Name + Environment.NewLine + table.Status;
                btn.Click += Btn_Click;
                switch (table.Status)
                {
                    case "Co nguoi": btn.BackColor = Color.Violet; 
                                    break;
                    default :btn.BackColor = Color.AliceBlue;
                                    break;
                }
                flowTable.Controls.Add(btn);
            }
        }


        void loadCategory()
        {
            List<Category> categories = new List<Category>();
            categories = CategoryDAO.Instance.loadCategory();
            foreach (Category category in categories)
            {
                cmbCategory.Items.Add(category);
                //cmbCategory.Items.Add(category.Name);
            }
            cmbCategory.DisplayMember = "Name";
        }

        void loadFoodByCategory(int id)
        {
            cmbFood.Items.Clear();
            List<Food> list = FoodDAO.Instance.loadFoodByIdCate(id);
            foreach (Food item in list)
            {
                cmbFood.Items.Add(item);
            }
            cmbFood.Tag = list;
            cmbFood.DisplayMember = "Name";           
        }
        
        #endregion

        void addColumns()
        {
            ColumnHeader columnName = new ColumnHeader();
            columnName.Text = "Name";
            columnName.Width=120;
            listViewFood.Columns.Add(columnName);
            ColumnHeader columnPrice = new ColumnHeader();
            columnPrice.Text = "Price";
            columnPrice.Width=120;
            listViewFood.Columns.Add(columnPrice);
            ColumnHeader columnCount = new ColumnHeader();
            columnCount.Text = "Count";
            columnCount.Width=120;
            listViewFood.Columns.Add(columnCount);
            ColumnHeader columnTotal = new ColumnHeader();
            columnTotal.Text = "Total Price";
            columnTotal.Width=150;
            listViewFood.Columns.Add(columnTotal);

        }

        void showBill(int idTable)
        {
            //La cac bill theo id table tu table tuong ung
            listViewFood.Items.Clear();
            List<Menu> menus= new List<Menu>();
            menus=MenuDAO.Instance.LoadMenu(idTable);
            double totalPrice = 0;
            //Load mon an va gia len list view Food
            foreach (Menu item in menus)
            {
                ListViewItem lv = new ListViewItem(item.Name);             
                lv.SubItems.Add(item.Price.ToString());
                lv.SubItems.Add(item.Count.ToString());
                lv.SubItems.Add(item.TotalPrice.ToString());
                totalPrice += item.TotalPrice;
                listViewFood.Items.Add(lv);
            }
            //Hien gia cho total price va final price textbox
            CultureInfo info = new CultureInfo("vi-VN");          
            double disCount = ((double)nmrDiscount.Value/100)*totalPrice;
            double finalPrice = totalPrice-disCount;
            txtFinalPrice.Text = finalPrice.ToString("C", info);
            txtTotal.Text=totalPrice.ToString("C",info);
        }

        #region event

        private void Btn_Click(object sender, EventArgs e)
        {
            listViewFood.Items.Clear();
            Table? table = ((sender as Button).Tag) as Table;
            listViewFood.Tag = table;
            if (table != null)
            {
                txtNbTable.Text = table.Id.ToString();
                int id = table.Id;
                showBill(id);
            }
            
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem == null)
            {
                return;
            }
            Category selected =  cmbCategory.SelectedItem as Category;            
            int id=selected.Id;          
            loadFoodByCategory(id);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Table table = (Table)listViewFood.Tag;
            if(table != null && cmbFood.SelectedItem != null && numericCount.Value != 0)
            {
                int idTable = table.Id;
                int number = (int)BillDAO.Instance.getUncheckBill(idTable);
                if(number <1)
                {
                    
                    BillDAO.Instance.InsertBill(idTable);
                    Food? food = cmbFood.SelectedItem as Food;
                
                    if (food != null)
                    {
                        int idFood = food.Id;
                        int countFood = (int)numericCount.Value;
                        BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instance.getIdBillMax(), idFood, countFood);
                    }
                }
                else
                {
                    Food? food = cmbFood.SelectedItem as Food;

                    if (food != null)
                    {
                        int idFood = food.Id;
                        int countFood = (int)numericCount.Value;
                        BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instance.getIDBill(idTable),idFood,countFood);
                    }

                }
                TableDAO.Instance.SetStatus(idTable);
                loadTable();
                showBill(idTable);
            }
        }

        private void btnPaid_Click(object sender, EventArgs e)
        {
            Table table = listViewFood.Tag as Table;
            if (table != null)
            {
                int idTable = table.Id;
                int idBill = BillDAO.Instance.getIDBill(table.Id);
                if(idBill > 0)
                {
                    double totalPrice = Convert.ToDouble(txtTotal.Text.Split(' ')[0])*1000;
                    double disCount = ((double)nmrDiscount.Value/100)*totalPrice;
                    double finalPrice = totalPrice-disCount;
                    BillDAO.Instance.CheckOut(idBill,disCount,totalPrice,finalPrice);                  
                    TableDAO.Instance.SetStatusintoUnCheck(idTable);
                    showBill(idTable);
                    loadTable();
                }
            }
        }

        private void nmrDiscount_ValueChanged(object sender, EventArgs e)
        {
            CultureInfo info = new CultureInfo("vi-VN");
            double totalPrice = Convert.ToDouble(txtTotal.Text.Split(' ')[0])*1000;
            double disCount = ((double)nmrDiscount.Value/100)*totalPrice;
            double finalPrice = totalPrice-disCount;
            txtFinalPrice.Text=finalPrice.ToString("C", info);
        }

        private void btnSwitch_Click(object sender, EventArgs e)
        {
            Table tableFirst = listViewFood.Tag as Table;
            Table tableSecond = cmbSwitchTable.SelectedItem as Table;
            if(tableFirst != null &&  tableSecond != null )
            {
                TableDAO.Instance.SwitchTable(tableFirst.Id, tableSecond.Id);
                showBill(tableFirst.Id);
                showBill(tableSecond.Id); 
                loadTable();
            }            
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Admin admin = new Admin(this.accountLogin);
            admin.ShowDialog();
        }

        private void accountInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Account accountForm = new Account(this.AccountLogin);
            accountForm.UpdateAccountEvent += updateDisplayName;
            accountForm.LogOutEvent += Logout;
            accountForm.ShowDialog();
        }
        public void Logout(object? sender, MyEventArgs e)
        {
            this.Close();
        }
        private void updateDisplayName(object? sender, MyEventArgs e)
        {
           accountToolStripMenuItem.Text = "Account("+e.Name+")";
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

        #endregion