using ClientSupportSystem.Enums;
using ClientSupportSystem.Models;

namespace ClientSupportSystem.Helper.Interfaces
{
    public interface ISessionService
    {
        void CreateUserSession(UserModel user);
        void RemoveUserSession();
        UserModel GetUserSession();
        int? GetUserId();
        RoleEnum GetUserRole();
    }
}
