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
    }
}