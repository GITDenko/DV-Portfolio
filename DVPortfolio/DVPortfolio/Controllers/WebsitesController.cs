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
    [Route("api/maincategory/{MainCategoryId}")]
    [ApiController]
    public class WebsitesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostEnvironment;

        public WebsitesController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IWebHostEnvironment hostEnvironment)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
        }

        // GET: All Websites from Main category
        [HttpGet("websites")]
        public IActionResult GetWebsitesForMaincateogry(int MainCategoryId)
        {
            var maincategory = _repository.MainCategory.GetMainCategory(MainCategoryId, trackChanges: false);
            if (maincategory == null)
            {
                _logger.LogInfo($"Main cateogry with id: {MainCategoryId} doesn't exist in the database.");
                return NotFound();
            }

            var websitesFromDb = _repository.Website.GetWebsitesByMain(MainCategoryId, trackChanges: false)
                .Select(x => new Website()
                {
                    Id = x.Id,
                    ProductUrl = x.ProductUrl,
                    Name = x.Name,
                    CreatedOn = x.CreatedOn,
                    Hidden = x.Hidden,
                    MainCategoryId = x.MainCategoryId,
                    SubcategoryId = x.SubcategoryId,
                    ImageURL = x.ImageURL,
                    ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, x.ImageURL)
                });

            //var websitesDto = _mapper.Map<IEnumerable<WebsiteDto>>(websitesFromDb);

            return Ok(websitesFromDb);
        }

        // GET: All Websites from Subcategory
        [HttpGet("subcategory/{SubcategoryId}/websites")]
        public IActionResult GetWebsitesForSubcateogry(int MainCategoryId, int SubcategoryId)
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

            var websitesFromDb = _repository.Website.GetWebsitesBySub(SubcategoryId, trackChanges: false)
                .Select(x => new Website()
                    {
                        Id = x.Id,
                        ProductUrl = x.ProductUrl,
                        Name = x.Name,
                        CreatedOn = x.CreatedOn,
                        Hidden = x.Hidden,
                        MainCategoryId = x.MainCategoryId,
                        SubcategoryId = x.SubcategoryId,
                        ImageURL = x.ImageURL,
                        ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, x.ImageURL)
                    });

            //var websitesDto = _mapper.Map<IEnumerable<WebsiteDto>>(websitesFromDb);

            return Ok(websitesFromDb);

        }

        // GET: Website from Main category
        [HttpGet("websites/{WebsiteId}", Name = "GetWebsiteForMaincateogry")]
        public IActionResult GetWebsiteForMaincateogry(int MainCategoryId, int WebsiteId)
        {
            var maincategory = _repository.MainCategory.GetMainCategory(MainCategoryId, trackChanges: false);
            if (maincategory == null)
            {
                _logger.LogInfo($"Main cateogry with id: {MainCategoryId} doesn't exist in the database.");
                return NotFound();
            }

            var websiteFromDb = _repository.Website.GetWebsiteByMain(MainCategoryId, WebsiteId, trackChanges: false);

            if (websiteFromDb == null)
            {
                _logger.LogInfo($"Website with id: {WebsiteId} doesn't exist in the database.");
                return NotFound();
            }

            var websiteDto = _mapper.Map<WebsiteDto>(websiteFromDb);

            return Ok(websiteDto);
        }

        // GET: Website from Subcategory
        [HttpGet("subcategory/{SubcategoryId}/websites/{WebsiteId}", Name = "GetWebsiteForSubcategory")]
        public IActionResult GetWebsiteForSubcategory(int MainCategoryId, int SubcategoryId, int WebsiteId)
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

            var websiteFromDb = _repository.Website.GetWebsiteBySub(SubcategoryId, WebsiteId, trackChanges: false);
            if (websiteFromDb == null)
            {
                _logger.LogInfo($"Website with id: {WebsiteId} doesn't exist in the database.");
                return NotFound();
            }

            var websiteDto = _mapper.Map<WebsiteDto>(websiteFromDb);

            return Ok(websiteDto);
        }

        // POST: Website to Main Category
        [HttpPost("websites")]
        public IActionResult CreateWebsiteForMaincateogry(int mainCategoryId, [FromForm] Website website)
        {
            if (website == null)
            {
                _logger.LogError("WebsiteForCreationDto object sent from client is null.");
                return BadRequest("WebsiteForCreationDto object is null");
            }

            var mainCategory = _repository.MainCategory.GetMainCategory(mainCategoryId, trackChanges: false);
            if (mainCategory == null)
            {
                _logger.LogInfo($"Main Category with id: {mainCategoryId} doesn't exist in the database.");
                return NotFound();
            }
            website.ImageURL = SaveImage(website.ImageFile);
            var websiteEntity = _mapper.Map<Website>(website);

            _repository.Website.CreateWebsiteByMain(mainCategoryId, websiteEntity);
            _repository.Save();


            //var websiteToReturn = _mapper.Map<WebsiteDto>(websiteEntity);
            //return CreatedAtRoute("GetWebsiteForMaincateogry", new { mainCategoryId, WebsiteId = websiteToReturn.Id }, websiteToReturn);
            return StatusCode(201);
        }

        // POST: Website to Subcategory
        [HttpPost("subcategory/{SubcategoryId}/websites")]
        public IActionResult CreateWebsiteForSubcateogry(int mainCategoryId, int subcategoryId, [FromForm] Website website)
        {
            if (website == null)
            {
                _logger.LogError("WebsiteForCreationDto object sent from client is null.");
                return BadRequest("WebsiteForCreationDto object is null");
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
            website.ImageURL = SaveImage(website.ImageFile);
            var websiteEntity = _mapper.Map<Website>(website);
            _repository.Website.CreateWebsiteBySub(mainCategoryId, subcategoryId, websiteEntity);
            _repository.Save();

            //var websiteToReturn = _mapper.Map<WebsiteDto>(websiteEntity);

            //return CreatedAtRoute("GetWebsiteForSubcategory", new { mainCategoryId, subcategoryId, WebsiteId = websiteToReturn.Id }, websiteToReturn);
            return StatusCode(201);
        }


        // DELETE: Website from Main Category
        [HttpDelete("websites/{WebsiteId}")]
        public IActionResult DeleteSubForMaincategory(int MaincategoryId, int WebsiteId)
        {
            var maincategory = _repository.MainCategory.GetMainCategory(MaincategoryId, trackChanges: false);
            if (maincategory == null)
            {
                _logger.LogInfo($"Main cateogry with id: {MaincategoryId} doesn't exist in the database.");
                return NotFound();
            }

            var websiteFromDb = _repository.Website.GetWebsiteByMain(MaincategoryId, WebsiteId, trackChanges: false);
            if (websiteFromDb == null)
            {
                _logger.LogInfo($"Website with id: {WebsiteId} doesn't exist in the database.");
                return NotFound();
            }
            DeleteImage(websiteFromDb.ProductUrl);
            _repository.Website.DeleteWebsite(websiteFromDb);
            _repository.Save();

            return NoContent();
        }

        // DELETE: Website from Subcategory
        [HttpDelete("subcategory/{SubcategoryId}/websites/{WebsiteId}")]
        public IActionResult DeleteSubForSubcategory(int MainCategoryId, int SubcategoryId, int WebsiteId)
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

            var websiteFromDb = _repository.Website.GetWebsiteBySub(SubcategoryId, WebsiteId, trackChanges: false);
            if (websiteFromDb == null)
            {
                _logger.LogInfo($"Website with id: {WebsiteId} doesn't exist in the database.");
                return NotFound();
            }
            DeleteImage(websiteFromDb.ProductUrl);
            _repository.Website.DeleteWebsite(websiteFromDb);
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
