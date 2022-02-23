using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IVideoRepository
    {
        IEnumerable<Video> GetVideosByMain(int mainCateogryId, bool trackChanges);
        IEnumerable<Video> GetVideosBySub(int subcateogryId, bool trackChanges);
        Video GetVideoByMain(int mainCategoryId, int Id, bool trackChanges);
        Video GetVideoBySub(int subcategoryId, int Id, bool trackChanges);

        void CreateVideoByMain(int mainCategoryId, Video video);
        void CreateVideoBySub(int mainCategoryId, int subcategoryId, Video video);

        void DeleteVideo(Video video);
    }
}
