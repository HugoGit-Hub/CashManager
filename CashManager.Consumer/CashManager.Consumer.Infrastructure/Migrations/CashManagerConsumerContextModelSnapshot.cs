﻿// <auto-generated />
using System;
using CashManager.Consumer.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CashManager.Consumer.Infrastructure.Migrations
{
    [DbContext(typeof(CashManagerConsumerContext))]
    partial class CashManagerConsumerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ArticleUsers", b =>
                {
                    b.Property<int>("ArticlesId")
                        .HasColumnType("int");

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("ArticlesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("ArticleUsers");
                });

            modelBuilder.Entity("CashManager.Consumer.Domain.Articles.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("CashManager.Consumer.Domain.CartItems.CartItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ArticleId")
                        .HasColumnType("int");

                    b.Property<int>("IdArticle")
                        .HasColumnType("int");

                    b.Property<int>("IdShoppingSession")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("ShoppingSessionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("ShoppingSessionId");

                    b.ToTable("CartItem");
                });

            modelBuilder.Entity("CashManager.Consumer.Domain.ShoppingSessions.ShoppingSession", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("State")
                        .HasColumnType("bit");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ShoppingSession");
                });

            modelBuilder.Entity("CashManager.Consumer.Domain.Transactions.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<string>("Creditor")
                        .IsRequired()
                        .HasMaxLength(34)
                        .HasColumnType("nvarchar(34)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Debtor")
                        .IsRequired()
                        .HasMaxLength(34)
                        .HasColumnType("nvarchar(34)");

                    b.Property<Guid>("Guid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("CashManager.Consumer.Domain.User.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ArticleUsers", b =>
                {
                    b.HasOne("CashManager.Consumer.Domain.Articles.Article", null)
                        .WithMany()
                        .HasForeignKey("ArticlesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CashManager.Consumer.Domain.User.Users", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CashManager.Consumer.Domain.CartItems.CartItem", b =>
                {
                    b.HasOne("CashManager.Consumer.Domain.Articles.Article", "Article")
                        .WithMany()
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CashManager.Consumer.Domain.ShoppingSessions.ShoppingSession", "ShoppingSession")
                        .WithMany("CartItems")
                        .HasForeignKey("ShoppingSessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Article");

                    b.Navigation("ShoppingSession");
                });

            modelBuilder.Entity("CashManager.Consumer.Domain.ShoppingSessions.ShoppingSession", b =>
                {
                    b.HasOne("CashManager.Consumer.Domain.User.Users", "User")
                        .WithMany("ShoppingSessions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CashManager.Consumer.Domain.ShoppingSessions.ShoppingSession", b =>
                {
                    b.Navigation("CartItems");
                });

            modelBuilder.Entity("CashManager.Consumer.Domain.User.Users", b =>
                {
                    b.Navigation("ShoppingSessions");
                });
#pragma warning restore 612, 618
        }
    }
}
