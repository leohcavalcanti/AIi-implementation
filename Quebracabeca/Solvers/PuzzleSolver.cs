using Quebracabeca.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//  Lógica para resolver o quebra-cabeça
namespace Quebracabeca.State_management
{
    public class PuzzleSolver
    {
        // Primeira versão: copia o estado
        public State MoveWithCopy(State parentState, string direction)
        {
            State newState = new State(parentState.board); // Cria uma cópia do estado
            if (newState.move(direction))
            {
                return newState;  // Retorna o novo estado
            }
            return null;  // Se o movimento for inválido, retorna nulo
        }

        // Segunda versão: modifica o estado diretamente
        public bool MoveWithDirectModification(State parentState, string direction)
        {
            return parentState.move(direction);  // Retorna se o movimento foi realizado com sucesso
        }

        // Desfazer o movimento (somente para a segunda versão)
        public void UndoMove(State parentState, string direction)
        {
            parentState.move(GetOppositeDirection(direction));  // Desfaz o movimento no estado pai
        }

        // Retorna a direção oposta
        private string GetOppositeDirection(string direction)
        {
            return direction switch
            {
                "up" => "down",
                "down" => "up",
                "left" => "right",
                "right" => "left",
                _ => null,
            };
        }
    }
}
