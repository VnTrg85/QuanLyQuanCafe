using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace QuanLyCafeReal.DTO
{
    public class Table
    {
        private int id;
        public int Id { get; set; }
        private string name;
        public string Name { get; set; }

        private string status;
        public string Status { get; set; }
        public Table(int id, string name, string status)
        {
            this.Id = id;
            this.Name = name;
            this.Status = status;
        }
        public Table( DataRow row)
        {
            this.Id =(int)row["id"];
            this.Name = (string)row["name"];
            if ((int)row["status"] != 0) { this.Status="Co nguoi"; }
            else { this.Status="Trong"; }
        }
    }
}
