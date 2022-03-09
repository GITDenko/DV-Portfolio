using AutoMapper;
using Contracts;
using Entities;
using Entities.DataTransferObjects;
using LoggerService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DVPortfolio.Controllers
{
    [Route("api/maincategory/{MainCategoryId}/subcategory")]
    [ApiController]
    public class SubcategoriesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostEnvironment;

        public SubcategoriesController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IWebHostEnvironment hostEnvironment)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
        }

        // GET: All Subcategories from Main Category
        [HttpGet]
        public IActionResult GetSubsForMaincategory(int maincategoryId)
        {
            var maincategory = _repository.MainCategory.GetMainCategory(maincategoryId, trackChanges: false);
            if(maincategory == null)
            {
                _logger.LogInfo($"Main cateogry with id: {maincategoryId} doesn't exist in the database.");
                return NotFound();
            }

            var subcategoriesFromDb = _repository.Subcategory.GetSubcategories(maincategoryId, trackChanges: false)
                .Select(x => new Subcategory()
                {
                    Id = x.Id,
                    MainCategoryId = x.MainCategoryId,
                    Name = x.Name,
                    Hidden = x.Hidden,
                    Photos = x.Photos,
                    Videos = x.Videos,
                    Websites = x.Websites,
                    ImageURL = x.ImageURL,
                    ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, x.ImageURL)
                });

            //var subcategoriesDto = _mapper.Map<IEnumerable<SubcategoryDto>>(subcategoriesFromDb);

            return Ok(subcategoriesFromDb);
        }
        // GET: Subcategory from Main Category
        [HttpGet("{subcategoryId}", Name = "GetSubForMaincateogry")]
        public IActionResult GetSubForMaincateogry(int maincategoryId, int subcategoryId)
        {
            var maincategory = _repository.MainCategory.GetMainCategory(maincategoryId, trackChanges: false);
            if (maincategory == null)
            {
                _logger.LogInfo($"Main cateogry with id: {maincategoryId} doesn't exist in the database.");
                return NotFound();
            }

            var subcategoriesFromDb = _repository.Subcategory.GetSubcategory(maincategoryId, subcategoryId, trackChanges: false);
            if(subcategoriesFromDb == null)
            {
                _logger.LogInfo($"Subcategory with id: {subcategoryId} doesn't exist in the database.");
                return NotFound();
            }

            var subcategoriesDto = _mapper.Map<SubcategoryDto>(subcategoriesFromDb);

            return Ok(subcategoriesDto);
        }

        // POST: Subcategory to Main Category
        [HttpPost]
        public IActionResult CreateSubForMaincategory(int mainCategoryId, [FromForm]Subcategory subcategory)
        {
            if(subcategory == null)
            {
                _logger.LogError("SubcategoryForCreationDto object sent from client is null.");
                return BadRequest("SubcategoryForCreationDto object is null");
            }

            var mainCategory = _repository.MainCategory.GetMainCategory(mainCategoryId, trackChanges: false);
            if (mainCategory == null)
            {
                _logger.LogInfo($"Main Category with id: {mainCategoryId} doesn't exist in the database.");
                return NotFound();
            }
            subcategory.ImageURL = SaveImage(subcategory.ImageFile);
            var subcategoryEntity = _mapper.Map<Subcategory>(subcategory);

            _repository.Subcategory.CreateSubcategory(mainCategoryId, subcategoryEntity);
            _repository.Save();

            //return CreatedAtRoute("GetSubForMaincateogry", new { mainCategoryId, subcategoryId = subcategoryToReturn.Id }, subcategoryToReturn);
            return StatusCode(201);
        }


        // DELETE: Subcategory from Main Category
        [HttpDelete("{subcategoryId}")]
        public IActionResult DeleteSubForMaincategory(int maincategoryId, int subcategoryId)
        {
            var maincategory = _repository.MainCategory.GetMainCategory(maincategoryId, trackChanges: false);
            if (maincategory == null)
            {
                _logger.LogInfo($"Main cateogry with id: {maincategoryId} doesn't exist in the database.");
                return NotFound();
            }

            var subcategoryFromDb = _repository.Subcategory.GetSubcategory(maincategoryId, subcategoryId, trackChanges: false);
            if (subcategoryFromDb == null)
            {
                _logger.LogInfo($"Subcategory with id: {subcategoryId} doesn't exist in the database.");
                return NotFound();
            }
            DeleteImage(subcategoryFromDb.ImageURL);
            _repository.Subcategory.DeleteSubcategory(subcategoryFromDb);
            _repository.Save();

            return NoContent();
        }

        [NonAction]
        public string SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                //await imageFile.CopyToAsync(fileStream); 55:17
                imageFile.CopyTo(fileStream);
            }
            return imageName;
        }

        [NonAction]
        public string DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            return null;
        }

    }
}
