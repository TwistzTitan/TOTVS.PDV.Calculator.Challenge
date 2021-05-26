

using System.ComponentModel.DataAnnotations;

namespace TOTVS.PDV.Calculator.Challenge.Model
{
    public class Operacao
    {
        public int OperacaoId { get; set; }
        
        public double ValorTotal { get; set; }
        
        public double ValorPago { get; set ; }
        
        public double ValorTroco { get; set; }

        public string NomeOperador { get; set; }

        public static Operacao FromDTO (OperacaoDTO dto) 
        {
            return new Operacao() { 
                ValorPago = dto.ValorPago, 
                ValorTotal = dto.ValorTotal, 
                NomeOperador = dto.NomeOperador 
            };
        }
    }


    public class OperacaoDTO 
    {
        [Required]
        public double ValorTotal { get; set; }

        [Required]
        public double ValorPago { get; set; }

        [StringLength(60)]
        public string NomeOperador { get; set; }

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
