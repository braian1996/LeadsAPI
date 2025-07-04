using LeadsAPI.Entidades;

namespace LeadsAPI.DTOs
{
    public class LeadDTO
    {
        public int PlaceId { get; set; }
        public DateTime AppointmentAt { get; set; }
        public string? ServiceType { get; set; }
        public ContactDTO? Contact { get; set; }
        public VehicleDTO? Vehicle { get; set; }
    }
}
