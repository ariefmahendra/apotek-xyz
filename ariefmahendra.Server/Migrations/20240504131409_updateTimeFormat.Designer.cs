﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ariefmahendra.Repositories;

#nullable disable

namespace ariefmahendra.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240504131409_updateTimeFormat")]
    partial class updateTimeFormat
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ariefmahendra.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("ProductCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("product_code");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("product_name");

                    b.Property<long>("ProductPrice")
                        .HasColumnType("bigint")
                        .HasColumnName("product_price");

                    b.Property<int>("Stock")
                        .HasColumnType("int")
                        .HasColumnName("stock");

                    b.HasKey("Id");

                    b.ToTable("mst_product");
                });

            modelBuilder.Entity("ariefmahendra.Entities.Purchase", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("NoInvoice")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("no_invoice");

                    b.Property<long>("Total")
                        .HasColumnType("bigint")
                        .HasColumnName("total");

                    b.Property<string>("TransactionDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("transaction_date");

                    b.HasKey("Id");

                    b.ToTable("tx_purchase");
                });

            modelBuilder.Entity("ariefmahendra.Entities.PurchaseDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("product_id");

                    b.Property<Guid>("PurchaseId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("purchase_id");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("PurchaseId");

                    b.ToTable("tx_purchase_detail");
                });

            modelBuilder.Entity("ariefmahendra.Entities.PurchaseDetail", b =>
                {
                    b.HasOne("ariefmahendra.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ariefmahendra.Entities.Purchase", null)
                        .WithMany("PurchaseDetails")
                        .HasForeignKey("PurchaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ariefmahendra.Entities.Purchase", b =>
                {
                    b.Navigation("PurchaseDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
