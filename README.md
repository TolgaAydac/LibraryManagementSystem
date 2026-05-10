# 🏛️ Library Management System API

**Clean Architecture** prensiplerine uygun olarak geliştirilmiş kütüphane yönetim sistemi.

---

## 🚀 Öne Çıkan Özellikler

- **Katmanlı Mimari (Clean Architecture):** Domain, Application ve Infrastructure katmanları arasında tam izolasyon sağlayan, Dependency Inversion (Interface-based) prensibiyle yapılandırılmış modüler mimari.
- **Unit Testing & Mocking:** xUnit ve Moq kütüphaneleri kullanılarak; iş mantığı, sayfalama ve veritabanı etkileşimlerini (DbSet Mocking) %100 doğrulukla denetleyen test altyapısı.
- **Observability & Logging:** Serilog entegrasyonu ile yapılandırılmış; Console ve Rolling File (günlük rotasyonlu) tabanlı profesyonel loglama sistemi.
- **Advanced Pagination & Metadata:** Büyük veri setleri için optimize edilmiş; `TotalCount`, `PageSize` ve `TotalPages` bilgilerini dönen profesyonel sayfalama mantığı.
- **Health Monitoring:** `/health` endpoint'i üzerinden uygulama ve veritabanı sağlığını anlık izleme imkanı.
- **Akıllı İş Mantığı:** Soft-delete mekanizması, borçlu üyelerin kitap alımını engelleyen kontrol sistemleri ve regex tabanlı veri doğrulama.
- **Migration Orchestration:** Uygulama ayağa kalkarken veritabanı şemasını otomatik olarak kontrol eden ve güncelleyen senkronizasyon yapısı.

---

## 🛠️ Teknolojiler

- **Backend:** .NET Web API
- **Veritabanı:** SQL Server (Entity Framework Core)
- **Logging:** Serilog
- **Testing:** xUnit, Moq
- **Dokümantasyon:** Swagger UI & Postman
- **Otomasyon:** Python (Data Injection)

---

## 📋 Proje Yapısı

- **LibraryProject.Domain:** Temel varlıklar (Entities), Value Object'ler ve kontratlar.
- **LibraryProject.Application:** İş mantığı (Manager), DTO'lar, Interface'ler ve sayfalama parametreleri.
- **LibraryProject.Infrastructure:** Veritabanı konfigürasyonları (Data), DbContext ve Migration dosyaları.
- **LibraryProject.WebAPI:** Endpoint tanımlamaları, Controller sınıfları, Middleware ve konfigürasyonlar.
- **LibraryProject.Tests:** Birim testlerin (Unit Tests) bulunduğu laboratuvar katmanı.
- **Scripts:** `data_injector.py` ve `member_injector.py` ile otomatik test verisi üretimi.

---

## ⚙️ Kurulum ve Çalıştırma

1. Projeyi klonlayın: `git clone https://github.com/TolgaAydac/LibraryManagementSystem.git`
2. `appsettings.json` dosyasındaki ConnectionString'i kendi yerel SQL Server bilgilerinizle güncelleyin.
3. Terminalde şu komutu çalıştırın (Migrationlar otomatik çalışacaktır): `dotnet run --project LibraryProject.WebAPI`
4. Tablolar oluştuktan sonra test verilerini yüklemek için scriptleri çalıştırın:`cd Scripts` `python data_injector.py` `python member_injector.py`
5. Sistemdeki iş mantığını doğrulamak için: `dotnet test`
