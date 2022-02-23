using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class CategoryDto
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public bool Hidden { get; set; }
    }
}