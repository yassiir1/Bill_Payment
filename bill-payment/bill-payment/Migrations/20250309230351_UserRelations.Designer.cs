﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using bill_payment.BillDbContext;

#nullable disable

namespace bill_payment.Migrations
{
    [DbContext(typeof(Bill_PaymentContext))]
    [Migration("20250309230351_UserRelations")]
    partial class UserRelations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("bill_payment.Domains.Admin", b =>
                {
                    b.Property<Guid>("AdminId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("AdminId");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("bill_payment.Domains.Banners", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("SettingId")
                        .HasColumnType("integer");

                    b.Property<string>("image")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("path")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("SettingId");

                    b.ToTable("Banners");
                });

            modelBuilder.Entity("bill_payment.Domains.CreditCards", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<int>("cvv")
                        .HasColumnType("integer");

                    b.Property<string>("holder_name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("last_4_digit")
                        .HasColumnType("integer");

                    b.Property<int>("month")
                        .HasColumnType("integer");

                    b.Property<string>("token_id")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("CreditCards");
                });

            modelBuilder.Entity("bill_payment.Domains.FavouritePayments", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<int>("bill_type")
                        .HasColumnType("integer");

                    b.Property<bool>("is_bill")
                        .HasColumnType("boolean");

                    b.Property<double>("last_paid_amount")
                        .HasColumnType("double precision");

                    b.Property<string>("package_code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("service_code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("service_provider_code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("user_account")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("FavouritePayments");
                });

            modelBuilder.Entity("bill_payment.Domains.Partner", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Partner");
                });

            modelBuilder.Entity("bill_payment.Domains.Roles", b =>
                {
                    b.Property<Guid>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("RoleDescription")
                        .HasColumnType("text");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("bill_payment.Domains.Setting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("primary_color")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("service_columns_number")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Setting");
                });

            modelBuilder.Entity("bill_payment.Domains.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("GiedeaUser_id")
                        .HasColumnType("text");

                    b.Property<string>("NationalId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("PartnerId")
                        .HasColumnType("uuid");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("session_id")
                        .HasColumnType("text");

                    b.Property<string>("skey")
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("bill_payment.Domains.UserPartners", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("PartnerId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PartnerId");

                    b.HasIndex("UserId");

                    b.ToTable("UserPartners");
                });

            modelBuilder.Entity("bill_payment.Domains.Banners", b =>
                {
                    b.HasOne("bill_payment.Domains.Setting", "setting")
                        .WithMany("banners")
                        .HasForeignKey("SettingId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("setting");
                });

            modelBuilder.Entity("bill_payment.Domains.UserPartners", b =>
                {
                    b.HasOne("bill_payment.Domains.Partner", "Partner")
                        .WithMany()
                        .HasForeignKey("PartnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("bill_payment.Domains.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Partner");

                    b.Navigation("User");
                });

            modelBuilder.Entity("bill_payment.Domains.Setting", b =>
                {
                    b.Navigation("banners");
                });
#pragma warning restore 612, 618
        }
    }
}
