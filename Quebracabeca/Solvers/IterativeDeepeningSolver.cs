using Quebracabeca.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quebracabeca.State_management
{
    // Implementação da busca iterativa
    public class IterativeDeepeningSolver
    {
        private PuzzleSolver solver;

        public IterativeDeepeningSolver()
        {
            this.solver = new PuzzleSolver();
        }

        // Implementação do algoritmo IDDFS (com cópia de estado)
        public bool solveWithCopy(State initialState)
        {
            for (int depth = 0; ; depth++)
            {
                if (DepthLimitedSearchWithCopy(initialState, depth))
                {
                    return true; // Solução encontrada
                }
            }

            return false;
        }

        // Implementação do algoritmo IDDFS (com modificação direta)
        public bool SolveWithDirectModification(State initialState)
        {
            for (int depth = 0; ; depth++)
            {
                if (DepthLimitedSearchWithDirectModification(initialState, depth))
                {
                    return true;  // Solução encontrada
                }
            }
            return false;  // Falha
        }

        // Pesquisa limitada por profundidade (com cópia)
        private bool DepthLimitedSearchWithCopy(State state, int depth)
        {
            if (state.isGoal())
            {
                return true;
            }
            if (depth == 0)
            {
                return false;
            }

            // Tenta mover em todas as direções possíveis
            foreach (string direction in new[] { "up", "down", "left", "right" })
            {
                State newState = solver.MoveWithCopy(state, direction);
                if (newState != null && DepthLimitedSearchWithCopy(newState, depth - 1))
                {
                    return true;
                }
            }
            return false;
        }

        // Pesquisa limitada por profundidade (com modificação direta)
        private bool DepthLimitedSearchWithDirectModification(State state, int depth)
        {
            if (state.isGoal())
            {
                return true;
            }
            if (depth == 0)
            {
                return false;
            }

            foreach (string direction in new[] { "up", "down", "left", "right" })
            {
                if (solver.MoveWithDirectModification(state, direction))
                {
                    if (DepthLimitedSearchWithDirectModification(state, depth - 1))
                    {
                        return true;
                    }
                    solver.UndoMove(state, direction);  // Desfaz o movimento
                }
            }
            return false;
        }
    }
}
