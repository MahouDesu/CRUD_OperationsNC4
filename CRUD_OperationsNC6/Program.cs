using System;
using MySql.Data.MySqlClient;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace CRUD_OperationsNC6
{
    class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            string jsonText = File.ReadAllText("appsettings.development.json");
#else
            string jsonText = File.ReadAllText("appsettings.release.json");
#endif
            var connString = JObject.Parse(jsonText)["ConnectionStrings"]["DefaultConnection"].ToString();
            var prod = new ProductRepository(connString);

            ProductRepository repo = new ProductRepository(connString);

            Console.WriteLine("please choose one of the following");
            Console.WriteLine("1) View a product");
            Console.WriteLine("2) update a product");
            Console.WriteLine("3) create a product");
            Console.WriteLine("4) delete a product by a Product ID");
            Console.WriteLine("5) delete a product by its Name");
            Console.WriteLine("6) delete a product by its Product ID and Name");
            Console.WriteLine("7) exit");

            string response = Console.ReadLine();
            Console.Clear();
            bool exitClause = true;
            while(exitClause == true)
            {
                switch (response)
                {
                    case "1":
                        var product = new ProductRepository(connString);
                        List<Product> products = product.GetProducts();
                        Console.ReadLine();
                        exitClause = false;
                        break;

                    case "2":
                        Console.WriteLine("what is the product ID you would like to use for this item?");
                        int productID = int.Parse(Console.ReadLine());

                        Console.WriteLine("what would you like it's name to be?");
                        string name = Console.ReadLine();

                        Console.WriteLine("what is the new product's category id?");
                        int categoryID = int.Parse(Console.ReadLine());

                        Console.WriteLine("what is the price of the new product?");
                        int price = int.Parse(Console.ReadLine());

                        repo.UpdateProduct(new Product() { ProductID = productID, Name = name, Price = price, CategoryID = categoryID, });
                        exitClause = false;
                        break;

                    case "3":
                        Console.WriteLine("what is the name of the product you would like to add?");
                        string name1 = Console.ReadLine();

                        Console.WriteLine("what is the price of your new product?");
                        decimal price1 = int.Parse(Console.ReadLine());

                        Console.WriteLine("what will your category ID be?");
                        int catID1 = int.Parse(Console.ReadLine());

                        repo.CreateProduct(new Product() { Name = name1, Price = price1, CategoryID = catID1, });
                        exitClause = false;
                        break;

                    case "4":
                        Console.WriteLine("what is the product ID of the item you wish to delete?");
                        int pID = int.Parse(Console.ReadLine());
                        repo.DeleteProductByID(pID);
                        exitClause = false;
                        break;
                    case "5":
                        Console.WriteLine("What is the Name of the item you wish to delete?");
                        string productName = Console.ReadLine();
                        repo.DeleteProductByName(productName);
                        exitClause = false;
                        break;
                    case "6":
                       Console.WriteLine("What is the Name of the item you wish to delete?");
                        productName = Console.ReadLine();
                        Console.WriteLine("What is the Product ID of the item you wish to delete?");
                        pID = int.Parse(Console.ReadLine());
                        repo.DeleteProduct(pID, productName);
                        exitClause = false;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
