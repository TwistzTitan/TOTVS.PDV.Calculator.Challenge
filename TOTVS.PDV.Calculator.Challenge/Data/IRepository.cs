using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TOTVS.PDV.Calculator.Challenge.Data
{
    public interface IRepository<T>
    {
        List<T> FindAll();

    }
}
