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
    [Route("api/maincategory")]
    [ApiController]
    public class MainCategoriesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostEnvironment;

        public MainCategoriesController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IWebHostEnvironment hostEnvironment)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public IActionResult GetMainCategories()
        {
            var maincategories = _repository.MainCategory.GetAllMainCategories(trackChanges: false)
                .Select(x => new MainCategory()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Hidden = x.Hidden,
                    Subcategories = x.Subcategories,
                    Photos = x.Photos,
                    Videos = x.Videos,
                    Websites = x.Websites,
                    ImageURL = x.ImageURL,
                    ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, x.ImageURL)
                });
            //.ToList();
            //var categoriesDto = _mapper.Map < IEnumerable <MainCategoryDto>>(maincategories);
            //^Kanske flyttar tbx på den här

            return Ok(maincategories);
        }

        [HttpGet("{id}", Name = "MainCategoryById")]
        public IActionResult GetMainCategory(int id)
        {
            var maincategory = _repository.MainCategory.GetMainCategory(id, trackChanges: false);
            if (maincategory == null)
            {
                _logger.LogInfo($"Main Category with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var categoriesDto = _mapper.Map<MainCategoryDto>(maincategory);
                return Ok(categoriesDto);
            }
        }

        [HttpPost]
        public IActionResult CreateMainCategory([FromForm] MainCategory mainCategory)
        {
            if (mainCategory == null)
            {
                _logger.LogError("MainCategoryForCreationDto object sent from client is null.");
                return BadRequest("CompanyForCreationDto object is null");
            }

            mainCategory.ImageURL = SaveImage(mainCategory.ImageFile); //55:51
            var mainCategoryEntity = _mapper.Map<MainCategory>(mainCategory);


            _repository.MainCategory.CreateMainCategory(mainCategoryEntity);
            _repository.Save();

            //var mainCategoryToReturn = _mapper.Map<MainCategoryDto>(mainCategoryEntity);

            mainCategory.Hidden = false;

            return StatusCode(201);
            //return CreatedAtRoute("MainCategoryById", new { id = mainCategoryToReturn.Id }, mainCategoryToReturn);
        }

        [HttpPut("{maincategoryId}")]
        public IActionResult PutMainCategroy(int maincategoryId, [FromForm] MainCategory mainCategory)
        {
            if (mainCategory == null)
            {
                _logger.LogError("Dto object sent from client is null.");
                return BadRequest("Dto object sent from client is null.");
            }

            var maincategoryEntity = _repository.MainCategory.GetMainCategory(maincategoryId, trackChanges: false);
            if (maincategoryEntity == null)
            {
                _logger.LogInfo($"Maincategory with id: {maincategoryId} doesn't exist in the database.");
                return NotFound();
            }

            if (maincategoryEntity.ImageFile != null)
            {
                DeleteImage(maincategoryEntity.ImageURL);
                mainCategory.ImageURL = SaveImage(mainCategory.ImageFile);
            }

            _mapper.Map(mainCategory, maincategoryEntity);
            _repository.Save();

            return NoContent();
        }


        [HttpDelete("{Id}")]
        public IActionResult DeleteMainCategory(int Id)
        {
            var maincategory = _repository.MainCategory.GetMainCategory(Id, trackChanges: false);
            if (maincategory == null)
            {
                _logger.LogInfo($"Main cateogry with id: {Id} doesn't exist in the database.");
                return NotFound();
            }

            DeleteImage(maincategory.ImageURL);
            _repository.MainCategory.DeleteMainCategory(maincategory);
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
