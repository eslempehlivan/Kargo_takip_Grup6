##  2. Hafta: Kargo Takip Sistemi
Bu haftanın amacı, kullanıcıların takip numarası ile kargo bilgilerini sorgulayabileceği temel işlevselliği tamamlamaktır. Backend tarafında sorgulama API'si geliştirilirken, frontend tarafında kullanıcı arayüzü ve entegrasyon sağlanacaktır. Hafta sonunda kullanıcı takip numarası girip kargo durumunu görebilmelidir.

| No | Teslim Edilecekler |
|----|---------------------|
| 1 | Kargo sorgulama ekranı |
| 2 | Takip numarası ile veri çekme |
| 3 | Sonuç ekranı (durum, gönderici/alıcı) |
| 4 | Hata mesajları |

---

###  Backend Görevleri 

| Görev | Detay |
|-------|-------|
| Kargo sorgulama API'si | GET /cargo/:trackingNumber endpoint'i oluşturma |
| Takip numarası ile veri çekme | Veritabanından takip numarasına göre kargo bilgisi sorgulama |
| API yanıt formatı | Kargo durumu, gönderici, alıcı, konum bilgilerini döndürme |
| Geçersiz takip numarası yönetimi | Bulunamayan kargo için 404 hatası ve mesaj döndürme |
| Veritabanı sorgu optimizasyonu | Takip numarası indeksleme (performans için) |


---

###  Frontend Görevleri

| Görev | Detay  |
|-------|-------|
| Kargo sorgulama ekranı | Takip numarası giriş formu (input + buton)  |
| Form validasyonu | Takip numarası boş olamaz, minimum karakter kontrolü  |
| API entegrasyonu | Backend'deki /cargo/:trackingNumber endpoint'ine istek atma  |
| Loading state | Veri çekilirken yükleniyor animasyonu  |
| Sonuç ekranı | Kargo durumu, gönderici, alıcı bilgilerini gösterme  |
| Hata mesajları | Geçersiz takip no veya API hatası durumunda kullanıcıya gösterim  |
| Sayfa tasarımı | Sorgulama ve sonuç ekranının responsive tasarımı  |


---

###  Proje Yönetimi 

| Görev | Açıklama |
|-------|----------|
| API dokümantasyonu | Kargo sorgulama endpoint'inin dokümantasyonunu hazırlama |
| Test senaryoları | Geçerli/geçersiz takip numarası test senaryoları oluşturma |
| Haftalık durum toplantısı | 2. hafta hedeflerinin değerlendirilmesi |
| Varsa engellerin kaldırılması | Backend-frontend entegrasyon sorunlarının çözümü |

---

###  Backend-Frontend Entegrasyonu

| Adım | Açıklama  |
|------|----------|
| 1 | Backend API'sinin çalıştığından emin ol |
| 2 | Frontend'den API'ye istek atma kodu yaz  |
| 3 | Gelen veriyi ekranda gösterme  |
| 4 | Hata durumlarını yakalama ve gösterim  |
| 5 | Tüm senaryoları test etme (Herkes) |

---

###  2. Hafta Kontrol Listesi

| Alan | Yapılacaklar  |
|------|--------------|
| Backend | Kargo sorgulama API'si oluşturuldu  |
| Backend | Takip numarası ile veri çekme çalışıyor  |
| Backend | Geçersiz takip no hatası yönetiliyor  |
| Frontend | Kargo sorgulama ekranı tamamlandı |
| Frontend | Takip numarası ile veri çekiliyor  |
| Frontend | Sonuç ekranı bilgileri gösteriyor |
| Frontend | Hata mesajları gösteriliyor |
| Frontend | Loading state çalışıyor  |
| Entegrasyon | Backend-Frontend bağlantısı çalışıyor  |
| Test | Tüm senaryolar test edildi  |
