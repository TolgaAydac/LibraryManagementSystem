// using System;
// using System.Buffers;
// using LibraryProject.Business;
// using Microsoft.AspNetCore.OpenApi;

// namespace LibraryProject
// {
//     class Program
//     {
//         static void Main(string[] args)
//         {
//             LibraryManager manager = new LibraryManager();
//             bool isRunning = true;

//             Console.WriteLine("=== PROFESYONEL KÜTÜPHANE SİSTEMİ v2.0 ===");

//             while (isRunning)
//             {
//                 Console.WriteLine("\n--- KİTAP İŞLEMLERİ ---");
//                 Console.WriteLine("1- Kitap Ekle");
//                 Console.WriteLine("2- Kitapları Listele");
//                 Console.WriteLine("3- Kitap Sil");

//                 Console.WriteLine("\n--- ÜYE İŞLEMLERİ ---");
//                 Console.WriteLine("4- Yeni Üye Kaydı");
//                 Console.WriteLine("5- Üye Sil");
//                 Console.WriteLine("6- Üyeleri Listele");

//                 Console.WriteLine("\n--- ÖDÜNÇ SİSTEMİ ---");
//                 Console.WriteLine("7- Kitap Ödünç Ver");
//                 Console.WriteLine("8- Kitabı İade Al");
//                 Console.WriteLine("9- Ödünç Takip Listesi");
//                 Console.WriteLine("0- Çıkış");

//                 Console.Write("\nSeçiminiz: ");
//                 string choice = Console.ReadLine() ?? "";

//                 switch (choice)
//                 {
//                     // --- KİTAPLAR ---
//                     case "1":
//                         Console.Write("Kitap Adı: ");
//                         string title = Console.ReadLine() ?? "";
//                         Console.Write("Yazar: ");
//                         string author = Console.ReadLine() ?? "";
//                         Console.Write("Basım Yılı: ");
//                         int year = int.Parse(Console.ReadLine() ?? "0");
//                         manager.AddBook(title, author, year);
//                         break;

//                     case "2":
//                         manager.ListBooks();
//                         break;

//                     case "3":
//                         manager.ListBooks(); // Önce listele ki ID görsün
//                         Console.Write("Silinecek Kitap ID: ");
//                         if (int.TryParse(Console.ReadLine(), out int deleteId))
//                             manager.DeleteBook(deleteId);
//                         break;

//                     // --- ÜYELER ---
//                     case "4":
//                         Console.Write("Üye Adı: ");
//                         string memberName = Console.ReadLine() ?? "";
//                         Console.Write("Üye Soyadı: ");
//                         string memberSurname = Console.ReadLine() ?? "";
//                         Console.Write("Telefon: ");
//                         string memberPhone = Console.ReadLine() ?? "";
//                         manager.AddMember(memberName, memberSurname, memberPhone);
//                         break;

//                     case "5":
//                         manager.ListMembers(); // Önce listele ki kimi sildiğini bilsin
//                         Console.Write("Silinecek Üye ID: ");
//                         if (int.TryParse(Console.ReadLine(), out int delMemId))
//                             manager.DeleteMember(delMemId);
//                         break;

//                     case "6":
//                         manager.ListMembers();
//                         break;

//                     // --- ÖDÜNÇ SİSTEMİ ---
//                     case "7":
//                         manager.ListBooks();
//                         Console.Write("Ödünç Verilecek Kitap ID: ");
//                         int bId = int.Parse(Console.ReadLine() ?? "0");
//                         manager.ListMembers();
//                         Console.Write("Ödünç Alacak Üye ID: ");
//                         int mId = int.Parse(Console.ReadLine() ?? "0");
//                         manager.IssueBook(bId, mId);
//                         break;

//                     case "8":
//                         Console.Write("İade edilecek Kitap ID: ");
//                         int returnBookId = int.Parse(Console.ReadLine() ?? "0");
//                         manager.ReturnBook(returnBookId);
//                         break;

//                     case "9":
//                         manager.ListLoans();
//                         break;

//                     case "0":
//                         isRunning = false;
//                         break;

//                     default:
//                         Console.WriteLine("❌ Geçersiz seçim!");
//                         break;
//                 }
//             }
//         }
//     }
// }

using LibraryProject.Business;
using LibraryProject.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<LibraryDbContext>();
builder.Services.AddScoped<LibraryManager>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Kütüphane API v1");
    c.RoutePrefix = "swagger";
});

app.MapControllers();

Console.WriteLine("🚀 API Ayaklandı: http://localhost:5000/swagger");
app.Run("http://localhost:5000");