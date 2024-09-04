namespace Application.DTOS.Utils
{
    public class ResetPasswordModel
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string Token { get; set; }
    }
}
