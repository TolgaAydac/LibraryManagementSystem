import pyodbc
import requests
from datetime import datetime

conn_str = (
    r'Driver={ODBC Driver 17 for SQL Server};'
    r'Server=DESKTOP-CMIPSPQ\SQLEXPRESS;'
    r'Database=LibraryManagementDb;'
    r'Trusted_Connection=yes;'
    r'TrustServerCertificate=yes;'
)

def inject_members():
    try:
        print("\n--- ADIM 1: JSONPlaceholder Üzerinden Veri Çekiliyor ---")
      
        res = requests.get("https://jsonplaceholder.typicode.com/users", timeout=10)
        users = res.json() 
        if not users:
            print("❌ HATA: Bu API de boş döndü, internet bağlantını kontrol et.")
            return

        print(f"✅ {len(users)} adet kullanıcı başarıyla alındı.")

        conn = pyodbc.connect(conn_str, autocommit=True)
        cursor = conn.cursor()

        print("\n--- ADIM 2: Yazma İşlemi ---")
        for user in users:

            first_name = user['name'].split()[0]
            last_name = user['name'].split()[-1]
            phone = user['phone']
            join_date = datetime.now().strftime('%Y-%m-%d %H:%M:%S')

            cursor.execute("""
                INSERT INTO Members (FirstName, LastName, PhoneNumber, JoinDate, IsDeleted) 
                VALUES (?, ?, ?, ?, 0)
            """, (first_name, last_name, phone, join_date))
            
            print(f"👤 Kaydedildi: {first_name} {last_name}")

        print("\n🚀 İŞLEM TAMAMLANDI!")
        conn.close()

    except Exception as e:
        print(f"\n❌ KRİTİK HATA: {e}")

if __name__ == "__main__":
    inject_members()