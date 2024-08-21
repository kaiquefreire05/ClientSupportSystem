using ClientSupportSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClientSupportSystem.Database.Maps
{
    public class UserMap : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.HasKey(u => u.Id); 
            builder.Property(u => u.Name).HasMaxLength(50).IsRequired();
            builder.Property(u => u.Email).HasMaxLength(255).IsRequired();
            builder.Property(u => u.PasswordHash).HasMaxLength(255).IsRequired();
            builder.Property(u => u.Role).IsRequired();
            builder.Property(u => u.CreatedAt).IsRequired();

            // Relationships

            builder.HasMany(u => u.Tickets)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId);

            builder.HasMany(u => u.Comments)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId);

            builder.HasMany(u => u.KnowledgeArticles)
                .WithOne(t => t.CreatedByUser)
                .HasForeignKey(f => f.CreatedByUserId);

            builder.HasMany(u => u.Feedbacks)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId);
        }
    }
}
