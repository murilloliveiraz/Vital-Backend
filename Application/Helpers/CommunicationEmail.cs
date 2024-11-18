namespace Application.Helpers
{
    public static class CommunicationEmail
    {       
        public static string ChangePasswordEmail(string callbackUrl)
        {
            return $@"
                <div style=""width: 100%; background-color: #ffffff; text-align: center; margin: 0 auto; font-family: sans-serif;"">
                <div style=""margin-bottom: 20px;"">
                  <img src=""https://raw.githubusercontent.com/murilloliveiraz/Vital-Frontend/0c3a550b90ba267c5648eff9f2ab64bd7653b006/src/assets/images/logoPrincipalCor2.svg"" 
                       style=""width: 120px;"" 
                       alt=""Logo Vital"">
                </div>
      
                <h1 style=""color: #108999; margin: 20px 0; font-size: 24px;"">Seja bem-vindo à VITAL!</h1>
        
                <p style=""color: #333333; font-size: 16px; margin: 20px 0;"">
                  Estamos entusiasmados em tê-lo conosco! Para acessar sua conta e começar, defina sua senha clicando no botão abaixo.
                </p>
      
                <div style=""margin: 30px 0;"">
                  <a href=""{callbackUrl}"" 
                     style=""background-color: #108999; color: #ffffff; text-decoration: none; padding: 15px 30px; font-size: 16px; font-weight: bold; border-radius: 5px; display: inline-block;"">
                    Definir Minha Senha
                  </a>
                </div>
      
                <div style=""margin: 30px auto;"">
                  <img src=""https://github.com/murilloliveiraz/Vital-Frontend/blob/main/src/assets/images/doutor.png?raw=true"" 
                       alt=""Doutor Vital"" 
                       style=""width: 100%; max-width: 340px; border-radius: 10px;"">
                </div>
      
                <div style=""margin-top: 30px; color: #666666; font-size: 14px;"">
                  <p>Precisa de ajuda? Entre em contato conosco:</p>
                  <p><a href=""mailto:vitalttc.tech@gmail.com"" style=""color: #108999; text-decoration: none;"">vitalttc.tech@gmail.com</a></p>
                </div>
      
                <footer style=""margin-top: 20px; color: #999999; font-size: 12px;"">
                  <p style=""margin: 0;"">VITAL © 2024. Todos os direitos reservados.</p>
                </footer>
              </div>
            ";
        }
        public static string ResultAvailableEmail(string portalUrl)
        {
            return $@"
                <div style=""width: 100%; background-color: #ffffff; text-align: center; margin: 0 auto; font-family: sans-serif;"">
                <div style=""margin-bottom: 20px;"">
                  <img src=""https://raw.githubusercontent.com/murilloliveiraz/Vital-Frontend/0c3a550b90ba267c5648eff9f2ab64bd7653b006/src/assets/images/logoPrincipalCor2.svg"" 
                       style=""width: 120px;"" 
                       alt=""Logo Vital"">
                </div>
                <h1 style=""color: #108999; margin: 20px 0; font-size: 24px;"">Resultado Disponível</h1>
        
                <p style=""color: #333333; font-size: 16px; margin: 20px 0;"">
                  Olá, <br> 
                  Seu resultado já está disponível no portal. Clique no botão abaixo para acessar:
                </p>
        
                <div style=""margin: 20px;"">
                  <a href=""{portalUrl}"" 
                  style=""background-color: #108999; color: #ffffff; text-decoration: none; padding: 15px 30px; font-size: 16px; font-weight: bold; border-radius: 5px; display: inline-block;"">
                    Acessar Portal
                  </a>
                </div>

                <div style=""margin: 30px auto;"">
                    <img src=""https://github.com/murilloliveiraz/Vital-Frontend/blob/main/src/assets/images/doutor.png?raw=true"" 
                         alt=""Doutor Vital"" 
                         style=""width: 100%; max-width: 340px; border-radius: 10px;"">
                  </div>
      
                <div style=""margin-top: 30px; color: #333333; font-size: 14px;"">
                  <p>Obrigado por usar nossos serviços.</p>
                  <p>Em caso de dúvidas, entre em contato:</p>
                  <p><a href=""mailto:vitaltech@gmail.com"" style=""color: #333333; text-decoration: none;"">vitaltech@gmail.com</a></p>
                </div>
              </div>
            ";
        }

        public static string AppointmentConfirmationEmail(string appointmentDetails)
        {
            return $@"
                <div style=""width: 100%; background-color: #ffffff; text-align: center; margin: 0 auto; font-family: sans-serif;"">
                <img src=""https://raw.githubusercontent.com/murilloliveiraz/Vital-Frontend/0c3a550b90ba267c5648eff9f2ab64bd7653b006/src/assets/images/logoPrincipalCor2.svg"" 
                     style=""width: 120px;"" 
                     alt=""Logo Vital"">

                <h1 style=""color: #108999; font-size: 24px; margin: 10px 0;"">Agendamento concluído</h1>
        
                <p style=""color: #333333; font-size: 16px; margin: 20px 0;"">
                    Olá, <br> 
                    Seu agendamento foi concluído com sucesso!
                </p>
                <p style=""color: #333333; font-size: 16px; margin: 10px 0;"">
                    <strong>Detalhes do agendamento:</strong> <br> 
                    {appointmentDetails}
                </p>

                <div style=""margin: 30px auto;"">
                    <img src=""https://github.com/murilloliveiraz/Vital-Frontend/blob/main/src/assets/images/doutor.png?raw=true"" 
                        alt=""Doutor Vital"" 
                        style=""width: 100%; max-width: 340px; border-radius: 10px;"">
                </div>
        
                <p style=""color: #333333; font-size: 16px; margin: 10px 0;"">
                    Caso precise alterar ou cancelar, entre em contato com a clínica. Estamos ansiosos para atendê-lo!
                </p>

                <div style=""margin-top: 30px; color: #333333; font-size: 14px;"">
                    <p>Em caso de dúvidas, entre em contato:</p>
                    <p><a href=""mailto:vitaltech@gmail.com"" style=""color: #333333; text-decoration: none;"">vitaltech@gmail.com</a></p>
                </div>
            </div>
            ";
        }

        public static string AppointmentCanceled(string appointmentDetails)
        {
            return $@"
                <div style=""width: 100%; background-color: #ffffff; text-align: center; margin: 0 auto; font-family: sans-serif;"">
                <img src=""https://raw.githubusercontent.com/murilloliveiraz/Vital-Frontend/0c3a550b90ba267c5648eff9f2ab64bd7653b006/src/assets/images/logoPrincipalCor2.svg"" 
                        style=""width: 120px;"" 
                        alt=""Logo Vital"">

                <h1 style=""color: #108999; font-size: 24px; margin: 10px 0;"">Agendamento Cancelado</h1>
        
                <p style=""color: #333333; font-size: 16px; margin: 20px 0;"">
                    Olá, <br> 
                    Seu agendamento foi cancelado com sucesso.
                </p>
                <p style=""color: #333333; font-size: 16px; margin: 10px 0;"">
                    <strong>Detalhes do agendamento:</strong> <br> 
                    {appointmentDetails}
                </p>

                <div style=""margin: 30px auto;"">
                    <img src=""https://github.com/murilloliveiraz/Vital-Frontend/blob/main/src/assets/images/doutor.png?raw=true"" 
                        alt=""Doutor Vital"" 
                        style=""width: 100%; max-width: 340px; border-radius: 10px;"">
                </div>
        
                <p style=""color: #333333; font-size: 16px; margin: 10px 0;"">
                    Caso deseje reagendar, entre em contato conosco para que possamos ajudá-lo.
                </p>

                <div style=""margin-top: 30px; color: #333333; font-size: 14px;"">
                    <p>Em caso de dúvidas, entre em contato:</p>
                    <p><a href=""mailto:vitaltech@gmail.com"" style=""color: #333333; text-decoration: none;"">vitaltech@gmail.com</a></p>
                </div>
            </div>
            ";
        }

        public static string PaymentReceived()
        {
            return $@"
               <div style=""width: 100%; background-color: #ffffff; text-align: center; margin: 0 auto; font-family: sans-serif;"">
                <img src=""https://raw.githubusercontent.com/murilloliveiraz/Vital-Frontend/0c3a550b90ba267c5648eff9f2ab64bd7653b006/src/assets/images/logoPrincipalCor2.svg"" 
                     style=""width: 120px;"" 
                     alt=""Logo Vital"">

                <h1 style=""color: #108999; font-size: 24px; margin: 10px 0;"">Pagamento Recebido</h1>
        
                <p style=""color: #333333; font-size: 16px; margin: 20px 0;"">
                    Olá, <br> 
                    Confirmamos o recebimento do seu pagamento com sucesso!
                </p>

                <div style=""margin: 30px auto;"">
                    <img src=""https://github.com/murilloliveiraz/Vital-Frontend/blob/main/src/assets/images/doutor.png?raw=true"" 
                        alt=""Doutor Vital"" 
                        style=""width: 100%; max-width: 340px; border-radius: 10px;"">
                </div>
        
                <p style=""color: #333333; font-size: 16px; margin: 10px 0;"">
                    Obrigado por escolher nossos serviços. Caso tenha dúvidas ou precise de suporte, estamos à disposição
                </p>

                <div style=""margin-top: 30px; color: #333333; font-size: 14px;"">
                    <p>Em caso de dúvidas, entre em contato:</p>
                    <p><a href=""mailto:vitaltech@gmail.com"" style=""color: #333333; text-decoration: none;"">vitaltech@gmail.com</a></p>
                </div>
            </div>
            ";
        }
    }
}
