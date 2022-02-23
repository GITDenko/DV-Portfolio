using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class ProductDto
    {
        public string ProductUrl { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool Hidden { get; set; }
    }
}
