using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Uniaraxa.AmigoSecreto.Application.Interfaces;
using Uniaraxa.AmigoSecreto.Core.Entities;
using Uniaraxa.AmigoSecreto.Infrastructure.Repository;

namespace Uniaraxa.AmigoSecreto.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoginController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Login>>> ObterTodos()
        {
            try
            {
                var logins = await _unitOfWork.Login.ObterTodosAsync();
                return Ok(logins);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar logins: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Login>> ObterPorId(long id)
        {
            try
            {
                var login = await _unitOfWork.Login.ObterPorIdAsync(id);
                if (login == null)
                {
                    return NotFound();
                }
                return Ok(login);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar login: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Login>> Inserir(Login login)
        {
            try
            {
                await _unitOfWork.Login.InserirAsync(login);
                return CreatedAtAction(nameof(ObterPorId), new { id = login.Id }, login);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar login: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(long id, Login login)
        {
            try
            {
                if (id != login.Id)
                {
                    return BadRequest("ID do login não corresponde ao ID da requisição");
                }

                await _unitOfWork.Login.AtualizarAsync(login);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar login: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            try
            {
                var loginToDelete = await _unitOfWork.Login.ObterPorIdAsync(id);
                if (loginToDelete == null)
                {
                    return NotFound("Login não encontrado");
                }

                await _unitOfWork.Login.RemoverAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao excluir login: {ex.Message}");
            }
        }
    }
}
