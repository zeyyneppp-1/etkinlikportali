# 📌 BST 206 - Etkinlik Yönetim Portalı Projesi

Bu proje, **Sakarya Üniversitesi Bilgi Sistemleri ve Teknolojileri Bölümü** bünyesinde yürütülen **BST 206 - Web Tasarımı ve Programlama II** dersi nihai proje ödevi kapsamında geliştirilmiştir. Uygulama, modern web standartlarına uygun, katmanlı mimari prensiplerini benimseyen ve kullanıcıların şehirdeki dinamik etkinlikleri takip edip yönetebilmelerini sağlayan bir **Etkinlik Yönetim Portalı**'dır.

---

## 👤 Öğrenci Bilgileri
* **Adı Soyadı:** Zeynep Çiftçi
* **Bölümü:** Bilgi Sistemleri ve Teknolojileri (2. Sınıf)
* **Üniversite:** Sakarya Üniversitesi
* **Öğrenci Numarası: B241204017

---

## 🛠️ Kullanılan Teknolojiler ve Mimari Yapı

Proje geliştirme sürecinde endüstri standartlarında modern mimari yaklaşımlar ve kütüphaneler tercih edilmiştir:

* **Platform / Framework:** ASP.NET Core MVC (Güncel LTS Sürümü)
* **Veritabanı ve ORM:** Entity Framework Core (EF Core) ile **Code-First** yaklaşımı
* **Güvenlik ve Yetkilendirme:** ASP.NET Core Identity (Authentication & Role-Based Authorization)
* **Tasarım / Arayüz:** Bootstrap v5, HTML5, CSS3, JavaScript (Responsive & Mobil Uyumlu)
* **Sürüm Kontrolü:** Git & GitHub (Klasör bazlı, düzenli ve açıklayıcı commit geçmişi)

### 📐 Mimari Tasarım Desenleri (Architecture & Patterns)
Projede kod karmaşasını önlemek, sürdürülebilirliği artırmak ve gevşek bağlı (*loosely coupled*) bir yapı kurmak adına **Repository Pattern** uygulanmıştır:
1. **Models:** Veritabanı tablolarına karşılık gelen nesnel veri modelleri ve aralarındaki bire çok/çoğa çok ilişkiler kurgulanmıştır.
2. **Interface (Soyutlama):** İş kurallarını ve veritabanı operasyon yeteneklerini tanımlayan `IEventRepository` yapısı kurulmuştur.
3. **Repositories (Somutlama):** Arayüzleri uygulayarak asenkron/senkron veri tabanı sorgularını (Ekleme, Listeleme, Filtreleme vb.) çalıştıran somut sınıflar yazılmıştır.
4. **ViewModels:** Güvenlik ve performans optimizasyonu için View katmanına doğrudan ham veri tabanı modelleri gönderilmemiş, yalnızca ilgili sayfanın ihtiyaç duyduğu verileri içeren özel View Model nesneleri (`EventViewModel`) tasarlanmıştır.
5. **Dependency Injection (DI):** Yazılan tüm servisler ve repository katmanları, `Program.cs` üzerinde Constructor Injection (Yapıcı Metot Enjeksiyonu) yöntemiyle sisteme dahil edilmiştir.

---

## 🌟 Öne Çıkan Özellikler ve İşlevsellikler

* **Dinamik Veri Tabanı Başlatma (Seeding Data):** Proje ilk kez ayağa kalktığında eğer veri tabanında hiçbir kategori bulunmuyorsa, sistem otomatik olarak "Eğitim", "Gezi", "Workshop" ve "Tiyatro" gibi varsayılan kategorileri kendisi oluşturur ve veri bütünlüğünü korur.
* **Arama ve Filtreleme:** Kullanıcılar ana sayfada yer alan dinamik arama çubuğu üzerinden etkinlik adlarına göre kelime bazlı arama yapabilir veya kategori dropdown menüsü vasıtasıyla içerikleri anlık olarak süzebilirler.
* **Gelişmiş Rol Yönetimi:** ASP.NET Core Identity altyapısı kullanılarak Admin, Organizatör ve Kullanıcı rolleri kurgulanmıştır. `[Authorize]` attribute'ları kullanılarak yetkisiz kullanıcıların etkinlik ekleme, silme veya düzenleme sayfalarına erişimi kesin olarak engellenmiştir.

### 🎁 BONUS ÖZELLİK: Kronolojik Tarih Sıralaması
Kullanıcı deneyimini en üst seviyeye çıkarmak amacıyla projeye **"Tarihe Göre Sıralama"** mekanizması entegre edilmiştir. 
* Kullanıcılar `En Yakın Tarih` veya `En Uzak Tarih` butonlarına tıkladıklarında, **o an yapmış oldukları arama terimi ve seçtikleri kategori filtresi kesinlikle kaybolmadan**, etkinlikler kronolojik olarak yeniden sıralanır. Bu işlem SQL/LINQ katmanında optimize edilmiş sorgularla gerçekleştirilir.

---

## 🚀 Projeyi Yerel Ortamda Çalıştırma Talimatları

Projeyi kendi yerel bilgisayarınızda sorunsuz bir şekilde ayağa kaldırmak için aşağıdaki adımları sırasıyla takip edebilirsiniz:

### 1. Depoyu Klonlayın
Proje kaynak kodlarını bilgisayarınıza indirin:
```bash
git clone [https://github.com/zeyneppp-1/etkinlikportali.git](https://github.com/zeyneppp-1/etkinlikportali.git)
cd etkinlikportali
### 💡 Bu Komutlar Ne İşe Yarar? (Geliştirici Notu)
 Bu komutlar, projenin yerel bir bilgisayara indirildiğinde eksiksiz ve hatasız bir şekilde ayağa kaldırılabilmesi için gerekli olan standart .NET ve Entity Framework Core işleyişidir:

* **dotnet restore (2. Adım):** Projenin derlenebilmesi ve çalışabilmesi için internet ortamından çekilmesi gereken tüm harici NuGet paketlerini (Bootstrap, Identity ve Veritabanı sürücüleri vb.) yerel ortama otomatik olarak indirir ve eksik bağımlılıkları tamamlar.
* **dotnet ef database update (3. Adım):** Kod katmanında kurgulanan veritabanı şemalarını, tabloları ve ASP.NET Core Identity üyelik yapılarını yerel bilgisayarınızda otomatik olarak işler ve sıfırdan güvenli bir `app.db` veritabanı dosyası oluşturur. Böylece uygulamanın çalışma esnasında veritabanı bağlantı hatası (Database Connection Error) vermesini engeller.
* **dotnet run (4. Adım):** Projenin yerel sunucusunu (Kestrel) tetikleyerek web uygulamasını aktif hale getirir ve tarayıcı üzerinden yerel adrese (`localhost`) erişim sağlayarak platformun canlı olarak test edilmesine olanak tanır.
