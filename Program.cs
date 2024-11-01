using System;
using System.Threading;

class Program
{
    static void Main()
    {
        
        int m = 4; 
        int n = 5; 

        
        int[,] matriz = new int[m, n];
        Random random = new Random();

        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                matriz[i, j] = random.Next(1, 10); 
            }
        }

        
        Console.WriteLine("Matriz:");
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write(matriz[i, j] + "\t");
            }
            Console.WriteLine();
        }

       
        Thread[] threads = new Thread[m];
        for (int i = 0; i < m; i++)
        {
            int linhaAtual = i;
            threads[i] = new Thread(() => SomarLinha(matriz, linhaAtual, n));
            threads[i].Start();
        }

        
        foreach (var thread in threads)
        {
            thread.Join();
        }
        int linhasA = 3;
        int colunasB = 3;

       
        int[,] matrizA = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
        int[,] matrizB = new int[,] { { 9, 8, 7 }, { 6, 5, 4 }, { 3, 2, 1 } };
        int[,] matrizResultado = new int[linhasA, colunasB]; 

        
        MultiplicarMatrizesParalelo(matrizA, matrizB, matrizResultado);
        Console.WriteLine("\nMatriz QUESTAO 02 Resultado (A x B):");
        ExibirMatriz(matrizResultado);
        
        
        
        Console.WriteLine("\n CORRIDA DE SAPOVISZQUISS QUESTAO 03 ");
      
        Console.WriteLine("Informe a distância total da corrida (em metros): ");
        int distanciaCorrida = int.Parse(Console.ReadLine());

        Console.WriteLine("Informe a quantidade de sapos: ");
        int qtdSapos = int.Parse(Console.ReadLine());

        // Inicia a corrida de sapos
        CorridaDeSapos corrida = new CorridaDeSapos(distanciaCorrida, qtdSapos);
        corrida.IniciarCorrida();
    }

  
    static void SomarLinha(int[,] matriz, int linha, int n)
    {
        int soma = 0;
        for (int j = 0; j < n; j++)
        {
            soma += matriz[linha, j];
        }
        Console.WriteLine($"Soma da linha {linha}: {soma}");
    }
    public static void MultiplicarMatrizesParalelo(int[,] matrizA, int[,] matrizB, int[,] matrizResultado)
    {
        int m = matrizA.GetLength(0); 
        int n = matrizA.GetLength(1); 
        int p = matrizB.GetLength(1); 

        Thread[] threads = new Thread[m];

        for (int i = 0; i < m; i++)
        {
            int linhaAtual = i; 
            threads[i] = new Thread(() => CalcularLinhaResultado(matrizA, matrizB, matrizResultado, linhaAtual, n, p));
            threads[i].Start();
        }

        
        foreach (var thread in threads)
        {
            thread.Join();
        }
    }

    
    private static void CalcularLinhaResultado(int[,] matrizA, int[,] matrizB, int[,] matrizResultado, int linha, int n, int p)
    {
        for (int j = 0; j < p; j++) 
        {
            int soma = 0;
            for (int k = 0; k < n; k++) 
            {
                soma += matrizA[linha, k] * matrizB[k, j];
            }
            matrizResultado[linha, j] = soma;
        }
    }
    private static void ExibirMatriz(int[,] matriz)
    {
        for (int i = 0; i < matriz.GetLength(0); i++)
        {
            for (int j = 0; j < matriz.GetLength(1); j++)
            {
                Console.Write(matriz[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }
    
class Sapo
{
    public int ID { get; private set; }
    public int DistanciaPercorrida { get; private set; } = 0;
    private int distanciaTotal; 
    private Random rand; 

    public Sapo(int id, int distanciaTotal)
    {
        ID = id; 
        this.distanciaTotal = distanciaTotal;
        rand = new Random(); 
    }

    
    public void Pular()
    {
        while (DistanciaPercorrida < distanciaTotal)
        {
            
            int distanciaPulo = rand.Next(1, 6); 
            DistanciaPercorrida += distanciaPulo;

            if (DistanciaPercorrida > distanciaTotal) 
            {
                DistanciaPercorrida = distanciaTotal; 
            }

            
            Console.WriteLine($"Sapo {ID} pulou {distanciaPulo} metros. Distância total percorrida: {DistanciaPercorrida} metros.");

            
            Thread.Sleep(500); 
        }
        Console.WriteLine($"Sapo {ID} terminou a corrida!"); 
    }
}

class CorridaDeSapos
{
    private int distanciaCorrida; 
    private int qtdSapos; 

    public CorridaDeSapos(int distanciaCorrida, int qtdSapos)
    {
        this.distanciaCorrida = distanciaCorrida; 
        this.qtdSapos = qtdSapos; 
    }

    public void IniciarCorrida()
    {
        Thread[] saposThreads = new Thread[qtdSapos]; 

        for (int i = 0; i < qtdSapos; i++)
        {
            Sapo sapo = new Sapo(i + 1, distanciaCorrida);

            
            saposThreads[i] = new Thread(sapo.Pular);
            saposThreads[i].Start();
        }

      
        foreach (var thread in saposThreads)
        {
            thread.Join(); 
        }

        Console.WriteLine("A corrida de sapos terminou!"); 
    }
}
}
