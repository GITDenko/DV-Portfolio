using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class SubcategoryRepository : RepositoryBase<Subcategory>, ISubcategoryRepository
    {
        public SubcategoryRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<Subcategory> GetSubcategories(int mainCateogryId, bool trackChanges) =>
         FindByCondition(e => e.MainCategoryId.Equals(mainCateogryId), trackChanges)
         .OrderBy(e => e.Name);

        public Subcategory GetSubcategory(int mainCateogryId, int id, bool trackChanges) =>
            FindByCondition(e => e.MainCategoryId.Equals(mainCateogryId) && e.Id.Equals(id), trackChanges)
            .SingleOrDefault();


        public void CreateSubcategory(int mainCateogryId, Subcategory subcategory)
        {
            subcategory.MainCategoryId = mainCateogryId;
            subcategory.Hidden = false;
            Create(subcategory);
        }

        public void DeleteSubcategory(Subcategory subcategory)
        {
            Delete(subcategory);
        }
    }
}