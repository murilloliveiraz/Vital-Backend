﻿namespace Application.DTOS.Exame
{
    public class AgendarExameResponseContract
    {
        public int ExameId { get; set; }
        public string Nome { get; set; }
        public string Status { get; set; }
        public string Local { get; set; }
        public DateTime Data { get; set; }
        public int PacienteId { get; set; }
        public string EmailParaReceberResultado { get; set; }
    }
}
