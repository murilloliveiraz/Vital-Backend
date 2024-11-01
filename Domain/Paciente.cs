﻿namespace Domain
{
    public class Paciente
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Sexo { get; set; }
        public string? PCD { get; set; }
        public string? Alergias { get; set; }
        public string? Medicamentos { get; set; }
        public string? HistoricoFamiliar { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime DataNascimento { get; set; }
        public ICollection<Exame> Exames { get; set; }
        public ICollection<Consulta> Consultas { get; set; }
        public Prontuario Prontuario { get; set; }
    }
}
