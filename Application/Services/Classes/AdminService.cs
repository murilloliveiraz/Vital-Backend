using Application.DTOS.Admin;
using Application.DTOS.Usuario;
using Application.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.Services.Classes
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;
        private readonly IUsuarioService _usuarioService;

        public AdminService(IAdminRepository adminRepository, IMapper mapper, IUsuarioService usuarioService)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
            _usuarioService = usuarioService;
        }

        public async Task<AdminResponseContract> Create(AdminRequestContract model)
        {
            var user = _mapper.Map<UsuarioRequestContract>(model);
            user.Role = "Administrador";
            var userCreated = await _usuarioService.Register(user);
            var admin = _mapper.Map<Administrador>(model);
            admin.UserId = userCreated.Id;
            await _adminRepository.Create(admin);
            return _mapper.Map<AdminResponseContract>(admin);
        }

        public async Task Delete(int id)
        {
            var admin = await _adminRepository.GetById(id);
            await _adminRepository.Delete(_mapper.Map<Administrador>(admin));
        }

        public async Task<IEnumerable<AdminResponseContract>> Get()
        {
            var admins = await _adminRepository.Get();
            return admins.Select(ad => _mapper.Map<AdminResponseContract>(ad));
        }

        public async Task<AdminResponseContract> GetById(int id)
        {
            var admin = await _adminRepository.GetById(id);
            return _mapper.Map<AdminResponseContract>(admin);
        }

        public async Task<AdminResponseContract> Update(int id, AdminRequestContract model)
        {
            var admin = await _adminRepository.GetById(id);
            _mapper.Map(model, admin);
            await _adminRepository.Update(admin);
            return _mapper.Map<AdminResponseContract>(admin);
        }
    }
}