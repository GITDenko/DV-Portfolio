using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Website : Product
    {
        [Required(ErrorMessage = "Employee name is a required field.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Thumbnail URL is a required field.")]
        public string ImageURL { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [NotMapped]
        public string ImageSrc { get; set; }
    }
}
