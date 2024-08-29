using Application.DTOS.Usuario;
using Application.Helpers;
using Application.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Contexts;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.Classes
{
    public class UsuarioService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailSender;
        private readonly TokenJWTService _tokenService;

        public UsuarioService(
            ApplicationContext context,
            UserManager<Usuario> userManager,
            IMapper mapper,
            IEmailService emailSender,
            TokenJWTService tokenService
        )
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
            _emailSender = emailSender;
            _tokenService = tokenService;
        }

        public async Task<Usuario> Register(UsuarioRequestContract model)
        {
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            var user = _mapper.Map<Usuario>(model);
            user.UserName = model.Email;
            user.DataCriacao = DateTime.UtcNow;
            if (existingUser != null)
            {
                return existingUser;
            }
            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = $"https://localhost:4200/reset-password?userId={user.Id}&token={token}";
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
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
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
