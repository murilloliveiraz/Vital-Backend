namespace Application.DTOS.Medico
{
    public class MedicoResponseContract : MedicoRequestContract
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string CRM { get; set; }
        public string CPF { get; set; }
        public string Role { get; set; }
        public int HospitalId { get; set; }
        public string Especialidade { get; set; } = string.Empty;
        public string CriadoPorEmail { get; set; }
        public DateTime? DataInativacao { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}