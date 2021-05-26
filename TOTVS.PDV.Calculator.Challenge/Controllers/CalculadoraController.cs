using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using TOTVS.PDV.Calculator.Challenge.Model;

namespace TOTVS.PDV.Calculator.Challenge.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CalculadoraController : ControllerBase
    {
        private IPDVCalculadora _calculadoraService;
        public CalculadoraController(IPDVCalculadora calc) 
        {

            _calculadoraService = calc;

        }

        [HttpPost]
        public ActionResult ObterTroco(OperacaoDTO op)
        {
            if (!ModelState.IsValid)
                return BadRequest("Modelo inválido");

            var operacao = Operacao.FromDTO(op);

            var lista = _calculadoraService.ObterTroco(operacao);

            if (lista.Any())
            {
                StringBuilder retorno = new StringBuilder();

                retorno.Append("Entregar ");

                var listaOrdenadaPorValor = lista.OrderBy(d => d.Valor).ToList();
                
                foreach( var d in listaOrdenadaPorValor)
                {   

                    retorno.Append(string.Format("{0} nota de {1:C2}", d.Quantidade, d.Valor));
                }

                return Created("Operacao Realizada",retorno.ToString());
            }

            else
                return NotFound();
        }
    }
}
