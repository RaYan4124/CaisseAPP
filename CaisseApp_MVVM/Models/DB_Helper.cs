using System.Configuration;
using System.Data;
using System.Windows;
using Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Models
{
    /* DB_Helper Class centralize everythings that is in relation with data base, connection, different query's
       and multiple methodes to add or get from it*/
    public class DB_Helper
    {
        //connection informations related to the data base.
        string Db_infos = "Server=localhost;Database=Products;User Id=root;Password=root;Port=3306;";

        //Methode from DB_Helper Class that get all products from the data base and return them in the form of a List.
        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            using (MySqlConnection connexion = new MySqlConnection(Db_infos)) //initialize connection (into using to insure auto-closing connection at the end.)
            {
                try
                {
                    connexion.Open();

                    string query = "SELECT BarCode, Name, Price, AddDate FROM product";  //query to get products.

                    using (MySqlCommand cmd = new MySqlCommand(query, connexion))  //prepare a command on the connection and a query .
                    using (MySqlDataReader reader = cmd.ExecuteReader())  //execute the commande that return one or multiple results (MySqlDataReader Object) and stock them in reader.
                    {
                        while (reader.Read()) 
                        {
                            products.Add(new Product(reader.GetInt32("BarCode"), reader.GetString("Name"), reader.GetInt32("Price"))); //create a new product with values returned by execution of the command and push it into the List.
                        }
                    }
                }
                catch (MySqlException sqlerr)
                {
                    MessageBox.Show("Error", $"{sqlerr.Message}", MessageBoxButton.OK);
                }
                catch (Exception err)
                {
                    MessageBox.Show("Error", $"{err.Message}", MessageBoxButton.OK);
                }
            }
            return products;
        }

        //push new product into the DataBase.
        public void AddProduct(int id, string name, int price)
        {
            Product newProduct = new Product(id, name, price); 
            using(MySqlConnection connection = new MySqlConnection(Db_infos))//initialize connection (into using to insure auto-closing connection at the end.)
            {
                try
                {
                    connection.Open();

                    string query = "INSERT INTO product (Name, Price, BarCode, AddDate) VALUES (@name, @price, @id, @adddate)";   //query to insert the product into database, using @ parameters to prevent against SQL injection;

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))  //prepare a command on the connection and a query .
                    {
                        cmd.Parameters.AddWithValue("@name", newProduct.Name);     //parameters of the query
                        cmd.Parameters.AddWithValue("@price", newProduct.Price);
                        cmd.Parameters.AddWithValue("@id", newProduct.Id);
                        cmd.Parameters.AddWithValue("@adddate", newProduct.AddDate);

                        int rows_affected = cmd.ExecuteNonQuery();  //return the number of affected rows

                        if (rows_affected > 0)  //if equal to 0, it's mean that no rows has affected 
                        {
                            MessageBox.Show("product added Successfuly!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("error insert !", "Failure", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }  
                }
                catch (MySqlException sqlerr)
                {
                    MessageBox.Show("Error", $"{sqlerr.Message}", MessageBoxButton.OK);
                }
                catch (Exception err)
                {
                    MessageBox.Show("Error", $"{err.Message}", MessageBoxButton.OK);
                }
            }

        }

        //check if product p exist into the database by sending a query
        public bool AlreadyExist(Product p)
        {
            if (p == null) {    //if product doesnt exist (not created)
                return false;
            }

            string query = "SELECT * FROM product WHERE Name LIKE CONCAT('%',@ProductName,'%')";  //select all product that contain name of the product on the database

            using(MySqlConnection connection = new MySqlConnection(Db_infos))   //create a connection
            {
                try {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))  //creat command
                    {
                        cmd.Parameters.AddWithValue("@ProductName", p.Name);  //set query's parameter to product name
                        MySqlDataReader reader = cmd.ExecuteReader();        //execute commande
                        if (reader.HasRows)      //if there is rows it's mean that query return a result
                        {
                            MessageBox.Show("product EXIST", "", MessageBoxButton.OK);
                            return true;
                        }
                    }

                }
                catch (MySqlException sqlerr)
                {
                    MessageBox.Show("Error", $"{sqlerr.Message}", MessageBoxButton.OK);
                }
                catch (Exception err)
                {
                    MessageBox.Show("Error", $"{err.Message}", MessageBoxButton.OK);
                }

                //at the end if nothing returned it's mean that product dont existe
                MessageBox.Show("product dont EXIST", "", MessageBoxButton.OK);
                return false;
            }
        }

        //delete product p from the database if the latter existe into the database (by ID)
        public void DeleteProduct(int id_product)
        {
            if (id_product == default) { return; }  //if id product equal to 0

            string query = "DELETE FROM product WHERE BarCode = @idproduct";

            using(MySqlConnection connection = new MySqlConnection(Db_infos))
            {
                try
                {
                    connection.Open();
                    using(MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@idproduct", id_product);
                        int rows_deleted = cmd.ExecuteNonQuery();  //return number of deleted rows

                        if (rows_deleted > 0){
                            MessageBox.Show("Product deleted", "", MessageBoxButton.OK);
                        }
                        else
                        {
                            MessageBox.Show("product not deleted", "", MessageBoxButton.OK);
                        }
                    }
                }
                catch (MySqlException sqlerr)
                {
                    MessageBox.Show("Error", $"{sqlerr.Message}", MessageBoxButton.OK);
                }
                catch (Exception err)
                {
                    MessageBox.Show("Error", $"{err.Message}", MessageBoxButton.OK);
                }
            }

        }
        //get product by id
        public Product GetProductById(int id)
        {
            Product product = null;   //initialize product to null first for case where product not found and not created
            string query = "SELECT * FROM product WHERE BarCode = @idproduct";
            
            using(MySqlConnection connection = new MySqlConnection(Db_infos))
            {
                try
                {
                    connection.Open();
                    using(MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@idproduct", id);
                        using(MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())  //if reader read a row
                            {
                                product = new Product(reader.GetInt32("BarCode"), reader.GetString("Name"), reader.GetInt32("Price"));
                            }
                        }
                    }
                }
                catch (MySqlException sqlerr)
                {
                    MessageBox.Show("Error", $"{sqlerr.Message}", MessageBoxButton.OK);
                }
                catch (Exception err)
                {
                    MessageBox.Show("Error", $"{err.Message}", MessageBoxButton.OK);
                }
            }

            return product;
        }
    }

}