﻿// <auto-generated />
using System;
using AccountsManager.DataModels.V1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AccountsManager.DataModels.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20230105193141_AddedDescriptionToVoucher")]
    partial class AddedDescriptionToVoucher
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AccountsManager.DataModels.V1.Models.TAccount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccountType")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("Id");

                    b.ToTable("TAccounts");
                });

            modelBuilder.Entity("AccountsManager.DataModels.V1.Models.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Credit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Debt")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("VoucherId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("VoucherId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("AccountsManager.DataModels.V1.Models.Voucher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("VoucherType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Vouchers");
                });

            modelBuilder.Entity("AccountsManager.DataModels.V1.Models.Transaction", b =>
                {
                    b.HasOne("AccountsManager.DataModels.V1.Models.TAccount", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AccountsManager.DataModels.V1.Models.Voucher", "Voucher")
                        .WithMany("Transactions")
                        .HasForeignKey("VoucherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Voucher");
                });

            modelBuilder.Entity("AccountsManager.DataModels.V1.Models.Voucher", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
