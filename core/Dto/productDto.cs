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
    }
}
