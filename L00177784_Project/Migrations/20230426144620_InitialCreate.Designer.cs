﻿// <auto-generated />
using System;
using L00177784_Project.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace L00177784_Project.Migrations
{
    [DbContext(typeof(LoyaltyGroupsContext))]
    [Migration("20230426144620_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("L00177784_Project.Models.LoyaltyGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Image")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Threshold")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("LoyaltyGroups");
                });

            modelBuilder.Entity("L00177784_Project.Models.LoyaltyScheme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CustomerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("LastFreeBag")
                        .HasColumnType("TEXT");

                    b.Property<int?>("LoyaltyGroup_Id")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("RemainingItems")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("LoyaltyGroup_Id");

                    b.ToTable("LoyaltySchemes");
                });

            modelBuilder.Entity("L00177784_Project.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Barcode")
                        .HasColumnType("TEXT");

                    b.Property<int?>("LoyaltyGroup_Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("Sku")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("LoyaltyGroup_Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("L00177784_Project.Models.LoyaltyScheme", b =>
                {
                    b.HasOne("L00177784_Project.Models.LoyaltyGroup", "LoyaltyGroup")
                        .WithMany("LoyaltySchemes")
                        .HasForeignKey("LoyaltyGroup_Id");

                    b.Navigation("LoyaltyGroup");
                });

            modelBuilder.Entity("L00177784_Project.Models.Product", b =>
                {
                    b.HasOne("L00177784_Project.Models.LoyaltyGroup", "LoyaltyGroup")
                        .WithMany("Products")
                        .HasForeignKey("LoyaltyGroup_Id");

                    b.Navigation("LoyaltyGroup");
                });

            modelBuilder.Entity("L00177784_Project.Models.LoyaltyGroup", b =>
                {
                    b.Navigation("LoyaltySchemes");

                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
