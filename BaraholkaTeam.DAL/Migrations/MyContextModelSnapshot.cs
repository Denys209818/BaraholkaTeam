﻿// <auto-generated />
using System;
using BaraholkaTeam.DAL.ContextData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BaraholkaTeam.DAL.Migrations
{
    [DbContext(typeof(MyContext))]
    partial class MyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("BaraholkaTeam.DAL.ContextData.FilterName", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.ToTable("tblFilterNamesTeamProject");
                });

            modelBuilder.Entity("BaraholkaTeam.DAL.ContextData.FilterNameValue", b =>
                {
                    b.Property<int>("FilterNameId")
                        .HasColumnType("integer");

                    b.Property<int>("FilterValueId")
                        .HasColumnType("integer");

                    b.HasKey("FilterNameId", "FilterValueId");

                    b.HasIndex("FilterValueId");

                    b.ToTable("tblFilterNameValuesTeamProject");
                });

            modelBuilder.Entity("BaraholkaTeam.DAL.ContextData.FilterProduct", b =>
                {
                    b.Property<int>("FilterNameId")
                        .HasColumnType("integer");

                    b.Property<int>("FilterValueId")
                        .HasColumnType("integer");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.HasKey("FilterNameId", "FilterValueId", "ProductId");

                    b.HasIndex("FilterValueId");

                    b.HasIndex("ProductId");

                    b.ToTable("tblFilterProductsTeamProject");
                });

            modelBuilder.Entity("BaraholkaTeam.DAL.ContextData.FilterValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.ToTable("tblFilterValuesTeamProject");
                });

            modelBuilder.Entity("BaraholkaTeam.DAL.ContextData.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("FullDescription")
                        .HasMaxLength(5000)
                        .HasColumnType("character varying(5000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<byte[]>("image")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("tblProductsTeamProject");
                });

            modelBuilder.Entity("BaraholkaTeam.DAL.ContextData.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<byte[]>("Image")
                        .HasColumnType("bytea");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.ToTable("tblUsersTeamProject");
                });

            modelBuilder.Entity("BaraholkaTeam.DAL.ContextData.FilterNameValue", b =>
                {
                    b.HasOne("BaraholkaTeam.DAL.ContextData.FilterName", "FilterName")
                        .WithMany("NameValues")
                        .HasForeignKey("FilterNameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BaraholkaTeam.DAL.ContextData.FilterValue", "FilterValue")
                        .WithMany("NameValues")
                        .HasForeignKey("FilterValueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FilterName");

                    b.Navigation("FilterValue");
                });

            modelBuilder.Entity("BaraholkaTeam.DAL.ContextData.FilterProduct", b =>
                {
                    b.HasOne("BaraholkaTeam.DAL.ContextData.FilterName", "FilterName")
                        .WithMany("FilterProducts")
                        .HasForeignKey("FilterNameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BaraholkaTeam.DAL.ContextData.FilterValue", "FilterValue")
                        .WithMany("FilterProducts")
                        .HasForeignKey("FilterValueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BaraholkaTeam.DAL.ContextData.Product", "Product")
                        .WithMany("FilterProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FilterName");

                    b.Navigation("FilterValue");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("BaraholkaTeam.DAL.ContextData.Product", b =>
                {
                    b.HasOne("BaraholkaTeam.DAL.ContextData.User", "user")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("BaraholkaTeam.DAL.ContextData.FilterName", b =>
                {
                    b.Navigation("FilterProducts");

                    b.Navigation("NameValues");
                });

            modelBuilder.Entity("BaraholkaTeam.DAL.ContextData.FilterValue", b =>
                {
                    b.Navigation("FilterProducts");

                    b.Navigation("NameValues");
                });

            modelBuilder.Entity("BaraholkaTeam.DAL.ContextData.Product", b =>
                {
                    b.Navigation("FilterProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
