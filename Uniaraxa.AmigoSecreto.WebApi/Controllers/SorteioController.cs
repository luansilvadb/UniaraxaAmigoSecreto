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
    public class SorteioController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public SorteioController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sorteio>>> ObterTodos()
        {
            try
            {
                var sorteios = await _unitOfWork.Sorteio.ObterTodosAsync();
                return Ok(sorteios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar sorteios: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sorteio>> ObterPorId(long id)
        {
            try
            {
                var sorteio = await _unitOfWork.Sorteio.ObterPorIdAsync(id);
                if (sorteio == null)
                {
                    return NotFound();
                }
                return Ok(sorteio);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar sorteio: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Sorteio>> Inserir()
        {
            try
            {
                // Criar um novo objeto Sorteio
                var sorteio = new Sorteio();

                // Inserir o sorteio e obter o ID gerado
                var idSorteio = await _unitOfWork.Sorteio.InserirAsync(sorteio);

                // Definir o ID gerado no objeto sorteio
                sorteio.Id = idSorteio;

                // Retornar uma resposta 200 OK com o objeto sorteio
                return Ok("Sorteio criado com sucesso e convite foi enviado por e-mail.");
            }
            catch (Exception ex)
            {
                // Em caso de erro, retornar uma resposta 500 Internal Server Error com a mensagem de erro
                return StatusCode(500, $"Erro ao criar sorteio: {ex.Message}");
            }
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(long id, Sorteio sorteio)
        {
            try
            {
                if (id != sorteio.Id)
                {
                    return BadRequest("ID do sorteio não corresponde ao ID da requisição");
                }

                await _unitOfWork.Sorteio.AtualizarAsync(sorteio);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar sorteio: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            try
            {
                var sorteioToDelete = await _unitOfWork.Sorteio.ObterPorIdAsync(id);
                if (sorteioToDelete == null)
                {
                    return NotFound("Sorteio não encontrado");
                }

                await _unitOfWork.Sorteio.RemoverAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao excluir sorteio: {ex.Message}");
            }
        }
    }
}
