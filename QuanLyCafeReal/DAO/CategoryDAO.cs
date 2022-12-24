using QuanLyCafeReal.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCafeReal.DAO
{
    internal class CategoryDAO
    {
        private static CategoryDAO instance;

        internal static CategoryDAO Instance { get { if (instance == null) instance = new CategoryDAO(); return instance; } private set => instance=value; }
        private CategoryDAO() { }

        public List<Category> loadCategory()
        {
            List<Category> list = new List<Category>();

            string query = "select * from FoodCategory ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Category cate = new Category(item);
                list.Add(cate);
            }
            return list;
        }
        public bool updateCategory( string id,string name)
        {
            string query = " update FoodCategory set Name = @name where id = @id";
            int check = DataProvider.Instance.ExecuteNonQuery(query,new object[] {name,id});
            if(check > 0)
            {
                return true;
            }
            return false;
        }
        public bool deleteCategory(string id)
        {
            string query = string.Format("delete from FoodCategory where id = N'{0}'", id);
            int check = DataProvider.Instance.ExecuteNonQuery(query);
            if (check >0)
                return true;
            else
                return false;
        }
        public bool addCategory(string name)
        {
            string query = string.Format("Insert into FoodCategory values(N'{0}')", name);
            int check=DataProvider.Instance.ExecuteNonQuery(query) ;
            if (check>0)
                return true;
            else
                return false;
        }
        public List<Category> getListBySearch(string text)
        {
            List<Category> list = new List<Category>();
            string query = string.Format("select * from FoodCategory where [dbo].[ConvertToUnsign](name) like '%'+[dbo].[ConvertToUnsign](N'{0}')+'%'", text);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Category cate = new Category(item);
                list.Add(cate);
            }
            return list;
        }
        public bool CheckCateOnFood(string idCate)
        {
            string query = "USP_CheckDeleteCate @idCate";
            int check = (int)DataProvider.Instance.ExecuteScalar(query, new object[] { idCate });
            if (check > 0)
                return true;
            return false;
        }
    }
}
