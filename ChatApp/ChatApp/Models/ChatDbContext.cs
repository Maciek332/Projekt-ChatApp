using System;
using System.Collections.Generic;
using ChatApp.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Models;

public partial class ChatDbContext : DbContext
{

    public ChatDbContext(DbContextOptions<ChatDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Messages> Messages { get; set; } = null!;
    public DbSet<GroupMessage> UserGroupMessages { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=localhost;Database=ChatDB;Uid=root;Pwd=;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.UserID).HasColumnName("UserID");

            entity.Property(e => e.E_Mail).HasMaxLength(25);

            entity.Property(e => e.UserName).HasMaxLength(25);

            entity.Property(e => e.Password).HasMaxLength(15);

            entity.Property(e => e.RegisterDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Messages>(entity =>
        {
            entity.ToTable("Messages");

            entity.Property(e => e.MessageID).HasColumnName("MessageID");

            entity.Property(e => e.SentDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("'getdate()'");

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
