using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using EtkinlikPortali.Interface;
using EtkinlikPortali.Models;
using EtkinlikPortali.ViewModels;

namespace EtkinlikPortali.Controllers
{ 

    public class EventController : Controller // BU SATIRI EKLEMEN ŞART (Hataların %90'ı bu eksik diye çıkıyor)
    { 
        private readonly IEventRepository _eventRepository;
        private readonly IEventRepository _categoryRepository;

        public EventController(IEventRepository eventRepository, IEventRepository categoryRepository)
        {
            _eventRepository = eventRepository;
            _categoryRepository = categoryRepository;
        }

        // Buradan sonra senin Index, Create, Edit metodların gelecek...

        // 1. ANA SAYFA (LİSTELEME)

        // 2. İNCELE (DETAY) SAYFASI - YENİ EKLEDİĞİMİZ YER
       public IActionResult Details(int id)
{
    // Veritabanından etkinliği ve kategorisini çekiyoruz
    var e = _eventRepository.GetById(id);

    if (e == null) return NotFound();

    // Sayfaya gönderirken Kategorisiz değil, gerçek kategori adını veriyoruz
    var viewModel = new EventViewModel
    {
        Id = e.Id,
        Title = e.Title,
        Date = e.Date,
        Location = e.Location,
        Description = e.Description, // Açıklamayı da ekleyelim ki sayfa boş kalmasın,
        ImageUrl = e.ImageUrl,
        CategoryName = e.Category?.Name ?? "Genel" // İşte burası gerçek kategori adını getirir
    };

    return View(viewModel);
}

        // 3. YENİ EKLE SAYFASI (GET)
        [HttpGet]
        public IActionResult Create()
        {
            var categories = _eventRepository.GetAllCategories();
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");
            return View();
        }

        // 4. YENİ EKLEME İŞLEMİ (POST)
        [HttpPost]
        public IActionResult Create(Event @event)
        {
            _eventRepository.Add(@event);
            _eventRepository.Save();
            return RedirectToAction("Index");
        }
        // DÜZENLEME SAYFASINI AÇAR (GET)
[HttpGet]
public IActionResult Edit(int id)
{
    var e = _eventRepository.GetById(id);
    if (e == null) return NotFound();

    var categories = _eventRepository.GetAllCategories();
    ViewBag.CategoryId = new SelectList(categories, "Id", "Name", e.CategoryId);
    
    return View(e);
}

// DÜZENLEME İŞLEMİNİ KAYDEDER (POST)
[HttpPost]
public IActionResult Edit(Event @event)
{
    _eventRepository.Update(@event);
    _eventRepository.Save();
    return RedirectToAction("Index");
}
public IActionResult Delete(int id)
{
    var e = _eventRepository.GetById(id);
    if (e != null)
    {
        _eventRepository.Delete(e); // 'id' yerine bulduğumuz 'e' nesnesini gönderiyoruz.
        _eventRepository.Save(); // Değişiklikleri veritabanına işle
    }
    return RedirectToAction("Index"); // Silme işleminden sonra ana sayfaya dön
}

public IActionResult Index(string category, string searchTerm)
{
    // 1. KATEGORİLERİ KONTROL ET VE YOKSA EKLE (Eski kodun buraya taşındı)
    var existingCategories = _eventRepository.GetAllCategories();
    if (!existingCategories.Any())
    {
        var defaultCategories = new List<Category>
        {
            new Category { Name = "Eğitim" },
            new Category { Name = "Gezi" },
            new Category { Name = "Workshop" },
            new Category { Name = "Tiyatro" }
        };

        foreach (var cat in defaultCategories)
        {
            _eventRepository.AddCategory(cat);
        }
        _eventRepository.Save();
    }

    // 2. TÜM ETKİNLİKLERİ ÇEK
    var events = _eventRepository.GetAllWithCategory();

    // 3. FİLTRELEME VE ARAMA YAP
    if (!string.IsNullOrEmpty(category))
    {
        events = events.Where(e => e.Category != null && e.Category.Name == category).ToList();
    }

    if (!string.IsNullOrEmpty(searchTerm))
    {
        events = events.Where(e => e.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    // 4. EKRANA GÖNDER
    var viewModel = events.Select(e => new EventViewModel
    {
        Id = e.Id,
        Title = e.Title,
        Description = e.Description,
        Location = e.Location,
        Date = e.Date,
        ImageUrl = e.ImageUrl,
        CategoryName = e.Category?.Name ?? "Genel"
    }).ToList();

    return View(viewModel);
}
    }
}