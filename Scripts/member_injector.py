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
        print("\n--- ADIM 1: Rastgele Üye Verileri Çekiliyor ---")
        res = requests.get("https://randomuser.me/api/?results=10&nat=tr")
        users = res.json().get('results', [])

        conn = pyodbc.connect(conn_str, autocommit=True)
        cursor = conn.cursor()
        print("✅ SQL Bağlantısı Başarılı.")

        print("\n--- ADIM 2: Üyeler SQL'e Yazılıyor ---")
        for user in users:
            first_name = user['name']['first']
            last_name = user['name']['last']
            phone = user['phone']
            join_date = datetime.now().strftime('%Y-%m-%d %H:%M:%S')

            cursor.execute("""
                INSERT INTO Members (FirstName, LastName, PhoneNumber, JoinDate) 
                VALUES (?, ?, ?, ?)
            """, (first_name, last_name, phone, join_date))
            
            print(f"👤 Kaydedildi: {first_name} {last_name}")

        print("\n🚀 TÜM ÜYELER BAŞARIYLA EKLENDİ!")
        conn.close()

    except Exception as e:
        print(f"\n❌ HATA OLUŞTU: {e}")

if __name__ == "__main__":
    inject_members()