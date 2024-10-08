﻿using CustomerSupportSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerSupportSystem.Database.Maps
{
    public class UserMap : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Name).HasMaxLength(50).IsRequired();
            builder.Property(u => u.Email).HasMaxLength(255).IsRequired();
            builder.Property(u => u.Password).HasMaxLength(255).IsRequired();
            builder.Property(u => u.Role).IsRequired();
            builder.Property(u => u.CreatedAt).IsRequired();
            builder.Property(u => u.UpdatedAt).IsRequired(false);

            // Relationships

            builder.HasMany(u => u.Tickets)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.Comments)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.KnowledgeArticles)
                .WithOne(t => t.CreatedByUser)
                .HasForeignKey(f => f.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.Feedbacks)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}