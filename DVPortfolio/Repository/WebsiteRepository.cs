using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class WebsiteRepository : RepositoryBase<Website>, IWebsiteRepository
    {
        public WebsiteRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
        // GET: Website
        public IEnumerable<Website> GetWebsitesByMain(int mainCategoryId, bool trackChanges) =>
            FindByCondition(e => e.MainCategoryId.Equals(mainCategoryId), trackChanges)
            .OrderBy(e => e.CreatedOn);

        public IEnumerable<Website> GetWebsitesBySub(int subcategoriesId, bool trackChanges) =>
            FindByCondition(e => 
            e.SubcategoryId.Equals(subcategoriesId), trackChanges)
            .OrderBy(e => e.CreatedOn);

        public Website GetWebsiteByMain(int mainCategoryId, int Id, bool trackChanges) =>
            FindByCondition(e =>
            e.MainCategoryId.Equals(mainCategoryId) &&
            e.Id.Equals(Id), trackChanges)
            .SingleOrDefault();

        public Website GetWebsiteBySub(int subcategoryId, int Id, bool trackChanges) =>
            FindByCondition(e =>
            e.SubcategoryId.Equals(subcategoryId) &&
            e.Id.Equals(Id), trackChanges)
            .SingleOrDefault();

        // POST: Website
        public void CreateWebsiteByMain(int mainCateogryId, Website website)
        {
            website.MainCategoryId = mainCateogryId;
            website.CreatedOn = DateTime.Now;
            website.Hidden = false;
            Create(website);
        }

        public void CreateWebsiteBySub(int mainCateogryId, int subcateogryId, Website website)
        {
            //Tror inte mainCategoryId behövs?
            website.SubcategoryId = subcateogryId;
            website.CreatedOn = DateTime.Now;
            website.Hidden = false;
            Create(website);
        }

        // DELETE: Video
        public void DeleteWebsite(Website website)
        {
            Delete(website);
        }

    }
}
