using LibraryProject.Domain;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Application;

public interface ILibraryDbContext
{
    DbSet<Book> Books { get; set; }
    DbSet<Member> Members { get; set; }
    DbSet<Loan> Loans { get; set; }
    DbSet<Category> Categories { get; set; }
    int SaveChanges();
}