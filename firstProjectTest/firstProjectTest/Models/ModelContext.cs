using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace firstProjectTest.Models;

public partial class ModelContext : DbContext
{
    public ModelContext()
    {
    }

    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aboutuspage> Aboutuspages { get; set; }

    public virtual DbSet<Bank> Banks { get; set; }

    public virtual DbSet<Beneficiary> Beneficiaries { get; set; }

    public virtual DbSet<Contactuspage> Contactuspages { get; set; }

    public virtual DbSet<Homepage> Homepages { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<Testimonialpage> Testimonialpages { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseOracle("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SID=xe)));User Id=C##Test_FirstProject;Password=Test_FirstProject;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("C##TEST_FIRSTPROJECT")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<Aboutuspage>(entity =>
        {
            entity.HasKey(e => e.PageId).HasName("SYS_C008700");

            entity.ToTable("ABOUTUSPAGE");

            entity.Property(e => e.PageId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("PAGE_ID");
            entity.Property(e => e.Email)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.HomepageId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("HOMEPAGE_ID");
            entity.Property(e => e.Image)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("IMAGE");
            entity.Property(e => e.Location)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LOCATION");
            entity.Property(e => e.Paragraph)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("PARAGRAPH");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("PHONE_NUMBER");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TITLE");

            entity.HasOne(d => d.Homepage).WithMany(p => p.Aboutuspages)
                .HasForeignKey(d => d.HomepageId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("SYS_C008701");
        });

        modelBuilder.Entity<Bank>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008691");

            entity.ToTable("BANK");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.Balance)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("BALANCE");
            entity.Property(e => e.Cardholder)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CARDHOLDER");
            entity.Property(e => e.Cardnumber)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("CARDNUMBER");
            entity.Property(e => e.Cvv)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("CVV");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.PaymentId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("PAYMENT_ID");
            entity.Property(e => e.Username)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("USERNAME");

            entity.HasOne(d => d.Payment).WithMany(p => p.Banks)
                .HasForeignKey(d => d.PaymentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("SYS_C008692");
        });

        modelBuilder.Entity<Beneficiary>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008688");

            entity.ToTable("BENEFICIARY");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.Age)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("AGE");
            entity.Property(e => e.Name)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.ProofImage)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("PROOF_IMAGE");
            entity.Property(e => e.Relationship)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("RELATIONSHIP");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("STATUS");
            entity.Property(e => e.SubscriptionId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("SUBSCRIPTION_ID");

            entity.HasOne(d => d.Subscription).WithMany(p => p.Beneficiaries)
                .HasForeignKey(d => d.SubscriptionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C008689");
        });

        modelBuilder.Entity<Contactuspage>(entity =>
        {
            entity.HasKey(e => e.PageId).HasName("SYS_C008707");

            entity.ToTable("CONTACTUSPAGE");

            entity.Property(e => e.PageId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("PAGE_ID");
            entity.Property(e => e.Email)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.HomepageId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("HOMEPAGE_ID");
            entity.Property(e => e.Message)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("MESSAGE");
            entity.Property(e => e.Name)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("NAME");

            entity.HasOne(d => d.Homepage).WithMany(p => p.Contactuspages)
                .HasForeignKey(d => d.HomepageId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("SYS_C008708");
        });

        modelBuilder.Entity<Homepage>(entity =>
        {
            entity.HasKey(e => e.PageId).HasName("SYS_C008694");

            entity.ToTable("HOMEPAGE");

            entity.Property(e => e.PageId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("PAGE_ID");
            entity.Property(e => e.Image)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("IMAGE");
            entity.Property(e => e.Paragraph)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("PARAGRAPH");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TITLE");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008682");

            entity.ToTable("PAYMENT");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.Amount)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("AMOUNT");
            entity.Property(e => e.PaymentDate)
                .HasColumnType("DATE")
                .HasColumnName("PAYMENT_DATE");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PAYMENT_METHOD");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008677");

            entity.ToTable("ROLE");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.RoleDescription)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("ROLE_DESCRIPTION");
            entity.Property(e => e.RoleName)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("ROLE_NAME");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008684");

            entity.ToTable("SUBSCRIPTION");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.EndDate)
                .HasColumnType("DATE")
                .HasColumnName("END_DATE");
            entity.Property(e => e.Organization)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("ORGANIZATION");
            entity.Property(e => e.PaymentId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("PAYMENT_ID");
            entity.Property(e => e.StartDate)
                .HasColumnType("DATE")
                .HasColumnName("START_DATE");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("'Pending'")
                .HasColumnName("STATUS");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USER_ID");

            entity.HasOne(d => d.Payment).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.PaymentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("SYS_C008686");

            entity.HasOne(d => d.User).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C008685");
        });

        modelBuilder.Entity<Testimonialpage>(entity =>
        {
            entity.HasKey(e => e.PageId).HasName("SYS_C008696");

            entity.ToTable("TESTIMONIALPAGE");

            entity.Property(e => e.PageId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("PAGE_ID");
            entity.Property(e => e.Content)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("CONTENT");
            entity.Property(e => e.HomepageId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("HOMEPAGE_ID");
            entity.Property(e => e.Image)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("IMAGE");
            entity.Property(e => e.Name)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Reviewmessage)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("REVIEWMESSAGE");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("'Pending'  ")
                .HasColumnName("STATUS");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TITLE");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USER_ID");

            entity.HasOne(d => d.Homepage).WithMany(p => p.Testimonialpages)
                .HasForeignKey(d => d.HomepageId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("SYS_C008698");

            entity.HasOne(d => d.User).WithMany(p => p.Testimonialpages)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C008697");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008679");

            entity.ToTable("USERS");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.Email)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Name)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Password)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("PHONE_NUMBER");
            entity.Property(e => e.ProfileImage)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("PROFILE_IMAGE");
            entity.Property(e => e.RoleId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROLE_ID");
            entity.Property(e => e.Username)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("USERNAME");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C008680");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
