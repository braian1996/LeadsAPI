using LeadsAPI.Entidades;

namespace LeadsAPI.Repositorios
{
    public class LeadRepository
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
