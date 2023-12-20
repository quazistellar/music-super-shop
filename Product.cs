using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoSystem10LabaKapetz
{
    public class Product
    {
        public Product(int id, string name, double price, int quantityInStock)
        {
            Id = id;
            Name = name;
            Price = price;
            QuantityInStock = quantityInStock;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int QuantityInStock { get; set; }
    }
}
