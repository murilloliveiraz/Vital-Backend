using Application.DTOS.Usuario;

namespace Application.DTOS.Medico
{
    public class MedicoRequestContract : UsuarioRequestContract
    {
        public string Especialidade { get; set; }
        public string CRM { get; set; }
        public int HospitalId { get; set; }
    }
}