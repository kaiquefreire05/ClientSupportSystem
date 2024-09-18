using CustomerSupportSystem.Enums;
using CustomerSupportSystem.Models;

namespace CustomerSupportSystem.Helper.Interfaces
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