using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TbcTestAppDAL.DAL.DBEntities;

namespace TbcTestAppDAL.DAL
{
    public partial class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext()
        {
        }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Cities> Cities { get; set; }
        public virtual DbSet<PersonFiles> PersonFiles { get; set; }
        public virtual DbSet<PersonGender> PersonGender { get; set; }
        public virtual DbSet<PersonPhoneNumbers> PersonPhoneNumbers { get; set; }
        public virtual DbSet<PersonRelationTypes> PersonRelationTypes { get; set; }
        public virtual DbSet<Persons> Persons { get; set; }
        public virtual DbSet<PhoneTypes> PhoneTypes { get; set; }
        public virtual DbSet<UnHandledExeptionLog> UnHandledExeptionLog { get; set; }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //                optionsBuilder.UseSqlServer(";Database=TestZura; User  ;integrated security=False;");
        //            }
        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cities>(entity =>
            {
                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateDeleted).HasColumnType("datetime");
            });

            modelBuilder.Entity<PersonFiles>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateDeleted).HasColumnType("datetime");

                entity.Property(e => e.FilePath).IsRequired();

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PersonFiles)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PersonFiles_Persons");
            });

            modelBuilder.Entity<PersonGender>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateDeleted).HasColumnType("datetime");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PersonPhoneNumbers>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateDeleted).HasColumnType("datetime");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PersonPhoneNumbers)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PersonPhoneNumbers_Persons");

                entity.HasOne(d => d.PhoneType)
                    .WithMany(p => p.PersonPhoneNumbers)
                    .HasForeignKey(d => d.PhoneTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PersonPhoneNumbers_PhoneTypes");
            });

            modelBuilder.Entity<PersonRelationTypes>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateDeleted).HasColumnType("datetime");

                entity.Property(e => e.RelationTypeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Persons>(entity =>
            {
                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateDeleted).HasColumnType("datetime");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PersonalNumber)
                    .IsRequired()
                    .HasMaxLength(11);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Persons)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_Persons_Cities");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Persons)
                    .HasForeignKey(d => d.GenderId)
                    .HasConstraintName("FK_Persons_PersonGender");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_Persons_PersonsParent");

                entity.HasOne(d => d.RelationType)
                    .WithMany(p => p.Persons)
                    .HasForeignKey(d => d.RelationTypeId)
                    .HasConstraintName("FK_Persons_PersonRelationTypes");
            });

            modelBuilder.Entity<PhoneTypes>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateDeleted).HasColumnType("datetime");

                entity.Property(e => e.PhoneType)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
