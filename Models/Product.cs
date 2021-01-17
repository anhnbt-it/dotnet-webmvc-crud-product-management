using System;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace crud_web_application.Models
{
    public class Product
    {

        public Product() {

        }
        public Product(int id, string name, double price) {
            Id = id;
            Name = name;
            Price = price;
        }

        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        public override string ToString() {
            return Id + "," + Name + "," + Price;
        }
    }
}