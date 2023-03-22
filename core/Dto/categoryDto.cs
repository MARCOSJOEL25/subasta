using core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Dto
{
    public class categoryDto
    {
        public int Id { get; set; }
        public string name { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
