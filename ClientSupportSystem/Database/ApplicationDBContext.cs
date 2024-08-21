using ClientSupportSystem.Database.Maps;
using ClientSupportSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding;

namespace ClientSupportSystem.Database
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
