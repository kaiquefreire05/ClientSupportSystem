using ClientSupportSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClientSupportSystem.Database.Maps
{
    public class TicketCommentMap : IEntityTypeConfiguration<TicketCommentModel>
    {
        public void Configure(EntityTypeBuilder<TicketCommentModel> builder)
        {
            builder.HasKey(tc => tc.Id);
            builder.Property(tc => tc.CommentText).IsRequired().HasMaxLength(255);
            builder.Property(tc => tc.TicketId).IsRequired();
            builder.Property(tc => tc.UserId).IsRequired();
            builder.Property(tc => tc.CreatedAt).IsRequired();

            // Relationship

            builder.HasOne(tc => tc.Ticket)
                .WithMany(t => t.TicketComments)
                .HasForeignKey(tc => tc.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(u => u.User)
                .WithMany(tc => tc.Comments)
                .HasForeignKey(tc => tc.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
