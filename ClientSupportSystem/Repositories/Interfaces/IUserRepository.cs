using ClientSupportSystem.Enums;
using ClientSupportSystem.Models;

namespace ClientSupportSystem.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<UserModel>
    {
        UserModel GetByEmail(string email);
        IEnumerable<UserModel> GetUserByRole(RoleEnum role);

    }
}
