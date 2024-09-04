using CustomerSupportSystem.Models;

namespace CustomerSupportSystem.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        T Create(T entity);
        T Update(T user);
        bool Delete(int id);
    }
}
