using Application.DTOS.Usuario;
using Application.DTOS.Utils;
using Application.Helpers;
using Application.Services.Interfaces;
using AutoMapper;
using Domain;
using Google.Apis.Auth;
using Infraestructure.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Web;

namespace Application.Services.Classes
{
    public class UsuarioService : IUsuarioService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailSender;
        private readonly IConfiguration _configuration;
        private readonly TokenJWTService _tokenService;

        public UsuarioService(
            ApplicationContext context,
            UserManager<Usuario> userManager,
            IMapper mapper,
            IEmailService emailSender,
            TokenJWTService tokenService,
            IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
            _emailSender = emailSender;
            _tokenService = tokenService;
            _configuration = configuration;
        }

        public async Task<Usuario> Register(UsuarioRequestContract model)
        {
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            var user = _mapper.Map<Usuario>(model);
            user.UserName = model.Email;
            user.DataCriacao = DateTime.Now;
            if (existingUser != null)
            {
                return existingUser;
            }
            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user); 
                var encodedToken = HttpUtility.UrlEncode(token);
                var callbackUrl = $"http://localhost:4200/atualizar-senha?userId={user.Id}&token={encodedToken}";
                MailRequest mailRequest = new MailRequest
                {
                    ToEmail = user.Email,
                    Subject = "Defina sua senha",
                    Body = CommunicationEmail.ChangePasswordEmail(callbackUrl)
                };
                await _emailSender.SendEmailAsync(mailRequest);
                return user;
            }
            else
            {
                return null;
            }
        }

        public async Task<UsuarioLoginResponseContract> Login(UsuarioLoginRequestContract model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && user.DataInativacao != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return new UsuarioLoginResponseContract
                {
                    Id = user.Id,
                    Email = user.Email,
                    CPF = user.CPF,
                    Role = user.Role,
                    Token = _tokenService.GenerateToken(_mapper.Map<Usuario>(user))
                };
            }

            throw new Exception("Credenciais inválidas.");
        }

        public async Task<OperationResult> ResetPasswordAsync(string email, string token, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return OperationResult.Failure("Usuário não encontrado.");
            }

            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            if (result.Succeeded)
            {
                return OperationResult.Success("A senha foi redefinida com sucesso.");
            }

            var errors = result.Errors.Select(e => e.Description);
            return OperationResult.Failure("Falha ao redefinir a senha.", errors);
        }

        public async Task<OperationResult> ForgotMyPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return OperationResult.Failure("Usuário não encontrado.");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = HttpUtility.UrlEncode(token);
            var callbackUrl = $"http://localhost:4200/atualizar-senha?userId={user.Id}&token={encodedToken}";
            MailRequest mailRequest = new MailRequest
            {
                ToEmail = user.Email,
                Subject = "Redefina sua senha",
                Body = CommunicationEmail.ChangePasswordEmail(callbackUrl)
            };
            await _emailSender.SendEmailAsync(mailRequest);
            return OperationResult.Success("O email para redefinir a senha foi enviado.");
        }

        public async Task<UsuarioLoginResponseContract> LoginWithGoogle(string credential)
        {
            Console.WriteLine(_configuration["google:client_id"]);
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { _configuration["google:client_id"] },
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(credential, settings);

            var user = await _userManager.FindByEmailAsync(payload.Email);

            if (user != null)
            {
                return new UsuarioLoginResponseContract
                {
                    Id = user.Id,
                    Email = user.Email,
                    CPF = user.CPF,
                    Role = user.Role,
                    Token = _tokenService.GenerateToken(_mapper.Map<Usuario>(user))
                };
            }

            throw new Exception("Credenciais inválidas.");
        }
    }
}
