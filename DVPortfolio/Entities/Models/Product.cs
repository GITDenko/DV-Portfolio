using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductUrl { get; set; }
        public DateTime CreatedOn { get; set; }
        [Required(ErrorMessage = "Hidden is a required field.")]
        public bool Hidden { get; set; }

        [ForeignKey(nameof(MainCategory))]
        public int? MainCategoryId { get; set; }
        public MainCategory MainCategory { get; set; }
        [ForeignKey(nameof(Subcategory))]
        public int? SubcategoryId { get; set; }
        public Subcategory Subcategory { get; set; }

    }
}
