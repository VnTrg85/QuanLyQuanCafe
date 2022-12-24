using QuanLyCafeReal.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCafeReal.DAO
{
    internal class TableDAO
    {
        private static TableDAO instance;
        

        internal static TableDAO Instance { get { if (instance == null) instance = new TableDAO();return instance; }
        private set { instance=value; } }
        public static int width=200;

        public static int height=100;


        private TableDAO() { }

        public List<Table> loadTableList()
        {
            List<Table> list = new List<Table>();
            string query = "select * from TableFood";
            DataTable listTable =DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in listTable.Rows)
            {
                Table table = new Table(item);
                list.Add(table);
            }
            return list;
        }
        public int getUnCheckTable(int idTable)
        {
            string query = "select Count(*) from TableFood where id = @idTable and status = 0";
            int number = (int)DataProvider.Instance.ExecuteScalar(query, new object[] { idTable });
            return number;
        }
        public void SetStatus(int idTable)
        {
            string query = " update TableFood set status = 1 where id = @id";
            DataProvider.Instance.ExecuteNonQuery(query, new object[] { idTable });
        }
        public void SetStatusintoUnCheck(int idTable)
        {
            string query = " update TableFood set status = 0 where id = @id";
            DataProvider.Instance.ExecuteNonQuery(query, new object[] { idTable });
        }
        public void SwitchTable(int idTable1 , int idTable2)
        {
            string query = "EXEC USP_SwitchTable @idTable1 , @idTable2";
            DataProvider.Instance.ExecuteNonQuery(query, new object[] { idTable1 ,idTable2 });
        }
        public bool updateTable(string id, string name, string status)
        {
            string query = " update TableFood set name = @name , status = @status where id = @id";
            int check = DataProvider.Instance.ExecuteNonQuery(query, new object[] { name,status, id });
            if (check > 0)
            {
                return true;
            }
            return false;
        }
        public bool deleteTable(string id)
        {
            string query = string.Format("delete from TableFood where id = N'{0}'", id);
            int check = DataProvider.Instance.ExecuteNonQuery(query);
            if (check >0)
                return true;
            else
                return false;
        }
        public bool addTable(string name)
        {
            string query = string.Format("Insert into TableFood values(N'{0}',0)", name);
            int check = DataProvider.Instance.ExecuteNonQuery(query);
            if (check>0)
                return true;
            else
                return false;
        }
        public List<Table> getListBySearch(string text)
        {
            List<Table> list = new List<Table>();
            string query = string.Format("select * from TableFood where [dbo].[ConvertToUnsign](name) like '%'+[dbo].[ConvertToUnsign](N'{0}')+'%'", text);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Table table = new Table(item);
                list.Add(table);
            }
            return list;
        }
    }
}
