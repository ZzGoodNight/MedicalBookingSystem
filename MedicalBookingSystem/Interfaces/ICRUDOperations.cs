using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace MedicalBookingSystem.Interfaces
{
    public interface ICRUDOperations<T>
    {
        void Add(T item);
        void Update(T item);
        void Delete(int id);
        T GetById(int id);
        List<T> GetAll();
    }

}

