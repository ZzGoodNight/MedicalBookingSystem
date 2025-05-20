using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalBookingSystem.Interfaces
{
    public interface IFilterable<T>
    {
        List<T> Filter(string keyword);
    }
}
