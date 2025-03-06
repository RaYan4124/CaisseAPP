using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{ 

    public class Product
    {
        [Key]
        public int Id { get; private set; }
        public string Name { get; private set; }

        public int Price { get; private set; }
        public DateTime AddDate { get; private set;}


        public Product(int Id, string name, int price)
        {
            this.Id = Id;
            this.Name = name;
            this.Price = price;
            AddDate = DateTime.Now;
        }
        
    }

}