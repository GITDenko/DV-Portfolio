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
    [Route("api/maincategory/{MainCategoryId}/subcategory")]
    [ApiController]
    public class SubcategoriesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public SubcategoriesController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
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

            var subcategoriesFromDb = _repository.Subcategory.GetSubcategories(maincategoryId, trackChanges: false);

            var subcategoriesDto = _mapper.Map<IEnumerable<SubcategoryDto>>(subcategoriesFromDb);

            return Ok(subcategoriesDto);
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
        public IActionResult CreateSubForMaincategory(int mainCategoryId, [FromBody]SubcategoryForCreationDto subcategory)
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

            var subcategoryEntity = _mapper.Map<Subcategory>(subcategory);

            _repository.Subcategory.CreateSubcategory(mainCategoryId, subcategoryEntity);
            _repository.Save();

            var subcategoryToReturn = _mapper.Map<SubcategoryDto>(subcategoryEntity);

            return CreatedAtRoute("GetSubForMaincateogry", new { mainCategoryId, subcategoryId = subcategoryToReturn.Id }, subcategoryToReturn);

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

            _repository.Subcategory.DeleteSubcategory(subcategoryFromDb);
            _repository.Save();

            return NoContent();
        }



    }
}
