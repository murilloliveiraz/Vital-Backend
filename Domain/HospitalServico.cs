namespace Domain
{
    public class HospitalServico
    {
        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; }
        public int ServicoId { get; set; }
        public Servico Servico { get; set; }
    }
}
