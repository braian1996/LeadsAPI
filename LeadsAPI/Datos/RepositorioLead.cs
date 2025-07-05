using LeadsAPI.Entidades;

namespace LeadsAPI.Datos
{
    public class RepositorioLead
    {
        private readonly List<Lead> leads = new();

        public bool Save(Lead lead, out string message)
        {
            if (ValidaTurnos(lead))
            {
                message = "Ya existe un turno con los mismos datos.";
                return false;
            }

            leads.Add(lead);
            message = "Su turno fue creado con exito.";
            return true;

        }

        public IEnumerable<Lead> GetAll()
        {
            return leads;
        }
        public bool ValidaTurnos(Lead lead)
        {
            return leads.Any(l =>
                l.PlaceId == lead.PlaceId &&
                l.AppointmentAt == lead.AppointmentAt &&
                l.Vehicle?.LicensePlate == lead.Vehicle?.LicensePlate);
        }
    }
}
