using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public string ThumbnailURL { get; set; }
    }
}
