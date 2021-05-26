using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TOTVS.PDV.Calculator.Challenge.Model
{
    public interface IPDVCalculadora
    {
        public List<Dinheiro> ObterTroco(Operacao op);

        public List<Dinheiro> CalcularNotas(ref double valor);

        public List<Dinheiro> CalcularMoedas(ref double valor);

        public string MensagemRetorno ();
    
    }
}
