using AutoMapper;
using LicenseManagementSystem.DA;
using LicenseManagementSystem.Models.Models;

namespace LicenseManagementSystem.Common.AutoMappers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Product, ProductModel>().ReverseMap();
            CreateMap<License, LicenseModel>().ReverseMap();
            CreateMap<User, UserModel>().ReverseMap();

        }
    }
}
