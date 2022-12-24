using QuanLyCafeReal.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCafeReal.DAO
{
    internal class MenuDAO
    {
        private static MenuDAO instance;

        internal static MenuDAO Instance { get { if (instance == null) instance = new MenuDAO(); return instance; } private set => instance=value; }
        private MenuDAO() { }
        public List<Menu> LoadMenu(int idTable)
        {
            List<Menu> list = new List<Menu>();
            string query = "select Food.Name, Food.price ,BillInfo.count, Food.price * BillInfo.count as totalPrice  from Food,Bill,BillInfo where Bill.idTable = @id AND Bill.status = 0 and Bill.id = BillInfo.idBill and BillInfo.idFood = Food.id";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { idTable });
            foreach (DataRow item in data.Rows)
            {
                Menu menu = new Menu(item);
                list.Add(menu);
            }
            return list;
        }
    }
}
