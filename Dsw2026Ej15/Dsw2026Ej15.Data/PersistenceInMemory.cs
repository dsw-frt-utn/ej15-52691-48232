using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.Json;
using Dsw2026Ej15.Data.Dto;

namespace Dsw2026Ej15.Data
{
    public class PersistenceInMemory : IPersistence
    {
    
        private List<Doctor>? _doctors = [];
        private List<Speciality>? _specialities = [];

        //constructor
        public PersistenceInMemory()
        {
            LoadSpecialities();
        }
        public Speciality? GetSpecialityById(Guid id)
        {
            return _specialities?.SingleOrDefault(e => e.Id == id);
        }

        public void SaveDoctor(Doctor doctor)
        {
            _doctors?.Add(doctor);
        }

        public void LoadSpecialities()
        {
            try
            {
                string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "specialities.json");

                var json = File.ReadAllText(jsonPath);

                var specialities = JsonSerializer.Deserialize<List<SpecialityDto>>(json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }) ?? [];

                _specialities = [.. specialities.Select(s => new Speciality(s.Name, s.Description, s.Id))];
            }
            catch (Exception)
            {

            }
        }

        public void AddDoctor (Doctor doctor)
        {
            _doctors?.Add(doctor);
        }

        public IEnumerable<Doctor> GetAllDoctors()
        {
            return _doctors ?? Enumerable.Empty<Doctor>() ;
        }

        public Doctor? GetDoctorById(Guid id)
        {

            return _doctors?.FirstOrDefault(d => d.Id == id);
        }
        public void DeleteDoctor(Guid id)
        {
            var doctorExistente = GetDoctorById(id);

            /*if (doctorExistente != null)
            {
                doctorExistente.IsActive = false;
            }*/ // Acá tira error, no se porque
        }

        /*private LoadSpecialities()
        {

        }*/
    }
}
