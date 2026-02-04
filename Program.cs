using System;
using System.Buffers;
using LibraryProject.Business;

namespace LibraryProject
{
    class Program
    {
        static void Main(string[] args)
        {
            LibraryManager manager = new LibraryManager();
            bool isRunning = true;

            Console.WriteLine("=== PROFESYONEL KÜTÜPHANE SİSTEMİ v2.0 ===");

            while (isRunning)
            {
                Console.WriteLine("\n--- KİTAP İŞLEMLERİ ---");
                Console.WriteLine("1- Kitap Ekle");
                Console.WriteLine("2- Kitapları Listele");
                Console.WriteLine("3- Kitap Sil");

                Console.WriteLine("");

                Console.WriteLine("\n--- ÜYE İŞLEMLERİ ---");
                Console.WriteLine("4- Yeni Üye Kaydı");
                Console.WriteLine("5- Üyeleri Listele");

                Console.WriteLine("\n--- ÖDÜNÇ SİSTEMİ ---");
                Console.WriteLine("6- Kitap Ödünç Ver");
                Console.WriteLine("7- Kitabı İade Al");
                Console.WriteLine("8- Ödünç Takip Listesi");

                Console.WriteLine("");
                Console.Write("Seçiminiz: ");

                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        Console.Write("Kitap Adı: ");
                        string title = Console.ReadLine() ?? "";
                        Console.Write("Yazar: ");
                        string author = Console.ReadLine() ?? "";
                        Console.Write("Basım Yılı: ");
                        int year = int.Parse(Console.ReadLine() ?? "0");

                        manager.AddBook(title, author, year);
                        break;

                    case "2":
                        manager.ListBooks();
                        break;

                    case "3":
                        Console.Write("Silmek istediğiniz kitabın ID numarasını giriniz: ");
                        if (int.TryParse(Console.ReadLine(), out int deleteId))
                        {
                            manager.DeleteBook(deleteId);
                        }
                        else
                        {
                            Console.WriteLine("Geçersiz ID numarası.");
                        }
                        break;

                    case "4":
                        Console.Write("Üye Adı: ");
                        string memberName = Console.ReadLine() ?? "";
                        Console.Write("Üye Soyadı: ");
                        string memberSurname = Console.ReadLine() ?? "";
                        Console.Write("Telefon: ");
                        string memberPhone = Console.ReadLine() ?? "";
                        manager.AddMember(memberName, memberSurname, memberPhone);
                        break;
                    case "5":
                        manager.ListMembers();
                        break;

                    case "6":
                        manager.ListBooks();
                        Console.Write("Ödünç Verilecek Kitap ID'si: ");
                        int BookId = int.Parse(Console.ReadLine() ?? "0");

                        manager.ListMembers();
                        Console.Write("Ödünç Alacak Üye ID'si: ");
                        int MemberId = int.Parse(Console.ReadLine() ?? "0");

                        manager.IssueBook(BookId, MemberId);
                        break;

                    case "7":
                        Console.Write("İade edilecek Kitap ID: ");
                        int returnBookId = int.Parse(Console.ReadLine() ?? "0");
                        manager.ReturnBook(returnBookId);
                        break;

                    case "8":
                        manager.ListLoans();
                        break;

                    case "0":
                        isRunning = false;
                        Console.WriteLine("Sistemden çıkılıyor... İyi günler!");
                        break;

                    default:
                        Console.WriteLine("Geçersiz seçim, tekrar deneyin.");
                        break;
                }
            }
        }
    }
}