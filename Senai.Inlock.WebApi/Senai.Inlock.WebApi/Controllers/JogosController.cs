using System;
using System.Collections.Generic;
using System.Linq;
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
    [Authorize]
    public class JogosController : ControllerBase
    {
        JogoRepository JogoRepository = new JogoRepository();

        
        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(JogoRepository.Listar());
        }

        [HttpGet("{JogoEstudio}")]
        public IActionResult JogoEstudio()
        {
            return Ok(JogoRepository.JogoEstudio());
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost]
        public IActionResult Cadastrar(Jogos jogo)
        {
            try
            {
                JogoRepository.Cadastrar(jogo);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Ih, deu erro." + ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult BuscaPorId(int id)
        {
            Jogos Cargo = JogoRepository.BuscarPorId(id);
            if (Cargo == null)
                return NotFound();
            return Ok(Cargo);
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPut("{id}")]
        public IActionResult Atualizar(Jogos jogo, int id)
        {
            try
            {
                Jogos JogoBuscado = JogoRepository.BuscarPorId
                    (id);
                if (JogoBuscado == null)
                    return NotFound();

                jogo.JogoId = id;
                JogoRepository.Atualizar(jogo);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            JogoRepository.Deletar(id);
            return Ok();
        }
    }
}