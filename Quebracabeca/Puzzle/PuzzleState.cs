namespace Quebracabeca.Puzzle
{
    public class PuzzleState
    {
        private const int SIZE = 3; // Tamanho do tabuleiro (3x3)
        public int[] Board { get; private set; } // Usando array unidimensional para otimizar
        public PuzzleState Parent { get; private set; }
        public string Move { get; private set; }
        public int Depth { get; private set; }

        // Construtor
        public PuzzleState(int[,] board, PuzzleState parent = null, string move = "", int depth = 0)
        {
            Board = new int[SIZE * SIZE];
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    Board[i * SIZE + j] = board[i, j];
                }
            }
            Parent = parent;
            Move = move;
            Depth = depth;
        }

        // Método para gerar o estado filho, movendo uma peça
        public PuzzleState GenerateChild(int emptyIndex, int targetIndex)
        {
            var newBoard = (int[])Board.Clone();
            newBoard[emptyIndex] = newBoard[targetIndex];
            newBoard[targetIndex] = 0;
            return new PuzzleState(ToMatrix(newBoard), this, $"{Move} -> {targetIndex}", Depth + 1);
        }

        // Método que converte o array de volta para matriz 2D
        private int[,] ToMatrix(int[] board)
        {
            int[,] matrix = new int[SIZE, SIZE];
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    matrix[i, j] = board[i * SIZE + j];
                }
            }
            return matrix;
        }

        // Verifica se o estado atual é o estado objetivo
        public bool IsGoalState()
        {
            for (int i = 0; i < SIZE * SIZE - 1; i++)
            {
                if (Board[i] != i + 1) return false;
            }
            return Board[SIZE * SIZE - 1] == 0; // O último espaço deve ser 0
        }

        // Imprime o tabuleiro em formato 3x3
        public void PrintBoard()
        {
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    Console.Write(Board[i * SIZE + j] + " ");
                }
                Console.WriteLine();
            }
        }

        // Encontra o índice do espaço vazio (0)
        public int FindEmptyTile()
        {
            for (int i = 0; i < Board.Length; i++)
            {
                if (Board[i] == 0)
                    return i;
            }
            return -1; // Deveria sempre encontrar o 0
        }

        // Função auxiliar para comparação de estados (útil para depuração)
        public override string ToString()
        {
            return string.Join(",", Board);
        }
    }
}