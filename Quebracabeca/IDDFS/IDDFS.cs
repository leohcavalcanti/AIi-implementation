using Quebracabeca.Puzzle;

namespace Quebracabeca.IDDFS
{
    public class IDDFS
    {
        // Movimentos possíveis (cima, baixo, esquerda, direita)
        private readonly int[] rowMoves = { -1, 1, 0, 0 };
        private readonly int[] colMoves = { 0, 0, -1, 1 };
        private const int SIZE = 3;

        private List<PuzzleState> exploredNodes = new List<PuzzleState>(); // Lista de nós explorados

        public PuzzleState Search(PuzzleState initialState)
        {
            int depth = 0;
            while (true)
            {
                var result = DepthLimitedSearch(initialState, depth);
                if (result != null) return result; // Encontrou a solução
                depth++;
            }
        }

        private PuzzleState DepthLimitedSearch(PuzzleState state, int limit)
        {
            exploredNodes.Add(state); // Armazena o estado explorado
            if (state.IsGoalState()) return state;
            if (state.Depth == limit) return null;

            int emptyIndex = state.FindEmptyTile();
            int emptyRow = emptyIndex / SIZE;
            int emptyCol = emptyIndex % SIZE;

            for (int i = 0; i < rowMoves.Length; i++)
            {
                int newRow = emptyRow + rowMoves[i];
                int newCol = emptyCol + colMoves[i];
                if (IsValidMove(newRow, newCol))
                {
                    int targetIndex = newRow * SIZE + newCol;
                    var childState = state.GenerateChild(emptyIndex, targetIndex);
                    var result = DepthLimitedSearch(childState, limit);
                    if (result != null) return result; // Se encontrar a solução, retorna
                }
            }
            return null; // Não encontrou solução neste limite
        }

        // Verifica se a movimentação é válida
        private bool IsValidMove(int row, int col)
        {
            return row >= 0 && row < SIZE && col >= 0 && col < SIZE;
        }

        // Função para imprimir a árvore de busca a partir do estado final
        public void PrintSearchTree(PuzzleState solution)
        {
            var path = new Stack<PuzzleState>();
            var currentState = solution;

            // Empilha o caminho da solução (da folha até a raiz)
            while (currentState != null)
            {
                path.Push(currentState);
                currentState = currentState.Parent;
            }

            // Imprime os estados na ordem da raiz para a folha
            int level = 0;
            while (path.Count > 0)
            {
                var state = path.Pop();
                Console.WriteLine($"\nNível {level}:");
                state.PrintBoard();
                level++;
            }
        }
    }
}