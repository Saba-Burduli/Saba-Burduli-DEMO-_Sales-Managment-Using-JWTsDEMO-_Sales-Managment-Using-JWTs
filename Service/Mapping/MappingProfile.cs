using AutoMapper;
using SalesManagementSystem.DATA.Entites;
using SalesManagementSystem.SERVICE.DTOs.PersonModels;
using SalesManagementSystem.SERVICE.DTOs.RoleModels;
using SalesManagementSystem.SERVICE.DTOs.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementSystem.SERVICE.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserModel>().ReverseMap();

            CreateMap<Person, PersonModel>().ReverseMap();

            CreateMap<User, RegisterUserModel>().ReverseMap();

            CreateMap<User, UpdateUserModel>().ReverseMap();

            CreateMap<User, UserRolesModel>()
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles)); // Ensure this mapping is correct

            CreateMap<Role, RoleModel>(); // Make sure Role -> RoleModel mapping exists
        }
    }
}
