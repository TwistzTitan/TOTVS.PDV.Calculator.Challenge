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

        [TestInitialize]
        public void Inicializa()
        {
            calculadoraTest = new Mock<IPDVCalculadora>();
            operacaoFixtures = new OperacaoFixtures();
        }
        
        [TestMethod]
        public void Calcular_Troco_Para_Moedas()
        {
            Operacao opTest = operacaoFixtures.Cria_Operacao_Correta_Com_Troco_Moeda();

            IPDVCalculadora calc = new PDVCalculadora();

            List<Dinheiro> listaMoeda =  calc.CalcularMoedas(opTest);

            Assert.IsTrue(listaMoeda.Any());
            Assert.IsTrue(listaMoeda.All( m => m.GetType() == typeof(Moeda)));
            Assert.IsTrue(listaMoeda.All(m => m.Quantidade > 0));
            Assert.IsTrue(listaMoeda.All(m => m.Valor > 0 && m.Valor < 1));
            Assert.IsNotNull(opTest.NomeOperador);
        }

        [TestMethod]
        public void Calcular_Troco_Para_Notas() 
        {
            Operacao opTest = operacaoFixtures.Cria_Operacao_Correta_Com_Troco_Nota();

            IPDVCalculadora calc = new PDVCalculadora();

            List<Dinheiro> listaMoeda = calc.CalcularNotas(opTest);

            Assert.IsTrue(listaMoeda.Any());
            Assert.IsTrue(listaMoeda.All(m => m.GetType() == typeof(Nota)));
            Assert.IsTrue(listaMoeda.All(m => m.Quantidade > 0));
            Assert.IsTrue(listaMoeda.All(m => m.Valor > 10));
            Assert.IsNotNull(opTest.NomeOperador);
        }

        public void Nao_Calcular_Troco_Quando_O_Valor_Total_E_Menor_Que_Zero() 
        { 
            
        }

        public void Nao_Calcular_Troco_Quando_O_Valor_Pago_E_Menor_Que_Zero() 
        {
        
        }
        public void Retornar_Operacao_Default_Quando_Nao_Tem_Troco()
        {


        }


    }
}
