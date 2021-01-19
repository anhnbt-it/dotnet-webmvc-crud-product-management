using System;
using System.IO;
using System.Collections.Generic;
using crud_web_application.Models;

namespace crud_web_application.Storage
{
    public class FileManager {
        string pathToFile = @"/home/anhnbt/Documents/dotnet/crud-web-application/wwwroot/products.txt";

        public List<Product> ReadFromFile() {
            List<Product> products = null;
            try
            {
                // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(pathToFile)) {
                    string line;
                    products = new List<Product>();
                    while ((line = sr.ReadLine()) != null) {
                        string[] prod = line.Split(',');
                        
                        Product product = new Product(Convert.ToInt32(prod[0]), prod[1], Convert.ToDouble(prod[2]));
                        products.Add(product);
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read: ");
                Console.WriteLine(e.Message);
            }
            return products;
        }
        
        public void WriteToFile(List<Product> products) {
            try
            {
                // Create a file to write to.
                if (!System.IO.File.Exists(pathToFile)) {
                    System.IO.File.Create(pathToFile).Dispose();
                }
                using (StreamWriter sw = new StreamWriter(pathToFile)) {
                    foreach (var item in products)
                    {
                        sw.WriteLine(item.Id + "," + item.Name + "," + item.Price);
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be write:");
                Console.WriteLine(e.Message);
            }
        }
    }   
}