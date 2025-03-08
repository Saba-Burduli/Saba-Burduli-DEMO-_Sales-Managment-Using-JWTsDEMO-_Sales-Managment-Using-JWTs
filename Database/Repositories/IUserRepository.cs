using Microsoft.EntityFrameworkCore;
using SalesManagementSystem.DATA;
using SalesManagementSystem.DATA.Entites;
using SalesManagementSystem.DATA.Infastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementSystem.DAL.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<List<User>> GetAllUsersWithPersonAsync();
        Task<User?> GetUserWithPersonsByIdAsync(int id);
        Task<User> AssignUserRolesAsync(int userId, List<int> roleIds);
        Task<User> GetUserWithRolesByEmailAsync(string email);
        Task<User> GetUserWithRolesByIdAsync(int userId);
    }

    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly SalesManagmentSystemDbContext _context;

        public UserRepository(SalesManagmentSystemDbContext context) : base(context)
        {
                _context = context;
        }

        public async Task<User> AssignUserRolesAsync(int userId, List<int> roleIds)
        {
            var user = await _context.Users
                .Include(u=>u.Roles)
                .FirstOrDefaultAsync(u=>u.UserId == userId);

            if (user == null)
                throw new Exception("User not found()");

            var roles = await _context.Roles
                .Where(r=> roleIds.Contains(r.RoleId))
                .ToListAsync();

            if (roles.Count != roleIds.Count)
                throw new Exception("Some roles were not found");

            user.Roles = roles; // Assign Roles (overwrites existing roles)

            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<List<User>> GetAllUsersWithPersonAsync()
        {
            return await _context.Users
            .Include(u => u.Person)  // Eagerly load Persons
            .ToListAsync();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            if (_context == null || _context.Users == null)
                throw new ArgumentNullException("Database context is not initialized");


            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserWithPersonsByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Person) // Eagerly load Persons
                .FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<User> GetUserWithRolesByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserWithRolesByIdAsync(int userId)
        {
            return await _context.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u=>u.UserId == userId);
        }
    }
}
