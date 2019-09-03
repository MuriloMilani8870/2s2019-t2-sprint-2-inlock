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

        /// <summary>
        /// Método que lista todos os estudios
        /// </summary>
        /// <returns>Lista de Estudios</returns>
        [HttpGet]
        public IActionResult ListarTodos()
        {
            return Ok(EstudioRepository.Listar());
        }

        /// <summary>
        /// Método que Cadastra os estudios
        /// </summary>
        /// <param name="estudio"></param>
        /// <returns>Um novo estudio cadastrado</returns>
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

        /// <summary>
        /// Método que altera o nome do estúdio
        /// </summary>
        /// <param name="estudio"></param>
        /// <param name="id"></param>
        /// <returns>Estúdio alterado</returns>
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

        /// <summary>
        /// Método que deleta um estúdio
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Estúdio deletado</returns>
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            EstudioRepository.Deletar(id);
            return Ok();
        }

        /// <summary>
        /// Método que busca o estúdio pelo país
        /// </summary>
        /// <param name="pais"></param>
        /// <returns>Um estúdio que foi buscado pelo país</returns>
        [HttpGet("BuscarPorPais/{Pais}")]
        public IActionResult BuscarPorPais(string pais)
        {
            List<Estudios> Estudios = EstudioRepository.BuscarPorPais(pais);
            if (Estudios == null)
                return NotFound();
            return Ok(Estudios);
        }

        /// <summary>
        /// Método que busca todos os estúdios de um determinado país
        /// </summary>
        /// <param name="pais"></param>
        /// <returns>Lista de estúdios que condizem com o mesmo país</returns>
        [HttpGet("BuscarTodosPorPais/{Pais}")]
        public IActionResult BuscarTodosPorPais(string pais)
        {
            List<Estudios> Estudios = EstudioRepository.BuscarTodosPorPais(pais);
            if (Estudios == null)
                return NotFound();
            return Ok(Estudios);
        }

        /// <summary>
        /// Busca o estúdio pelo Id do usuário 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Lista de estúdios cadastrados por um determinado usuario</returns>
        [HttpGet("BuscarPorUsuario/{id}")]
        public IActionResult BuscarPorUsuario(int id)
        {
            List<Estudios> Estudios = EstudioRepository.BuscarPorUsuario(id);
            if (Estudios == null)
                return NotFound();
            return Ok(Estudios);
        }

        /// <summary>
        /// Busca uma lista de estúdios procurando por um determinado nome
        /// </summary>
        /// <param name="nome"></param>
        /// <returns>Lista de estúdios de um determinado nome</returns>
        [HttpGet("BuscarPorNome/{nome}")]
        public IActionResult BuscarPorNome(string nome)
        {
            List<Jogos> Jogos = EstudioRepository.JogosPorNomeEstudio(nome);
            if (Jogos == null)
                return NotFound();
            return Ok(Jogos);
        }

        /// <summary>
        /// Busca todos os jogos de todos os estúdios
        /// </summary>
        /// <returns>Lista de jogos de cada estúdio </returns>
        [HttpGet("BuscarTodos")]
        public IActionResult BuscarTodos()
        {
            List<Estudios> Estudios = EstudioRepository.BuscarTodos();
            if (Estudios == null)
                return NotFound();
            return Ok(Estudios);
        }

        [HttpGet("BuscarRecentes")]
        public IActionResult BuscarRecentes()
        {
            List<Estudios> Estudios = EstudioRepository.BuscarRecentes();
            if (Estudios == null)
                return NotFound();
            return Ok(Estudios);
        }

    }
}