﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MiniBackend.Models;

#nullable disable

namespace MiniBackend.Migrations
{
    [DbContext(typeof(MiniContext))]
    [Migration("20220302185945_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-preview.1.22076.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MiniBackend.Models.ColorFamily", b =>
                {
                    b.Property<int>("ColorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ColorId"), 1L, 1);

                    b.Property<string>("ColorFamilyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ColorId");

                    b.ToTable("ColorFamily");
                });

            modelBuilder.Entity("MiniBackend.Models.Game", b =>
                {
                    b.Property<int>("GameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GameId"), 1L, 1);

                    b.Property<string>("BoxArtUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GameName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MiniMetaMetaId")
                        .HasColumnType("int");

                    b.Property<string>("YearPublished")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GameId");

                    b.HasIndex("MiniMetaMetaId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("MiniBackend.Models.Mini", b =>
                {
                    b.Property<int>("MiniId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MiniId"), 1L, 1);

                    b.Property<DateTime>("CompletionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<string>("MiniName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sculptor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MiniId");

                    b.HasIndex("GameId");

                    b.ToTable("Minis");
                });

            modelBuilder.Entity("MiniBackend.Models.MiniMeta", b =>
                {
                    b.Property<int>("MetaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MetaId"), 1L, 1);

                    b.Property<string>("Scale")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Style")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MetaId");

                    b.ToTable("MiniMeta");
                });

            modelBuilder.Entity("MiniBackend.Models.Paint", b =>
                {
                    b.Property<int>("PaintId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaintId"), 1L, 1);

                    b.Property<int>("ColorFamilyColorId")
                        .HasColumnType("int");

                    b.Property<string>("ColorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PaintBrandBrandId")
                        .HasColumnType("int");

                    b.Property<string>("SerialNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PaintId");

                    b.HasIndex("ColorFamilyColorId");

                    b.HasIndex("PaintBrandBrandId");

                    b.ToTable("Paints");
                });

            modelBuilder.Entity("MiniBackend.Models.PaintBrand", b =>
                {
                    b.Property<int>("BrandId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BrandId"), 1L, 1);

                    b.Property<string>("BrandName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BrandId");

                    b.ToTable("PaintBrand");
                });

            modelBuilder.Entity("MiniBackend.Models.Photo", b =>
                {
                    b.Property<int>("PhotoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PhotoId"), 1L, 1);

                    b.Property<string>("Filename")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MiniId")
                        .HasColumnType("int");

                    b.HasKey("PhotoId");

                    b.HasIndex("MiniId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("MiniPaint", b =>
                {
                    b.Property<int>("MinisMiniId")
                        .HasColumnType("int");

                    b.Property<int>("PaintsPaintId")
                        .HasColumnType("int");

                    b.HasKey("MinisMiniId", "PaintsPaintId");

                    b.HasIndex("PaintsPaintId");

                    b.ToTable("MiniPaint");
                });

            modelBuilder.Entity("MiniBackend.Models.Game", b =>
                {
                    b.HasOne("MiniBackend.Models.MiniMeta", "MiniMeta")
                        .WithMany()
                        .HasForeignKey("MiniMetaMetaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MiniMeta");
                });

            modelBuilder.Entity("MiniBackend.Models.Mini", b =>
                {
                    b.HasOne("MiniBackend.Models.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("MiniBackend.Models.Paint", b =>
                {
                    b.HasOne("MiniBackend.Models.ColorFamily", "ColorFamily")
                        .WithMany()
                        .HasForeignKey("ColorFamilyColorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MiniBackend.Models.PaintBrand", "PaintBrand")
                        .WithMany()
                        .HasForeignKey("PaintBrandBrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ColorFamily");

                    b.Navigation("PaintBrand");
                });

            modelBuilder.Entity("MiniBackend.Models.Photo", b =>
                {
                    b.HasOne("MiniBackend.Models.Mini", "Mini")
                        .WithMany("Photos")
                        .HasForeignKey("MiniId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mini");
                });

            modelBuilder.Entity("MiniPaint", b =>
                {
                    b.HasOne("MiniBackend.Models.Mini", null)
                        .WithMany()
                        .HasForeignKey("MinisMiniId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MiniBackend.Models.Paint", null)
                        .WithMany()
                        .HasForeignKey("PaintsPaintId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MiniBackend.Models.Mini", b =>
                {
                    b.Navigation("Photos");
                });
#pragma warning restore 612, 618
        }
    }
}
