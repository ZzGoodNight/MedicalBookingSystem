using MedicalBookingSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MedicalBookingSystem.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly List<T> _entities = new List<T>();

        public void Add(T entity)
        {
            _entities.Add(entity);
        }

        public void Update(T entity)
        {
            // Для спрощення: замінюємо об'єкт за ID (припускаємо, що сутності мають Id)
            var existing = _entities.FirstOrDefault(e => (int)e.GetType().GetProperty("Id").GetValue(e) ==
                (int)entity.GetType().GetProperty("Id").GetValue(entity));
            if (existing != null)
            {
                var index = _entities.IndexOf(existing);
                _entities[index] = entity;
            }
        }

        public void Delete(int id)
        {
            var entity = _entities.FirstOrDefault(e => (int)e.GetType().GetProperty("Id").GetValue(e) == id);
            if (entity != null)
                _entities.Remove(entity);
        }

        public T GetById(int id)
        {
            return _entities.FirstOrDefault(e => (int)e.GetType().GetProperty("Id").GetValue(e) == id);
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.OrderBy(e => (int)e.GetType().GetProperty("Id").GetValue(e)); // Сортування за ID
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return _entities.Where(predicate);
        }
    }
}
