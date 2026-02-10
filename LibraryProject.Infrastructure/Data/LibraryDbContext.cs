using Microsoft.EntityFrameworkCore;
using LibraryProject.Domain;
using LibraryProject.Application;

namespace LibraryProject.Infrastructure.Data
{
    public class LibraryDbContext : DbContext, ILibraryDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-CMIPSPQ\SQLEXPRESS;Database=LibraryManagementDb;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Member> Members { get; set; } = null!;
        public DbSet<Loan> Loans { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Yazılım" },
                new Category { Id = 2, Name = "Bilim Kurgu" },
                new Category { Id = 3, Name = "Psikoloji" },
                new Category { Id = 4, Name = "Ekonomi" },
                new Category { Id = 5, Name = "Tarih" },
                new Category { Id = 6, Name = "Felsefe" },
                new Category { Id = 7, Name = "Polisiye" },
                new Category { Id = 8, Name = "Klasik Edebiyat" }
            );

            modelBuilder.Entity<Member>().HasData(
            new Member { Id = -1, FirstName = "Tolga", LastName = "Aydaç", PhoneNumber = "1234567890", JoinDate = new DateTime(2024, 1, 1), IsDeleted = false }
);
        }

    }
}