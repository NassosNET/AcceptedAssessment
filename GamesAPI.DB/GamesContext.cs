using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GamesAPI.DB
{
    public partial class GamesContext : DbContext
    {
        public virtual DbSet<Match> Match { get; set; }
        public virtual DbSet<MatchOdds> MatchOdds { get; set; }

        public GamesContext(DbContextOptions<GamesContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Match>(entity =>
            {
                entity.Property(e => e.ID).HasDefaultValueSql("(newsequentialid())");
                
                entity.Property(e => e.Sport)
                    .HasDefaultValueSql("((1))")
                    .HasComment("1=Football, 2=Basketball");
            });

            modelBuilder.Entity<MatchOdds>(entity =>
            {
                entity.Property(e => e.ID).HasDefaultValueSql("(newsequentialid())");

                entity.HasOne(d => d.Match)
                    .WithMany(p => p.MatchOdds)
                    .HasForeignKey(d => d.MatchId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_MatchOdds_Match");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
