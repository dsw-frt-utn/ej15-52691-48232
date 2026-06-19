using Dsw2026Ej15.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Interfaces
{
    public interface IPersistence
    {
        Speciality? GetSpecialityById(Guid id);
        void SaveDoctor(Doctor doctor);
        void AddDoctor(Doctor doctor);
        IEnumerable<Doctor> GetAllDoctors();
        Doctor? GetDoctorById(Guid id);
        void DeleteDoctor(Guid id);
    }
}
