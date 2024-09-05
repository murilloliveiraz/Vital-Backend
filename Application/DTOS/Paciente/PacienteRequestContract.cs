using Application.DTOS.Usuario;

namespace Application.DTOS.Paciente
{
    public class PacienteRequestContract : UsuarioRequestContract
    {
        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; }
    }
}