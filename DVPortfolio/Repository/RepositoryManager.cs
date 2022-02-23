using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private IMainCategoryRepository _mainCategoryRepository;
        private ISubcategoryRepository _subcategoryRepository;
        private IPhotoRepository _photoRepository;
        private IVideoRepository _videoRepository;
        private IWebsiteRepository _websiteRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public IMainCategoryRepository MainCategory
        {
            get
            {
                if (_mainCategoryRepository == null)
                    _mainCategoryRepository = new MainCategoryRepository(_repositoryContext);

                return _mainCategoryRepository;
            }
        }

        public ISubcategoryRepository Subcategory
        {
            get
            {
                if (_subcategoryRepository == null)
                    _subcategoryRepository = new SubcategoryRepository(_repositoryContext);

                return _subcategoryRepository;
            }
        }

        public IPhotoRepository Photo
        {
            get
            {
                if (_photoRepository == null)
                    _photoRepository = new PhotoRepository(_repositoryContext);

                return _photoRepository;
            }
        }

        public IVideoRepository Video
        {
            get
            {
                if (_videoRepository == null)
                    _videoRepository = new VideoRepository(_repositoryContext);

                return _videoRepository;
            }
        }

        public IWebsiteRepository Website
        {
            get
            {
                if (_websiteRepository == null)
                    _websiteRepository = new WebsiteRepository(_repositoryContext);

                return _websiteRepository;
            }
        }


        public void Save() => _repositoryContext.SaveChanges();
    }
}
