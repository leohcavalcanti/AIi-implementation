using Quebracabeca.Model;
using Quebracabeca.State_management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quebracabeca.Teste
{
    // Código para comparar as duas abordagens
    public class PerformanceTest
    {
        public void Compare()
        {
            // Exemplo de tabuleiro inicial
            int[][] initialBoard = new int[][]
            {
                new int[] { 1, 2, 3 },
                new int[] { 4, 0, 5 },
                new int[] { 7, 8, 6 }
            };

            State initialState = new State(initialBoard);
            IterativeDeepeningSolver solver = new IterativeDeepeningSolver();

            // Testar a primeira versão (com cópia)
            DateTime startTime = DateTime.Now;
            bool resultWithCopy = solver.solveWithCopy(initialState);
            DateTime endTime = DateTime.Now;
            Console.WriteLine("Solução encontrada com cópia: " + resultWithCopy);
            Console.WriteLine("Tempo com cópia: " + (endTime - startTime).TotalMilliseconds + " ms");

            // Testar a segunda versão (com modificação direta)
            startTime = DateTime.Now;
            bool resultWithDirectModification = solver.SolveWithDirectModification(initialState);
            endTime = DateTime.Now;
            Console.WriteLine("Solução encontrada com modificação direta: " + resultWithDirectModification);
            Console.WriteLine("Tempo com modificação direta: " + (endTime - startTime).TotalMilliseconds + " ms");
        }
    }
}
