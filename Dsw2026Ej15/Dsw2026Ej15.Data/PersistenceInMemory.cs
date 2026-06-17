using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.Json;

namespace Dsw2026Ej15.Data
{
    public class PersistenceInMemory : IPersistence
    {
        private readonly List<Doctor> _doctors = new List<Doctor>();

        private List<Speciality>? _specialities = new List<Speciality>();


        //constructor
        public PersistenceInMemory()
        {
            LoadSpecialities();
        }

        public void LoadSpecialities()
        {
            var json =  File.ReadAllText("specialities.json");
            var specialities = JsonSerializer.Deserialize<List<Speciality>>(json);
        }


        public void AddDoctor (Doctor doctor)
        {
            _doctors.Add(doctor);
        }

        public IEnumerable<Doctor> GetAllDoctors()
        {
            return _doctors;
        }

        public Doctor? GetDoctorById(Guid id)
        {

            return _doctors.FirstOrDefault(d => d.Id == id);
        }
        public void DeleteDoctor(Guid id)
        {
            var doctorExistente = GetDoctorById(id);

            if (doctorExistente != null)
            {
                doctorExistente.IsActive = false;
            }
        }

        /*private LoadSpecialities()
        {

        }*/
    }
}
