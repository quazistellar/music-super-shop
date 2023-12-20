using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoSystem10LabaKapetz
{
    public class SelectedProduct : Product
    {
        public int SelectedQuantity { get; set; }

        public SelectedProduct(int id, string name, double price, int quantityInStock, int selectedQuantity) : base(id, name, price, quantityInStock)
        {
            SelectedQuantity = selectedQuantity;
        }
    }
}
