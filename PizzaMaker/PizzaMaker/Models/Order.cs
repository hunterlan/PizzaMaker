using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaMaker.Models
{
    public class Order
    {
        public int ID { get; set; }
        public int NumberOrder { get; set; }
        public string NameReciver { get; set; }
        public string Phone { get; set; }
        public string Adress { get; set; }
        public decimal TotalPrice { get; set; }
        public int PizzaID { get; set; }
        public int Count { get; set; }

        public Order() { }

        public Order(Order oldOrder)
        {
            NumberOrder = oldOrder.NumberOrder;
            NameReciver = oldOrder.NameReciver;
            Phone = oldOrder.Phone;
            Adress = oldOrder.Adress;
            TotalPrice = oldOrder.TotalPrice;
        }
    }
}
