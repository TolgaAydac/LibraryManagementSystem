using LibraryProject.Models;
using LibraryProject.Data;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Business
{
    public class LibraryManager
    {

        private readonly LibraryDbContext _context = new LibraryDbContext();

        public void AddBook(string title, string author, int year)
        {
            Book newBook = new Book
            {
                Title = title,
                Author = author,
                publishYear = year,
                IsAvailable = true
            };


            _context.Books.Add(newBook);
            _context.SaveChanges();

            Console.WriteLine($"Başarılı: '{title}' SQL Server'a kaydedildi.");
        }

        public void ListBooks()
        {

            var books = _context.Books.Where(b => !b.IsDeleted).ToList();

            Console.WriteLine("\n--- KİTAP LİSTESİ ---");
            foreach (var book in books)
            {
                string status = book.IsAvailable ? "Rafta" : "Ödünçte";
                Console.WriteLine($"ID: {book.Id} | {book.Title} - {book.Author} [{status}]");
            }
        }

        public List<Book> GetAllBooks()
        {
            return _context.Books.Where(b => !b.IsDeleted).ToList();
        }

        public void DeleteBook(int id)
        {

            var book = _context.Books.Find(id);
            if (book != null)
            {
                book.IsDeleted = true;
                _context.SaveChanges();
                Console.WriteLine("[Sistem]: Kitap Arşivlendi.");
            }
            else
            {
                Console.WriteLine("Hata: Kitap bulunamadı.");
            }
        }

        public void AddMember(string firstname, string lastname, string phone)
        {
            var Member = new Member
            {
                FirstName = firstname,
                LastName = lastname,
                PhoneNumber = phone
            };

            _context.Members.Add(Member);
            _context.SaveChanges();
            Console.WriteLine("\n[Sistem]: Üye kaydı tamamlandı.");

            Console.WriteLine($"Başarılı: '{firstname} {lastname}' SQL Server'a kaydedildi.");
        }
        public void ListMembers()
        {
            var members = _context.Members.ToList();

            Console.WriteLine("\n--- KAYITLI ÜYELER ---");
            if (!members.Any())
            {
                Console.WriteLine("Kayıtlı üye bulunmamaktadır.");
                return;
            }
            foreach (var member in members)
            {
                Console.WriteLine($"ID: {member.Id} | Ad Soyad:{member.FirstName} {member.LastName} | Telefon: {member.PhoneNumber}");
            }
        }

        public void DeleteMember(int memberId)
        {

            var member = _context.Members.FirstOrDefault(m => m.Id == memberId && !m.IsDeleted);

            if (member != null)
            {

                member.IsDeleted = true;
                _context.SaveChanges();
                Console.WriteLine($"\n[Business] {member.FirstName} {member.LastName} sistemde pasife çekildi.");
            }
            else
            {
                Console.WriteLine("\n[Business] Hata: Üye bulunamadı veya zaten silinmiş.");
            }
        }
        public void IssueBook(int bookId, int memberId)
        {
            var book = _context.Books.Find(bookId);
            if (book != null && book.IsAvailable)
            {
                book.IsAvailable = false;

                var loan = new Loan
                {
                    BookId = bookId,
                    MemberId = memberId,

                };

                _context.Loans.Add(loan);
                _context.SaveChanges();
                Console.WriteLine($"{book.Title} ödünç verildi.");
            }
            else
            {
                Console.WriteLine("Hata: Kitap bulunamadı veya zaten ödünç verilmiş.");
            }


        }
        public void ReturnBook(int bookId)
        {

            var book = _context.Books.Find(bookId);
            if (book == null)
            {
                Console.WriteLine("\n[Hata]: Bu ID ile kayıtlı bir kitap bulunamadı.");
                return;
            }


            var loan = _context.Loans
                .FirstOrDefault(l => l.BookId == bookId && l.ReturnDate == null);

            if (loan == null)
            {
                Console.WriteLine("\n[Hata]: Bu kitap zaten kütüphanede görünüyor veya ödünç kaydı bulunamadı.");
                return;
            }

            loan.ReturnDate = DateTime.Now;
            book.IsAvailable = true;

            _context.SaveChanges();
            Console.WriteLine($"\n[Başarılı]: '{book.Title}' iade alındı ve rafa eklendi.");
        }

        public void ListOverdueBooks()
        {
            var today = DateTime.Now;
            var overdueLoans = _context.Loans
                .Include(l => l.Book)
                .Include(l => l.Member)
                .Where(l => l.ReturnDate == null && l.DueDate < today)
                .ToList();

            Console.WriteLine("\n--- SÜRESİ GEÇEN KİTAPLAR ---");
            foreach (var l in overdueLoans)
            {
                var gecikmeGunu = (today - l.DueDate).Days;
                Console.WriteLine($"Kitap: {l.Book.Title} | Üye: {l.Member.FirstName} | {gecikmeGunu} gün gecikti!");
            }
        }

        public void ListLoans()
        {

            var loans = _context.Loans
                .Include(l => l.Book)
                .Include(l => l.Member)
                .ToList();

            Console.WriteLine("\n--- ÖDÜNÇ TAKİP LİSTESİ ---");
            foreach (var l in loans)
            {
                string durum = l.ReturnDate == null ? "Teslim Edilmedi" : $"İade Edildi ({l.ReturnDate})";
                Console.WriteLine($"Kitap: {l.Book.Title} | Üye: {l.Member.FirstName} {l.Member.LastName} | Tarih: {l.LoanDate} | Durum: {durum}");
            }
        }

        internal void DeleteMember(object context)
        {
            throw new NotImplementedException();
        }
    }
}