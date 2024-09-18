using CustomerSupportSystem.Database;
using CustomerSupportSystem.DTOs;
using CustomerSupportSystem.Enums;
using CustomerSupportSystem.Models;
using CustomerSupportSystem.Repositories.Interfaces;

namespace CustomerSupportSystem.Repositories
{
    public class UserRepository : RepositoryBase<UserModel>, IUserRepository
    {
        // Dependence Injection
        public UserRepository(ApplicationDBContext context) : base(context)
        {
        }

        public UserModel ChangePassword(ChangePassDto changePassDto)
        {
            var user = GetById(changePassDto.Id);

            if (user == null)
            {
                throw new Exception("There was an error updating your password. User not found.");
            }

            if (!user.ValidPassword(changePassDto.CurrentPassword))
            {
                throw new Exception("Current password does not match.");
            }

            if (user.ValidPassword(changePassDto.NewPassword))
            {
                throw new Exception("New password must be different from current password");
            }

            user.SetNewPass(changePassDto.NewPassword);
            user.UpdatedAt = DateTime.UtcNow;

            _dbSet.Update(user);
            _context.SaveChanges();
            return user;
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