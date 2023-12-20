using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoSystem10LabaKapetz
{
    public class CashierSystem
    {
        private readonly int _idPos;
        private readonly int _namePos;
        private readonly int _pricePos;
        private readonly int _quantityPos;
        private readonly AppDbContext _dbContext;
        private List<SelectedProduct> _selectedProducts;

        public CashierSystem()
        {
            _idPos = (int)(Console.WindowWidth * 0.15);
            _namePos = _idPos * 2;
            _pricePos = _idPos * 3;
            _quantityPos = _idPos * 4;
            _dbContext = FileManager.GetAppDbContext();
        }
        public void ProceedCashierSystem(User wareHouseManager)
        {
            if (wareHouseManager.Position != Position.Cashier)
                return;
            ShowCashierMenu(wareHouseManager);
        }
        private void ShowCashierMenu(User wareHouseManager)
        {
            _selectedProducts =
                _dbContext.Products.Select(x => new SelectedProduct(x.Id, x.Name, x.Price, x.QuantityInStock, 0)).ToList();
            int choice = 0;
            while (choice != -1)
            {
                ConsoleHelper.ShowHeader(wareHouseManager);
                ConsoleHelper.ShowMainCashierSystemKeys();
                ShowProducts();
                var numberOfProducts = _dbContext.Products.Count;
                choice = ArrowsMenu.Show(3, 3, numberOfProducts + 2);
                switch (choice)
                {
                    case -2:
                        {
                            Update();
                            _selectedProducts =
                                _dbContext.Products.Select(x => new SelectedProduct(x.Id, x.Name, x.Price, x.QuantityInStock, 0)).ToList();
                            break;
                        }
                    case -5:
                    case -4:
                    case -6:
                    case -7:
                    case -3:
                    case -1:
                        break;
                    default:
                        ShowChangeForm(wareHouseManager, choice - 3);
                        break;
                }
            }
        }

        private void Update()
        {
            foreach (var selectedProduct in _selectedProducts.Where(x => x.SelectedQuantity > 0))
            {
                var product = _dbContext.Products.FirstOrDefault(x => x.Id == selectedProduct.Id);
                product.QuantityInStock -= selectedProduct.SelectedQuantity;
                _dbContext.AccountingRecords.Add(new AccountingRecord(_dbContext.AccountingRecords.Count, "Покупка", selectedProduct.SelectedQuantity * selectedProduct.Price, DateTime.Now, true));
            }
        }

        private void ShowChangeForm(User admin, int productPosition)
        {
            int choice = 0;
            var product = _selectedProducts[productPosition];
            while (choice != -1)
            {
                ConsoleHelper.ShowHeader(admin);
                ConsoleHelper.ShowAddCashierSystemKeys();
                PrintChangeForm(product.Id.ToString(), product.Name, product.Price.ToString(), product.SelectedQuantity.ToString());
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.OemPlus:
                        {
                            if (_selectedProducts[productPosition].SelectedQuantity <
                                _dbContext.Products[productPosition].QuantityInStock)
                                _selectedProducts[productPosition].SelectedQuantity++;
                            break;
                        }
                    case ConsoleKey.OemMinus:
                        {
                            if (_selectedProducts[productPosition].SelectedQuantity > 0)
                                _selectedProducts[productPosition].SelectedQuantity--;
                            break;
                        }
                    case ConsoleKey.Escape:
                        {
                            choice = -1;
                            break;
                        }
                }
            }
            Console.Clear();
        }

        private void PrintChangeForm(string id, string name, string price, string quantity)
        {
            Console.SetCursorPosition(0, 2);
            Console.WriteLine($"  ID:{id}");
            Console.WriteLine($"  Название:{name}");
            Console.WriteLine($"  Цена:{price}");
            Console.WriteLine($"  Кол-во:{quantity}");
        }

        private void ShowProducts()
        {
            Console.SetCursorPosition(_idPos, 2);
            Console.Write("ID");
            Console.SetCursorPosition(_namePos, 2);
            Console.Write("Название");
            Console.SetCursorPosition(_pricePos, 2);
            Console.Write("Цена");
            Console.SetCursorPosition(_quantityPos, 2);
            Console.Write("Количество");

            var cursorHeight = 3;
            foreach (var product in _selectedProducts)
            {
                Console.SetCursorPosition(_idPos, cursorHeight);
                Console.Write($"{product.Id}");
                Console.SetCursorPosition(_namePos, cursorHeight);
                Console.Write($"{product.Name}");
                Console.SetCursorPosition(_pricePos, cursorHeight);
                Console.Write($"{product.Price}");
                Console.SetCursorPosition(_quantityPos, cursorHeight);
                Console.Write($"{product.SelectedQuantity}");
                cursorHeight++;
            }
        }
    }
}
