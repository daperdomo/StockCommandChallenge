using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using StockCommandChallenge.Data.Interfaces;

#nullable disable

namespace StockCommandChallenge.Core.Models
{
    public partial class StockCommandChallengeContext : DbContext, IDbContext
    {
        public StockCommandChallengeContext(DbContextOptions<StockCommandChallengeContext> options)
       : base(options)
        {
        }

        public virtual DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>(entity =>
            {
                entity.Property(e => e.MessageText)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Created)
                    .IsRequired()
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
