using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TOTVS.PDV.Calculator.Challenge.Model;

namespace TOTVS.PDV.Calculator.Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculadoraController : ControllerBase
    {
        private IPDVCalculadora _calculadoraService;

        public CalculadoraController(IPDVCalculadora calc) 
        {

            _calculadoraService = calc;
        }


        [HttpPost]
        public ActionResult ObterTroco(Operacao op)
        {
            
            _calculadoraService.ObterTroco(op);

            return Ok();
        }
    }
}
