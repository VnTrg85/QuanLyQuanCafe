using QuanLyCafeReal.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCafeReal.DAO
{
    internal class BillDAO
    {
        private static BillDAO instance;

        internal static BillDAO Instance { get { if (instance == null) instance = new BillDAO();return instance;
            } private set => instance=value; }

        private BillDAO() { }

        public List<Menu> loadBill(int id)
        {
            List<Menu> list = new List<Menu>();         
            string query= "select Food.Name, Food.price ,BillInfo.count, Food.price * BillInfo.count as totalPrice  from Food,Bill,BillInfo where Bill.idTable = @id AND Bill.status = 0 and Bill.id = BillInfo.idBill and BillInfo.idFood = Food.id";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] {id});
            
            return list;
        }

        public int getUncheckBill(int idTable)
        {
            string query = "select count(*) from Bill where idTable = @id and status = 0";
            if(DataProvider.Instance.ExecuteScalar(query, new object[] { idTable }) != null)
            {
                int number = (int)DataProvider.Instance.ExecuteScalar(query, new object[] { idTable });
                return number;
            }else
            {
                return 0;
            }
        }
        public void InsertBill(int idTable )
        {
            string query = "EXEC USP_InsertBill @idTable";
            DataProvider.Instance.ExecuteNonQuery(query, new object[] {idTable});
        }
        public int getIdBillMax()
        {
            string query = "select Max(id) from Bill";
            return (int)DataProvider.Instance.ExecuteScalar(query);
        }
        public int getIDBill(int idTable)
        {
            string query = "select id from Bill where idTable = @id and status = 0";
            
            if(DataProvider.Instance.ExecuteScalar(query, new object[] { idTable }) != null) 
            {
                return (int)DataProvider.Instance.ExecuteScalar(query, new object[] { idTable });
            }
            return 0;
        }
        public void CheckOut(int id, double disCount , double totalPrice, double finalPrice)
        {
            string query = "update Bill set status = 1 , timeCheckout = GETDATE() , discount = @disCount , totalprice = @totalPrice , finalPrice = @finalPrice where id = @id";
            DataProvider.Instance.ExecuteNonQuery(query, new Object[]{ disCount,totalPrice,finalPrice,id });
        }
        public DataTable getAllBillAdmin(DateTime checkin , DateTime checkout)
        {          
            string query = " EXEC USP_getAllBill @timeCheckin , @timeCheckout";
            DataTable data = new DataTable();
            data =DataProvider.Instance.ExecuteQuery(query, new Object[] { checkin,checkout});
            return data;
        }
        public DataTable getAllBillAdminByPage(DateTime checkin, DateTime checkout, int PageNum)
        {
            string query = " EXEC USP_GetListBillByDateAndPage @timeCheckin , @timeCheckout , @PageNum";
            DataTable data = new DataTable();
            data =DataProvider.Instance.ExecuteQuery(query, new Object[] { checkin, checkout, PageNum });
            return data;
        }
        public int getNumberBills(DateTime checkin, DateTime checkout)
        {
            string query = " EXEC USP_GetNumBill  @timeCheckin , @timeCheckout";
            if(DataProvider.Instance.ExecuteScalar(query, new object[] {checkin, checkout })!=null)
            {
               return (int)DataProvider.Instance.ExecuteScalar(query, new Object[] { checkin , checkout });
                
            }
            return 0;
        }
  

    }
    
}
