using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoSystem10LabaKapetz
{
    public interface ICrudWareHouse
    {
        List<Product> ReadAllProducts();
        Product Read(int position);
        void Update(int productPosition, int id, string name, double price, int quantity);
        void Delete(Product product);
        void Create(Product product);
        List<Product> ReadById(int id);
        List<Product> ReadByName(string name);
        List<Product> ReadByPrice(double price);
        List<Product> ReadByQuantity(int quantity);
    }
}
