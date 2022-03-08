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
    public class CategoryContent
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Category name is a required field.")]
        public string Name { get; set; }
        public string ImageURL { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [NotMapped]
        public string ImageSrc { get; set; }

        [Required(ErrorMessage = "Hidden is a required field.")]
        public bool Hidden { get; set; }
        public List<Photo> Photos { get; set; }
        public List<Video> Videos { get; set; }
        public List<Website> Websites { get; set; }

    }
}
