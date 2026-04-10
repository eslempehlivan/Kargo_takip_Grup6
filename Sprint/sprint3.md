## 3. Hafta: Admin ve Geçmiş Sistemi
Bu haftanın amacı, sisteme yönetici yetkilerini ve kargo hareket geçmişi özelliğini kazandırmaktır. Admin kullanıcıları giriş yapabilecek, kargoların durumunu güncelleyebilecek ve tüm hareket geçmişini görebilecektir. Normal kullanıcılar ise sadece kendi kargolarının geçmişini görüntüleyebilecektir. Hafta sonunda yetkilendirme sistemi tamamen çalışır durumda olmalıdır.

| No | Teslim Edilecekler |
|----|---------------------|
| 1 | Admin giriş ekranı |
| 2 | Yetkilendirme sistemi |
| 3 | Kargo geçmişi  |
| 4 | Kargo durum güncelleme |

---

###  Backend Görevleri 

| Görev | Detay |
|-------|-------|
| Admin giriş API'si | POST /auth/login endpoint'i (email + şifre kontrolü) |
| JWT token oluşturma | Başarılı girişte token üretme ve döndürme |
| Yetkilendirme middleware'i | Admin yetkisi olmayan kullanıcıları engelleme |
| Kargo geçmişi API'si | GET /cargo/:id/history (kargonun tüm hareketlerini getirme) |
| Kargo durum güncelleme API'si | PUT /cargo/:id/status (sadece admin yapabilir) |
| Şifre güvenliği | Şifrelerin hash'lenerek veritabanında saklanması (bcrypt) |


---

###  Frontend Görevleri 

| Görev | Detay |
|-------|-------|
| Admin giriş ekranı | Email + şifre formu, giriş butonu  |
| Giriş validasyonu | Email formatı, şifre boş olamaz kontrolü  |
| Token yönetimi | Gelen token'ı localStorage'a kaydetme  |
| Route koruma | Giriş yapmayan kullanıcıyı admin sayfalarına engelleme  |
| Kargo geçmişi ekranı | Timeline veya tablo şeklinde geçmiş gösterme |
| Kargo durum güncelleme | Dropdown/buton ile durum değiştirme arayüzü  |
| Admin panel tasarımı | Tüm admin sayfalarının şık ve kullanıcı dostu tasarımı  |
| Çıkış yap butonu | Token'ı silip ana sayfaya yönlendirme  |


---
##  Proje Yönetimi 
 Görev | Açıklama |
|-------|----------|
| Haftalık toplantı planı | durum değerlendirmesi |
| Görev takip tablosu | GitHub görev takibi |
| Haftalık durum toplantısı | Hafta hedeflerinin değerlendirilmesi |


###  Yetkilendirme Akışı 

| Adım | Yapılacak İşlem  |
|------|-----------------|
| 1 | Frontend'den giriş bilgilerini backend'e gönderme |
| 2 | Backend'de kullanıcı doğrulama ve token oluşturma  |
| 3 | Token'ı frontend'de kaydetme  |
| 4 | Her admin isteğinde token'ı header'a ekleme |
| 5 | Backend'de token doğrulama ve admin yetkisi kontrolü  |
| 6 | Yetkisiz erişimde hata mesajı gösterme |

### 3. Hafta Kontrol Listesi

| Alan | Yapılacaklar |
|------|--------------|
| Backend | Admin giriş API'si oluşturuldu  |
| Backend | JWT token sistemi çalışıyor  |
| Backend | Yetkilendirme middleware'i hazır  |
| Backend | Kargo geçmişi API'si çalışıyor  |
| Backend | Kargo durum güncelleme API'si çalışıyor  |
| Frontend | Admin giriş ekranı tamamlandı  |
| Frontend | Token yönetimi çalışıyor |
| Frontend | Route koruma aktif  |
| Frontend | Kargo geçmişi timeline gösterimi hazır  |
| Frontend | Kargo durum güncelleme arayüzü hazır  |
| Yetkilendirme | Admin - Normal kullanıcı ayrımı çalışıyor  |
| Test | Tüm yetki senaryoları test edildi |

