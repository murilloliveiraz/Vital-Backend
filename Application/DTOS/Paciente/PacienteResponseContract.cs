namespace Application.DTOS.Paciente
{
    public class PacienteResponseContract : PacienteRequestContract
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataInativacao { get; set; }
        public string? PCD { get; set; }
        public string? Alergias { get; set; }
        public string? Medicamentos { get; set; }
        public string? HistoricoFamiliar { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int CriadoPorUsuarioId { get; set; }
        public string CPF { get; set; }
        public string Role { get; set; }
    }
}