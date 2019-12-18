using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace aspFF.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        public int Reserved { get; set; }

        public int Multiply()
        {
            return Reserved * Price;
        }

        public int Sum(IEnumerable<Item> items)
        {
            int sum = 0;
            foreach (var item in items)
            {
                sum += (item.Reserved * item.Price);
            }
            return sum;

        }
    }
}
