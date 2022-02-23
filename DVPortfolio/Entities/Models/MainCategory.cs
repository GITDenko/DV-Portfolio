using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class MainCategory : CategoryContent
    {
        public ICollection<Subcategory> Subcategories { get; set; }
    }

}
