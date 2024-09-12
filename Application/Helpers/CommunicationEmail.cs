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
        public static string ResultAvailableEmail(string portalUrl)
        {
            string response = "<div style=\"width:100%;background-color:#FFAC1C;text-align:center;margin:10px\">";
            response += "<h1>Resultado Disponível</h1>";
            response += "<p>Olá,</p>";
            response += "<p>Seu resultado está disponível no portal. Por favor, clique no link abaixo para acessar:</p>";
            response += $"<a href='{portalUrl}' style='color:white;background-color:#007BFF;padding:10px;text-decoration:none;border-radius:5px;'>Acessar Portal</a>";
            response += "<p>Obrigado por usar nossos serviços.</p>";
            response += "<div><h3>vitaltech@gmail.com</h3></div>";
            response += "</div>";
            return response;
        }

        public static string AppointmentConfirmationEmail(string appointmentDetails)
        {
            string response = "<div style=\"width:100%;background-color:#FFAC1C;text-align:center;margin:10px\">";
            response += "<h1>Consulta Agendada</h1>";
            response += "<p>Olá,</p>";
            response += "<p>Sua consulta foi agendada com sucesso!</p>";
            response += $"<p>Detalhes da consulta: {appointmentDetails}</p>";
            response += "<p>Se você precisar alterar ou cancelar a consulta, entre em contato com a clínica.</p>";
            response += "<p>Estamos ansiosos para atendê-lo!</p>";
            response += "<div><h3>vitaltech@gmail.com</h3></div>";
            response += "</div>";
            return response;
        }

    }
}
