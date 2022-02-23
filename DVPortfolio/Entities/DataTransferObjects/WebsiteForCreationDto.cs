using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class WebsiteForCreationDto : ProductDto
    {
        public string Name { get; set; }
        public string ThumbnailURL { get; set; }
    }
}
