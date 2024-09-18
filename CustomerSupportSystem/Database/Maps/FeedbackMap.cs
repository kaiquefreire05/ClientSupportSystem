using CustomerSupportSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerSupportSystem.Database.Maps
{
    public class FeedbackMap : IEntityTypeConfiguration<FeedbackModel>
    {
        public void Configure(EntityTypeBuilder<FeedbackModel> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Rating).IsRequired();
            builder.Property(f => f.Comments).IsRequired().HasMaxLength(255);
            builder.Property(f => f.TicketId).IsRequired();
            builder.Property(f => f.UserId).IsRequired();
            builder.Property(f => f.CreatedAt).IsRequired();
            builder.Property(f => f.UpdatedAt).IsRequired(false);

            // Relationships

            builder.HasOne(f => f.Ticket)
                .WithOne(t => t.Feedback)
                .HasForeignKey<FeedbackModel>(f => f.TicketId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(u => u.User)
                .WithMany(f => f.Feedbacks)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}