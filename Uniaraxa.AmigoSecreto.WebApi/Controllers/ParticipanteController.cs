using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Uniaraxa.AmigoSecreto.Application.Interfaces;
using Uniaraxa.AmigoSecreto.Core.Entities;

namespace Uniaraxa.AmigoSecreto.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipanteController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ParticipanteController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Participante>>> ObterTodos()
        {
            try
            {
                var participantes = await _unitOfWork.Participante.ObterTodosAsync();
                return Ok(participantes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar participantes: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Participante>> ObterPorId(long id)
        {
            try
            {
                var participante = await _unitOfWork.Participante.ObterPorIdAsync(id);
                if (participante == null)
                {
                    return NotFound();
                }
                return Ok(participante);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar participante: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Participante>> Inserir(Participante participante)
        {
            try
            {
                await _unitOfWork.Participante.InserirAsync(participante);
                return CreatedAtAction(nameof(ObterPorId), new { id = participante.Id }, participante);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar participante: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(long id, Participante participante)
        {
            try
            {
                if (id != participante.Id)
                {
                    return BadRequest("ID do participante não corresponde ao ID da requisição");
                }

                await _unitOfWork.Participante.AtualizarAsync(participante);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar participante: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            try
            {
                var participanteToDelete = await _unitOfWork.Participante.ObterPorIdAsync(id);
                if (participanteToDelete == null)
                {
                    return NotFound("Participante não encontrado");
                }

                await _unitOfWork.Participante.RemoverAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao excluir participante: {ex.Message}");
            }
        }
    }
}
