# 🏛️ Library Management System API

Modern ve ölçeklenebilir bir kütüphane yönetim sistemi.

## 🚀 Öne Çıkan Özellikler

- **Katmanlı Mimari:** Modern Clean Architecture prensipleriyle yapılandırılmış; Domain, Application ve Infrastructure katmanları arasında Dependency Injection ile izolasyon sağlayan, modüler ve yüksek ölçeklenebilir mimari.
- **Gelişmiş Raporlama:** Teslim tarihi geçen kitaplar ve kütüphane genel istatistikleri için optimize edilmiş özel endpointler.
- **Akıllı İş Mantığı:** Soft-delete mekanizması, borçlu üyelerin kitap alımını engelleyen kontrol sistemleri.
- **Veri Otomasyonu (Python Seeding):** Test verilerini(Kitaplar, Yazarlar, Üyeler) SQL Server'a saniyeler içinde enjekte eden özel Python scriptleri.

## 🛠️ Teknolojiler

- **Backend:** .NET Web API
- **Veritabanı:** SQL Server (Entity Framework Core)
- **Dokümantasyon:** Swagger UI & Postman
- **Otomasyon:** Python

## 📋 Proje Yapısı

- **Business:** İş mantığı ve Manager sınıflarının bulunduğu katman.
- **Data:** DbContext ve Veritabanı konfigürasyonları.
- **Controllers:** API endpoint tanımlamaları.
- **Python Scripts:** `data_injector.py` ve `member_injector.py` ile otomatik veri üretimi.

## ⚙️ Kurulum ve Çalıştırma

1. Projeyi klonlayın: `git clone https://github.com/TolgaAydac/LibraryManagementSystem.git`
2. `appsettings.json` dosyasındaki ConnectionString'i kendi yerel SQL Server bilgilerinizle güncelleyin.
3. Terminalde şu komutları çalıştırın:
   ```bash
   dotnet ef database update
   dotnet run
   ```
