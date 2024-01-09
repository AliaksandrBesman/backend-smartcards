using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace backend_smartcards.Models;

public partial class SmartcardsdbContext : DbContext
{
    public SmartcardsdbContext()
    {
    }

    public SmartcardsdbContext(DbContextOptions<SmartcardsdbContext> options)
        : base(options)
    {
        this.ChangeTracker.LazyLoadingEnabled = false;
        Database.EnsureCreated();   // создаем базу данных при первом обращении
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<GrantingAccess> GrantingAccesses { get; set; }

    public virtual DbSet<QuestionAnswer> QuestionAnswers { get; set; }

    public virtual DbSet<SubjectLesson> SubjectLessons { get; set; }

    public virtual DbSet<TestAnswer> TestAnswers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserDetail> UserDetails { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=smartcardsdb;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Comment");

            entity.Property(e => e.Comment1)
                .HasMaxLength(100)
                .HasColumnName("Comment");
            entity.Property(e => e.Hidden).HasDefaultValue(false);

            entity.HasOne(d => d.Subject).WithMany(p => p.Comments)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("FK_Comment_to_SubjectLessons");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comment_to_Users");
        });

        modelBuilder.Entity<GrantingAccess>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_GratingAccess");

            entity.ToTable("GrantingAccess");

            entity.HasOne(d => d.SubjectLesson).WithMany(p => p.GrantingAccesses)
                .HasForeignKey(d => d.SubjectLessonId)
                .HasConstraintName("FK_GrantingAccess_to_Subject");

            entity.HasOne(d => d.User).WithMany(p => p.GrantingAccesses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GrantingAccess_to_Users");
        });

        modelBuilder.Entity<QuestionAnswer>(entity =>
        {
            entity.ToTable("QuestionAnswer");

            entity.Property(e => e.Answer).HasMaxLength(400); 
            entity.Property(e => e.Question).HasMaxLength(150);

            entity.HasOne(d => d.SubjectLesson).WithMany(p => p.QuestionAnswers)
                .HasForeignKey(d => d.SubjectLessonId)
                .HasConstraintName("FK_QuestionAnswer_to_SubjectLesson");
        });

        modelBuilder.Entity<SubjectLesson>(entity =>
        {
            entity.Property(e => e.Subject).HasMaxLength(40);
            entity.Property(e => e.Title).HasMaxLength(40);

            entity.HasOne(d => d.CreatedBy).WithMany(p => p.SubjectLessons)
                .HasForeignKey(d => d.CreatedById)
                .HasConstraintName("FK_SubjectLesson_to_UserCreater");
        });

        modelBuilder.Entity<TestAnswer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TestAnswer");

            entity.Property(e => e.Answer).HasMaxLength(400);

            entity.HasOne(d => d.Question).WithMany(p => p.TestAnswers)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK_TestAnswer_to_QuestionAnswer");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PF_User");

            entity.HasIndex(e => e.Login, "UQ__Users__5E55825BC90E9F8D").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Login).HasMaxLength(30);
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.Password).HasMaxLength(30);
            entity.Property(e => e.RoleId).HasColumnName("roleId");
            entity.Property(e => e.SecondName).HasMaxLength(40);
            entity.Property(e => e.Surname).HasMaxLength(30);
            entity.Property(e => e.Userdetailsid).HasColumnName("userdetailsid");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_Users_to_UserRoles");

            entity.HasOne(d => d.Userdetails).WithMany(p => p.Users)
                .HasForeignKey(d => d.Userdetailsid)
                .HasConstraintName("FK_Users_to_UserDetails");
        });

        modelBuilder.Entity<UserDetail>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Course).HasColumnName("course");
            entity.Property(e => e.Department)
                .HasMaxLength(50)
                .HasColumnName("department");
            entity.Property(e => e.Faculty)
                .HasMaxLength(50)
                .HasColumnName("faculty");
            entity.Property(e => e.Group).HasColumnName("group");
            entity.Property(e => e.Speciality)
                .HasMaxLength(50)
                .HasColumnName("speciality");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PF_UserRoles");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description).HasMaxLength(80);
            entity.Property(e => e.Name).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
