import pyodbc
import requests

conn_str = (
    r'Driver={ODBC Driver 17 for SQL Server};'
    r'Server=DESKTOP-CMIPSPQ\SQLEXPRESS;'
    r'Database=LibraryManagementDb;'
    r'Trusted_Connection=yes;'
    r'TrustServerCertificate=yes;'
)

def start_injection():
    try:
        print("\n--- ADIM 1: Open Library API Veri Çekme ---")
        url = "https://openlibrary.org/subjects/programming.json?limit=10"
        res = requests.get(url)
        
        if res.status_code != 200:
            print(f"❌ API Hatası! Kod: {res.status_code}")
            return

        data = res.json()
        works = data.get('works', [])
        
        if not works:
            print("❌ Veri bulunamadı. Alternatif veriler yükleniyor...")
            works = [
                {'title': 'Clean Code', 'authors': [{'name': 'Robert C. Martin'}]},
                {'title': 'The Pragmatic Programmer', 'authors': [{'name': 'Andrew Hunt'}]}
            ]
        
        print(f"✅ BAŞARILI: {len(works)} adet kitap hazır.")
        print("\n--- ADIM 2: SQL İşlemleri ---")
        conn = pyodbc.connect(conn_str, autocommit=True)
        cursor = conn.cursor()

        for book in works:
            title = book.get('title', 'Bilinmeyen Kitap')[0:150]
            authors_list = book.get('authors', [])
            author = authors_list[0].get('name', 'Anonim') if authors_list else "Anonim"

            year = book.get('first_publish_year', 2024)
            
            cursor.execute("""
                INSERT INTO Books (Title, Author, IsAvailable, IsDeleted, publishYear) 
                VALUES (?, ?, 1, 0, ?)
            """, (title, author, year))
            print(f"🚀 SQL'e Fırlatıldı: {title} - {year}")
            
        cursor.execute("SELECT COUNT(*) FROM Books")
        toplam = cursor.fetchone()[0]
        print(f"\n✅ İŞLEM TAMAM: Şu an veritabanında toplam {toplam} kitap var!")
        
        conn.close()

    except Exception as e:
        print(f"\n❌ BİR HATA OLUŞTU: {e}")

if __name__ == "__main__":
    start_injection()