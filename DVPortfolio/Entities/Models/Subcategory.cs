using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Subcategory : CategoryContent
    {
        [Required(ErrorMessage = "Subcategory must have a Main Category")]
        [ForeignKey(nameof(MainCategory))]
        public int MainCategoryId { get; set; }
        public MainCategory MainCategory { get; set; }
    }
}
