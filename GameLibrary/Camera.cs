using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SDLC_.GameLibrary
{
    internal class Camera
    {
        public Vector2 Position { get; private set; }

        public Camera(Vector2 position)
        {
            Position = position;
        }

        public void Follow(GameObject target)
        {
            // Ajusta a posição da câmera para seguir o jogador
            Position = new Vector2(target.GetPosition().X - (View.WINDOW_WIDTH / 2), target.GetPosition().Y - (View.WINDOW_HEIGHT / 2));
        }

        public Vector2 GetAdjustedPosition(Vector2 position)
        {
            return new Vector2(position.X - Position.X, position.Y - Position.Y);
        }

    }
}
