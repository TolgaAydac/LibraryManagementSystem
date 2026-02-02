using LibraryProject.Models;
using LibraryProject.Data;
using System;
using System.Linq;

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

            var books = _context.Books.ToList();

            Console.WriteLine("\n--- SQL SERVER KİTAP LİSTESİ ---");
            foreach (var book in books)
            {
                string status = book.IsAvailable ? "Rafta" : "Ödünçte";
                Console.WriteLine($"ID: {book.Id} | {book.Title} - {book.Author} [{status}]");
            }
        }

        public void DeleteBook(int id)
        {

            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
                Console.WriteLine("Kitap SQL'den tamamen silindi.");
            }
            else
            {
                Console.WriteLine("Hata: Kitap bulunamadı.");
            }
        }
    }
}