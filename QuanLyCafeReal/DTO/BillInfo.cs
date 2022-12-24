using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCafeReal.DTO
{
    internal class BillInfo
    {
        private int id;      
        public int Id { get => id; set => id=value; }
        private Date timeCheckin;
        public Date TimeCheckin { get => timeCheckin; set => timeCheckin=value; }
        private Date timeCheckout;
        public Date TimeCheckout { get => timeCheckout; set => timeCheckout=value; }
        private int idTable;
        public int IdTable { get => idTable; set => idTable=value;}

        BillInfo(int id, Date timeCheckin, Date timeCheckout, int idTable) 
        {
            this.id= id;
            this.timeCheckin= timeCheckin;
            this.timeCheckout= timeCheckout;
            this.idTable= idTable;
        }
        BillInfo(DataRow data)
        {
            this.id = (int)data["id"];
            this.timeCheckin = (Date)data["timeCheckin"];
            this.timeCheckout = (Date)data["timeCheckout"];
            this.idTable = (int)data["idTable"];
        }
    }
}
