using Contracts;
using LoggerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVPortfolio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IRepositoryManager _repository;

        public WeatherForecastController(IRepositoryManager repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            //_repository.MainCategory.GetMainCategories();
            //_repository.Subcategory.AnyMethodFromSubcategoryRepository();
            //_repository.Photo.AnyMethodFromPhotoRepository();
            //_repository.Video.AnyMethodFromVideoRepository();
            //_repository.Website.AnyMethodFromWebsiteRepository();

            return new string[] { "value1", "value2" };
        }
    }
}
