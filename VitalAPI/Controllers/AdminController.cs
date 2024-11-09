using Application.DTOS.Admin;
using Application.Services.Classes;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VitalAPI.Controllers
{
    [Route("api/administradores")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Create(AdminRequestContract model)
        {
            var admin = await _adminService.Create(model);
            return CreatedAtAction(nameof(GetById), new { id = admin.Id }, admin);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Delete(int id)
        {
            await _adminService.Delete(id);
            return NoContent();
        }

        [HttpGet]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _adminService.Get());
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _adminService.GetById(id));
        }

        [HttpGet("pesquisar/email")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            return Ok(await _adminService.GetByEmail(email));
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Update(int id, AdminRequestContract model)
        {
            await _adminService.Update(id, model);
            return Ok();
        }
    }
}
