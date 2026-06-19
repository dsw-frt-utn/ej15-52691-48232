using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Dsw2026Ej15.Api.Controllers.Models;


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
            return BadRequest("Nombre y matricula son requeridos");
        }

        var speciality = _persistence.GetSpecialityById(request.SpecialityId);
        if (speciality is null)
        {
            return BadRequest("Especialidad no existe");
        }

        var doctor = new Doctor(request.Name, request.LicenseNumber, speciality);
        _persistence.SaveDoctor(doctor);

        return Created();
    }


    //2do endpoint

    // (IActionResult) interfaz que representa cualquier respuesta de un metodo basado en algun metodo del protocolo
    [HttpGet("api/doctors")]
    public IActionResult GetDoctors()
    {
        //ok es un metodo def en 200
        return Ok();
    }

    //3er endpoint
    [HttpGet("api/doctors/{id}")]
    public IActionResult GetDoctors(Guid id)
    {
        return Ok(id);
    }

    //4to endpoint DELETE (FALTA HACER)
}
