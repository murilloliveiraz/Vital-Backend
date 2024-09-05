using Domain;

namespace Application.DTOS.Prontuarios
{
    public class ProntuarioResponseContract
    {
        public Prontuario Prontuario { get; set; }
        public ICollection<ProntuarioRegistro> Registros { get; set; }
    }
}
