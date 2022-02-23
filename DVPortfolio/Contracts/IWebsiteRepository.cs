using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IWebsiteRepository
    {
        IEnumerable<Website> GetWebsitesByMain(int mainCateogryId, bool trackChanges);
        IEnumerable<Website> GetWebsitesBySub(int subcateogryId, bool trackChanges);

        Website GetWebsiteByMain(int mainCategoryId, int Id, bool trackChanges);
        Website GetWebsiteBySub(int subcategoryId, int Id, bool trackChanges);


        void CreateWebsiteByMain(int mainCategoryId, Website website);
        void CreateWebsiteBySub(int mainCategoryId, int subcategoryId, Website website);

        void DeleteWebsite(Website website);
    }
}
