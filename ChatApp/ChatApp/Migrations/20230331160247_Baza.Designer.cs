﻿// <auto-generated />
using System;
using ChatApp.DBModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ChatApp.Migrations
{
    [DbContext(typeof(ChatDbContext))]
    [Migration("20230331160247_Baza")]
    partial class Baza
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ChatApp.Models.GroupMessage", b =>
                {
                    b.Property<int>("GroupMessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("GroupMessageID");

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("MessageAuthor")
                        .HasColumnType("int(11)");

                    b.Property<string>("MessageContent")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("MessageGroup")
                        .HasColumnType("int(11)");

                    b.Property<DateTime>("SentDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("GroupMessageId")
                        .HasName("PRIMARY");

                    b.ToTable("groupmessage", (string)null);
                });

            modelBuilder.Entity("ChatApp.Models.Message", b =>
                {
                    b.Property<int>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("MessageID");

                    b.Property<int>("MessageAuthor")
                        .HasColumnType("int(11)");

                    b.Property<string>("MessageContent")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("MessageDestination")
                        .HasColumnType("int(11)");

                    b.Property<DateTime>("SentDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("MessageId")
                        .HasName("PRIMARY");

                    b.ToTable("messages", (string)null);
                });

            modelBuilder.Entity("ChatApp.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("UserID");

                    b.Property<string>("EMail")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("E_Mail");

                    b.Property<bool>("IsLogedIn")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)");

                    b.HasKey("UserId")
                        .HasName("PRIMARY");

                    b.ToTable("user", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
