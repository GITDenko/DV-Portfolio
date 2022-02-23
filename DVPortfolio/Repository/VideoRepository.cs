using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class VideoRepository : RepositoryBase<Video>, IVideoRepository
    {
        public VideoRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
        // GET: Video
        public IEnumerable<Video> GetVideosByMain(int mainCategoryId, bool trackChanges) =>
            FindByCondition(e => e.MainCategoryId.Equals(mainCategoryId), trackChanges)
            .OrderBy(e => e.CreatedOn);

        public IEnumerable<Video> GetVideosBySub(int subcategoriesId, bool trackChanges) =>
            FindByCondition(e => e.SubcategoryId.Equals(subcategoriesId), trackChanges)
            .OrderBy(e => e.CreatedOn);

        public Video GetVideoByMain(int mainCategoryId, int Id, bool trackChanges) =>
            FindByCondition(e =>
            e.MainCategoryId.Equals(mainCategoryId) &&
            e.Id.Equals(Id), trackChanges)
            .SingleOrDefault();

        public Video GetVideoBySub(int subcategoryId, int Id, bool trackChanges) =>
            FindByCondition(e => e.SubcategoryId.Equals(subcategoryId) &&
            e.Id.Equals(Id), trackChanges)
            .SingleOrDefault();

        // POST: Video
        public void CreateVideoByMain(int mainCateogryId, Video video)
        {
            video.MainCategoryId = mainCateogryId;
            video.CreatedOn = DateTime.Now;
            video.Hidden = false;
            Create(video);
        }
        public void CreateVideoBySub(int mainCateogryId, int subcateogryId, Video video)
        {
            //Tror inte mainCategoryId behövs?
            video.SubcategoryId = subcateogryId;
            video.CreatedOn = DateTime.Now;
            video.Hidden = false;
            Create(video);
        }

        // DELETE: Video
        public void DeleteVideo(Video video)
        {
            Delete(video);
        }


    }
}
