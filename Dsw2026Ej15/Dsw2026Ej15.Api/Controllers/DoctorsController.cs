using Microsoft.AspNetCore.Mvc;

namespace Dsw2026Ej15.Api.Controllers;
// para que el controlador sea reconocido como conrolador de una API
[ApiController]

//controller hereda de controller base, pero
//controller tiene un monton de funcionalidades
//para vistas y como no nos interesa ahora a nosotros
//ponemos controller base
public class DoctorsController : ControllerBase
{
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
}
