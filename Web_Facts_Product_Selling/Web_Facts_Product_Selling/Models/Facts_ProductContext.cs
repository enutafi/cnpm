using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Web_Facts_Product_Selling.Models
{
    public partial class Facts_ProductContext : DbContext
    {
        public Facts_ProductContext()
        {
        }

        public Facts_ProductContext(DbContextOptions<Facts_ProductContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblAdmin> TblAdmins { get; set; }
        public virtual DbSet<TblCategoryProduct> TblCategoryProducts { get; set; }
        public virtual DbSet<TblProduct> TblProducts { get; set; }
        public virtual DbSet<TblShipping> TblShippings { get; set; }
        public virtual DbSet<TblSupplier> TblSuppliers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DCC\\SQLEXPRESS;Database=Facts_Product;Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<TblAdmin>(entity =>
            {
                entity.HasKey(e => e.AdminId);

                entity.ToTable("tbl_admin");

                entity.Property(e => e.AdminId)
                    .ValueGeneratedNever()
                    .HasColumnName("admin_id");

                entity.Property(e => e.AdminEmail)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("admin_email");

                entity.Property(e => e.AdminName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("admin_name");

                entity.Property(e => e.AdminPassword)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("admin_password");

                entity.Property(e => e.AdminPhone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("admin_phone");
            });

            modelBuilder.Entity<TblCategoryProduct>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.ToTable("tbl_category_product");

                entity.Property(e => e.CategoryId)
                    .ValueGeneratedNever()
                    .HasColumnName("category_id");

                entity.Property(e => e.CategoryDesc)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("category_desc");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("category_name");

                entity.Property(e => e.CategoryStatus).HasColumnName("category_status");
            });

            modelBuilder.Entity<TblProduct>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.ToTable("tbl_product");

                entity.Property(e => e.ProductId)
                    .ValueGeneratedNever()
                    .HasColumnName("product_id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.ProductContent)
                    .HasColumnType("text")
                    .HasColumnName("product_content");

                entity.Property(e => e.ProductDesc)
                    .HasColumnType("text")
                    .HasColumnName("product_desc");

                entity.Property(e => e.ProductImage)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("product_image");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("product_name");

                entity.Property(e => e.ProductPrice)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("product_price");

                entity.Property(e => e.ProductStatus).HasColumnName("product_status");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.TblProducts)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_category_product_id");
            });

            modelBuilder.Entity<TblShipping>(entity =>
            {
                entity.HasKey(e => e.ShippingId);

                entity.ToTable("tbl_shipping");

                entity.Property(e => e.ShippingId)
                    .ValueGeneratedNever()
                    .HasColumnName("shipping_id");

                entity.Property(e => e.ShippingAddress)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("shipping_address");

                entity.Property(e => e.ShippingEmail)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("shipping_email");

                entity.Property(e => e.ShippingName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("shipping_name");

                entity.Property(e => e.ShippingNote)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("shipping_note");

                entity.Property(e => e.ShippingPhone)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("shipping_phone");
            });

            modelBuilder.Entity<TblSupplier>(entity =>
            {
                entity.HasKey(e => e.SupplierId);

                entity.ToTable("tbl_supplier");

                entity.Property(e => e.SupplierId)
                    .ValueGeneratedNever()
                    .HasColumnName("supplier_id");

                entity.Property(e => e.SupplierEmail)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("supplier_email");

                entity.Property(e => e.SupplierName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("supplier_name");

                entity.Property(e => e.SupplierPassword)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("supplier_password");

                entity.Property(e => e.SupplierPhone)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("supplier_phone");

                entity.Property(e => e.SupplierUsername)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("supplier_username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
