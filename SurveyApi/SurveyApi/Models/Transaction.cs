using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Models
{
    public class Transaction
    {
        public int transaction_id { get; set; }

        public int food_id { get; set; }
        public Food Food { get; set; }
        public int customer_id { get; set;}
        public Customer Customer { get; set; }
        public DateTime transaction_date { get; set; }
        public decimal amount { get; set; }
    }
}
