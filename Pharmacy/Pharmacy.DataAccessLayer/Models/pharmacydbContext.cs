using System;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Pharmacy.DataAccessLayer.Models
{
    public partial class PharmacydbContext : DbContext
    {
        private string _connectionString;
        public PharmacydbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public PharmacydbContext(DbContextOptions<PharmacydbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<Medicine> Medicine { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderMedicine> OrderMedicine { get; set; }
        public virtual DbSet<Pharmacy> Pharmacy { get; set; }
        public virtual DbSet<PharmacyWarehouse> PharmacyWarehouse { get; set; }
        public virtual DbSet<Prescription> Prescription { get; set; }
        public virtual DbSet<PrescriptionMedicine> PrescriptionMedicine { get; set; }
        public virtual DbSet<Stockpile> Stockpile { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<Warehouse> Warehouse { get; set; }
        public virtual DbSet<WarehouseMedicine> WarehouseMedicine { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.LocationId).ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Medicine>(entity =>
            {
                entity.Property(e => e.MedicineId).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("money");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId).ValueGeneratedNever();

                entity.Property(e => e.OrderFulfilledTime).HasColumnType("datetime");

                entity.Property(e => e.OrderIssuedTime).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_User");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.WarehouseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Warehouse");
            });

            modelBuilder.Entity<OrderMedicine>(entity =>
            {
                entity.HasKey(e => new { e.MedicineId, e.OrderId });

                entity.HasOne(d => d.Medicine)
                    .WithMany(p => p.OrderMedicine)
                    .HasForeignKey(d => d.MedicineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderMedicine_Medicine");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderMedicine)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderMedicine_Order");
            });

            modelBuilder.Entity<Pharmacy>(entity =>
            {
                entity.Property(e => e.PharmacyId).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Pharmacy)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_Pharmacy_Location");
            });

            modelBuilder.Entity<PharmacyWarehouse>(entity =>
            {
                entity.HasKey(e => new { e.PharmacyId, e.WarehouseId })
                    .HasName("PK_PharmacyWarehouse_1");

                entity.HasOne(d => d.Pharmacy)
                    .WithMany(p => p.PharmacyWarehouse)
                    .HasForeignKey(d => d.PharmacyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PharmacyWarehouse_Pharmacy");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.PharmacyWarehouse)
                    .HasForeignKey(d => d.WarehouseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PharmacyWarehouse_Warehouse");
            });

            modelBuilder.Entity<Prescription>(entity =>
            {
                entity.Property(e => e.PrescriptionId).ValueGeneratedNever();

                entity.Property(e => e.Buyer)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.SaleTime).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Prescription)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Prescription_User");
            });

            modelBuilder.Entity<PrescriptionMedicine>(entity =>
            {
                entity.HasKey(e => new { e.PrescriptionId, e.MedicineId })
                    .HasName("PK_PrescriptionMedicine_1");

                entity.HasOne(d => d.Medicine)
                    .WithMany(p => p.PrescriptionMedicine)
                    .HasForeignKey(d => d.MedicineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PrescriptionMedicine_Medicine");

                entity.HasOne(d => d.Prescription)
                    .WithMany(p => p.PrescriptionMedicine)
                    .HasForeignKey(d => d.PrescriptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PrescriptionMedicine_Prescription");
            });

            modelBuilder.Entity<Stockpile>(entity =>
            {
                entity.Property(e => e.StockpileId).ValueGeneratedNever();

                entity.HasOne(d => d.Medicine)
                    .WithMany(p => p.Stockpile)
                    .HasForeignKey(d => d.MedicineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Stockpile__Medic__74AE54BC");

                entity.HasOne(d => d.Pharmacy)
                    .WithMany(p => p.Stockpile)
                    .HasForeignKey(d => d.PharmacyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stockpile_Pharmacy");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.HasOne(d => d.Pharmacy)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.PharmacyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__User__PharmacyId__656C112C");

                entity.HasOne(d => d.UserRole)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.UserRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_UserRole");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.Property(e => e.UserRoleId).ValueGeneratedNever();

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Warehouse>(entity =>
            {
                entity.Property(e => e.WarehouseId).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Warehouse)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Warehouse_Location");
            });

            modelBuilder.Entity<WarehouseMedicine>(entity =>
            {
                entity.HasKey(e => new { e.WarehouseId, e.MedicineId })
                    .HasName("PK_WarehouseMedicine_1");

                entity.HasOne(d => d.Medicine)
                    .WithMany(p => p.WarehouseMedicine)
                    .HasForeignKey(d => d.MedicineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WarehouseMedicine_Medicine");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.WarehouseMedicine)
                    .HasForeignKey(d => d.WarehouseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WarehouseMedicine_Warehouse");
            });
        }
    }
}
