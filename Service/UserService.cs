using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SalesManagementSystem.DAL.Repositories;
using SalesManagementSystem.DATA.Entites;
using SalesManagementSystem.SERVICE.DTOs.PersonModels;
using SalesManagementSystem.SERVICE.DTOs.UserModels;
using SalesManagementSystem.SERVICE.Helpers;
using SalesManagementSystem.SERVICE.Interfaces;

namespace SalesManagementSystem.SERVICE
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;
        private readonly IConfiguration _configuration;

        public UserService(IConfiguration configuration, IUserRepository userRepository, IMapper mapper, IPasswordHasher passwordHasher, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _roleRepository = roleRepository;
            _configuration = configuration;
        }

        public async Task<IEnumerable<UserModel>> GetAllUsersWithPersonAsync()
        {
            var users = await _userRepository.GetAllUsersWithPersonAsync();

            return users.Select(u => new UserModel
            {
                UserId = u.UserId,
                UserName = u.UserName,
                Email = u.Email,
                Person = new PersonModel
                {
                    FirstName = u.Person.FirstName,
                    LastName = u.Person.LastName,
                    Phone = u.Person.Phone,
                    Address = u.Person.Address,
                    PersonTypeId = u.Person.PersonTypeId
                }
            }).ToList();

            //return users.Select(user => new UserModel
            //{
            //    UserName = user.UserName,
            //    Email = user.Email,
            //    UserId = user.UserId
            //});

            //return _mapper.Map<List<UserModel>>(users);
        }
        public async Task<UserModel?> GetUserWithPersonByIdAsync(int id)
        {
            var user = await _userRepository.GetUserWithPersonsByIdAsync(id);
            if (user == null)
                return null;

            return new UserModel
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                Person = new PersonModel
                {
                    FirstName = user.Person.FirstName,
                    LastName = user.Person.LastName,
                    Phone = user.Person.Phone,
                    Address = user.Person.Address,
                    PersonTypeId = user.Person.PersonTypeId
                }
            };
        }      
        public async Task<AuthResponseModel> RegisterUserWithPersonAsync(RegisterUserModel model)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(model.Email);

            if (existingUser != null)
            {
                return new AuthResponseModel { Success = false, Message = "User already exists" };
            }

            Person registerPerson = _mapper.Map<Person>(model.PersonModel);

            //Person registerPerson = new Person
            //{
            //    FirstName = model.PersonModel.FirstName,
            //    LastName = model.PersonModel.LastName,
            //    Address = model.PersonModel.Address,
            //    PersonTypeId = model.PersonModel.PersonTypeId,
            //    Phone = model.PersonModel.Phone,
            //};
            var roles = await _roleRepository.GetAllAsync();
            var userRole = roles.Where(r => r.RoleId == model.RoleId).ToList();

            var registerUser = _mapper.Map<User>(model);
            registerUser.PasswordHash = await _passwordHasher.HashPassword(model.Password);
            registerUser.Person = registerPerson;
            registerUser.Roles = userRole;



            //var registerUser = new User
            //{
            //    UserName = model.UserName,
            //    Email = model.Email,
            //    PasswordHash = await _passwordHasher.HashPassword(model.Password),
            //    Person = registerPerson
            //};

            await _userRepository.AddAsync(registerUser);

            return new AuthResponseModel { Success = true, Message = "User Registered Successfully" };
        }
        public async Task<AuthResponseModel> LoginUserAsync(LoginModel model)
        {
            var loggedUser = await _userRepository.GetUserByEmailAsync(model.UserName);

            if (loggedUser == null || !await _passwordHasher.VerifyPassword(model.Password, loggedUser.PasswordHash))
                return new AuthResponseModel { Success = false, Message = "Invalid credentials" };            

            return new AuthResponseModel { Success = true, Message = "Login successful" };
        }
        public async Task<AuthResponseModel> UpdateUserAsync(int userId, UpdateUserModel model)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return new AuthResponseModel { Success = false, Message = "User not found" };

            user.UserName = model.UserName;
            user.PasswordHash = await _passwordHasher.HashPassword(model.Password);

            await _userRepository.UpdateAsync(user);

            return new AuthResponseModel { Success = true, Message = "Profile updated" };
        }
        public async Task<AuthResponseModel> AssignRoleAsync(int userId, List<int> roleIds)
        {
            var user = await _userRepository.AssignUserRolesAsync(userId, roleIds);
            if (user == null) 
                return new AuthResponseModel { Success = false, Message = "User has not assigned to role" };

            return new AuthResponseModel { Success = true, Message = "User has successfully assigned to role" };
        }
        public async Task<AuthResponseModel> DeleteUserAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return new AuthResponseModel { Success = false, Message = "User not found" };


            await _userRepository.DeleteAsync(user.UserId);
            return new AuthResponseModel { Success = true, Message = "User deleted" };
        }

        public async Task<UserRolesModel?> GetUserWithRolesByIdAsync(int id)
        {
            var userRoles = await _userRepository.GetUserWithRolesByIdAsync(id);

            if (userRoles == null)
            {
                return null;
            }

            //UserRolesModel userRolesModel = new UserRolesModel
            //{
            //    UserName = userRoles.UserName,
            //    Email = userRoles.Email,
            //    Roles = _mapper.Map<List<RoleModel>>(userRoles.Roles)
            //};

            return _mapper.Map<UserRolesModel>(userRoles);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }
    }
}
