using System;
using System.Text;

namespace Vetor
{
    class Program
    {
        static void Main(string[] args)
        {
            //Todos os elementos do vetor principal são implementados como arrays, que podem ter um só elemento.
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

        /// <summary>
        /// Ordena o vetor principal através do Bubble Sort
        /// </summary>
        static void OrdenarVetorPai(ref double[][] vetorPai)
        {
            //Primeiro ordena internamente os vetores filhos
            for (int i = 0; i < vetorPai.Length; i++)
            {
                OrdenarVetorFilho(ref vetorPai[i]);
            }

            //Finalmente, ordena o vetor pai, usando o primeiro elemento de cada vetor filho como base
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

        /// <summary>
        /// Ordena os vetores contidos no vetor principal através do Bubble Sort
        /// </summary>
        static void OrdenarVetorFilho(ref double[] vetor)
        {
            double aux;
            for (int i = vetor.Length - 1; i > 0; i--)//Se o vetor só tiver um elemento, "i" será 0 ...
            {
                for (int j = 0; j < i; j++)//...e este For nem será executado, pois "j" nunca será menor que "i = 0"
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

        /// <summary>
        /// Monta string formatada que exibe o vetor
        /// </summary>
        static string VetorToString(double[][] vetorPai)
        {
            StringBuilder strPai = new StringBuilder(); //String para o vetor pai
            StringBuilder strFilho; //String para o vetor filho
            double[] vetorFilho;

            strPai.Append("V = [");
            if (vetorPai.Length > 0)
            {
                for (int i = 0; i < vetorPai.Length; i++)//para cada vetor filho
                {
                    vetorFilho = vetorPai[i];//o vetor filho é o elemento "i" do vetor pai
                    if (vetorFilho.Length > 1)//Se filho tiver mais de um elemento, repetir para ele PROCESSO SEMELHANTE ao de impressão do pai ...;[X, Y, Z];...
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
                    else//Se filho não tem mais de um elemento, apenas imprimir seu valor dentro do pai ...;X ;...
                    {
                        strPai.Append(string.Format("{0:0.0}", vetorPai[i][0]));
                    }
                    strPai.Append("; ");//Separador (vírgula e espaço) sempre adicionado ao final de cada elemento; o último será removido antes do fechamento da string
                }
                strPai.Remove(strPai.Length - 2, 2); //Último separador removido
            }
            strPai.Append("]");

            return strPai.ToString();
        }

    }
}
