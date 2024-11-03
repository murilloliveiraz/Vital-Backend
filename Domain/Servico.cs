namespace Domain
{
    public class Servico
    {
        public int ServicoId { get; set; }
        public string Nome { get; set; }
        public string Especializacao { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public DateTime? DataInativacao { get; set; }
        public ICollection<HospitalServico> Hospitais { get; set; }
    }
}
