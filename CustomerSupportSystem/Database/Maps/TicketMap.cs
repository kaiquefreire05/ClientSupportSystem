using CustomerSupportSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerSupportSystem.Database.Maps
{
    public class TicketMap : IEntityTypeConfiguration<TicketModel>
    {
        public void Configure(EntityTypeBuilder<TicketModel> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Title).IsRequired().HasMaxLength(100);
            builder.Property(t => t.Description).IsRequired().HasMaxLength(500);
            builder.Property(t => t.Status).IsRequired();
            builder.Property(t => t.Category).IsRequired();
            builder.Property(t => t.Priority).IsRequired();
            builder.Property(t => t.UserId).IsRequired();
            builder.Property(t => t.CreatedAt).IsRequired();
            builder.Property(t => t.UpdatedAt).IsRequired(false);

            // Relationships

            builder.HasOne(u => u.User)
                .WithMany(u => u.Tickets)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(ct => ct.TicketComments)
                .WithOne(t => t.Ticket)
                .HasForeignKey(t => t.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(f => f.Feedback)
                .WithOne(t => t.Ticket)
                .HasForeignKey<FeedbackModel>(f => f.TicketId)
                .OnDelete(DeleteBehavior.Cascade); // Cascata ao deletar Ticket

        }
    }
}
