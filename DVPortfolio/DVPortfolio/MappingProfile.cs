using AutoMapper;
using Entities;
using Entities.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVPortfolio
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MainCategory, MainCategoryDto>();
            CreateMap<Subcategory, SubcategoryDto>();
            CreateMap<Photo, PhotoDto>();
            CreateMap<Video, VideoDto>();
            CreateMap<Website, WebsiteDto>();

            CreateMap<MainCategoryForCreationDto, MainCategory>();
            CreateMap<SubcategoryForCreationDto, Subcategory>();
            CreateMap<PhotoForCreationDto, Photo>();
            CreateMap<VideoForCreationDto, Video>();
            CreateMap<WebsiteForCreationDto, Website>();
        }
    }
}
