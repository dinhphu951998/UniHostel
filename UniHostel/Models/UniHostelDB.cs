namespace UniHostel.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class UniHostelDB : DbContext
    {
        public UniHostelDB()
            : base("name=UniHostelDB")
        {
        }

        public virtual DbSet<AdvancedService> AdvancedServices { get; set; }
        public virtual DbSet<BillAdvancedServiceDetail> BillAdvancedServiceDetails { get; set; }
        public virtual DbSet<BillCompulsoryServiceDetail> BillCompulsoryServiceDetails { get; set; }
        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<CompulsoryService> CompulsoryServices { get; set; }
        public virtual DbSet<Host> Hosts { get; set; }
        public virtual DbSet<Renter> Renters { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Roommate> Roommates { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdvancedService>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<AdvancedService>()
                .Property(e => e.HostID)
                .IsUnicode(false);

            modelBuilder.Entity<AdvancedService>()
                .HasMany(e => e.BillAdvancedServiceDetails)
                .WithRequired(e => e.AdvancedService)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BillAdvancedServiceDetail>()
                .Property(e => e.AdvancedServiceID)
                .IsUnicode(false);

            modelBuilder.Entity<BillAdvancedServiceDetail>()
                .Property(e => e.BillID)
                .IsUnicode(false);

            modelBuilder.Entity<BillCompulsoryServiceDetail>()
                .Property(e => e.BillID)
                .IsUnicode(false);

            modelBuilder.Entity<BillCompulsoryServiceDetail>()
                .Property(e => e.CompulsoryServiceID)
                .IsUnicode(false);

            modelBuilder.Entity<Bill>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<Bill>()
                .Property(e => e.RoomID)
                .IsUnicode(false);

            modelBuilder.Entity<Bill>()
                .HasMany(e => e.BillAdvancedServiceDetails)
                .WithRequired(e => e.Bill)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Bill>()
                .HasMany(e => e.BillCompulsoryServiceDetails)
                .WithRequired(e => e.Bill)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CompulsoryService>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<CompulsoryService>()
                .Property(e => e.HostID)
                .IsUnicode(false);

            modelBuilder.Entity<CompulsoryService>()
                .HasMany(e => e.BillCompulsoryServiceDetails)
                .WithRequired(e => e.CompulsoryService)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Host>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<Host>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Host>()
                .Property(e => e.Mail)
                .IsUnicode(false);

            modelBuilder.Entity<Host>()
                .HasMany(e => e.AdvancedServices)
                .WithRequired(e => e.Host)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Host>()
                .HasMany(e => e.CompulsoryServices)
                .WithRequired(e => e.Host)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Host>()
                .HasMany(e => e.Rooms)
                .WithRequired(e => e.Host)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Renter>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<Renter>()
                .Property(e => e.Mail)
                .IsUnicode(false);

            modelBuilder.Entity<Renter>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Renter>()
                .Property(e => e.RoomID)
                .IsUnicode(false);

            modelBuilder.Entity<Renter>()
                .HasMany(e => e.Roommates)
                .WithRequired(e => e.Renter)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.RoleName)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Roommate>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<Roommate>()
                .Property(e => e.Mail)
                .IsUnicode(false);

            modelBuilder.Entity<Roommate>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Roommate>()
                .Property(e => e.RenterID)
                .IsUnicode(false);

            modelBuilder.Entity<Room>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<Room>()
                .Property(e => e.HostID)
                .IsUnicode(false);

            modelBuilder.Entity<Room>()
                .HasMany(e => e.Bills)
                .WithRequired(e => e.Room)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Room>()
                .HasMany(e => e.Renters)
                .WithRequired(e => e.Room)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasOptional(e => e.Host)
                .WithRequired(e => e.User);

            modelBuilder.Entity<User>()
                .HasOptional(e => e.Renter)
                .WithRequired(e => e.User);
        }
    }
}
