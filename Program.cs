using System;
using System.IO;

class Program
{
    static string arqDatEleicao = "eleicao in.txt";
    static string arqOutEleicao = "eleicao out.txt";

    static bool veriExistArq()
    {
        if (File.Exists(arqDatEleicao))
        {
            return true;
        }
        else
        {
            Console.WriteLine("Erro ao ler o arquivo!");
            return false;
        }
    }

    static void exibirResultado(int[] votos, int candidato1, int candidato2, StreamWriter sw)
    {
        int votosCandidato1 = 0, votosCandidato2 = 0, votosBrancos = 0, votosNulos = 0;

        for (int i = 0; i < votos.Length; i++)
        {
            int voto = votos[i];
            if (voto == candidato1)
            {
                votosCandidato1++;
            }
            else if (voto == candidato2)
            {
                votosCandidato2++;
            }
            else if (voto == 0)
            {
                votosBrancos++;
            }
            else
            {
                votosNulos++;
            }
        }

        string resultado = "Candidato " + candidato1 + ": " + votosCandidato1 + " votos\n" +
                           "Candidato " + candidato2 + ": " + votosCandidato2 + " votos\n" +
                           "Votos em branco: " + votosBrancos + "\n" +
                           "Votos nulos: " + votosNulos + "\n";

        Console.WriteLine(resultado);
        sw.WriteLine(resultado);

        if (votosCandidato1 > votosCandidato2)
        {
            resultado = "Candidato " + candidato1 + " venceu a eleição!\n";
        }
        else if (votosCandidato2 > votosCandidato1)
        {
            resultado = "Candidato " + candidato2 + " venceu a eleição!\n";
        }
        else
        {
            resultado = "Houve um empate!\n";
        }

        Console.WriteLine(resultado);
        sw.WriteLine(resultado);
    }

    static void lerVotos(int quantE, int candidato1, int candidato2, ref int[] votos, ref bool[] situacao)
    {
        using (StreamReader sr = new StreamReader(arqDatEleicao))
        {
            // Pular as linhas até encontrar o número de eleitores válido
            string linha;
            while ((linha = sr.ReadLine()) != null)
            {
                int numeroEleitores = Convert.ToInt32(linha);
                if (numeroEleitores >= 10)
                {
                    break;
                }
            }

            // Pular as linhas dos dados dos eleitores e candidatos
            for (int j = 0; j < quantE * 4 + 2; j++)
            {
                sr.ReadLine();
            }

            // Ler os votos dos eleitores
            for (int k = 0; k < quantE; k++)
            {
                votos[k] = Convert.ToInt32(sr.ReadLine());
                situacao[k] = true; // Marca que o eleitor votou
            }
        }
    }

    static void lerNumCandidatos(ref int candidato1, ref int candidato2, int quantE)
    {
        using (StreamReader sr = new StreamReader(arqDatEleicao))
        {
            string linha;
            // Pular as linhas até encontrar o número de eleitores válido
            while ((linha = sr.ReadLine()) != null)
            {
                int numeroEleitores = Convert.ToInt32(linha);
                if (numeroEleitores >= 10)
                {
                    break;
                }
            }

            // Pular as linhas dos dados dos eleitores
            for (int j = 0; j < quantE * 4; j++)
            {
                sr.ReadLine();
            }

            // Ler os candidatos
            candidato1 = Convert.ToInt32(sr.ReadLine());
            candidato2 = Convert.ToInt32(sr.ReadLine());
        }
    }

    static void lerDadosEleitores(ref int[] tE, ref string[] n, ref string[] dN, ref int[] i, int quantE)
    {
        using (StreamReader sr = new StreamReader(arqDatEleicao))
        {
            // Pular as linhas até encontrar o número de eleitores válido
            string linha;
            while ((linha = sr.ReadLine()) != null)
            {
                int numeroEleitores = Convert.ToInt32(linha);
                if (numeroEleitores >= 10)
                {
                    break;
                }
            }

            // Ler os dados dos eleitores
            for (int ind = 0; ind < quantE; ind++)
            {
                tE[ind] = Convert.ToInt32(sr.ReadLine());
                n[ind] = sr.ReadLine();
                dN[ind] = sr.ReadLine();
                i[ind] = Convert.ToInt32(sr.ReadLine());
            }
        }
    }

    static void quantEleitores(ref int quantE, ref int pos)
    {
        int linhaConvert;
        using (StreamReader sr = new StreamReader(arqDatEleicao))
        {
            string linha;
            while ((linha = sr.ReadLine()) != null)
            {
                linhaConvert = Convert.ToInt32(linha);
                if (linhaConvert >= 10)
                {
                    quantE = linhaConvert;
                    break;
                }
                Console.WriteLine("Valor encontrado " + linhaConvert + ", é preciso ter pelo menos 10 eleitores!!!");
            }
        }
    }

    static void Main()
    {
        bool existeArquivo = veriExistArq();
        if (!existeArquivo)
        {
            Console.WriteLine("Erro ao ler o arquivo!!!");
            return;
        }

        using (StreamWriter sw = new StreamWriter(arqOutEleicao))
        {
            int quantE = 0;
            int posicaoLinha = 0;
            quantEleitores(ref quantE, ref posicaoLinha);
            sw.WriteLine("A quantidade de eleitores é " + quantE + ".");
            Console.WriteLine("A quantidade de eleitores é " + quantE + ".");

            int[] titulo = new int[quantE];
            string[] nome = new string[quantE];
            string[] dataNasc = new string[quantE];
            int[] idade = new int[quantE];
            bool[] situacaoVoto = new bool[quantE];

            lerDadosEleitores(ref titulo, ref nome, ref dataNasc, ref idade, quantE);

            for (int i = 0; i < titulo.Length; i++)
            {
                sw.WriteLine(titulo[i]);
                sw.WriteLine(nome[i]);
                sw.WriteLine(dataNasc[i]);
                sw.WriteLine(idade[i]);

                Console.WriteLine(titulo[i]);
                Console.WriteLine(nome[i]);
                Console.WriteLine(dataNasc[i]);
                Console.WriteLine(idade[i]);
            }

            int teobaldoNum = 0;
            int astrogildoNum = 0;

            lerNumCandidatos(ref teobaldoNum, ref astrogildoNum, quantE);
            sw.WriteLine("Teobaldo número de candidato = " + teobaldoNum + ".");
            sw.WriteLine("Astrogildo número de candidato = " + astrogildoNum + ".");
            Console.WriteLine("Teobaldo número de candidato = " + teobaldoNum + ".");
            Console.WriteLine("Astrogildo número de candidato = " + astrogildoNum + ".");

            int[] votos = new int[quantE];
            lerVotos(quantE, teobaldoNum, astrogildoNum, ref votos, ref situacaoVoto);

            exibirResultado(votos, teobaldoNum, astrogildoNum, sw);

            sw.WriteLine("Situação dos eleitores:");
            Console.WriteLine("Situação dos eleitores:");
            for (int i = 0; i < situacaoVoto.Length; i++)
            {
                string situacao = "Eleitor " + titulo[i] + " - Votou: " + (situacaoVoto[i] ? "Sim" : "Não");
                sw.WriteLine(situacao);
                Console.WriteLine(situacao);
            }
        }
    }
}
