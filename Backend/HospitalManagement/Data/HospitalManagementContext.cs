using System;
using System.Collections.Generic;
using HospitalManagement.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Data;

public partial class HospitalManagementContext : DbContext
{
    public HospitalManagementContext()
    {
    }

    public HospitalManagementContext(DbContextOptions<HospitalManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<InsuranceDatum> InsuranceData { get; set; }

    public virtual DbSet<PatientDatum> PatientData { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning 
        => optionsBuilder.UseMySQL("server=Deepak;port=4406;user=root;password=admin@123;database=hospital_management");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InsuranceDatum>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("insurance_data");

            entity.HasIndex(e => e.InsuranceId, "insurance_id");

            entity.Property(e => e.InsuranceId)
                .ValueGeneratedOnAdd()
                .HasColumnName("insurance_id");
            entity.Property(e => e.InsuranceName)
                .HasMaxLength(30)
                .HasColumnName("insurance_name");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");

            entity.HasOne(d => d.Insurance).WithMany()
                .HasForeignKey(d => d.InsuranceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("insurance_data_ibfk_1");
        });

        modelBuilder.Entity<PatientDatum>(entity =>
        {
            entity.HasKey(e => e.PatientId).HasName("PRIMARY");

            entity.ToTable("patient_data");

            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .HasColumnName("email");
            entity.Property(e => e.Instance).HasColumnName("instance");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
