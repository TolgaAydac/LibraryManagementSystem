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

            Console.WriteLine("=== PROFESYONEL KÜTÜPHANE SİSTEMİ v1.0 ===");

            while (isRunning)
            {
                Console.WriteLine("\n--- MENÜ ---");
                Console.WriteLine("1- Kitap Ekle");
                Console.WriteLine("2- Kitapları Listele");
                Console.WriteLine("3- Kitap Sil");
                Console.WriteLine("0- Çıkış");
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