using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCafeReal.DTO
{
    internal class Food
    {
        private int id;
        public int Id { get; set; }
        private string name;
        public string Name { get; set; }
        private int idCategory;
        public int IdCategory { get => idCategory; set => idCategory=value; }
        private float price;
        public float Price { get => price; set => price=value; }

        public Food(int id,string name, int idCategory, float price)
        {
            this.id = id;
            this.name = name;
            this.idCategory = idCategory;
            this.price = price;
        }
        public Food() { }
        public Food(DataRow data)
        {
            this.Id =(int)data["id"];
            this.Name =(string)data["Name"];
            this.idCategory = (int)data["idCategory"];
            this.price = (float)Convert.ToDouble(data["price"].ToString());
        }

    }
}
