using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TOTVS.PDV.Calculator.Challenge.Model
{
    public class Operacao
    {
        public int? OperacaoId;

        public double ValorTotal;

        public double ValorPago;

        public string NomeOperador; 
    }


    public abstract class Dinheiro 
    {
        public int Quantidade;

        public double Valor;

        public TipoDinheiro Tipo;
    }

    public enum TipoDinheiro 
    {
        Nota = 1,
        Moeda = 2, 
    }

    public class Moeda : Dinheiro
    {   
        public Moeda (int? quantidade, double valor = 0) 
        {
            Quantidade = quantidade.GetValueOrDefault();

            Valor = valor;

            Tipo = TipoDinheiro.Moeda;
        }
    }

    public class Nota : Dinheiro
    {
        public Nota(int? quantidade, double valor = 0)
        {
            Quantidade = quantidade.GetValueOrDefault();

            Valor = valor;

            Tipo = TipoDinheiro.Nota;
        }

    } 
}
