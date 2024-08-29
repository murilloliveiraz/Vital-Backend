namespace Application.DTOS.Usuario
{
    public class UsuarioResponseContract : UsuarioRequestContract
    {
        public string Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataInativacao { get; set; }
    }
}
