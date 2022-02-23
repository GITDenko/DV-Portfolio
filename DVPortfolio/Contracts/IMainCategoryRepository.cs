using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IMainCategoryRepository
    {
        IEnumerable<MainCategory> GetAllMainCategories(bool trackChanges);
        MainCategory GetMainCategory(int Id, bool trackChanges);

        void CreateMainCategory(MainCategory mainCategory);

        void DeleteMainCategory(MainCategory mainCategory);
    }
}
