using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using MyShopApi.App.DTOs.ProductDTOs;
using MyShopApi.Models;
using System.Runtime.InteropServices;

namespace MyShopApi.App.Mappers
{
    public class CategoryMapper:Profile
    {
        private readonly IMapper _mapper;
        public CategoryMapper(IMapper mapper)
        {
            _mapper = mapper;
        }
        public CategoryMapper() 
        {
            
            CreateMap<CategoryInput, Category>();
            CreateMap<Category, CategoryOutput>();
            
        }
    }
}
