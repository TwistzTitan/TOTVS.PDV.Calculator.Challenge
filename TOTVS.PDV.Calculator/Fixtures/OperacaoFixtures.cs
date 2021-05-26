using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
using TOTVS.PDV.Calculator.Challenge.Model;

namespace TOTVS.PDV.Calculator.Tests.Fixtures
{
    public class OperacaoFixtures
    {

        public Operacao Cria_Operacao_Correta_Com_Troco_Moeda()
        {
            var op = new Faker<Operacao>()
                    .RuleFor(op => op.NomeOperador, set => set.Name.FirstName())
                    .RuleFor(op => op.OperacaoId, set => set.Random.Number(10000, 30000))
                    .RuleFor(op => op.ValorTotal, set => set.Random.Number(0, 10))
                    .RuleFor(op => op.ValorPago, (set, op) => op.ValorTotal + set.Random.Number(1,9))
                    .Generate();

            return op;
        }

    }
}
