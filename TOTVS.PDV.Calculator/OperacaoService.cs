using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using TOTVS.PDV.Calculator.Challenge.Model;
using TOTVS.PDV.Calculator.Challenge.Services;
using TOTVS.PDV.Calculator.Tests.Fixtures;
using System.Linq;
using FluentAssertions;

namespace TOTVS.PDV.Calculator
{
    [TestClass]
    public class OperacaoService
    {
        public Mock<IPDVCalculadora> calculadoraTest;
        public OperacaoFixtures operacaoFixtures;

        [TestInitialize]
        public void Inicializa()
        {
            calculadoraTest = new Mock<IPDVCalculadora>();
            operacaoFixtures = new OperacaoFixtures();
        }
        
        [TestMethod]
        public void Obter_Troco_Em_Moedas()
        {
            Operacao opTest = operacaoFixtures.Cria_Operacao_Correta_Com_Troco_Moeda();

            IPDVCalculadora calc = new PDVCalculadora();

            List<Dinheiro> listaMoeda =  calc.ObterTroco(opTest);

            Assert.IsTrue(listaMoeda.Any());
            Assert.IsTrue(listaMoeda.All( m => m.GetType() == typeof(Moeda)));
            Assert.IsTrue(listaMoeda.All(m => m.Quantidade > 0));
            Assert.IsTrue(listaMoeda.All(m => m.Valor > 0 && m.Valor < 1));
        }

        public void Obter_Troco_Em_Notas() 
        { 
        
        }

        public void Obter_Troco_Em_Notas_E_Moedas() 
        { 
        
        }

        public void Nao_Obter_Troco() 
        { 
        
        }

    }
}
