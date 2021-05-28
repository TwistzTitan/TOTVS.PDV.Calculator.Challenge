using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TOTVS.PDV.Calculator.Challenge.Data;
using TOTVS.PDV.Calculator.Challenge.Model;

namespace TOTVS.PDV.Calculator.Challenge.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CalculadoraController : ControllerBase
    {
        private IPDVCalculadora _calculadoraService;

        private IRepository<Operacao> _repo;
        
        public CalculadoraController(IPDVCalculadora calc, IRepository<Operacao> repo) 
        {

            _calculadoraService = calc;
            _repo = repo;
        }

        /// <summary>
        ///  Retornar todas as operações realizadas.
        /// </summary>
        /// <returns>
        /// Lista de operações.
        /// </returns>
        [HttpGet]
        public ActionResult Operacoes()
        {
            var ops = _repo.ObterTodos();

            if (ops.Any())
                return Ok(ops);
            
            return NotFound("Sem operações registradas até o momento");
        }
        
        
        /// <summary>
        ///  Obter o troco de uma operação de valor com gasto do cliente.
        /// </summary>
        /// <param name="op"></param>
        /// <returns>
        /// Retorna as notas ou moedas para o troco da operação.
        /// </returns>
        [HttpPost]
        [Consumes("application/json")]
       
        public ActionResult ObterTroco(OperacaoDTO op)
        {
            if (!ModelState.IsValid)
                return BadRequest("Modelo inválido");

            var operacao = Operacao.FromDTO(op);

            var lista = _calculadoraService.ObterTroco(operacao);

            if (lista.Any())
            {
                
                return Created("PDV Calculadora",_calculadoraService.MensagemRetorno());
            }

            else
            {

                return NotFound(_calculadoraService.MensagemRetorno());
            
            }
        }

    }
}
