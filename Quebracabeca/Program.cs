using Quebracabeca.Puzzle;
using System;
using System.Collections.Generic;

namespace Quebracabeca.IDDFS
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Tabuleiro inicial (aleatório pode ser usado também)
            int[,] initialBoard = new int[,]
            {
                { 1, 2, 3 },
                { 4, 0, 5 },
                { 7, 8, 6 }
            };

            var initialState = new PuzzleState(initialBoard);
            var iddfs = new IDDFS();

            Console.WriteLine("Estado inicial:");
            initialState.PrintBoard();

            var solution = iddfs.Search(initialState);

            if (solution != null)
            {
                Console.WriteLine("Estado final:");
                solution.PrintBoard();

                Console.WriteLine("\nÁrvore de busca IDDFS:");
                iddfs.PrintSearchTree(solution);
            }
            else
            {
                Console.WriteLine("Nenhuma solução encontrada.");
            }
        }
    }
}