using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace aspFF.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Item item { get; set; }
        [Required]
        public string ownerID { get; set; }
    }
}
