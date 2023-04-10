using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Windows.System;

namespace ChatApp.DBModels;

public partial class ChatDbContext : DbContext
{
    public ChatDbContext()
    {
    }

    public ChatDbContext(DbContextOptions<ChatDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Groupmessage> Groupmessages { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=localhost;Port=3306;Database=ChatDB;Uid=root;Pwd=;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("PRIMARY");

            entity.ToTable("group");

            entity.Property(e => e.GroupId)
                .HasColumnType("int(11)")
                .HasColumnName("GroupID");
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.GroupName)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<Groupmessage>(entity =>
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

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PRIMARY");

            entity.ToTable("message");

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

        modelBuilder.Entity<User>(entity =>
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

        modelBuilder.Entity<User>()
            .HasMany(e => e.Groups)
            .WithMany(e => e.Users)
            .UsingEntity(e => e.ToTable("UserGroup"));

        modelBuilder.Entity<Group>()
            .HasMany(e => e.Users)
            .WithMany(e => e.Groups)
            .UsingEntity(e => e.ToTable("UserGroup"));

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
