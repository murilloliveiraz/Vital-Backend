namespace Application.DTOS.Paciente
{
    public class PacienteResponseContract : PacienteRequestContract
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataInativacao { get; set; }
    }
}