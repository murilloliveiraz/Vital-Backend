using Application.DTOS.Consulta;
using Application.DTOS.Exame;
using Application.Services.Classes;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VitalAPI.Controllers
{
    [Route("api/consultas")]
    [ApiController]
    public class ConsultaController : ControllerBase
    {
        private readonly IConsultaService _consultaService;

        public ConsultaController(IConsultaService consultaService)
        {
            _consultaService = consultaService;
        }


        [HttpPost("agendar")]
        [Authorize]
        public async Task<IActionResult> Create(AgendarConsultaRequestContract model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var consulta = await _consultaService.Create(model);
            return CreatedAtAction(nameof(GetById), new { id = consulta.Id }, consulta);
        }

        [HttpPost("agendar-remota")]
        [Authorize]
        public async Task<IActionResult> CreateRemote(AgendarConsultaRequestContract model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var consulta = await _consultaService.CreateRemoteAppointment(model);
            return CreatedAtAction(nameof(GetById), new { id = consulta.Id }, consulta);
        }


        [HttpGet]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Get()
        {
            var consultas = await _consultaService.Get();
            return Ok(consultas);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var consulta = await _consultaService.GetById(id);
            if (consulta == null)
            {
                return NotFound();
            }

            return Ok(consulta);
        }

        [HttpGet("concluidos")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> GetAllCompleted()
        {
            var consultasConcluidos = await _consultaService.GetAllCompleted();
            return Ok(consultasConcluidos);
        }

        [HttpGet("paciente/{id}/concluidos")]
        [Authorize]
        public async Task<IActionResult> GetAllPatientAppointmentsCompleted(int id)
        {
            var consultasConcluidos = await _consultaService.GetAllPatientAppointmentsCompleted(id);
            return Ok(consultasConcluidos);
        }

        [HttpGet("paciente/{id}/agendados")]
        [Authorize]
        public async Task<IActionResult> GetAllPatientAppointmentsScheduled(int id)
        {
            var consultasAgendados = await _consultaService.GetAllPatientAppointmentsScheduled(id);
            return Ok(consultasAgendados);
        }

        [HttpGet("medico/{id}/concluidos")]
        [Authorize]
        public async Task<IActionResult> GetAllDoctorAppointmentsCompleted(int id)
        {
            var consultasConcluidos = await _consultaService.GetAllDoctorAppointmentsCompleted(id);
            return Ok(consultasConcluidos);
        }

        [HttpGet("medico/{id}/agendados")]
        [Authorize]
        public async Task<IActionResult> GetAllDoctorAppointmentsScheduled(int id)
        {
            var consultasAgendados = await _consultaService.GetAllDoctorAppointmentsScheduled(id);
            return Ok(consultasAgendados);
        }

        [HttpGet("agendados")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> GetAllScheduled()
        {
            var consultasAgendados = await _consultaService.GetAllScheduled();
            return Ok(consultasAgendados);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Update(int id, [FromBody] AgendarConsultaRequestContract model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _consultaService.Update(id, model);
            return Ok(result);
        }

        [HttpPut("concluir/{id}")]
        [Authorize(Policy = "AdminOrMedico")]
        public async Task<IActionResult> SetExamAsCompleted(int id)
        {
            var result = await _consultaService.SetAppointmentAsCompleted(id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Delete(int id)
        {
            await _consultaService.Delete(id);
            return NoContent();
        }
    }
}
