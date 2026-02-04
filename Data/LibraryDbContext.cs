using Microsoft.EntityFrameworkCore;
using LibraryProject.Models;
namespace LibraryProject.Data
{
    public class LibraryDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-CMIPSPQ\SQLEXPRESS;Database=LibraryManagementDb;Trusted_Connection=True;TrustServerCertificate=True;");
        }



        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Loan> Loans { get; set; }

    }
}