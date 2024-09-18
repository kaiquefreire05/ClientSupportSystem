namespace CustomerSupportSystem.Helper.Interfaces
{
    public interface IEmail
    {
        bool Sent(string email, string subject, string message);
    }
}