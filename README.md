**🏛️ Library Management System API**

Modern, test odaklı ve Clean Architecture prensiplerine uygun olarak geliştirilmiş bir kütüphane yönetim sistemi.

**🚀 Öne Çıkan Özellikler**

**Katmanlı Mimari (Clean Architecture):** Domain, Application ve Infrastructure katmanları arasında tam izolasyon sağlayan, Dependency Inversion (Interface-based) prensibiyle yapılandırılmış modüler mimari.

**Unit Testing & Mocking:** xUnit ve Moq kütüphaneleri kullanılarak, iş mantığını ve veritabanı etkileşimlerini (DbSet Mocking) doğrulayan test.

**Observability & Logging:** Serilog entegrasyonu ile yapılandırılmış; Console ve Rolling File (günlük rotasyonlu) tabanlı loglama altyapısı.

**Advanced Pagination:** Client-side performansını optimize eden, TotalCount, PageSize ve TotalPages meta-verilerini içeren sayfalama sistemi.

**Health Monitoring:** /health endpoint'i üzerinden veritabanı ve uygulama servis sağlığını anlık izleme.

**Akıllı İş Mantığı:** Soft-delete mekanizması ve regex tabanlı veri doğrulama.

**Migration Orchestration:** Veritabanı ve tablo yapısının uygulama ayağa kalkarken otomatik olarak oluşturulması ve senkronize edilmesi.

**🛠️ Teknolojiler**

Backend: .NET Web API

Veritabanı: SQL Server (Entity Framework Core)

Testing: xUnit, Moq

Dokümantasyon: Swagger UI & Postman

Otomasyon: Python

**📋 Proje Yapısı**

LibraryProject.Domain: Temel varlıklar (Entities) ve kontratlar.

LibraryProject.Application: İş mantığı (Manager), DTO'lar, Interface'ler ve Pagination logic.

LibraryProject.Infrastructure: Data access katmanı, DbContext ve Migration süreçleri.

LibraryProject.WebAPI: Endpoint tanımlamaları, Middleware ve Program.cs konfigürasyonları.

LibraryProject.Tests: Birim testlerin bulunduğu katman.

Scripts: data_injector.py ve member_injector.py ile otomatik veri üretimi.

**⚙️ Kurulum ve Çalıştırma**

Projeyi klonlayın: git clone https://github.com/TolgaAydac/LibraryManagementSystem.git

appsettings.json dosyasındaki ConnectionString'i yerel SQL Server bilgilerinizle güncelleyin.

Proje ana dizininde uygulamayı başlatın (Migrationlar otomatik çalışacaktır):

Bash
dotnet run --project LibraryProject.WebAPI
Veri otomasyonu için Python scriptlerini çalıştırın:

Bash
cd Scripts
python data_injector.py
🧪 Testlerin Çalıştırılması
Sistemdeki iş mantığının doğruluğunu teyit etmek için şu komutu kullanın:

Bash
dotnet test
