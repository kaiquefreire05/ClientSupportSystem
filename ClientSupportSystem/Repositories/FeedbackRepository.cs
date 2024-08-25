using ClientSupportSystem.Database;
using ClientSupportSystem.Models;
using ClientSupportSystem.Repositories.Interfaces;

namespace ClientSupportSystem.Repositories
{
    public class FeedbackRepository : RepositoryBase<FeedbackModel>, IFeedbackRepository
    {
        // Dependence Injection
        public FeedbackRepository(ApplicationDBContext context) : base(context)
        {
        }

    }
}
