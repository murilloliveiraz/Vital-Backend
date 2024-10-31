using Application.DTOS.Usuario;

namespace Application.DTOS.Paciente
{
    public class PacienteRequestContract : UsuarioRequestContract
    {
        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; }
        public string? PCD { get; set; }
        public string? Alergias { get; set; }
        public string? Medicamentos { get; set; }
        public string? HistoricoFamiliar { get; set; }
    }
}