using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IPhotoRepository
    {
        IEnumerable<Photo> GetPhotosByMain(int mainCateogryId, bool trackChanges);
        IEnumerable<Photo> GetPhotosBySub(int subcateogryId, bool trackChanges);

        Photo GetPhotoByMain(int mainCategoryId, int Id, bool trackChanges);
        Photo GetPhotoBySub(int subcategoryId, int Id, bool trackChanges);

        void CreatePhotoByMain(int mainCategoryId, Photo photo);
        void CreatePhotoBySub(int mainCategoryId, int subcategoryId, Photo photo);

        void DeletePhoto(Photo photo);
    }
}
