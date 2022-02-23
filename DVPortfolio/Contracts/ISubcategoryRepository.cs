using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ISubcategoryRepository
    {
        IEnumerable<Subcategory> GetSubcategories(int mainCateogryId, bool trackChanges);
        Subcategory GetSubcategory(int mainCategoryId, int Id, bool trackChanges);

        void CreateSubcategory(int mainCategoryId, Subcategory subcategory);

        void DeleteSubcategory(Subcategory subcategory);
    }
}
