using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TOTVS.PDV.Calculator.Challenge.Data;
using TOTVS.PDV.Calculator.Challenge.Model;

namespace TOTVS.PDV.Calculator.Challenge.Services
{
    public class PDVCalculadoraService : IPDVCalculadora
    {
        public List<Dinheiro> listaRetorno = new List<Dinheiro>();

        public IRepository<Operacao> _repoOperacao;

        public string mensagemRetorno { get; set; }

        public PDVCalculadoraService(IRepository<Operacao> repo)
        {
            _repoOperacao = repo;
        }

        public List<Dinheiro> ObterTroco(Operacao op)
        {
             double resultadoTroco;

            #region Verificacao Erro

            if (op.ValorTotal <= 0 || op.ValorPago <= 0)
            {
                mensagemRetorno = string.Format("Valores inseridos menor que zero: {0:C2} Valor Pago, {1:C2} Valor Total. Operação não realizada", op.ValorPago, op.ValorTotal);

                return listaRetorno;
            }
            
            op.ValorTroco = op.ValorPago - op.ValorTotal;     

            if(op.ValorTroco < 0) 
            {
                mensagemRetorno = "Valor pago insuficiente. Operação não realizada";
                return listaRetorno;
            }

            #endregion

            if (op.ValorTroco == 0)
            {
                if(_repoOperacao.Registrar(op))

                    mensagemRetorno = "Não há troco para essa operação. Operaçao realizada com sucesso";

                return listaRetorno;

            }

            resultadoTroco = op.ValorTroco;

            if (resultadoTroco >= 10) 
            {
                listaRetorno.AddRange(CalcularNotas(ref resultadoTroco));
            }
            
            if(resultadoTroco < 10)
            {
                listaRetorno.AddRange(CalcularMoedas(ref resultadoTroco));
            }

            if(_repoOperacao.Registrar(op))

                mensagemRetorno = "Operação realizada com sucesso.";
            
            return listaRetorno;
            

        }

        public List<Dinheiro> CalcularNotas(ref double retornoTroco)
        {
            List<Dinheiro> notas = new List<Dinheiro>();
            
            int contaNota = 0;

            if (retornoTroco == 0) return listaRetorno;
       
            if (retornoTroco >= 100)
            {
                for(int i = 0; retornoTroco >= 100; i++) 
                {
                    retornoTroco -= 100;
                    contaNota = i + 1;
                }

                notas.Add(new Nota(contaNota,100));
                contaNota = 0;
            }

            if (retornoTroco >= 50)
            {
                for (int i = 0; retornoTroco >= 50; i++)
                {
                    retornoTroco -= 50;
                    contaNota = i + 1;
                }

                notas.Add(new Nota(contaNota, 50));
                contaNota = 0;

            }

            if (retornoTroco >= 20)
            {
                for (int i = 0; retornoTroco >= 20; i++)
                {
                    retornoTroco -= 20;
                    contaNota = i + 1;
                }
                notas.Add(new Nota(contaNota, 20));
                contaNota = 0;

            }
            
            if (retornoTroco >= 10)
            {
                for (int i = 0; retornoTroco >= 10; i++)
                {
                    retornoTroco -= 10;
                    contaNota = i + 1;
                }
                notas.Add(new Nota(contaNota, 100));
            }

            return notas;

        }

        public List<Dinheiro> CalcularMoedas(ref double retornoTroco)
        {
            List<Dinheiro> moedas = new List<Dinheiro>();
            
            int contaMoeda = 0;

            if (retornoTroco >= 0.50)
            {
                for (int i = 0; retornoTroco >= 0.50; i++)
                {
                    retornoTroco -= 0.50;
                    contaMoeda = i + 1;
                }

                moedas.Add(new Moeda(contaMoeda, 0.50));
                contaMoeda = 0;
            }

            if (retornoTroco >= 0.10)
            {
                for (int i = 0; retornoTroco >= 0.10; i++)
                {
                    retornoTroco -= 0.10;
                    contaMoeda = i + 1;
                }

                moedas.Add(new Moeda(contaMoeda, 0.10));
                contaMoeda = 0;
            }

            if (retornoTroco >= 0.05)
            {
                for (int i = 0; retornoTroco >= 0.05; i++)
                {
                    retornoTroco -= 20;
                    contaMoeda = i + 1;
                }
                moedas.Add(new Moeda(contaMoeda, 0.05));
                contaMoeda = 0;

            }

            if(retornoTroco >= 0.01){ 
                for (int i = 0; retornoTroco >= 0.01; i++)
                {
                    retornoTroco -= 0.01;
                    contaMoeda = i + 1;
                }

                moedas.Add(new Moeda(contaMoeda, 0.01));
            }
            
            return moedas;

        }

        public string MensagemRetorno()
        {

            if (listaRetorno.Any()) 
            {
                StringBuilder retorno = new StringBuilder();

                retorno.Append("Entregar ");

                var listaOrdenadaPorValor = listaRetorno.OrderBy(d => d.Valor).ToList();

                foreach (var d in listaOrdenadaPorValor)
                {

                    retorno.Append(string.Format("{0} {1} de {2:C2} ", d.Quantidade, Enum.GetName(typeof(TipoDinheiro),d.Tipo), d.Valor));
                }

                return retorno.ToString();
            }
            else 
            {
                return mensagemRetorno;
            }
            
        }

    }
}
