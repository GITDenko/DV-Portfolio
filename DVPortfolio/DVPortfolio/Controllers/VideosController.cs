using AutoMapper;
using Contracts;
using Entities;
using Entities.DataTransferObjects;
using LoggerService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVPortfolio.Controllers
{
    [Route("api/maincategory/{MainCategoryId}")]
    [ApiController]
    public class VideosController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public VideosController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: All videos from Main Category
        [HttpGet("videos")]
        public IActionResult GetVideosForMaincateogry(int MainCategoryId)
        {
            var maincategory = _repository.MainCategory.GetMainCategory(MainCategoryId, trackChanges: false);
            if (maincategory == null)
            {
                _logger.LogInfo($"Main cateogry with id: {MainCategoryId} doesn't exist in the database.");
                return NotFound();
            }

            var videosFromDb = _repository.Video.GetVideosByMain(MainCategoryId, trackChanges: false);

            var videosDto = _mapper.Map<IEnumerable<VideoDto>>(videosFromDb);

            return Ok(videosDto);
        }

        // GET: All Videos from Subcategory
        [HttpGet("subcategory/{SubcategoryId}/videos")]
        public IActionResult GetVideosForSubcateogry(int MainCategoryId, int SubcategoryId)
        {
            var maincategory = _repository.MainCategory.GetMainCategory(MainCategoryId, trackChanges: false);
            if (maincategory == null)
            {
                _logger.LogInfo($"Main cateogry with id: {MainCategoryId} doesn't exist in the database.");
                return NotFound();
            }

            var subcategoriesFromDb = _repository.Subcategory.GetSubcategory(MainCategoryId, SubcategoryId, trackChanges: false);
            if (subcategoriesFromDb == null)
            {
                _logger.LogInfo($"Subcategory with id: {SubcategoryId} doesn't exist in the database.");
                return NotFound();
            }

            var videosFromDb = _repository.Video.GetVideosBySub(SubcategoryId, trackChanges: false);

            var videosDto = _mapper.Map<IEnumerable<VideoDto>>(videosFromDb);

            return Ok(videosDto);

        }

        // GET: Video from Main category
        [HttpGet("videos/{VideoId}", Name = "GetVideoForMaincateogry")]
        public IActionResult GetVideoForMaincateogry(int MainCategoryId, int VideoId)
        {
            var maincategory = _repository.MainCategory.GetMainCategory(MainCategoryId, trackChanges: false);
            if (maincategory == null)
            {
                _logger.LogInfo($"Main cateogry with id: {MainCategoryId} doesn't exist in the database.");
                return NotFound();
            }

            var videoFromDb = _repository.Video.GetVideoByMain(MainCategoryId, VideoId, trackChanges: false);

            if (videoFromDb == null)
            {
                _logger.LogInfo($"Video with id: {VideoId} doesn't exist in the database.");
                return NotFound();
            }

            var videoDto = _mapper.Map<VideoDto>(videoFromDb);

            return Ok(videoDto);
        }

        // GET: Video from Subcategory
        [HttpGet("subcategory/{SubcategoryId}/videos/{VideoId}", Name = "GetVideoForSubcategory")]
        public IActionResult GetVideoForSubcategory(int MainCategoryId, int SubcategoryId, int VideoId)
        {
            var maincategory = _repository.MainCategory.GetMainCategory(MainCategoryId, trackChanges: false);
            if (maincategory == null)
            {
                _logger.LogInfo($"Main cateogry with id: {MainCategoryId} doesn't exist in the database.");
                return NotFound();
            }

            var subcategoriesFromDb = _repository.Subcategory.GetSubcategory(MainCategoryId, SubcategoryId, trackChanges: false);
            if (subcategoriesFromDb == null)
            {
                _logger.LogInfo($"Subcategory with id: {SubcategoryId} doesn't exist in the database.");
                return NotFound();
            }

            var videoFromDb = _repository.Video.GetVideoBySub(SubcategoryId, VideoId, trackChanges: false);
            if (videoFromDb == null)
            {
                _logger.LogInfo($"Video with id: {VideoId} doesn't exist in the database.");
                return NotFound();
            }

            var videoDto = _mapper.Map<VideoDto>(videoFromDb);

            return Ok(videoDto);
        }

        // POST: Video to Main Category
        [HttpPost("videos")]
        public IActionResult CreateVideoForMaincateogry(int mainCategoryId, [FromBody] VideoForCreationDto video)
        {
            if (video == null)
            {
                _logger.LogError("VideoForCreationDto object sent from client is null.");
                return BadRequest("VideoForCreationDto object is null");
            }

            var mainCategory = _repository.MainCategory.GetMainCategory(mainCategoryId, trackChanges: false);
            if (mainCategory == null)
            {
                _logger.LogInfo($"Main Category with id: {mainCategoryId} doesn't exist in the database.");
                return NotFound();
            }

            var videoEntity = _mapper.Map<Video>(video);

            _repository.Video.CreateVideoByMain(mainCategoryId, videoEntity);
            _repository.Save();

            var videoToReturn = _mapper.Map<VideoDto>(videoEntity);

            return CreatedAtRoute("GetVideoForMaincateogry", new { mainCategoryId, VideoId = videoToReturn.Id }, videoToReturn);

        }

        // POST: Video to Subcategory
        [HttpPost("subcategory/{SubcategoryId}/videos")]
        public IActionResult CreateVideoForSubcateogry(int mainCategoryId, int subcategoryId, [FromBody] VideoForCreationDto video)
        {
            if (video == null)
            {
                _logger.LogError("VideoForCreationDto object sent from client is null.");
                return BadRequest("VideoForCreationDto object is null");
            }

            var mainCategory = _repository.MainCategory.GetMainCategory(mainCategoryId, trackChanges: false);
            if (mainCategory == null)
            {
                _logger.LogInfo($"Main Category with id: {mainCategoryId} doesn't exist in the database.");
                return NotFound();
            }

            var subcategory = _repository.Subcategory.GetSubcategory(mainCategoryId, subcategoryId, trackChanges: false);
            if (subcategory == null)
            {
                _logger.LogInfo($"Subcategory with id: {subcategoryId} doesn't exist in the database.");
                return NotFound();
            }

            var videoEntity = _mapper.Map<Video>(video);

            _repository.Video.CreateVideoBySub(mainCategoryId, subcategoryId, videoEntity);
            _repository.Save();


            var videoToReturn = _mapper.Map<VideoDto>(videoEntity);

            return CreatedAtRoute("GetVideoForSubcategory", new { mainCategoryId, subcategoryId, VideoId = videoToReturn.Id }, videoToReturn);

        }


        // DELETE: Video from Main Category
        [HttpDelete("videos/{VideoId}")]
        public IActionResult DeleteSubForMaincategory(int MaincategoryId, int VideoId)
        {
            var maincategory = _repository.MainCategory.GetMainCategory(MaincategoryId, trackChanges: false);
            if (maincategory == null)
            {
                _logger.LogInfo($"Main cateogry with id: {MaincategoryId} doesn't exist in the database.");
                return NotFound();
            }

            var videoFromDb = _repository.Video.GetVideoByMain(MaincategoryId, VideoId, trackChanges: false);
            if (videoFromDb == null)
            {
                _logger.LogInfo($"Video with id: {VideoId} doesn't exist in the database.");
                return NotFound();
            }

            _repository.Video.DeleteVideo(videoFromDb);
            _repository.Save();

            return NoContent();
        }

        // DELETE: Video from Subcategory
        [HttpDelete("subcategory/{SubcategoryId}/videos/{VideoId}")]
        public IActionResult DeleteSubForSubcategory(int MainCategoryId, int SubcategoryId, int VideoId)
        {
            var maincategory = _repository.MainCategory.GetMainCategory(MainCategoryId, trackChanges: false);
            if (maincategory == null)
            {
                _logger.LogInfo($"Main cateogry with id: {MainCategoryId} doesn't exist in the database.");
                return NotFound();
            }

            var subcategory = _repository.Subcategory.GetSubcategory(MainCategoryId, SubcategoryId, trackChanges: false);
            if (subcategory == null)
            {
                _logger.LogInfo($"Subcategory with id: {SubcategoryId} doesn't exist in the database.");
                return NotFound();
            }

            var videoFromDb = _repository.Video.GetVideoBySub(SubcategoryId, VideoId, trackChanges: false);
            if (videoFromDb == null)
            {
                _logger.LogInfo($"Video with id: {VideoId} doesn't exist in the database.");
                return NotFound();
            }

            _repository.Video.DeleteVideo(videoFromDb);
            _repository.Save();

            return NoContent();
        }


    }
}
