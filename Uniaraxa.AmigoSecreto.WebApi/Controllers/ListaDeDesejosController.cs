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
    public class ListaDeDesejosController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ListaDeDesejosController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListaDeDesejos>>> ObterTodos()
        {
            try
            {
                var listasDeDesejos = await _unitOfWork.ListaDeDesejos.ObterTodosAsync();
                return Ok(listasDeDesejos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar listas de desejos: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ListaDeDesejos>> ObterPorId(long id)
        {
            try
            {
                var listaDeDesejos = await _unitOfWork.ListaDeDesejos.ObterPorIdAsync(id);
                if (listaDeDesejos == null)
                {
                    return NotFound();
                }
                return Ok(listaDeDesejos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar lista de desejos: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ListaDeDesejos>> Inserir(ListaDeDesejos listaDeDesejos)
        {
            try
            {
                await _unitOfWork.ListaDeDesejos.InserirAsync(listaDeDesejos);
                return CreatedAtAction(nameof(ObterPorId), new { id = listaDeDesejos.Id }, listaDeDesejos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar lista de desejos: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(long id, ListaDeDesejos listaDeDesejos)
        {
            try
            {
                if (id != listaDeDesejos.Id)
                {
                    return BadRequest("ID da lista de desejos não corresponde ao ID da requisição");
                }

                await _unitOfWork.ListaDeDesejos.AtualizarAsync(listaDeDesejos);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar lista de desejos: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            try
            {
                var listaDeDesejosToDelete = await _unitOfWork.ListaDeDesejos.ObterPorIdAsync(id);
                if (listaDeDesejosToDelete == null)
                {
                    return NotFound("Lista de desejos não encontrada");
                }

                await _unitOfWork.ListaDeDesejos.RemoverAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao excluir lista de desejos: {ex.Message}");
            }
        }
    }
}
