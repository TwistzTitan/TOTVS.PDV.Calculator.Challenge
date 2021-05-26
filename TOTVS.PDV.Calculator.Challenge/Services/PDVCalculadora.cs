using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TOTVS.PDV.Calculator.Challenge.Model;

namespace TOTVS.PDV.Calculator.Challenge.Services
{
    public class PDVCalculadora : IPDVCalculadora
    {
        public List<Dinheiro> listaRetorno = new List<Dinheiro>();
        public List<Dinheiro> ObterTroco(Operacao op)
        {


            if (op.ValorTotal <= 0 || op.ValorPago <= 0)
                return null;
            
            double valorDiferenca = op.ValorPago - op.ValorTotal;

            if (valorDiferenca == 0)

                return listaRetorno;
            
            if (valorDiferenca >= 10) 
            {
                listaRetorno.AddRange(CalcularNotas(op));
            }
            
            if(valorDiferenca < 10)
            {
                listaRetorno.AddRange(CalcularMoedas(op));
            }

            return listaRetorno;
            

        }

        public List<Dinheiro> CalcularNotas(Operacao op)
        {
            List<Dinheiro> listaRetorno = new List<Dinheiro>();
            
            double valorTroco = op.ValorPago - op.ValorTotal;
            
            int contaNota = 0;

            if (valorTroco == 0) return listaRetorno;
       
            if (valorTroco >= 100)
            {
                for(int i = 0; valorTroco >= 100; i++) 
                {
                    valorTroco -= 100;
                    contaNota = i + 1;
                }

                listaRetorno.Add(new Nota(contaNota,100));
                contaNota = 0;
            }

            if (valorTroco >= 50)
            {
                for (int i = 0; valorTroco >= 50; i++)
                {
                    valorTroco -= 50;
                    contaNota = i + 1;
                }

                listaRetorno.Add(new Nota(contaNota, 50));
                contaNota = 0;

            }

            if (valorTroco >= 20)
            {
                for (int i = 0; valorTroco >= 20; i++)
                {
                    valorTroco -= 20;
                    contaNota = i + 1;
                }
                listaRetorno.Add(new Nota(contaNota, 20));
                contaNota = 0;

            }
            
            if (valorTroco >= 10)
            {
                for (int i = 0; valorTroco >= 10; i++)
                {
                    valorTroco -= 10;
                    contaNota = i + 1;
                }
                listaRetorno.Add(new Nota(contaNota, 100));
            }

            return listaRetorno;

        }

        public List<Dinheiro> CalcularMoedas(Operacao op)
        {
            List<Dinheiro> listaRetorno = new List<Dinheiro>();

            double valorTroco = op.ValorPago - op.ValorTotal;

            int contaMoeda = 0;

            if (valorTroco == 0) return listaRetorno;

            if (valorTroco >= 0.50)
            {
                for (int i = 0; valorTroco >= 0.50; i++)
                {
                    valorTroco -= 0.50;
                    contaMoeda = i + 1;
                }

                listaRetorno.Add(new Moeda(contaMoeda, 0.50));
                contaMoeda = 0;
            }

            if (valorTroco >= 0.10)
            {
                for (int i = 0; valorTroco >= 0.10; i++)
                {
                    valorTroco -= 0.10;
                    contaMoeda = i + 1;
                }

                listaRetorno.Add(new Moeda(contaMoeda, 0.10));
                contaMoeda = 0;
            }

            if (valorTroco >= 0.05)
            {
                for (int i = 0; valorTroco >= 0.05; i++)
                {
                    valorTroco -= 20;
                    contaMoeda = i + 1;
                }
                listaRetorno.Add(new Moeda(contaMoeda, 0.05));
                contaMoeda = 0;

            }

            if(valorTroco >= 0.01){ 
                for (int i = 0; valorTroco >= 0.01; i++)
                {
                    valorTroco -= 0.01;
                    contaMoeda = i + 1;
                }

                listaRetorno.Add(new Moeda(contaMoeda, 0.01));
            }
            
            return listaRetorno;

        }

    }
}
