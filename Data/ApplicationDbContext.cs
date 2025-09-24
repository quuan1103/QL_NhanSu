  using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAPP.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.ComponentModel.DataAnnotations;
using WebAPP.Models.ModelDanhMuc;
 using WebAPP.Models.Luong;
 using WebAPP.Models.HoSo;

namespace WebAPP.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet cho TaiKhoan
        public DbSet<TaiKhoanNhanVien> TaiKhoanNhanVien { get; set; }
        public DbSet<Phongban> PhongBan { get; set; }
        public DbSet<ChucVuDM> ChucVu { get; set; }
        public DbSet<BangLuong> BangLuong { get; set; }
        public DbSet<LyLich> LyLich { get; set; }
        public DbSet<WebAPP.Models.DaoTao.Daotao> DaoTao { get; set; }

        public DbSet<HinhThucBDDM> HinhThucDaoTao { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Ignore<FilterDescriptor>();
            modelBuilder.Ignore<BindingSource>();
            modelBuilder.Ignore<ModelExplorer>();
            modelBuilder.Ignore<ModelMetadata>();
            modelBuilder.Ignore<IModelMetadataProvider>();
            modelBuilder.Ignore<ValidationContext>();

            // Khai báo khóa chính cho LyLich để EF nhận diện entity đúng
            modelBuilder.Entity<LyLich>(entity =>
            {
                entity.HasKey(e => e.ID_NV);
                entity.ToTable("LyLich");
            });

            // Khai báo khóa chính cho Daotao
            modelBuilder.Entity<WebAPP.Models.DaoTao.Daotao>(entity =>
            {
                entity.HasKey(e => e.ID_DaoTao);
                entity.ToTable("DaoTao");
            });

        }
        // Override OnConfiguring nếu cần thiết để cấu hình retry khi gặp lỗi tạm thời
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=(localdb)\\mssqllocaldb;Database=DESKTOP-Q7LVNM5;Trusted_Connection=True;MultipleActiveResultSets=true",
                    options => options.EnableRetryOnFailure());
            }
        }

        // Loại bỏ FilterDescriptor khỏi mô hình
       
    }
}