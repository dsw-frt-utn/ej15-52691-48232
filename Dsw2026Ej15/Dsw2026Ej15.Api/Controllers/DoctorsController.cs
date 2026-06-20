using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Dsw2026Ej15.Api.Controllers.Models;
using Dsw2026Ej15.Domain.Exceptions; //para el post al usar el validation

namespace Dsw2026Ej15.Api.Controllers;
// para que el controlador sea reconocido como conrolador de una API
[ApiController]

//controller hereda de controller base, pero
//controller tiene un monton de funcionalidades
//para vistas y como no nos interesa ahora a nosotros
//ponemos controller base
public class DoctorsController : AppControllers
{
    private readonly IPersistence _persistence;

    public DoctorsController(IPersistence persistence) //inyeccion de dependencia, recibimos la interfaz
    {
        _persistence = persistence;
    }

    //1er endopint: POST

    [HttpPost("doctors")]
    public async Task<IActionResult> CreateDoctor(DoctorModels.Request request) //lamo del doctor models
    {
        if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.LicenseNumber))
        {
            //si la validacion falla, ahi nomas se lanza la excepcion y frena ahi.
            throw new Dsw2026Ej15.Domain.Exceptions.ValidationException("El Nombre y la matricula son requeridos");
        }

        var speciality = _persistence.GetSpecialityById(request.SpecialityId);
        if (speciality is null)
        {
            throw new Dsw2026Ej15.Domain.Exceptions.ValidationException("La especialidad no existe");
        }
    }

        var doctor = new Doctor(request.Name, request.LicenseNumber, speciality);
        _persistence.SaveDoctor(doctor);

        return Created();
    }


    //2do endpoint

    // (IActionResult) interfaz que representa cualquier respuesta de un metodo basado en algun metodo del protocolo
    [HttpGet("doctors")]
    public IActionResult GetDoctors()
    {
        //ok es un metodo def en 200
        var listaMedicos = _persistence.GetAllDoctors();

        return Ok(listaMedicos);
    }

    //3er endpoint
    [HttpGet("doctors/{id}")]
    public IActionResult GetDoctors(Guid id)
    {
        //busco el doctor 
        var doctor = _persistence.GetDoctorById(id);
        if (doctor == null || !doctor.IsActive)
        {
            return NotFound("El medico no existe o ya esta inactivo");
        }

        //si pasa el filtro entonces existe y ahi largo la info
        var datosMedico = new
        {
            Name = doctor.Name,
            LicenseNumber = doctor.LicenseNumber,
            Speciality = doctor.Speciality?.Name
        };
        return Ok(datosMedico);
    }

    //4to endpoint
    [HttpDelete("doctors/{id}")]
    public IActionResult DeleteDoctor(Guid id)
    {
        var doctor = _persistence.GetDoctorById(id);

        if (doctor == null || !doctor.IsActive)
        {
            return NotFound("El medico no existe o ya esta inactivo");
        }
        
        _persistence.DeleteDoctor(id);
        return NoContent();
    }




}
