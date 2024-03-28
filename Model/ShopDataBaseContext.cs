using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PracticeAPI.Model;

public partial class ShopDataBaseContext : DbContext
{
    public ShopDataBaseContext()
    {
    }

    public ShopDataBaseContext(DbContextOptions<ShopDataBaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<OrdersT> OrdersTs { get; set; }

    public virtual DbSet<ProductsT> ProductsTs { get; set; }

    public virtual DbSet<RolesT> RolesTs { get; set; }

    public virtual DbSet<UsersT> UsersTs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("host=localhost;port=5432;username=postgres;password=chloe700A;database=ShopDataBase");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("categories_pkey");

            entity.ToTable("categories");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Name).HasColumnType("character varying");
        });

        modelBuilder.Entity<OrdersT>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("orders_t_pkey");

            entity.ToTable("orders_t");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.ProductKeyId).HasColumnName("ProductKeyID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.ProductKey).WithMany(p => p.OrdersTs)
                .HasForeignKey(d => d.ProductKeyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Order/Product/Id_FK");

            entity.HasOne(d => d.User).WithMany(p => p.OrdersTs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Order/User/ID_FK");
        });

        modelBuilder.Entity<ProductsT>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("products_t_pkey");

            entity.ToTable("products_t");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Image).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(45);

            entity.HasOne(d => d.Category).WithMany(p => p.ProductsTs)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Product/Category/ID_FK");
        });

        modelBuilder.Entity<RolesT>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Roles_t_pkey");

            entity.ToTable("roles_t");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.NameRole).HasMaxLength(45);
        });

        modelBuilder.Entity<UsersT>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_t_pkey");

            entity.ToTable("users_t");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Login).HasMaxLength(20);
            entity.Property(e => e.NickName).HasMaxLength(20);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Salt).HasMaxLength(255);

            entity.HasOne(d => d.Role).WithMany(p => p.UsersTs)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Users/Roles/ID_FK");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
