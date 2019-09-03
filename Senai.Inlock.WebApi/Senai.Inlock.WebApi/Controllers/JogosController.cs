using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Inlock.WebApi.Domains;
using Senai.Inlock.WebApi.Repositories;
using Senai.Inlock.WebApi.ViewModels;

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

        [HttpGet("ListarPorPreco")]
        public IActionResult ListarPorPreco()
        {
            return Ok(JogoRepository.ListarPorPreco());
        }

        [HttpGet("ListarPorData")]
        public IActionResult ListarPorData()
        {
            return Ok(JogoRepository.ListarPorData());
        }

        [HttpGet("JogoEstudio")]
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
            Jogos Jogo = JogoRepository.BuscarPorId(id);
            if (Jogo == null)
                return NotFound();
            return Ok(Jogo);
        }

        [HttpGet("BuscarPorNome/{nome}")]
        public IActionResult BuscarPorNome(string nome)
        {
            Jogos Jogo = JogoRepository.BuscarPorNome(nome);
            if (Jogo == null)
                return NotFound();
            return Ok(Jogo);
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

        [HttpGet("TempoParaLancamento")]
        public IActionResult TempoParaLancamento()
        {
            return Ok(JogoRepository.TempoParaLancamento());
        }

    }
}