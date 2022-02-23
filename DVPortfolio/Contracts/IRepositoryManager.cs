using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        IMainCategoryRepository MainCategory { get; }
        ISubcategoryRepository Subcategory { get; }

        IPhotoRepository Photo { get; }
        IVideoRepository Video { get; }
        IWebsiteRepository Website { get;  }

        void Save();

    }
}
