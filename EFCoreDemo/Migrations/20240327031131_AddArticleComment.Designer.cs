﻿// <auto-generated />
using System;
using EFCoreDemo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EFCoreDemo.Migrations
{
    [DbContext(typeof(EFDbContext))]
    [Migration("20240327031131_AddArticleComment")]
    partial class AddArticleComment
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EFCoreDemo.Model.Article", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Articles", (string)null);
                });

            modelBuilder.Entity("EFCoreDemo.Model.Author", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasComment("作者");

                    b.HasKey("Id");

                    b.ToTable("Authors", null, t =>
                        {
                            t.HasComment("作者信息");
                        });
                });

            modelBuilder.Entity("EFCoreDemo.Model.Book", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("AuthorName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasComment("作者");

                    b.Property<double>("Price")
                        .HasColumnType("float")
                        .HasComment("价格");

                    b.Property<DateTime>("PublishTime")
                        .HasColumnType("datetime2")
                        .HasComment("发布时间");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasComment("标题");

                    b.HasKey("Id");

                    b.HasIndex("AuthorName");

                    b.HasIndex("Title", "PublishTime");

                    b.ToTable("Books", null, t =>
                        {
                            t.HasComment("书籍信息");
                        });

                    b.ToView("Book", (string)null);
                });

            modelBuilder.Entity("EFCoreDemo.Model.Comment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("ArticleId")
                        .HasColumnType("bigint");

                    b.Property<string>("Message")
                        .IsRequired()
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.ToTable("Comments", (string)null);
                });

            modelBuilder.Entity("EFCoreDemo.Model.Comment", b =>
                {
                    b.HasOne("EFCoreDemo.Model.Article", "Article")
                        .WithMany("Comments")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Article");
                });

            modelBuilder.Entity("EFCoreDemo.Model.Article", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
