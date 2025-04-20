using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models;
using Shared;

namespace Services.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductResultDto>()
                      .ForMember(d => d.BrandName, o => o.MapFrom(s => s.ProductBrand.Name))
                      .ForMember(d => d.BrandName, o => o.MapFrom(s => s.ProductType.Id))
                     // .ForMember(d => d.PictureUrl, o => o.MapFrom(s => $"/{s.PictureUrl}"));
                     .ForMember(d => d.PictureUrl, o => o.MapFrom<PictureUrlResolver>());



            CreateMap<ProductBrand, BrandsResultDto>();
            CreateMap<ProductType, TypeResultDto>();



        }
    }
}
