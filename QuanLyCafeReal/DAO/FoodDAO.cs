using QuanLyCafeReal.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCafeReal.DAO
{
    internal class FoodDAO
    {
        private static FoodDAO instance;

        internal static FoodDAO Instance { get { if (instance == null) instance = new FoodDAO(); return  instance; } set => instance=value; }
        private FoodDAO() { }
        public List<Food> loadFoodByIdCate(int id)
        {
            List<Food> foods= new List<Food>();
            string query = "select * from Food where idCategory = @id";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] {id});
            foreach (DataRow row in data.Rows)
            {
                Food item = new Food(row);
                foods.Add(item);
            }
            return foods;
        }
        public DataTable loadFoodAdmin()
        {
            string query = "select id,Name,idCategory,price from Food";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }
        public bool InsertFood( string name, string idCategory, float price)
        {
            string query = string.Format(" Insert into Food values( N'{0}' , N'{1}' , N'{2}'  )",name,idCategory,price);
            int numcheck=DataProvider.Instance.ExecuteNonQuery(query);
            if (numcheck>0)
                return true;
            return false;
        }
        public bool UpdateFood(string name, string idCategory, string price, string id)
        {
            string query = "Update Food set Name = @name , idCategory = @idCategory , price = @price where id = @id";
            if (DataProvider.Instance.ExecuteNonQuery(query, new object[] { name, idCategory, price ,id }) > 0) 
            {
                return true;
            }
            return false;
        }
        public bool DeleteFood(string idFood)
        {
            string query = "EXEC USP_DeleteFood @idFood";
            if (DataProvider.Instance.ExecuteNonQuery(query, new object[] { idFood }) > 0)
            {
                return true;
            }
            return false;
        }

        public DataTable ListFoodBySearch(string name)
        {
            List<Food> list = new List<Food>();
            string query = string.Format(" select id,Name,idCategory,price from Food where [dbo].[ConvertToUnsign](Name) like '%'+[dbo].[ConvertToUnsign](N'{0}')+'%' ", name);
            DataTable data =DataProvider.Instance.ExecuteQuery(query);
            return data;
        }
    }
}
