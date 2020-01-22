using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Text;

namespace CRUD_OperationsNC6
{
    public class ProductRepository
    {
        public string connString;

        public ProductRepository(string _connectionString)
        {
            connString = _connectionString;
        }
        public List<Product> GetProducts()
        {
            MySqlConnection conn = new MySqlConnection(connString);
            List<Product> products = new List<Product>();

            using (conn)
            {
                conn.Open();

                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT StockLevel, Name, Price, ProductID FROM products;";

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        StockLevel = (string)reader["StockLevel"],
                        Name = (string)reader["Name"],
                        Price = (decimal)reader["Price"],
                        ProductID = (int)reader["ProductID"]

                    };
                    products.Add(product);
                    Console.WriteLine($"{product.ProductID}.....{product.Name}.....{product.Price}.....{product.StockLevel}");
                }
                
            }
            return products;
        }

        public void CreateProduct(Product prod)
        {
            MySqlConnection conn = new MySqlConnection(connString);

            using (conn)
            {
                conn.Open();

                var cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO products (Name, Price, CategoryID, OnSale) VALUES (@n, @p, @cID, @sale);";
                cmd.Parameters.AddWithValue("n", prod.Name);
                cmd.Parameters.AddWithValue("p", prod.Price);
                cmd.Parameters.AddWithValue("cID", prod.CategoryID);
                cmd.Parameters.AddWithValue("sale", prod.OnSale);
                cmd.ExecuteNonQuery();
            }

        }

        public void UpdateProduct(Product prod)
        {
            var conn = new MySqlConnection(connString);

            using (conn)
            {
                conn.Open();

                var cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE products SET Name = @n, Price = @p, CategoryID = @cID, OnSale = @sale WHERE ProductID = @pID;";
                cmd.Parameters.AddWithValue("n", prod.Name);
                cmd.Parameters.AddWithValue("p", prod.Price);
                cmd.Parameters.AddWithValue("cID", prod.CategoryID);
                cmd.Parameters.AddWithValue("sale", prod.OnSale);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteProductByID(int id)
        {
            var conn = new MySqlConnection(connString);

            using (conn)
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM products WHERE ProductID = @id;";
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteProductByName(string name)
        {
            var conn = new MySqlConnection(connString);

            using (conn)
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM products WHERE Name = @name;";
                cmd.Parameters.AddWithValue("name", name);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteProduct(int id, string name)
        {
            var conn = new MySqlConnection(connString);

            using (conn)
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM products WHERE Name = @name AND ProductID = @id;";
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("name", name);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
