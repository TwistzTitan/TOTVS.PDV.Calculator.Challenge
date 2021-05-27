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

        public Dictionary<double, TipoDinheiro> dinheiroDict = new Dictionary<double, TipoDinheiro>();

        public IRepository<Operacao> _repoOperacao;

        public string mensagemRetorno { get; set; }

        public PDVCalculadoraService(IRepository<Operacao> repo)
        {
            _repoOperacao = repo;

            AdicionarCedula();
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


            Calcular(ref resultadoTroco);


            if(_repoOperacao.Registrar(op))

                mensagemRetorno = "Operação realizada com sucesso.";
            
            return listaRetorno;
            

        }

        public List<Dinheiro> Calcular(ref double retornoTroco)
        {
            List<Dinheiro> notas = new List<Dinheiro>();

            dinheiroDict.OrderByDescending(d => d.Key);

            foreach (var d in dinheiroDict) 
            {

                if (retornoTroco >= d.Key)
                {
                    int contaNota = 0;

                    for (int i = 0; retornoTroco >= d.Key; i++)
                    {
                        retornoTroco -= d.Key;
                        contaNota = i + 1;
                    }

                    if (d.Value == TipoDinheiro.Nota)

                        notas.Add(new Nota(contaNota, d.Key));

                    else
                        notas.Add(new Moeda(contaNota, d.Key));

                }

            }

            return notas;

        }

        public void AdicionarCedula() 
        {

            dinheiroDict.Add(100, TipoDinheiro.Nota);
            dinheiroDict.Add(50, TipoDinheiro.Nota);
            dinheiroDict.Add(20, TipoDinheiro.Nota);
            dinheiroDict.Add(0.50, TipoDinheiro.Moeda);
            dinheiroDict.Add(0.10, TipoDinheiro.Moeda);
            dinheiroDict.Add(0.05, TipoDinheiro.Moeda);
            dinheiroDict.Add(0.01, TipoDinheiro.Moeda);

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
