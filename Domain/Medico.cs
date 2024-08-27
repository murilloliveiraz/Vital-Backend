namespace Domain
{
    public class Medico
    {
        public int Id { get; set; }
        public string Especialidade { get; set; }
        public string CRM { get; set; }
        public string UserId { get; set; }
        public Usuario Usuario { get; set; }
        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; }
    }
}
