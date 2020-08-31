using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farmakotriftis.ViewModels
{
    public class CartItemViewModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public float Price { get; set; }
        public float UnitPrice { get; set; }
    }
}
