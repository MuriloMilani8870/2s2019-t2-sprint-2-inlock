using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Inlock.WebApi.Domains;
using Senai.Inlock.WebApi.Repositories;

namespace Senai.Inlock.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize(Roles = "ADMINISTRADOR")]
    public class EstudiosController : ControllerBase
    {
        EstudioRepository EstudioRepository = new EstudioRepository();

        [HttpGet]
        public IActionResult ListarTodos()
        {
            return Ok(EstudioRepository.Listar());
        }

        [HttpPost]
        public IActionResult Cadastrar(Estudios estudio)
        {
            string EmailUsuario = User.FindFirst(ClaimTypes.Email)?.Value;
            string PermissaoUsuario = User.FindFirst(ClaimTypes.Role)?.Value;
            string IdUsuario = User.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
            int IntId = int.Parse(IdUsuario);

            estudio.UsuarioId = IntId;
          
            try
            {
                EstudioRepository.Cadastrar(estudio);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Ih, deu erro." + ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(Estudios estudio, int id)
        {
            try
            {
                Estudios JogoBuscado = EstudioRepository.BuscarPorId
                    (id);
                if (JogoBuscado == null)
                    return NotFound();

                estudio.EstudioId = id;
                EstudioRepository.Atualizar(estudio);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            EstudioRepository.Deletar(id);
            return Ok();
        }
    }
}