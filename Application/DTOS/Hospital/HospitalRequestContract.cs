namespace Application.DTOS.Hospital
{
    public interface HospitalRequestContract
    {
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public string Estado { get; set; }
    }
}