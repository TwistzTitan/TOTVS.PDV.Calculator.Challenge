using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using TOTVS.PDV.Calculator.Challenge.Model;
using TOTVS.PDV.Calculator.Challenge.Services;
using TOTVS.PDV.Calculator.Tests.Fixtures;
using System.Linq;


namespace TOTVS.PDV.Calculator
{
    [TestClass]
    public class CalculadoraService
    {
        public Mock<IPDVCalculadora> calculadoraTest;
        public OperacaoFixtures operacaoFixtures;
        public IPDVCalculadora calculadora;
        [TestInitialize]
        public void Inicializa()
        {
            calculadoraTest = new Mock<IPDVCalculadora>();
            operacaoFixtures = new OperacaoFixtures();
            calculadora = new PDVCalculadoraService();
        }

        [TestMethod]
        public void Calcular_Troco_Para_Moedas()
        {
            Operacao opTest = operacaoFixtures.Cria_Operacao_Correta_Com_Troco_Moeda();

            double compareTroco = opTest.ValorTroco;

            double trocoTest = opTest.ValorTroco;

            List<Dinheiro> listaMoeda = calculadora.CalcularMoedas(ref trocoTest);

            Assert.IsTrue(listaMoeda.Any());
            Assert.IsTrue(compareTroco > trocoTest);
            Assert.IsTrue(listaMoeda.All(m => m.GetType() == typeof(Moeda)));
            Assert.IsTrue(listaMoeda.All(m => m.Quantidade > 0));
            Assert.IsTrue(listaMoeda.All(m => m.Valor > 0 && m.Valor < 1));
            Assert.IsNotNull(opTest.NomeOperador);
        }

        [TestMethod]
        public void Calcular_Troco_Para_Notas()
        {
            Operacao opTest = operacaoFixtures.Cria_Operacao_Correta_Com_Troco_Nota();

            double compareTroco = opTest.ValorTroco;
            
            double trocoTest = opTest.ValorTroco;

            List<Dinheiro> listaNotas = calculadora.CalcularNotas(ref trocoTest);

            Assert.IsTrue(listaNotas.Any());
            Assert.IsTrue(compareTroco > trocoTest);
            Assert.IsTrue(listaNotas.All(m => m.GetType() == typeof(Nota)));
            Assert.IsTrue(listaNotas.All(m => m.Quantidade > 0));
            Assert.IsTrue(listaNotas.All(m => m.Valor > 10));
            Assert.IsNotNull(opTest.NomeOperador);
        }

        [TestMethod]
        public void Nao_Calcular_Troco_Quando_O_Valor_Total_E_Menor_Que_Zero() 
        {
            Operacao opTest = operacaoFixtures.ComNomeOperador("Joao").ComValorPago(100).ComValorTotal(-10).Build();

            List<Dinheiro> listaSemTroco = calculadora.ObterTroco(opTest);

            Assert.AreEqual(listaSemTroco.Count(),0);

        }
        [TestMethod]
        public void Nao_Calcular_Troco_Quando_O_Valor_Pago_E_Menor_Que_Zero() 
        {
            Operacao opTest = operacaoFixtures.ComNomeOperador("Gisele").ComValorPago(-17).ComValorTotal(1000).Build();

            List<Dinheiro> listaSemTroco = calculadora.ObterTroco(opTest);
            
            Assert.AreEqual(listaSemTroco.Count(),0);

            Assert.IsTrue(calculadora.MensagemRetorno().Contains("Operação não realizada"));
        }

        [TestMethod]
        public void Nao_Calcular_Troco_Quando_O_Valor_Pago_E_Insuficiente()
        {
            Operacao opTest = operacaoFixtures.ComNomeOperador("Gisele").ComValorPago(100).ComValorTotal(1000).Build();

            List<Dinheiro> listaSemTroco = calculadora.ObterTroco(opTest);

            Assert.AreEqual(listaSemTroco.Count(), 0);

            Assert.IsTrue(calculadora.MensagemRetorno().Contains("Operação não realizada"));

        }

        [TestMethod]
        public void Retornar_Padrao_Quando_Nao_Tem_Troco()
        {

            Operacao opTest = operacaoFixtures.ComNomeOperador("Janice").ComValorPago(1000).ComValorTotal(1000).Build();

            List<Dinheiro> listaPadrao = calculadora.ObterTroco(opTest);
            
            Assert.AreEqual(listaPadrao.Count,0);

            Assert.IsTrue(calculadora.MensagemRetorno().Contains("Não há troco para essa operação"));


        }

        [TestMethod]
        public void Obter_Troco_Com_Notas_E_Moedas()
        {

            Operacao opTest = operacaoFixtures.ComNomeOperador("Jefferson").ComValorPago(25.78).ComValorTotal(10).Build();

            List<Dinheiro> listaPadrao = calculadora.ObterTroco(opTest);
            
            Assert.IsTrue(listaPadrao.Any());
            
            Assert.IsTrue(listaPadrao.Select(d => d.Tipo == TipoDinheiro.Moeda ).Count() > 0);
            
            Assert.IsTrue(listaPadrao.Select(d => d.Tipo == TipoDinheiro.Nota).Count() > 0);

        }
    }
}
