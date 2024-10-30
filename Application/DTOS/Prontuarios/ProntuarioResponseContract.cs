using Domain;

namespace Application.DTOS.Prontuarios
{
    public class ProntuarioResponseContract
    {
        public ICollection<ProntuarioRegistro> Registros { get; set; }
    }
}
