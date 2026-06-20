namespace Dsw2026Ej15.Api.Controllers.Models
{
    public record DoctorModels
    {
        public record Request(string Name, string LicenseNumber, Guid SpecialityId);
    }
}
