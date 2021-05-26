using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TOTVS.PDV.Calculator.Challenge.Model
{
    public interface IPDVCalculadora
    {
        public List<Dinheiro> ObterTroco(Operacao op);
    
    }
}
