using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Dto
{
    public class DtoResponse
    {
        public bool isSuccess { get; set; }

        public string Message { get; set; }

        public List<string> ErrorMessages{ get; set; }

        public object result { get; set; }

    }
}
