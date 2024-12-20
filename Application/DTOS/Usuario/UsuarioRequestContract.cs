﻿namespace Application.DTOS.Usuario
{
    public class UsuarioRequestContract
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string CriadoPorEmail { get; set; }
        public string CPF { get; set; }
        public string Role { get; set; }
    }
}
