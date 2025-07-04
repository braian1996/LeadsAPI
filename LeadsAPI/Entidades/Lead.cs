namespace LeadsAPI.Entidades
{
    public class Lead
    {
        public int PlaceId { get; set; }
        public DateTime AppointmentAt { get; set; }
        public string? ServiceType { get; set; }
        public Contact? Contact { get; set; }
        public Vehicle? Vehicle { get; set; }
    }
}
