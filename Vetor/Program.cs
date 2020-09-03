using System;
using System.Text;

namespace Vetor
{
    class Program
    {
        static void Main(string[] args)
        {
            double[][] vetor = new double[][]{
                new double[] { 5 },
                new double[] { 7 },
                new double[] { 0 },
                new double[] { 0.1 },
                new double[] { 4, 7, 9, 0 },
                new double[] { 1 },
                new double[] { 2 },
                new double[] { 90, 9, 7, 3 },
                new double[] { 9.7 },
            };
            OrdenarVetorPai(ref vetor);

            Console.WriteLine(VetorToString(vetor));
        }

        static void OrdenarVetorPai(ref double[][] vetorPai)
        {
            for (int i = 0; i < vetorPai.Length; i++)//foreach (var vetorFIlho in vetorPai)
            {
                OrdenarVetorFilho(ref vetorPai[i]);
            }

            double[] aux;
            for (int i = vetorPai.Length - 1; i > 0; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    if (vetorPai[j][0] > vetorPai[j + 1][0])
                    {
                        aux = vetorPai[j];
                        vetorPai[j] = vetorPai[j + 1];
                        vetorPai[j + 1] = aux;
                    }
                }
            }
        }

        static void OrdenarVetorFilho(ref double[] vetor)
        {
            double aux;
            for (int i = vetor.Length - 1; i > 0; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    if (vetor[j] > vetor[j + 1])
                    {
                        aux = vetor[j];
                        vetor[j] = vetor[j + 1];
                        vetor[j + 1] = aux;
                    }
                }
            }
        }

        static string VetorToString(double[][] vetorPai)
        {
            StringBuilder strPai = new StringBuilder();
            StringBuilder strFilho;
            double[] vetorFilho;

            strPai.Append("V = [");
            if (vetorPai.Length > 0)
            {
                for (int i = 0; i < vetorPai.Length; i++)
                {
                    vetorFilho = vetorPai[i];
                    if (vetorFilho.Length > 1)
                    {
                        strFilho = new StringBuilder();
                        strFilho.Append("[");
                        for (int j = 0; j < vetorFilho.Length; j++)
                        {
                            strFilho.Append(string.Format("{0:0.0}", vetorFilho[j]));
                            strFilho.Append("; ");
                        }
                        strFilho.Remove(strFilho.Length - 2, 2);
                        strFilho.Append("]");
                        strPai.Append(strFilho);
                    }
                    else
                    {
                        strPai.Append(string.Format("{0:0.0}", vetorPai[i][0]));
                    }
                    strPai.Append("; ");
                }
                strPai.Remove(strPai.Length - 2, 2); //Remove vírgula e espaço finais que não serão usados devido ao encerramento do vetor
            }
            strPai.Append("]");

            return strPai.ToString();
        }

    }
}
