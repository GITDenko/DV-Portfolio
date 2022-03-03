﻿using AutoMapper;
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
            var maincategories = _repository.MainCategory.GetAllMainCategories(trackChanges: false);
            var categoriesDto = _mapper.Map < IEnumerable <MainCategoryDto>>(maincategories);

            return Ok(categoriesDto);
        }

        [HttpGet("{id}", Name = "MainCategoryById")]
        public IActionResult GetMainCategory(int id)
        {
            var maincategory = _repository.MainCategory.GetMainCategory(id, trackChanges: false);
            if(maincategory == null)
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
        public IActionResult CreateMainCategory([FromForm]MainCategory mainCategory)
        {
            if(mainCategory == null)
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
    
    
        [HttpDelete("{maincategoryId}")]
        public IActionResult DeleteMainCategory(int maincategoryId)
        {
            var maincategory = _repository.MainCategory.GetMainCategory(maincategoryId, trackChanges: false);
            if (maincategory == null)
            {
                _logger.LogInfo($"Main cateogry with id: {maincategoryId} doesn't exist in the database.");
                return NotFound();
            }

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
    }
}
