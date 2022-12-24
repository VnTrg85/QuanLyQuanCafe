using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCafeReal.DAO
{
    internal class BillInfoDAO
    {
        private static BillInfoDAO instance;

        internal static BillInfoDAO Instance { get { if (instance == null) instance = new BillInfoDAO();return instance; }private set => instance=value; }
        private BillInfoDAO() { }


        public void InsertBillInfo(int idBill, int idFood, int count)           
        {
            string query = "EXEC USP_InsertBillInfo @idBill , @idFood , @count";
            DataProvider.Instance.ExecuteNonQuery(query, new Object[] { idBill, idFood, count });
        }
    }
}
