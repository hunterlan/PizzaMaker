using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaMaker.Models
{
    /// <summary>
    /// Class <c>Order</c>, which describe table in MySQL.
    /// Has own context. 
    /// </summary>
    public class Order
    {
        public int ID { get; set; }
        /// <value>
        /// Generating randomly
        /// </value>
        public int NumberOrder { get; set; }
        /// <value>
        /// Need to identify a person, who will get
        /// </value>
        public string NameReciver { get; set; }
        /// <value>
        /// Phone of person
        /// </value>
        public string Phone { get; set; }
        /// <value>
        /// Where to deliver
        /// </value>
        public string Adress { get; set; }
        /// <value>
        /// How much have to pay
        /// </value>
        public decimal TotalPrice { get; set; }
        /// <value>
        /// Relation with Pizza. One row for one pizza. 
        /// </value>
        public int PizzaID { get; set; }
        /// <value>
        /// Count of pizza
        /// </value>
        public int Count { get; set; }

        public Order() { }

        /// <summary>
        /// Copy constructor. 
        /// </summary>
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
