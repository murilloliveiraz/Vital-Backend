namespace Application.Helpers
{
    public static class CommunicationEmail
    {       
        public static string ChangePasswordEmail(string callbackUrl)
        {
            string Response = "<div style=\"width:100%;background-color:#FFAC1C;text-align:center;margin:10px\">";
            Response += "<h1>\"Defina sua senha\"</h1>";
            Response += "<img src=\"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSggdch-qeLH8k6laogfDGkEPGQKVjawcTmnA&s\" />";
            Response += $"<h3>\"Por favor, defina sua senha clicando aqui: <a href='{callbackUrl}'>link</a>\"</h3>";
            Response += "<div><h1>vitaltech@gmail.com</h1></div>";
            Response += "</div>";
            return Response;
        }
    }
}
