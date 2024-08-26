using ClientSupportSystem.Database;
using ClientSupportSystem.Enums;
using ClientSupportSystem.Models;
using ClientSupportSystem.Repositories.Interfaces;

namespace ClientSupportSystem.Repositories
{
    public class UserRepository : RepositoryBase<UserModel>, IUserRepository
    {
        // Dependence Injection
        public UserRepository(ApplicationDBContext context) : base(context)
        {
        }

        public UserModel GetByEmail(string email)
        {
            return _dbSet.FirstOrDefault(u => u.Email.ToUpper() == email.ToUpper());
        }

        public IEnumerable<UserModel> GetUserByRole(RoleEnum role)
        {
            return _dbSet.Where(u => u.Role == role).ToList();
        }
    }
}
