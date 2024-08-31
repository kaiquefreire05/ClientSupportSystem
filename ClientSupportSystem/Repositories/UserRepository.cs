using ClientSupportSystem.Database;
using ClientSupportSystem.Enums;
using ClientSupportSystem.Models;
using ClientSupportSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ClientSupportSystem.Repositories
{
    public class UserRepository : RepositoryBase<UserModel>, IUserRepository
    {
        // Dependence Injection
        public UserRepository(ApplicationDBContext context) : base(context)
        {
        }

        public override UserModel Create(UserModel user)
        {
            user.CreatedAt = DateTime.Now;
            user.setPasswordHash();
            _dbSet.Add(user);
            _context.SaveChanges();
            return user;
        }
        public UserModel GetByEmail(string email)
        {
            return _dbSet.FirstOrDefault(u => u.Email.ToUpper() == email.ToUpper());
        }

        public IEnumerable<UserModel> GetUserByRole(RoleEnum role)
        {
            return _dbSet.Where(u => u.Role == role).ToList();
        }

        public override UserModel Update(UserModel user)
        {
            // Verifyng if user exists
            var existingUser = _dbSet.Find(user.Id);
            if (existingUser == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            // Updating values
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Role = user.Role;
            existingUser.CreatedAt = DateTime.Now;

            _context.SaveChanges();

            return existingUser;

        }
    }
}
