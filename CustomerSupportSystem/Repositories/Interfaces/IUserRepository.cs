using CustomerSupportSystem.Enums;
using CustomerSupportSystem.Models;

namespace CustomerSupportSystem.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<UserModel>
    {
        UserModel GetByEmail(string email);
        IEnumerable<UserModel> GetUserByRole(RoleEnum role);

    }
}
