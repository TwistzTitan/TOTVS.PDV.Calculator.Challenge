using System.Linq;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;
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

        
        [HttpGet]

        public ActionResult<HttpStatusCode> Operacoes()
        {
            var ops = _repo.ObterTodos();
            
            return Ok(ops);
        }
        
        [HttpPost]
        public ActionResult<HttpStatusCode> ObterTroco(OperacaoDTO op)
        {
            if (!ModelState.IsValid)
                return BadRequest("Modelo inválido");

            var operacao = Operacao.FromDTO(op);

            var lista = _calculadoraService.ObterTroco(operacao);

            if (lista.Any())
            {
             
                return Created("Operação realizada.", _calculadoraService.MensagemRetorno());
            }

            else
            {

                return NotFound(_calculadoraService.MensagemRetorno());
            
            }
        }

    }
}
