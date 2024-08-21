using ClientSupportSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClientSupportSystem.Database.Maps
{
    public class KnowledgeBaseMap : IEntityTypeConfiguration<KnowledgeBaseModel>
    {
        public void Configure(EntityTypeBuilder<KnowledgeBaseModel> builder)
        {
            builder.HasKey(kb => kb.Id);
            builder.Property(kb => kb.Title).IsRequired().HasMaxLength(100);
            builder.Property(kb => kb.Content).IsRequired().HasMaxLength(255);
            builder.Property(kb => kb.Category).IsRequired();
            builder.Property(kb => kb.CreatedByUserId).IsRequired();
            builder.Property(kb => kb.CreatedAt).IsRequired();
            builder.Property(kb => kb.UpdatedAt).IsRequired(false);

            // Relationships

            builder.HasOne(kb => kb.CreatedByUser)
                .WithMany(u => u.KnowledgeArticles)
                .HasForeignKey(kb => kb.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
