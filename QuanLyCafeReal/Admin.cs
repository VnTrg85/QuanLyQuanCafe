using QuanLyCafeReal.DAO;
using QuanLyCafeReal.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCafeReal
{
    public partial class Admin : Form
    {
        private  DTO.Account account;
        public  DTO.Account Account { get { if (account == null) account = new DTO.Account();return account; } set => account=value; }
        BindingSource listFood = new BindingSource();
        public Admin(DTO.Account account)
        {
            InitializeComponent();
            this.Account = account;
            loadBill();
            loadFood();
            loadCate();
            loadTable();
            loadAccount();   
        }
        #region Bill
        public void loadBill()
        {
            loadDateTimePicker();
        }
        public void loadDateTimePicker()
        {
            DateTime today = DateTime.Now;
            dateTimeCheckin.Value = new DateTime(today.Year, today.Month, 1);
            dateTimeCheckout.Value = dateTimeCheckin.Value.AddMonths(1).AddDays(-1);
        }
        public DataTable loadDataGridBill(DateTime checkin, DateTime checkout , int pageNum)
        {
            if (pageNum != 0)
            {
                return BillDAO.Instance.getAllBillAdminByPage(checkin, checkout, pageNum);
            }
            else
                return BillDAO.Instance.getAllBillAdmin(checkin, checkout);
        }
        
        private void btnList_Click(object sender, EventArgs e)
        {
            dataBills.DataSource= loadDataGridBill(dateTimeCheckin.Value, dateTimeCheckout.Value,0);
        }
        private void btnFirst_Click(object sender, EventArgs e)
        {
            dataBills.DataSource = loadDataGridBill(dateTimeCheckin.Value, dateTimeCheckout.Value,1);
            txtPageNum.Text = "1";
        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            int NumberPage = int.Parse(txtPageNum.Text) - 1;
            if (NumberPage > 0)
            {
                dataBills.DataSource = loadDataGridBill(dateTimeCheckin.Value, dateTimeCheckout.Value, NumberPage);
                txtPageNum.Text = NumberPage.ToString();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int numBillCheck = BillDAO.Instance.getNumberBills(dateTimeCheckin.Value, dateTimeCheckout.Value);
            int Number = numBillCheck / 10;
            if(numBillCheck % 10 != 0)
            {
                Number+=1;
            }
            int NumberPage = int.Parse(txtPageNum.Text) + 1;

            if (NumberPage  <= Number)
            {
                dataBills.DataSource = loadDataGridBill(dateTimeCheckin.Value, dateTimeCheckout.Value, NumberPage);
                txtPageNum.Text = NumberPage.ToString();
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            int numBillCheck = BillDAO.Instance.getNumberBills(dateTimeCheckin.Value,dateTimeCheckout.Value);
            int Number = numBillCheck / 10;
            if (numBillCheck % 10 != 0)
            {
                Number+=1;
            }
            dataBills.DataSource = loadDataGridBill(dateTimeCheckin.Value, dateTimeCheckout.Value, Number);
            txtPageNum.Text = Number.ToString();
        }
        
        #endregion

        #region Food
        public void loadFood()
        {
            dataFoods.DataSource = listFood;
            loadFoodAdmin();
            BindingFood();
            loadComBoCate();
        }
        private void loadFoodAdmin()
        {
            listFood.DataSource = FoodDAO.Instance.loadFoodAdmin();
        }
        private void btbnViewFood_Click(object sender, EventArgs e)
        {
            loadFoodAdmin();
        }
        public void BindingFood()
        {
            txtNameFood.DataBindings.Add(new Binding("Text", dataFoods.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txtIDFood.DataBindings.Add(new Binding("Text", dataFoods.DataSource, "id", true, DataSourceUpdateMode.Never));
            txtPriceFood.DataBindings.Add(new Binding("Text", dataFoods.DataSource, "price", true, DataSourceUpdateMode.Never));

        }
        private void dataFoods_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string check = dataFoods.SelectedCells[0].OwningRow.Cells[2].Value.ToString();
            Console.WriteLine(check);
            if (check != "")
            {
                int id = (int)dataFoods.SelectedCells[0].OwningRow.Cells[2].Value;
                List<Category> list = (List<Category>)comboCate.DataSource;
                foreach (Category item in list)
                {
                    if (item.Id == id)
                    {
                        comboCate.SelectedItem = item; break;
                    }
                }
            }
        }
        

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            string name = txtNameFood.Text;
            float price = float.Parse(txtPriceFood.Text);
            Category cate = comboCate.SelectedItem as Category;
            string idCategory = "";
            if (cate != null)
            {
                idCategory = (cate.Id).ToString();
                if (FoodDAO.Instance.InsertFood(name, idCategory, price))
                {
                    MessageBox.Show("Inserted success");
                }
                else
                {
                    MessageBox.Show("Error occured!!!");
                }
            }
            loadFoodAdmin();
        }

        private void btnUpdateFood_Click(object sender, EventArgs e)
        {
            string id = txtIDFood.Text;
            string name = txtNameFood.Text;
            string price = txtPriceFood.Text;
            Category cate = comboCate.SelectedItem as Category;
            string idCategory = "";
            if (cate != null)
            {
                idCategory = (cate.Id).ToString();
                if (FoodDAO.Instance.UpdateFood(name, idCategory, price, id))
                {
                    MessageBox.Show("Updated success");
                }
                else
                {
                    MessageBox.Show("Error occured!!!");
                }
            }
            loadFoodAdmin();
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            string idFood = txtIDFood.Text;
            if (FoodDAO.Instance.DeleteFood(idFood))
            {
                MessageBox.Show("Deleted success");
            }
            else
            {
                MessageBox.Show("Error occured!!!");
            }
            loadFoodAdmin();
        }

        private void btnSearchFood_Click(object sender, EventArgs e)
        {
            string name = txtSearchFood.Text;
            listFood.DataSource = FoodDAO.Instance.ListFoodBySearch(name);
        }
        public void loadComBoCate()
        {
            List<Category> listCate = new List<Category>();
            listCate = CategoryDAO.Instance.loadCategory();
            comboCate.DataSource = listCate;
            comboCate.DisplayMember = "Name";
        }
        #endregion

        #region Category
        BindingSource listCate = new BindingSource();
        public void loadCate()
        {
            dataCategory.DataSource = listCate;
            loadCateAdmin();
            BindingCategory();
        }
        private void btnAddCate_Click(object sender, EventArgs e)
        {
            string name = txtNameCategory.Text;
            if (CategoryDAO.Instance.addCategory(name))
            {
                MessageBox.Show("Added success");
            } else
            {
                MessageBox.Show("Error Occured");
            }
            loadCateAdmin();
            loadComBoCate();
        }

        private void btnViewCate_Click(object sender, EventArgs e)
        {
            loadCateAdmin();
        }
        public void loadCateAdmin()
        {
            listCate.DataSource = CategoryDAO.Instance.loadCategory();
        }
        public void BindingCategory()
        {
            txtIDCategory.DataBindings.Add(new Binding("Text", dataCategory.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txtNameCategory.DataBindings.Add(new Binding("Text", dataCategory.DataSource, "Name", true, DataSourceUpdateMode.Never));
        }


        private void btnDeleteCate_Click(object sender, EventArgs e)
        {
            string id = txtIDCategory.Text;
            if(CategoryDAO.Instance.CheckCateOnFood(id) == false)
            {
                if (CategoryDAO.Instance.deleteCategory(id))
                {
                    MessageBox.Show("Deleted success");
                }
                else
                {
                    MessageBox.Show("Error Occured");
                }
                loadCateAdmin();
                loadComBoCate();
            }
            else
            {
                MessageBox.Show("Can not delete Cateogry(Exist Food belongs to that Category)");
            }
           
        }

        private void btnUpdateCate_Click(object sender, EventArgs e)
        {
            string id = txtIDCategory.Text;
            string name = txtNameCategory.Text;
            if (CategoryDAO.Instance.updateCategory(id, name))
            {
                MessageBox.Show("Updated success");
            }
            else
            {
                MessageBox.Show("Error Occured");
            }
            loadCateAdmin();
            loadComBoCate();
        }
        private void btnSearchCate_Click(object sender, EventArgs e)
        {
            string txtSearch = txtSearchCate.Text;
            List<Category> listCategory = CategoryDAO.Instance.getListBySearch(txtSearch);
            listCate.DataSource = listCategory;
        }
        #endregion

        #region Table
        BindingSource listTable = new BindingSource();
        public void loadTable()
        {
            dataTable.DataSource = listTable;
            loadTableAdmin();
            BindingTable();
        }
        public void BindingTable()
        {
            txtIDTable.DataBindings.Add(new Binding("Text", dataTable.DataSource, "Id"));
            txtNameTable.DataBindings.Add(new Binding("Text", dataTable.DataSource, "Name"));
        }

        public void loadTableAdmin()
        {
            listTable.DataSource = TableDAO.Instance.loadTableList();
        }

        private void dataTable_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string check = (string)dataTable.SelectedCells[0].OwningRow.Cells[2].Value;
            if(check.ToString() != "")
            {
                if(check.Equals("Trong"))
                {
                    cmbStatus.SelectedIndex = 0;
                }
                else
                {
                    cmbStatus.SelectedIndex = 1;
                }
            }
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            string name = txtNameTable.Text;
            if(TableDAO.Instance.addTable(name))
            {
                MessageBox.Show("Added success");
            }
            else
            {
                MessageBox.Show("Error occured");
            }
            loadTableAdmin();
        }

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            string id = txtIDTable.Text;
            if(TableDAO.Instance.deleteTable(id))
            {
                MessageBox.Show("Deleted success");
            }
            else
            {
                MessageBox.Show("Error occured");
            }
            loadTableAdmin();
        }

        private void btnUpdateTable_Click(object sender, EventArgs e)
        {
            string id = txtIDTable.Text;
            string name = txtNameTable.Text;
            string status = cmbStatus.SelectedIndex == 0 ? "0" : "1";
            if(TableDAO.Instance.updateTable(id, name, status))
            {
                MessageBox.Show("Updated success");
            }
            else
            {
                MessageBox.Show("Error occured");
            }
            loadTableAdmin();
        }

        private void btnViewTable_Click(object sender, EventArgs e)
        {
            loadTableAdmin();
        }


        private void btnSearchTable_Click(object sender, EventArgs e)
        {
            string textSearch = txtSearchTable.Text;
            List<Table> listTables = TableDAO.Instance.getListBySearch(textSearch);
            listTable.DataSource = listTables;
        }
        #endregion

        #region Account
        BindingSource listAccount = new BindingSource();

        

        public void loadAccount()
        {
            DataAcc.DataSource = listAccount;
            loadAccountAdmin();
            BindingAcocunt();
        }

        string userNameCheck;//Check update Account
        public void BindingAcocunt()
        {
            txtDisplayNAcc.DataBindings.Add(new Binding("Text", DataAcc.DataSource, "DisplayedName",true,DataSourceUpdateMode.Never));
            txtUserNAcc.DataBindings.Add(new Binding("Text", DataAcc.DataSource, "UserName", true, DataSourceUpdateMode.Never));
            userNameCheck = txtUserNAcc.Text;
        }
        public void loadAccountAdmin()
        {
            listAccount.DataSource = AccountDAO.Instance.getAllAccount();
        }

        private void DataAcc_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var check = DataAcc.SelectedCells[0].OwningRow.Cells[2].Value;
            if(check != null)
            {
                if(check.ToString() != "")
                {
                    if (check.ToString().Equals("0"))
                    {
                        cmbTypeAcc.SelectedIndex = 0;
                    }
                    else
                        cmbTypeAcc.SelectedIndex =1;
                }
            }
        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            loadAccountAdmin();
        }

        private void btnThemAcc_Click(object sender, EventArgs e)
        {
            string DisPlayName = txtDisplayNAcc.Text;
            string UserName = txtUserNAcc.Text;
            string Type = cmbTypeAcc.SelectedIndex == 0 ? "0" : "1";

            if (AccountDAO.Instance.CheckDupAcocunt(UserName) == false)
            {
                if (AccountDAO.Instance.AddAccount(DisPlayName, UserName, Type))
                {
                    MessageBox.Show("Added Success");
                }
                else
                {
                    MessageBox.Show("Error occured");
                }
            }
            else
                MessageBox.Show("Username is duplicate");
            loadAccountAdmin();
        }

        private void btnDeleteAcc_Click(object sender, EventArgs e)
        {
            string UserName = txtUserNAcc.Text;
            if(UserName != this.Account.UserName)
            {
                if (AccountDAO.Instance.DeleteAccount(UserName))
                {
                    MessageBox.Show("Deleted Success");
                }
                else
                {
                    MessageBox.Show("Error occurred");
                }
            }else
            {
                MessageBox.Show("Can not DELETE this account");
            }
            loadAccountAdmin();
        }
        private void btnUpdateAcc_Click(object sender, EventArgs e)
        {
            
            string UserName = txtUserNAcc.Text;
            string DisPlayName = txtDisplayNAcc.Text;
            string Type = cmbTypeAcc.SelectedIndex == 0 ? "0" : "1";
            if (userNameCheck != UserName)
            {
                if (AccountDAO.Instance.UpdateAccount(UserName, DisPlayName, Type))
                {
                    MessageBox.Show("Updated Success");
                }
                else
                {
                    MessageBox.Show("Can not update Username");
                }
            }
            else
                MessageBox.Show("Username can not be updated");
            loadAccountAdmin();
        }


        #endregion

        
    }
}

