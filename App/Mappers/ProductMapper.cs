using AutoMapper;
using MyShopApi.App.DTOs.ProductDTOs;
using MyShopApi.Models;

namespace MyShopApi.App.Mappers
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<Product, ProductSearchOutput>()
                .ForMember(m => m.CategoryTitle, opt => opt.MapFrom((src, dest) =>
                {
                    return src.Category?.Title ?? "";
                }));
        }
    }
}
