using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SDLC_.GameLibrary
{
    // Classe que representa a câmera no jogo.
    internal class Camera
    {
        // Propriedade para obter ou definir a posição da câmera.
        public Vector2 Position { get; private set; }

        // Construtor que inicializa a câmera com uma posição.
        public Camera(Vector2 position)
        {
            Position = position;
        }

        // Método para fazer a câmera seguir um objeto.
        public void Follow(GameObject target)
        {
            Position = new Vector2(target.GetPosition().X - (View.WINDOW_WIDTH / 2), target.GetPosition().Y - (View.WINDOW_HEIGHT / 2));
        }

        // Método para obter a posição ajustada com base na posição da câmera.
        public Vector2 GetAdjustedPosition(Vector2 position)
        {
            return new Vector2(position.X - Position.X, position.Y - Position.Y);
        }
    }
}
