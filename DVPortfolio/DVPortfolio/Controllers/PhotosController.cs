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
    public class PhotosController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public PhotosController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: All Photos from Main category
        [HttpGet("photos")]
        public IActionResult GetPhotosForMaincateogry(int MainCategoryId)
        {
            var maincategory = _repository.MainCategory.GetMainCategory(MainCategoryId, trackChanges: false);
            if (maincategory == null)
            {
                _logger.LogInfo($"Main cateogry with id: {MainCategoryId} doesn't exist in the database.");
                return NotFound();
            }

            var photosFromDb = _repository.Photo.GetPhotosByMain(MainCategoryId, trackChanges: false);

            var photosDto = _mapper.Map<IEnumerable<PhotoDto>>(photosFromDb);

            return Ok(photosDto);
        }
        
        // GET: All Photos from Subcategory
        [HttpGet("subcategory/{SubcategoryId}/photos")]
        public IActionResult GetPhotosForSubcateogry(int MainCategoryId, int SubcategoryId)
        {
            var subcategoriesFromDb = _repository.Subcategory.GetSubcategory(MainCategoryId, SubcategoryId, trackChanges: false);
            if (subcategoriesFromDb == null)
            {
                _logger.LogInfo($"Subcategory with id: {SubcategoryId} doesn't exist in the database.");
                return NotFound();
            }

            var photosFromDb = _repository.Photo.GetPhotosBySub(SubcategoryId, trackChanges: false);

            var photosDto = _mapper.Map<IEnumerable<PhotoDto>>(photosFromDb);

            return Ok(photosDto);

        }
        // GET: Photo from Main category
        [HttpGet("photos/{PhotoId}", Name = "GetPhotoForMaincateogry")]
        public IActionResult GetPhotoForMaincateogry(int MainCategoryId, int PhotoId)
        {
            var maincategory = _repository.MainCategory.GetMainCategory(MainCategoryId, trackChanges: false);
            if (maincategory == null)
            {
                _logger.LogInfo($"Main cateogry with id: {MainCategoryId} doesn't exist in the database.");
                return NotFound();
            }

            var photoFromDb = _repository.Photo.GetPhotoByMain(MainCategoryId, PhotoId, trackChanges: false);

            if (photoFromDb == null)
            {
                _logger.LogInfo($"Photo with id: {PhotoId} doesn't exist in the database.");
                return NotFound();
            }

            var photoDto = _mapper.Map<PhotoDto>(photoFromDb);

            return Ok(photoDto);
        }

        // GET: Photo from Subcategory
        [HttpGet("subcategory/{SubcategoryId}/photos/{PhotoId}", Name = "GetPhotoForSubcategory")]
        public IActionResult GetPhotoForSubcategory(int MainCategoryId, int SubcategoryId, int PhotoId)
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

            var photoFromDb = _repository.Photo.GetPhotoBySub(SubcategoryId, PhotoId, trackChanges: false);
            if (photoFromDb == null)
            {
                _logger.LogInfo($"Photo with id: {PhotoId} doesn't exist in the database.");
                return NotFound();
            }

            var photoDto = _mapper.Map<PhotoDto>(photoFromDb);

            return Ok(photoDto);
        }


        // POST: Photo to Main Category
        [HttpPost("photos")]
        public IActionResult CreatePhotoForMaincateogry(int mainCategoryId, [FromBody] PhotoForCreationDto photo)
        {
            if (photo == null)
            {
                _logger.LogError("PhotoForCreationDto object sent from client is null.");
                return BadRequest("PhotoForCreationDto object is null");
            }

            var mainCategory = _repository.MainCategory.GetMainCategory(mainCategoryId, trackChanges: false);
            if (mainCategory == null)
            {
                _logger.LogInfo($"Main Category with id: {mainCategoryId} doesn't exist in the database.");
                return NotFound();
            }

            var photoEntity = _mapper.Map<Photo>(photo);

            _repository.Photo.CreatePhotoByMain(mainCategoryId, photoEntity);
            _repository.Save();

            
            var photoToReturn = _mapper.Map<PhotoDto>(photoEntity);

            return CreatedAtRoute("GetPhotoForMaincateogry", new { mainCategoryId, PhotoId = photoToReturn.Id }, photoToReturn);

        }

        // POST: Photo to Subcategory
        [HttpPost("subcategory/{SubcategoryId}/photos")]
        public IActionResult CreatePhotoForSubcateogry(int mainCategoryId, int subcategoryId, [FromBody] PhotoForCreationDto photo)
        {
            if (photo == null)
            {
                _logger.LogError("PhotoForCreationDto object sent from client is null.");
                return BadRequest("PhotoForCreationDto object is null");
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

            var photoEntity = _mapper.Map<Photo>(photo);

            _repository.Photo.CreatePhotoBySub(mainCategoryId, subcategoryId, photoEntity);
            _repository.Save();


            var photoToReturn = _mapper.Map<PhotoDto>(photoEntity);

            return CreatedAtRoute("GetPhotoForSubcategory", new { mainCategoryId, subcategoryId, PhotoId = photoToReturn.Id }, photoToReturn);

        }


        // DELETE: Photo from Main Category
        [HttpDelete("photos/{PhotoId}")]
        public IActionResult DeleteSubForMaincategory(int MaincategoryId, int PhotoId)
        {
            var maincategory = _repository.MainCategory.GetMainCategory(MaincategoryId, trackChanges: false);
            if (maincategory == null)
            {
                _logger.LogInfo($"Main cateogry with id: {MaincategoryId} doesn't exist in the database.");
                return NotFound();
            }

            var photoFromDb = _repository.Photo.GetPhotoByMain(MaincategoryId, PhotoId, trackChanges: false);
            if (photoFromDb == null)
            {
                _logger.LogInfo($"Photo with id: {PhotoId} doesn't exist in the database.");
                return NotFound();
            }

            _repository.Photo.DeletePhoto(photoFromDb);
            _repository.Save();

            return NoContent();
        }

        // DELETE: Photo from Subcategory
        [HttpDelete("subcategory/{SubcategoryId}/photos/{PhotoId}")]
        public IActionResult DeleteSubForSubcategory(int MainCategoryId, int SubcategoryId, int PhotoId)
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

            var photoFromDb = _repository.Photo.GetPhotoBySub(SubcategoryId, PhotoId, trackChanges: false);
            if (photoFromDb == null)
            {
                _logger.LogInfo($"Photo with id: {PhotoId} doesn't exist in the database.");
                return NotFound();
            }

            _repository.Photo.DeletePhoto(photoFromDb);
            _repository.Save();

            return NoContent();
        }
    }
}
