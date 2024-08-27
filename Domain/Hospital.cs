namespace Domain
{
    public class Hospital
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public DateTime? DataInativacao { get; set; }
        public ICollection<HospitalServico> Servicos { get; set; }
        public ICollection<Medico> Medicos { get; set; }
    }
}
