using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
using TOTVS.PDV.Calculator.Challenge.Model;

namespace TOTVS.PDV.Calculator.Tests.Fixtures
{
    public class OperacaoFixtures
    {
        public Operacao operacao;


        public OperacaoFixtures()
        {
            operacao = new Operacao();
        }
        
        public Operacao Cria_Operacao_Correta_Com_Troco_Moeda()
        {
            operacao = new Faker<Operacao>()
                    .RuleFor(op => op.NomeOperador, set => set.Name.FirstName())
                    .RuleFor(op => op.OperacaoId, set => set.Random.Number(10000, 30000))
                    .RuleFor(op => op.ValorTotal, set => set.Random.Number(0, 10))
                    .RuleFor(op => op.ValorPago, (set, op) => op.ValorTotal + set.Random.Number(1,9))
                    .Generate();

            return operacao;
        }

        public Operacao Cria_Operacao_Correta_Com_Troco_Nota()
        {
            operacao = new Faker<Operacao>()
                .RuleFor(op => op.NomeOperador, set => set.Name.FirstName())
                .RuleFor(op => op.OperacaoId, set => set.Random.Number(10000, 30000))
                .RuleFor(op => op.ValorTotal, set => set.Random.Number(10, 100000))
                .RuleFor(op => op.ValorPago, (set, op) => op.ValorTotal + set.Random.Number(1, 1000))
                .Generate();

            return operacao;
        }

        public Operacao ComValorPago(double valor) 
        {
            this.operacao.ValorPago = valor;

            return operacao;
        }

        public Operacao ComValorTotal(double valor)
        {
            this.operacao.ValorTotal = valor;
            return operacao;
        }

        public Operacao ComNomeOperador(string nome)
        {
            this.operacao.NomeOperador = nome;
            return operacao;
        }
        public Operacao Build()
        {
            return this.operacao;
        }


    }
}
