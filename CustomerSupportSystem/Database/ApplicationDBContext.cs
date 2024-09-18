using CustomerSupportSystem.Database.Maps;
using CustomerSupportSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerSupportSystem.Database
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<TicketModel> Ticket { get; set; }
        public DbSet<TicketCommentModel> TicketComment { get; set; }
        public DbSet<KnowledgeBaseModel> KnowledgeBases { get; set; }
        public DbSet<FeedbackModel> Feedbacks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new TicketMap());
            modelBuilder.ApplyConfiguration(new TicketCommentMap());
            modelBuilder.ApplyConfiguration(new KnowledgeBaseMap());
            modelBuilder.ApplyConfiguration(new FeedbackMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}