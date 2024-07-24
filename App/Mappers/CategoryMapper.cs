using AutoMapper;
using MyShopApi.App.DTOs.ProductDTOs;
using MyShopApi.Models;
using System.Runtime.InteropServices;

namespace MyShopApi.App.Mappers
{
    public class CategoryMapper:Profile
    {
        public CategoryMapper() 
        {
            CreateMap<CategoryInput, Category>();
        }
    }
}
