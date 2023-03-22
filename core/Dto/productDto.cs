using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Dto
{
    public class productDto
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public string description { get; set; }
        public double price { get; set; } = 0.5;
        public string location { get; set; } = "sdA";
        public int CategoryId { get; set; } = 2;
    }
}
