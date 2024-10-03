using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

// Funções auxiliares, como validação de estados

namespace Quebracabeca.Model
{
    public class State
    {
        public int[][] board { get; set; }    // Matriz 3x3 representa o tabuleiro
        private int emptyRow { get; set; }   // Linha onde está o espaço vazio
        private int emptyCol { get; set; }  // Coluna onde está o espaço vazio

        public State(int[][] initialBoard)
        {
            this.board = copyBoard(initialBoard);
            findEmptyPosition();
        }

        // Função para copiar o tabuleiro (usada na primeira abordagem)
        private int[][] copyBoard(int[][] board)
        {
            int[][] newBoard = new int[board.Length][];
            for (int i = 0;  i < board.Length; i++)
            {
                newBoard[i] = new int[board[i].Length];
                Array.Copy(board[i], newBoard[i], board[i].Length);
            }
            return newBoard;
        }

        // Função que encontra a posição vazia
        private void findEmptyPosition()
        {
            for(int i = 0; i < board.Length; i++)
            {
                for(int j = 0; j < board[i].Length; j++)
                {
                    if (board[i][j] == 0) // Verificar espaço vazio
                    {
                        emptyRow = i;
                        emptyCol = j;
                        return;
                    }
                }
            }
        }

        // Função para fazer um movimento
        public bool move(string direction)
        {
            int newRow = emptyRow, newCol = emptyCol;

            switch(direction.ToLower())
            {
                case "up":
                    newRow = emptyRow - 1;
                    break;
                case "down":
                    newRow = emptyRow + 1;
                    break;
                case "left":
                    newCol =  emptyCol - 1;
                    break;
                case "right":
                    newCol = emptyCol + 1;
                    break;
                default:
                    return false; // Movimento inválido
            }

            if (isValidPosition(newRow, newCol))
            {
                swap(emptyRow, emptyCol, newRow, newCol);
                emptyRow = newRow;
                emptyCol = newCol;
                return true;
            }

            return false;
        }

        // Função para desfazer o movimento (somente para a segunda abordagem)
        public void und0Move(string direction)
        {
            switch (direction.ToLower())
            {
                case "up":
                    move("down");
                    break;
                case "down":
                    move("up");
                    break;
                case "left":
                    move("right");
                    break;
                case "right":
                    move("left");
                    break;
            }
        }

        // Verifica se a posição está dentro dos limites da matriz
        private bool isValidPosition(int row, int col)
        {
            return row >= 0 && row < board.Length && col >= 0 && col < board[0].Length;
        }

        // Troca os valores entre duas posições
        private void swap(int row1, int col1, int row2, int col2)
        {
            int temp = board[row1][col1];
            board[row1][col1] = board[row2][col2];
            board[row2][col2] = temp;
        }

        // Comparação de dois estados (verifica se são iguais)s
        public bool isEqual(State other)
        {
            if (other == null) return false;

            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board[i].Length; j++)
                {
                    if (board[i][j] != other.board[i][j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        // Verifica se o estado atual é a solução (tabuleiro ordenado)
        public bool isGoal()
        {
            int expectedValue = 1;

            for(int i =  0; i < board.Length;i++)
            {
                for(int j = 0;j < board[i].Length; j++)
                {
                    if (i == board.Length - 1 && j == board[i].Length - 1)
                    {
                        return board[i][j] == 0; // O último espaço deve estar vazio
                    }
                    if (board[i][j] != expectedValue)
                    {
                        return false;
                    }
                    expectedValue++;
                }
                
            }
            return true;
        }
    }
}
