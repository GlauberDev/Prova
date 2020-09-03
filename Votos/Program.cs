using System;

namespace Votos
{
    class Program
    {
        static void Main(string[] args)
        {
            Votacao votacao = new Votacao(500000, 100000, 50000, 700000);

            Console.WriteLine(string.Format("Votos válidos do total de eleitores: {0:0.00}%", votacao.PercentualValidosDoTotal));
            Console.WriteLine(string.Format("Votos brancos e nulo do total de eleitores: {0:0.00}%", votacao.PercentualBrancosENulosDoTotal));
            Console.WriteLine(string.Format("Eleitores que se abstiveram: {0:0.00}%", votacao.EleitoresAbstentes));
        }
    }
    class Votacao
    {
        public decimal VotosValidos { get; set; }
        public decimal VotosBrancos { get; set; }
        public decimal VotosNulos { get; set; }
        public decimal TotalEleitores { get; set; }

        public Votacao(decimal votosValidos, decimal votosBrancos, decimal votosNulos, decimal totalEleitores)
        {
            VotosValidos = votosValidos;
            VotosBrancos = votosBrancos;
            VotosNulos = votosNulos;
            TotalEleitores = totalEleitores;
        }

        public decimal PercentualValidosDoTotal
        {
            get
            {
                return VotosValidos * 100 / TotalEleitores;
            }
        }
        public decimal PercentualBrancosENulosDoTotal
        {
            get
            {
                return (VotosBrancos + VotosNulos) * 100 / TotalEleitores;
            }
        }
        public decimal EleitoresAbstentes
        {
            get
            {
                return (TotalEleitores - VotosValidos - VotosBrancos - VotosNulos) * 100 / TotalEleitores;
            }
        }

    }


}
