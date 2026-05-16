using EtkinlikPortali.Data;
using EtkinlikPortali.Interface;
using EtkinlikPortali.Models;
using Microsoft.EntityFrameworkCore;

namespace EtkinlikPortali.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _context;

        public EventRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Event> GetAllWithCategory()
        {
            return _context.Events.Include(e => e.Category).ToList();
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }

        public void Add(Event @event)
        {
            _context.Events.Add(@event);
        }

        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
        public Event? GetById(int id)
{
    return _context.Events.Include(e => e.Category).FirstOrDefault(e => e.Id == id);
}

public void Update(Event @event)
{
    _context.Events.Update(@event);
}

public void Delete(Event @event)
{
    _context.Events.Remove(@event);
}

    } 
}
   