using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using SalesManagementSystem.DATA.Entites;
using SalesManagementSystem.SERVICE.DTOs.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementSystem.SERVICE.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserModel>> GetAllUsersWithPersonAsync();
        Task<UserModel?> GetUserWithPersonByIdAsync(int id);
        Task<UserRolesModel?> GetUserWithRolesByIdAsync(int id);
        Task<User> GetUserByEmailAsync(string email);
        Task<AuthResponseModel> RegisterUserWithPersonAsync(RegisterUserModel model);
        Task<AuthResponseModel> LoginUserAsync(LoginModel model);
        Task<AuthResponseModel> UpdateUserAsync(int userId, UpdateUserModel model);
        Task<AuthResponseModel> AssignRoleAsync(int userId, List<int> roleIds);
        Task<AuthResponseModel> DeleteUserAsync(int userId);
    }
}
