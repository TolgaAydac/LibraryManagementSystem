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
        conn = pyodbc.connect(conn_str, autocommit=True)
        cursor = conn.cursor()

        print("--- ADIM 1: Kategoriler Hazırlanıyor ---")

        search_queries = {
            "Yazılım": "programming",
            "Bilim Kurgu": "science_fiction",
            "Psikoloji": "psychology",
            "Tarih": "history",
            "Felsefe": "philosophy",
            "Polisiye": "mystery",
            "Klasik Edebiyat": "classic_literature",
            "Ekonomi": "finance"
        }
        
        category_map = {}

        for cat_name in search_queries.keys():
            cursor.execute("SELECT Id FROM Categories WHERE Name = ?", (cat_name,))
            row = cursor.fetchone()
            if row:
                category_map[cat_name] = row[0]
            else:
                cursor.execute("INSERT INTO Categories (Name) VALUES (?)", (cat_name,))
                cursor.execute("SELECT @@IDENTITY") 
                category_map[cat_name] = cursor.fetchone()[0]
        
        print(f"✅ {len(category_map)} Kategori hazırlandı.")

        print("\n--- ADIM 2: Kitaplar Raflara Diziliyor ---")

        for cat_name, api_subject in search_queries.items():
            print(f"\n📂 {cat_name} kategorisi çekiliyor...")
            
            url = f"https://openlibrary.org/subjects/{api_subject}.json?limit=10"
            res = requests.get(url)
            works = res.json().get('works', [])

            for work in works:
                title = work.get('title', 'Bilinmeyen')[0:150]
                author = work.get('authors', [{}])[0].get('name', 'Anonim')[0:100]
                year = work.get('first_publish_year', 2024)

                cursor.execute("""
                    INSERT INTO Books (Title, Author, IsAvailable, IsDeleted, PublishYear, CategoryId) 
                    VALUES (?, ?, 1, 0, ?, ?)
                """, (title, author, year, category_map[cat_name]))
                print(f"📚 {cat_name} -> {title}")

        conn.close()
        print("\n🚀 İŞLEM TAMAM! Kütüphane raflara göre tıkır tıkır dolduruldu.")

    except Exception as e:
        print(f"\n❌ HATA: {e}")

if __name__ == "__main__":
    start_injection()