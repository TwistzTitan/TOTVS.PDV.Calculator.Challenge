using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TOTVS.PDV.Calculator.Challenge.Model
{
    public class PDVCalculadora
    {

        public int PDVCalculadoraId { get; set; }

        public List<Dinheiro> RetornoOperacao { get; set; }

        public List<Operacao> Operacoes { get; set; }

    }
}
