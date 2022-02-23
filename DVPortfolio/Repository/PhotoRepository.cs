using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PhotoRepository : RepositoryBase<Photo>, IPhotoRepository
    {
        public PhotoRepository(RepositoryContext repositoryContext) : base(repositoryContext) 
        {
        }

        // GET: Photos
        public IEnumerable<Photo> GetPhotosByMain(int mainCategoryId, bool trackChanges) =>
            FindByCondition(e => e.MainCategoryId.Equals(mainCategoryId), trackChanges)
            .OrderBy(e => e.CreatedOn);

        public IEnumerable<Photo> GetPhotosBySub(int subcategoriesId, bool trackChanges) =>
            FindByCondition(e => e.SubcategoryId.Equals(subcategoriesId), trackChanges)
            .OrderBy(e => e.CreatedOn);

        public Photo GetPhotoByMain(int mainCategoryId, int Id, bool trackChanges) =>
            FindByCondition(e => 
            e.MainCategoryId.Equals(mainCategoryId) && 
            e.Id.Equals(Id), trackChanges)
            .SingleOrDefault();

        public Photo GetPhotoBySub(int subcategoryId, int Id, bool trackChanges) =>
            FindByCondition(e =>
            e.SubcategoryId.Equals(subcategoryId) && 
            e.Id.Equals(Id), trackChanges)
            .SingleOrDefault();

        // POST: Photo
        public void CreatePhotoByMain(int mainCateogryId, Photo photo)
        {
            photo.MainCategoryId = mainCateogryId;
            photo.CreatedOn = DateTime.Now;
            photo.Hidden = false;
            Create(photo);
        }

        public void CreatePhotoBySub(int mainCateogryId, int subcateogryId, Photo photo)
        {
            //Tror inte mainCategoryId behövs?
            photo.SubcategoryId = subcateogryId;
            photo.CreatedOn = DateTime.Now;
            photo.Hidden = false;
            Create(photo);
        }

        // DELETE: Photo
        public void DeletePhoto(Photo photo)
        {
            Delete(photo);
        }

    }
}
