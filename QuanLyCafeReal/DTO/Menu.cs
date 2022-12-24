using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCafeReal.DTO
{
    public class Menu
    {
        private string name;

        public string Name { get => name; set => name=value; }

        private int count;
        public int Count { get => count; set => count=value; }

        private double price;
        public double Price { get => price; set => price=value; }


        private double totalPrice;
        public double TotalPrice { get => totalPrice; set => totalPrice=value; }

        public Menu(string name, int count, int price, double totalPrice)
        {
            this.Name = name;
            this.Count = count;
            this.Price = price;
            this.TotalPrice = totalPrice;
        }
        public Menu(DataRow data)
        {
            this.Name = (string)data["Name"];
            this.Count = (int)data["count"];
            this.Price =  (double)data["price"];
            this.TotalPrice = (double)data["totalPrice"];
        }

    }
}
