﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MyKidsReg.Models;

public partial class MyKidsRegContext : DbContext
{
    public MyKidsRegContext()
    {
    }

    public MyKidsRegContext(DbContextOptions<MyKidsRegContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdminRelation> AdminRelations { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Institution> Institutions { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<ParentsRelation> ParentsRelations { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentLog> StudentLogs { get; set; }

    public virtual DbSet<TeacherRelation> TeacherRelations { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=HAYDAR-AL-GHAZA\\MSSQLSERVER19;Initial Catalog=MyKidsReg;Integrated Security=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdminRelation>(entity =>
        {
            entity.HasOne(d => d.Institution).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AdminRelations_Institution");

            entity.HasOne(d => d.User).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AdminRelations_Users");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Class");

            entity.HasOne(d => d.Institution).WithMany(p => p.Departments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Department_Institution");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasOne(d => d.User).WithMany(p => p.Messages).HasConstraintName("FK_Messages_Institution");

            entity.HasOne(d => d.UserNavigation).WithMany(p => p.Messages).HasConstraintName("FK_Messages_Users");
        });

        modelBuilder.Entity<ParentsRelation>(entity =>
        {
            entity.HasOne(d => d.Student).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ParentsRelations_Student");

            entity.HasOne(d => d.User).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ParentsRelations_Users");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Student__3214EC0796FE819D");

            entity.HasOne(d => d.Department).WithMany(p => p.Students).HasConstraintName("FK_Student_Department");
        });

        modelBuilder.Entity<StudentLog>(entity =>
        {
            entity.HasOne(d => d.Student).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentLog_Student");
        });

        modelBuilder.Entity<TeacherRelation>(entity =>
        {
            entity.HasOne(d => d.Department).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TecherRelations_Department");

            entity.HasOne(d => d.User).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TeacherRelations_Users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}