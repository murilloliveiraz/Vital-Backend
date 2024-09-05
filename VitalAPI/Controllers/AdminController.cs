using Application.DTOS.Admin;
using Application.Services.Interfaces;
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
        public async Task<IActionResult> Create(AdminRequestContract model)
        {
            var admin = await _adminService.Create(model);
            return CreatedAtAction(nameof(GetById), new { id = admin.Id }, admin);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _adminService.Delete(id);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _adminService.Get());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _adminService.GetById(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AdminRequestContract model)
        {
            await _adminService.Update(id, model);
            return Ok();
        }
    }
}
