using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigiKeyCrawler.Models
{ 
    public partial class DigiKeyProductContext : DbContext
    {  

        public DigiKeyProductContext()
        {
        }

        public DigiKeyProductContext(DbContextOptions<DigiKeyProductContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }
        public DbSet<ProductAdditionalResource> ProductAdditionalResourcess { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<ProductCategory> ProductCategorys { get; set; }
        public DbSet<ProductDocument> ProductDocuments { get; set; }
        public DbSet<ProductEnvExportClassification> ProductEnvExportClassifications { get; set; }
        public DbSet<ProductPicture> ProductPictures { get; set; }
        public DbSet<ProductPrice> ProductPrices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("Server=114.67.251.186;User Id=root;Password=fengchao!@#123;Database=DigiKeyProduct;");
                 
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Product>(entity =>
            { 
                entity.HasKey(e => e.ProductKey); 

                entity.Property(e => e.ProductId)
                    .IsRequired()
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.ImageTitle) 
                    .HasColumnType("varchar(1200)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.DigiKeyPartNumber) 
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.ManufacturerProductNumber) 
                   .HasColumnType("varchar(200)");

                entity.Property(e => e.Supplier) 
                   .HasColumnType("varchar(200)"); 

                entity.Property(e => e.DefPdf)
                    .IsRequired()
                    .HasColumnType("varchar(2000)");

                entity.Property(e => e.DefPic)
                   .IsRequired()
                   .HasColumnType("varchar(2000)"); 

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.DetailedDescription)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Html)
                    .IsRequired()
                    .HasColumnType("text"); 

            });

            modelBuilder.Entity<ProductAdditionalResource>(entity =>
            {
                entity.HasKey(e => e.ProductAdditionalResourceKey);

                entity.Property(e => e.ProductId)
                    .IsRequired()
                    .HasColumnType("varchar(36)"); 

                entity.Property(e => e.ProductAdditionalResourceJson)
                    .IsRequired()
                    .HasColumnType("text"); 
            });

            modelBuilder.Entity<ProductAttribute>(entity =>
            {
                entity.HasKey(e => e.ProductAttributeKey);

                entity.Property(e => e.ProductId)
                    .IsRequired()
                    .HasColumnType("varchar(36)"); 

                entity.Property(e => e.ProductAttributeJson)
                    .IsRequired()
                    .HasColumnType("text"); 
            });

            modelBuilder.Entity<ProductDocument>(entity =>
            {
                entity.HasKey(e => e.ProductDocumentKey);

                entity.Property(e => e.ProductId)
                    .IsRequired()
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.ProductDocumentJson)
                    .IsRequired()
                    .HasColumnType("text"); 
            });

            modelBuilder.Entity<ProductEnvExportClassification>(entity =>
            {
                entity.HasKey(e => e.ProductEnvExportClassificationKey);

                entity.Property(e => e.ProductId)
                    .IsRequired()
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.ProductEnvExportClassificationJson)
                    .IsRequired()
                    .HasColumnType("text"); 
            }); 

            modelBuilder.Entity<ProductPicture>(entity =>
            {
                entity.HasKey(e => e.ProductPictureKey);

                entity.Property(e => e.ProductId)
                    .IsRequired()
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.PictureUrl)
                    .IsRequired()
                    .HasColumnType("varchar(1000)");
            });

            modelBuilder.Entity<ProductPrice>(entity =>
            {
                entity.HasKey(e => e.ProductPriceKey);

                entity.Property(e => e.ProductId)
                    .IsRequired()
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.ProductPriceJson)
                    .IsRequired()
                    .HasColumnType("text");
            });
             
            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.HasKey(e => e.ProductCategoryKey);

                entity.Property(e => e.CategoryId)
                    .IsRequired();

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.DetailUrl)
                   .IsRequired()
                   .HasColumnType("varchar(200)");
            });
        }
    }
}
