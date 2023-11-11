using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SDLC_.GameLibrary
{
    // Classe que representa o mundo do jogo.
    internal class World
    {
        // Largura e altura do mundo.
        public const int WIDTH = 50;
        public const int HEIGHT = 50;

        // Matriz de tiles que compõem o mundo.
        private World_Tile[,] map;

        // Construtor que inicializa o mundo e constrói seus tiles.
        public World()
        {
            this.map = new World_Tile[WIDTH, HEIGHT];
            BuildWorld();
        }

        // Método privado para construir o mundo com tiles padrão.
        private void BuildWorld()
        {
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    // Cada tile inicializado com um Sprite de grama.
                    map[i, j] = new World_Tile(new Sprite(Resources.Images_path.ENV_GRASS_1));
                }
            }

            // Alteramos um tile específico para ter um Sprite de solo.
            map[5, 5] = new World_Tile(new Sprite(Resources.Images_path.ENV_GROUND_1));
        }

        // Método para obter a matriz de tiles do mundo.
        public World_Tile[,] GetMap()
        {
            return this.map;
        }

        // Método interno para obter a posição no mundo a partir de coordenadas.
        internal static Vector2 GetPosition(float X, float Y)
        {
            return new Vector2(X * General.TILE_WIDTH, Y * General.TILE_HEIGHT);
        }

        // Método interno para converter coordenadas de pixels para posição na matriz.
        internal static Vector2 PixelToMatrixPosition(int x_pixel, int y_pixel)
        {
            // Calculamos a posição na matriz com base nas coordenadas de pixels.
            int matrixX = (x_pixel / General.TILE_WIDTH);
            int matrixY = (y_pixel / General.TILE_HEIGHT);

            // Garantimos que as posições estejam dentro dos limites da matriz.
            matrixX = Math.Clamp(matrixX, 0, WIDTH - 1);
            matrixY = Math.Clamp(matrixY, 0, HEIGHT - 1);

            // Retornamos a posição na matriz.
            return new Vector2(matrixX, matrixY);
        }
    

        public World_Tile GetWorldTile(Vector2 tilePosition)
        {
            return this.map[(int)tilePosition.X, (int)tilePosition.Y];
        }
    }
}
