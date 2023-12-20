using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoSystem10LabaKapetz
{
    //складмен
    public class WareHouseSystem : ICrudWareHouse
    {
        private readonly int _idPos;
        private readonly int _namePos;
        private readonly int _pricePos;
        private readonly int _quantityPos;
        private readonly AppDbContext _dbContext;

        public WareHouseSystem()
        {
            _idPos = (int)(Console.WindowWidth * 0.15);
            _namePos = _idPos * 2;
            _pricePos = _idPos * 3;
            _quantityPos = _idPos * 4;
            _dbContext = FileManager.GetAppDbContext();
        }
        public void ProceedWareHouseSystem(User wareHouseManager)
        {
            if (wareHouseManager.Position != Position.WarehouseManager)
                return;
            ShowWareHouseMenu(wareHouseManager, ReadAllProducts());
        }

        private void ShowWareHouseMenu(User wareHouseManager, List<Product> products)
        {
            int choice = 0;
            while (choice != -1)
            {
                ConsoleHelper.ShowHeader(wareHouseManager);
                ConsoleHelper.ShowMainSystemKeys();
                ShowProducts(products);
                var numberOfProducts = ReadAllProducts().Count;
                choice = ArrowsMenu.Show(3, 3, numberOfProducts + 2);

                switch (choice)
                {
                    case -5:
                        ShowAddForm(wareHouseManager);
                        break;
                    case -4:
                        ShowFindForm(wareHouseManager);
                        break;
                    case -6:
                    case -7:
                    case -3:
                    case -2:
                    case -1:
                        break;
                    default:
                        ShowChangeForm(wareHouseManager, choice - 3);
                        break;
                }
            }
        }

        //эврибади дэнс нау
        private void ShowFindForm(User admin)
        {
            int choice = 0;

            while (choice != -1 && choice > 6 || choice < 3)
            {
                Console.Clear();
                ConsoleHelper.ShowHeader(admin);

                Console.SetCursorPosition(0, 2);
                Console.WriteLine("  > Поиск по: <");
                Console.WriteLine("  ID");
                Console.WriteLine("  Название");
                Console.WriteLine("  Цена");
                Console.WriteLine("  Количество");
                choice = ArrowsMenu.Show(3, 3, 6);
            }

            switch (choice)
            {
                case 3:
                    Console.WriteLine("Напишите ID для поиска");
                    int id = 0;
                    while (!int.TryParse(Console.ReadLine(), out id)) ;
                    ShowWareHouseMenu(admin, ReadById(id));
                    break;
                case 4:
                    Console.WriteLine("Напишите название для поиска");
                    ShowWareHouseMenu(admin, ReadByName(Console.ReadLine()));
                    break;
                case 5:
                    Console.WriteLine("Напишите цену для поиска");
                    double price = 0;
                    while (!double.TryParse(Console.ReadLine(), out price)) ;
                    ShowWareHouseMenu(admin, ReadByPrice(price));
                    break;
                case 6:
                    Console.WriteLine("Напишите количество для поиска");
                    int quantity;
                    while (!int.TryParse(Console.ReadLine(), out quantity)) ;
                    ShowWareHouseMenu(admin, ReadByQuantity(quantity));
                    break;
                default:
                    break;
            }
            Console.Clear();

        }

        private void ShowAddForm(User wareHouseManager)
        {
            Console.Clear();
            var choice = 0;
            var id = 0;
            var name = "";
            double price = 0;
            int quantity = 0;
            while (choice != -1)
            {
                ConsoleHelper.ShowHeader(wareHouseManager);
                PrintChangeForm(id.ToString(), name, price.ToString(), quantity.ToString());
                ConsoleHelper.ShowAddSystemKeys();
                choice = ArrowsMenu.Show(2, 2, 5);
                Console.Clear();
                ConsoleHelper.ShowAddSystemKeys();
                ConsoleHelper.ShowHeader(wareHouseManager);
                switch (choice)
                {
                    case 2:
                        {
                            PrintChangeForm("", name, price.ToString(), quantity.ToString());
                            Console.SetCursorPosition(5, 2);
                            while (!int.TryParse(Console.ReadLine(), out id))
                            {
                                Console.SetCursorPosition(5, 2);
                            }
                            break;
                        }
                    case 3:
                        {
                            PrintChangeForm(id.ToString(), "", price.ToString(), quantity.ToString());
                            Console.SetCursorPosition(7, 3);
                            name = Console.ReadLine();
                            break;
                        }
                    case 4:
                        {
                            PrintChangeForm(id.ToString(), name, "", quantity.ToString());
                            Console.SetCursorPosition(8, 4);
                            while (!double.TryParse(Console.ReadLine(), out price))
                            {
                                Console.SetCursorPosition(8, 4);
                            }
                            break;
                        }
                    case 5:
                        {
                            PrintChangeForm(id.ToString(), name, price.ToString(), "");
                            Console.SetCursorPosition(11, 5);
                            while (!int.TryParse(Console.ReadLine(), out quantity))
                            {
                                Console.SetCursorPosition(11, 5);
                            }
                            break;
                        }
                    case -2:
                        {
                            Create(new Product(id, name, price, quantity));
                            choice = -1;
                            Console.Clear();
                            break;
                        }
                    case -3:
                        break;
                    default:
                        break;
                }
            }
        }

        private void ShowChangeForm(User admin, int productPosition)
        {
            int choice = 0;
            var product = Read(productPosition);
            var id = product.Id;
            var name = product.Name;
            var price = product.Price;
            int quantity = product.QuantityInStock;


            while (choice != -1)
            {
                ConsoleHelper.ShowHeader(admin);
                PrintChangeForm(id.ToString(), name, price.ToString(), quantity.ToString());
                ConsoleHelper.ShowChangeSystemKeys();
                choice = ArrowsMenu.Show(2, 2, 5);
                Console.Clear();
                ConsoleHelper.ShowChangeSystemKeys();
                ConsoleHelper.ShowHeader(admin);
                switch (choice)
                {
                    case 2:
                        {
                            PrintChangeForm("", name, price.ToString(), quantity.ToString());
                            Console.SetCursorPosition(5, 2);
                            while (!int.TryParse(Console.ReadLine(), out id))
                            {
                                Console.SetCursorPosition(5, 2);
                            }
                            break;
                        }
                    case 3:
                        {
                            PrintChangeForm(id.ToString(), "", price.ToString(), quantity.ToString());
                            Console.SetCursorPosition(7, 3);
                            name = Console.ReadLine();
                            break;
                        }
                    case 4:
                        {
                            PrintChangeForm(id.ToString(), name, "", quantity.ToString());
                            Console.SetCursorPosition(8, 4);
                            while (!double.TryParse(Console.ReadLine(), out price))
                            {
                                Console.SetCursorPosition(8, 4);
                            }
                            break;
                        }
                    case 5:
                        {
                            PrintChangeForm(id.ToString(), name, price.ToString(), "");
                            Console.SetCursorPosition(11, 5);
                            while (!int.TryParse(Console.ReadLine(), out quantity))
                            {
                                Console.SetCursorPosition(11, 5);
                            }
                            break;
                        }
                    case -2:
                        {
                            Update(productPosition, id, name, price, quantity);
                            break;
                        }
                    case -3:
                        Delete(product);
                        choice = -1;
                        Console.Clear();
                        break;
                    default:
                        break;
                }
            }
        }

        private void PrintChangeForm(string id, string name, string price, string quantity)
        {
            Console.SetCursorPosition(0, 2);
            Console.WriteLine($"  ID:{id}");
            Console.WriteLine($"  Название:{name}");
            Console.WriteLine($"  Цена/шт:{price}");
            Console.WriteLine($"  Количество:{quantity}");
        }

        private void ShowProducts(List<Product> products)
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

            foreach (var product in products)
            {
                Console.SetCursorPosition(_idPos, cursorHeight);
                Console.Write($"{product.Id}");
                Console.SetCursorPosition(_namePos, cursorHeight);
                Console.Write($"{product.Name}");
                Console.SetCursorPosition(_pricePos, cursorHeight);
                Console.Write($"{product.Price}");
                Console.SetCursorPosition(_quantityPos, cursorHeight);
                Console.Write($"{product.QuantityInStock}");
                cursorHeight++;
            }
        }


        public List<Product> ReadAllProducts()
        {
            return _dbContext.Products;
        }

        public Product Read(int position)
        {
            return _dbContext.Products[position];
        }

        public void Update(int productPosition, int id, string name, double price, int quantity)
        {
            _dbContext.Products[productPosition] = new Product(id, name, price, quantity);
        }

        public void Delete(Product product)
        {
            _dbContext.Products.Remove(product);
        }

        public void Create(Product product)
        {
            _dbContext.Products.Add(product);
        }

        public List<Product> ReadById(int id)
        {
            return _dbContext.Products.Where(x => x.Id == id).ToList();
        }

        public List<Product> ReadByName(string name)
        {
            return _dbContext.Products.Where(x => Equals(x.Name, name)).ToList();
        }

        public List<Product> ReadByPrice(double price)
        {
            return _dbContext.Products.Where(x => Math.Abs(x.Price - price) < 0.001).ToList();
        }

        public List<Product> ReadByQuantity(int quantity)
        {
            return _dbContext.Products.Where(x => x.QuantityInStock == quantity).ToList();
        }
    }
}
