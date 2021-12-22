﻿// <auto-generated />
using System;
using Donkey.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Donkey.Infrastructure.Migrations
{
    [DbContext(typeof(DonkeyDbContext))]
    [Migration("20211222232419_database_initialization")]
    partial class database_initialization
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Donkey.Core.Entities.Blog", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("OwnerEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Name");

                    b.HasIndex("OwnerEmail");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("Donkey.Core.Entities.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AuthorEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("BlogName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BlogName");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Donkey.Core.Entities.User", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Email");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Donkey.Core.Entities.Blog", b =>
                {
                    b.HasOne("Donkey.Core.Entities.User", null)
                        .WithMany("Blogs")
                        .HasForeignKey("OwnerEmail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Donkey.Core.Entities.Post", b =>
                {
                    b.HasOne("Donkey.Core.Entities.Blog", null)
                        .WithMany("Posts")
                        .HasForeignKey("BlogName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Donkey.Core.Entities.Blog", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("Donkey.Core.Entities.User", b =>
                {
                    b.Navigation("Blogs");
                });
#pragma warning restore 612, 618
        }
    }
}
