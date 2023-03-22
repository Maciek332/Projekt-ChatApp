using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ChatApp.Core.Models;

public partial class ChatDbContext : DbContext
{
    public ChatDbContext()
    {
    }

    public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Messages> Messages { get; set; } = null!;
    public DbSet<GroupMessage> UserGroupMessages { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("datasource=localhost; username=root; password=; databse=ChatDB");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.UserID).HasColumnType("UserID");

            entity.Property(e => e.E_Mail).HasMaxLength(25);

            entity.Property(e => e.UserName).HasMaxLength(25);

            entity.Property(e => e.Password).HasMaxLength(15);

            entity.Property(e => e.RegisterDate).HasColumnType("text");
        });

        modelBuilder.Entity<Messages>(entity =>
        {
            entity.ToTable("Messages");

            entity.Property(e => e.MessageID).HasColumnName("MessageID");

            entity.Property(e => e.SentDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.MessageAuthor).HasColumnType("text");

            entity.Property(d => d.MessageDestination).HasColumnName("MessageDestination");

            entity.Property(d => d.MessageContent).HasColumnName("MessageContent");
        });

        modelBuilder.Entity<GroupMessage>(entity =>
        {
            entity.ToTable("GroupMessage");

            entity.Property(e => e.GroupMessageID).HasColumnName("GroupMessageID");

            entity.Property(e => e.SentDate)
                .HasColumnType("datetime");

            entity.Property(e => e.MessageAuthor).HasColumnType("text");

            entity.Property(d => d.MessageGroup).HasColumnName("MessageGroup");

            entity.Property(d => d.MessageContent).HasColumnName("MessageContent");
        });
        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}