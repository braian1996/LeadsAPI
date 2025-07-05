using LeadsAPI.Entidades;

namespace LeadsAPI.Datos
{
    public class RepositorioLead
    {
        private readonly List<Lead> leads = new();

        public void Save(Lead lead)
        {
            leads.Add(lead);
        }

        public IEnumerable<Lead> GetAll()
        {
            return leads;
        }
    }
}
