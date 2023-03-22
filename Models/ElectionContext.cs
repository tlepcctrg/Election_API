using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace election_api.Models
{
    public partial class ElectionContext : DbContext
    {
        public ElectionContext()
        {
        }

        public ElectionContext(DbContextOptions<ElectionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Candidate> Candidate { get; set; }
        public virtual DbSet<UserProflie> UserProflie { get; set; }
        public virtual DbSet<VotedResult> VotedResult { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=Election;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Candidate>(entity =>
            {
                entity.HasKey(e => e.CaId);

                entity.Property(e => e.CaId)
                    .HasColumnName("Ca_ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CaImage).HasColumnName("Ca_Image");

                entity.Property(e => e.CaMotto)
                    .HasColumnName("Ca_Motto")
                    .HasMaxLength(255);

                entity.Property(e => e.CaName)
                    .HasColumnName("Ca_Name")
                    .HasMaxLength(255);

                entity.Property(e => e.CaNumer).HasColumnName("Ca_Numer");
            });

            modelBuilder.Entity<UserProflie>(entity =>
            {
                entity.HasKey(e => e.UprId);

                entity.Property(e => e.UprId)
                    .HasColumnName("UPr_ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.UprAge).HasColumnName("UPr_Age");

                entity.Property(e => e.UprGender)
                    .HasColumnName("UPr_Gender")
                    .HasMaxLength(50);

                entity.Property(e => e.UprUserId)
                    .HasColumnName("UPr_UserID")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<VotedResult>(entity =>
            {
                entity.HasKey(e => e.VreId);

                entity.Property(e => e.VreId)
                    .HasColumnName("VRe_ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.VreFkCaId).HasColumnName("VRe_FK_Ca_ID");

                entity.Property(e => e.VreFkUprId).HasColumnName("VRe_FK_UPr_ID");

                entity.HasOne(d => d.VreFkUpr)
                    .WithMany(p => p.VotedResult)
                    .HasForeignKey(d => d.VreFkUprId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserProfile");

                entity.HasOne(d => d.VreFkCa)
                    .WithMany(p => p.VotedResult)
                    .HasForeignKey(d => d.VreFkCaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Candidate");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
