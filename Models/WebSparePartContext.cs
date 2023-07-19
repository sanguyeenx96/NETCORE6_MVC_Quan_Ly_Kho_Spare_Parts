using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace webspare.Models;

public partial class WebSparePartContext : DbContext
{
    public WebSparePartContext()
    {
    }

    public WebSparePartContext(DbContextOptions<WebSparePartContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Danhsachlinhkien> Danhsachlinhkiens { get; set; }

    public virtual DbSet<Dathang> Dathangs { get; set; }

    public virtual DbSet<LichsuLaylinhkien> LichsuLaylinhkiens { get; set; }

    public virtual DbSet<Model> Models { get; set; }

    public virtual DbSet<TblAdmin> TblAdmins { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=192.168.1.254;User Id=sa;Password=123;Initial Catalog=WebSparePart;Trusted_connection=false;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Danhsachlinhkien>(entity =>
        {
            entity.ToTable("danhsachlinhkien");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Dongia).HasColumnName("dongia");
            entity.Property(e => e.Donvi).HasColumnName("donvi");
            entity.Property(e => e.Ghichu).HasColumnName("ghichu");
            entity.Property(e => e.Image)
                .IsUnicode(false)
                .HasColumnName("image");
            entity.Property(e => e.Majig).HasColumnName("majig");
            entity.Property(e => e.Maker).HasColumnName("maker");
            entity.Property(e => e.Malinhkien).HasColumnName("malinhkien");
            entity.Property(e => e.Model)
                .HasMaxLength(50)
                .HasColumnName("model");
            entity.Property(e => e.Tenjig).HasColumnName("tenjig");
            entity.Property(e => e.Tenlinhkien).HasColumnName("tenlinhkien");
            entity.Property(e => e.Tonkho).HasColumnName("tonkho");
        });

        modelBuilder.Entity<Dathang>(entity =>
        {
            entity.ToTable("dathang");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Dongia).HasColumnName("dongia");
            entity.Property(e => e.Donvi).HasColumnName("donvi");
            entity.Property(e => e.Ghichu).HasColumnName("ghichu");
            entity.Property(e => e.Image)
                .IsUnicode(false)
                .HasColumnName("image");
            entity.Property(e => e.Majig).HasColumnName("majig");
            entity.Property(e => e.Maker).HasColumnName("maker");
            entity.Property(e => e.Malinhkien).HasColumnName("malinhkien");
            entity.Property(e => e.Model)
                .HasMaxLength(100)
                .HasColumnName("model");
            entity.Property(e => e.Ngaydathang)
                .HasMaxLength(100)
                .HasColumnName("ngaydathang");
            entity.Property(e => e.Ngaydukienhangve)
                .HasMaxLength(100)
                .HasColumnName("ngaydukienhangve");
            entity.Property(e => e.Ngaydukienhangvedot2)
                .HasMaxLength(100)
                .HasColumnName("ngaydukienhangvedot2");
            entity.Property(e => e.Ngayhangve)
                .HasMaxLength(100)
                .HasColumnName("ngayhangve");
            entity.Property(e => e.Ngayhangvedot2)
                .HasMaxLength(100)
                .HasColumnName("ngayhangvedot2");
            entity.Property(e => e.Ngayyeucau)
                .HasMaxLength(100)
                .HasColumnName("ngayyeucau");
            entity.Property(e => e.Nguoidathang).HasColumnName("nguoidathang");
            entity.Property(e => e.Soluong).HasColumnName("soluong");
            entity.Property(e => e.Soluongtonkho).HasColumnName("soluongtonkho");
            entity.Property(e => e.Soluongvedot1).HasColumnName("soluongvedot1");
            entity.Property(e => e.Soluongvedot2).HasColumnName("soluongvedot2");
            entity.Property(e => e.Tenjig).HasColumnName("tenjig");
            entity.Property(e => e.Tenlinhkien).HasColumnName("tenlinhkien");
            entity.Property(e => e.Thanhtien).HasColumnName("thanhtien");
            entity.Property(e => e.Trangthai).HasColumnName("trangthai");
        });

        modelBuilder.Entity<LichsuLaylinhkien>(entity =>
        {
            entity.ToTable("lichsu_laylinhkien");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Donvi).HasColumnName("donvi");
            entity.Property(e => e.Image)
                .IsUnicode(false)
                .HasColumnName("image");
            entity.Property(e => e.Majig).HasColumnName("majig");
            entity.Property(e => e.Maker).HasColumnName("maker");
            entity.Property(e => e.Malinhkien).HasColumnName("malinhkien");
            entity.Property(e => e.Model).HasColumnName("model");
            entity.Property(e => e.Ngaylay)
                .HasColumnType("datetime")
                .HasColumnName("ngaylay");
            entity.Property(e => e.Nguoilay).HasColumnName("nguoilay");
            entity.Property(e => e.Soluong).HasColumnName("soluong");
            entity.Property(e => e.Tenjig).HasColumnName("tenjig");
            entity.Property(e => e.Tenlinhkien).HasColumnName("tenlinhkien");
        });

        modelBuilder.Entity<Model>(entity =>
        {
            entity.ToTable("model");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Tenmodel)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("tenmodel");
        });

        modelBuilder.Entity<TblAdmin>(entity =>
        {
            entity.HasKey(e => e.AdId);

            entity.ToTable("tbl_admin");

            entity.Property(e => e.AdId).HasColumnName("ad_id");
            entity.Property(e => e.AdName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ad_name");
            entity.Property(e => e.AdPass)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ad_pass");
            entity.Property(e => e.Hoten)
                .HasMaxLength(255)
                .HasColumnName("hoten");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
