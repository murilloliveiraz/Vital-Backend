using System.Xml.Linq;

namespace Domain
{
    public class Paciente
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime DataNascimento { get; set; }
        public ICollection<Exame> Exames { get; set; }
    }
}
