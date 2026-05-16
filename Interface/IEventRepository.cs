using EtkinlikPortali.Models;

namespace EtkinlikPortali.Interface
{
    public interface IEventRepository
    {
        IEnumerable<Event> GetAllWithCategory();
        IEnumerable<Category> GetAllCategories();
        Event? GetById(int id);
        void Add(Event @event);
        void AddCategory(Category category); 
        void Update(Event @event);
        void Delete(Event @event);
        void Save();
    }
    
}