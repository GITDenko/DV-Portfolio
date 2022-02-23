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
    [Route("api/maincategory")]
    [ApiController]
    public class MainCategoriesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public MainCategoriesController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
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
        public IActionResult CreateMainCategory([FromBody]MainCategoryForCreationDto mainCategory)
        {
            if(mainCategory == null)
            {
                _logger.LogError("MainCategoryForCreationDto object sent from client is null.");
                return BadRequest("CompanyForCreationDto object is null");
            }

            var mainCategoryEntity = _mapper.Map<MainCategory>(mainCategory);

            _repository.MainCategory.CreateMainCategory(mainCategoryEntity);
            _repository.Save();

            var mainCategoryToReturn = _mapper.Map<MainCategoryDto>(mainCategoryEntity);

            mainCategory.Hidden = false;

            return CreatedAtRoute("MainCategoryById", new { id = mainCategoryToReturn.Id }, mainCategoryToReturn);
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

    }
}
