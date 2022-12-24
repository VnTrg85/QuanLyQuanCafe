using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCafeReal.DTO
{
    internal class Category
    {
        private int id;
        public int Id { get; set; }
        private string name;
        public string Name { get => name; set => name=value; }
        public Category(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
        public Category() { }

        public Category(DataRow data)
        {
            this.Id = (int)data["id"];
            this.Name = (string)data["Name"];
        }

        
    }
}
