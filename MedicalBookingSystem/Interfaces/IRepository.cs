using System;
using System.Collections.Generic;

namespace MedicalBookingSystem.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Add(T item);
        void Update(T item);
        void Delete(int id);

        // Метод для пошуку за умовою
        T Find(Func<T, bool> predicate);
    }
}
