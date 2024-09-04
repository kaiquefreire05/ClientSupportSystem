using CustomerSupportSystem.Database;
using CustomerSupportSystem.Models;
using CustomerSupportSystem.Repositories.Interfaces;

namespace CustomerSupportSystem.Repositories
{
    public class TicketCommentRepository : RepositoryBase<TicketCommentModel>, ITicketCommentRepository
    {
        public TicketCommentRepository(ApplicationDBContext context) : base(context)
        {
        }

        public IEnumerable<TicketCommentModel> GetCommentsByTicketId(int ticketId)
        {
            return _dbSet.Where(tc => tc.TicketId == ticketId).ToList();
        }

        public override TicketCommentModel Update(TicketCommentModel comment)
        {
            // Verifyng if comment exist
            var existentComment = _dbSet.Find(comment.Id);
            if (existentComment == null)
            {
                throw new InvalidOperationException("Comment not found.");
            }

            // Updating values
            existentComment.Id = comment.Id;
            existentComment.TicketId = comment.TicketId;
            existentComment.CommentText = comment.CommentText;
            existentComment.UpdatedAt = comment.UpdatedAt;

            _context.SaveChanges();
            return comment;
        }
    }
}
