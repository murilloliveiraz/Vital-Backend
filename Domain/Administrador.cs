namespace Domain
{
    internal class Administrador
    {
        public int Id { get; set; }
        public string Cargo { get; set; }
        public string UserId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
