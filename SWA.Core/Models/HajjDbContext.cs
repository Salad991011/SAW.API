using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SWA.Core.Models;

public partial class HajjDbContext : DbContext
{
    public HajjDbContext()
    {
    }

    public HajjDbContext(DbContextOptions<HajjDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ApiErrorLog> ApiErrorLogs { get; set; }

    public virtual DbSet<Business> Businesses { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Permit> Permits { get; set; }

    public virtual DbSet<PermitLocation> PermitLocations { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Volunteer> Volunteers { get; set; }

    public virtual DbSet<Worker> Workers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-40I5EP86;Database=HajjPermits;User Id=db;Password=root123123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApiErrorLog>(entity =>
        {
            entity.HasKey(e => e.ApiLogId).HasName("PK__ApiError__F842F3C06C237BFC");

            entity.ToTable("ApiErrorLog");

            entity.Property(e => e.ApiLogId).HasColumnName("ApiLogID");
            entity.Property(e => e.ApiName).HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ErrorMessage).HasMaxLength(255);
            entity.Property(e => e.ErrorMessageAr).HasMaxLength(255);
            entity.Property(e => e.LogId).HasColumnName("LogID");

            entity.HasOne(d => d.Log).WithMany(p => p.ApiErrorLogs)
                .HasForeignKey(d => d.LogId)
                .HasConstraintName("FK__ApiErrorL__LogID__61316BF4");
        });

        modelBuilder.Entity<Business>(entity =>
        {
            entity.HasKey(e => e.BusinessId).HasName("PK__Business__F1EAA34EAF2807EB");

            entity.ToTable("Business");

            entity.Property(e => e.BusinessId)
                .ValueGeneratedNever()
                .HasColumnName("BusinessID");
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.BusinessName).HasMaxLength(200);
            entity.Property(e => e.BusinessType).HasMaxLength(100);
            entity.Property(e => e.ContactInfo).HasMaxLength(150);
            entity.Property(e => e.OwnerId).HasColumnName("OwnerID");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__Log__5E5499A8006BD1EF");

            entity.ToTable("Log");

            entity.Property(e => e.LogId).HasColumnName("LogID");
            entity.Property(e => e.Action).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.EntityId).HasColumnName("EntityID");
            entity.Property(e => e.EntityName).HasMaxLength(100);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Logs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Log__UserID__5D60DB10");
        });

        modelBuilder.Entity<Permit>(entity =>
        {
            entity.HasKey(e => e.UnifiedPermitNumber).HasName("PK__Permit__2C85A7BCB77D139F");

            entity.ToTable("Permit");

            entity.Property(e => e.UnifiedPermitNumber).ValueGeneratedNever();
            entity.Property(e => e.BusinessId).HasColumnName("BusinessID");
            entity.Property(e => e.CancelReason).HasMaxLength(255);
            entity.Property(e => e.ClientIpaddress)
                .HasMaxLength(100)
                .HasColumnName("ClientIPAddress");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.HolderPhone).HasColumnType("bigint");

            entity.Property(e => e.Lang).HasMaxLength(10);
            entity.Property(e => e.OperatorId).HasColumnName("OperatorID");
            entity.Property(e => e.PermitExpiryDateG).HasColumnType("datetime");
            entity.Property(e => e.PermitExpiryDateH).HasColumnType("int");
            entity.Property(e => e.PermitHolderId).HasColumnName("PermitHolderID");
            entity.Property(e => e.PermitIssueDateG).HasColumnType("datetime");
            entity.Property(e => e.PermitIssueDateH).HasColumnType("int");
            entity.Property(e => e.RequestCreationDateH).HasMaxLength(20);
            entity.Property(e => e.RequestReceivingDateH).HasMaxLength(20);
            entity.Property(e => e.RequestTimestamp).HasColumnType("datetime");
            entity.Property(e => e.ResponseTimestamp).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Business).WithMany(p => p.Permits)
                .HasForeignKey(d => d.BusinessId)
                .HasConstraintName("FK__Permit__Business__55BFB948");

            entity.HasOne(d => d.Operator).WithMany(p => p.Permits)
                .HasForeignKey(d => d.OperatorId)
                .HasConstraintName("FK__Permit__Operator__56B3DD81");

            entity.HasOne(d => d.PermitHolder).WithMany(p => p.Permits)
                .HasForeignKey(d => d.PermitHolderId)
                .HasConstraintName("FK__Permit__PermitHo__54CB950F");
        });

        modelBuilder.Entity<PermitLocation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PermitLo__3214EC273131F1A7");

            entity.ToTable("PermitLocation");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.LocationDescAr).HasMaxLength(100);
            entity.Property(e => e.LocationDescEn).HasMaxLength(100);

            entity.HasOne(d => d.UnifiedPermitNumberNavigation).WithMany(p => p.PermitLocations)
                .HasForeignKey(d => d.UnifiedPermitNumber)
                .HasConstraintName("FK__PermitLoc__Unifi__59904A2C");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK__Person__AA2FFB85159DEC59");

            entity.ToTable("Person");

            entity.Property(e => e.PersonId).HasColumnName("PersonID");
            entity.Property(e => e.DateOfBirthH).HasMaxLength(20);
            entity.Property(e => e.DocumentNumber).HasMaxLength(50);
            entity.Property(e => e.DocumentType).HasMaxLength(50);
            entity.Property(e => e.FamilyNameT).HasMaxLength(100);
            entity.Property(e => e.FatherNameT).HasMaxLength(100);
            entity.Property(e => e.FirstNameT).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(20);
            entity.Property(e => e.GrandfatherNameT).HasMaxLength(100);
            entity.Property(e => e.NationalityDescAr).HasMaxLength(100);
            entity.Property(e => e.NationalityDescEn).HasMaxLength(100);
            entity.Property(e => e.PermitHolderFamilyName).HasMaxLength(100);
            entity.Property(e => e.PermitHolderFatherName).HasMaxLength(100);
            entity.Property(e => e.PermitHolderFirstName).HasMaxLength(100);
            entity.Property(e => e.PermitHolderGrandfatherName).HasMaxLength(100);
            entity.Property(e => e.PermitHolderNationality).HasMaxLength(100);
            entity.Property(e => e.PersonStatus).HasMaxLength(20);
            entity.Property(e => e.SexDescAr).HasMaxLength(50);
            entity.Property(e => e.SexDescEn).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CCACE0D264A7");

            entity.ToTable("User");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.Username).HasMaxLength(100);
        });

        modelBuilder.Entity<Volunteer>(entity =>
        {
            entity.HasKey(e => e.VolunteerId).HasName("PK__Voluntee__716F6FCC23BBE73B");

            entity.ToTable("Volunteer");

            entity.HasIndex(e => e.PersonId, "UQ__Voluntee__AA2FFB84BC98C3E9").IsUnique();

            entity.Property(e => e.VolunteerId).HasColumnName("VolunteerID");
            entity.Property(e => e.District).HasMaxLength(100);
            entity.Property(e => e.EducationLevel).HasMaxLength(100);
            entity.Property(e => e.PersonId).HasColumnName("PersonID");
            entity.Property(e => e.PreferredLocation).HasMaxLength(100);
            entity.Property(e => e.Region).HasMaxLength(100);
            entity.Property(e => e.Specialization).HasMaxLength(100);
            entity.Property(e => e.VolunteerCategory).HasMaxLength(100);

            entity.HasOne(d => d.Person).WithOne(p => p.Volunteer)
                .HasForeignKey<Volunteer>(d => d.PersonId)
                .HasConstraintName("FK__Volunteer__Perso__4F12BBB9");
        });

        modelBuilder.Entity<Worker>(entity =>
        {
            entity.HasKey(e => e.WorkerId).HasName("PK__Worker__077C8806D3D1D7A4");

            entity.ToTable("Worker");

            entity.HasIndex(e => e.PersonId, "UQ__Worker__AA2FFB841C99CDCE").IsUnique();

            entity.Property(e => e.WorkerId).HasColumnName("WorkerID");
            entity.Property(e => e.Branch).HasMaxLength(100);
            entity.Property(e => e.Department).HasMaxLength(100);
            entity.Property(e => e.EmployeeType).HasMaxLength(100);
            entity.Property(e => e.JobTitle).HasMaxLength(100);
            entity.Property(e => e.PersonId).HasColumnName("PersonID");
            entity.Property(e => e.Region).HasMaxLength(100);
            entity.Property(e => e.SapemployeeId)
                .HasMaxLength(50)
                .HasColumnName("SAPEmployeeID");
            entity.Property(e => e.Sector).HasMaxLength(100);
            entity.Property(e => e.SubSector).HasMaxLength(100);

            entity.HasOne(d => d.Person).WithOne(p => p.Worker)
                .HasForeignKey<Worker>(d => d.PersonId)
                .HasConstraintName("FK__Worker__PersonID__4B422AD5");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
