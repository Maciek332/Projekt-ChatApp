﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace ChatApp.Models;

public partial class ChatDbContext : DbContext
{
    public ChatDbContext()
    {
    }

    public ChatDbContext(DbContextOptions<ChatDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<GroupMessages> Groupmessages { get; set; }

    public virtual DbSet<Messages> Messages { get; set; }

    public virtual DbSet<Users> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();


            string connectionString = config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseMySQL(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GroupMessages>(entity =>
        {
            entity.HasKey(e => e.GroupMessageId).HasName("PRIMARY");

            entity.ToTable("groupmessage");

            entity.Property(e => e.GroupMessageId)
                .HasColumnType("int(11)")
                .HasColumnName("GroupMessageID");
            entity.Property(e => e.GroupName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.MessageAuthor).HasColumnType("int(11)");
            entity.Property(e => e.MessageContent).IsRequired();
            entity.Property(e => e.MessageGroup).HasColumnType("int(11)");
            entity.Property(e => e.SentDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Messages>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PRIMARY");

            entity.ToTable("messages");

            entity.Property(e => e.MessageId)
                .HasColumnType("int(11)")
                .HasColumnName("MessageID");
            entity.Property(e => e.MessageAuthor).HasColumnType("int(11)");
            entity.Property(e => e.MessageContent).IsRequired();
            entity.Property(e => e.MessageDestination).HasColumnType("int(11)");
            entity.Property(e => e.SentDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("user");

            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("UserID");
            entity.Property(e => e.EMail)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("E_Mail");
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(128);
            entity.Property(e => e.RegisterDate).HasColumnType("datetime");
            entity.Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(25);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
