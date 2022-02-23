using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class MainCategoryRepository : RepositoryBase<MainCategory>, IMainCategoryRepository
    {
        public  MainCategoryRepository(RepositoryContext repositoryContext) : base(repositoryContext) 
        {
        }

        public IEnumerable<MainCategory> GetAllMainCategories(bool trackChanges) =>
            FindAll(trackChanges)
            .OrderBy(c => c.Name)
            .ToList();

        public MainCategory GetMainCategory(int Id, bool trackChanges) =>
            FindByCondition(c => c.Id.Equals(Id), trackChanges)
            .SingleOrDefault();


        public void CreateMainCategory(MainCategory mainCategory) => Create(mainCategory);

        public void DeleteMainCategory(MainCategory mainCategory)
        {
            Delete(mainCategory);
        }

    }
}
