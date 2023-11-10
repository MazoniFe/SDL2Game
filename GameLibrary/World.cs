using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDLC_.GameLibrary
{
    internal class World
    {
        public const int WIDTH = 50;
        public const int HEIGHT = 50;

        private World_Tile[,] map;

        public World()
        {
            this.map = new World_Tile[WIDTH, HEIGHT];
            BuildWorld();
        }

        private void BuildWorld()
        {
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    map[i, j] = new World_Tile(new Sprite(Resources.Images_path.ENV_GRASS_1));
                }
            }
            map[5,5] = new World_Tile(new Sprite(Resources.Images_path.ENV_GROUND_1));
        }

        public World_Tile[,] GetMap()
        {
            return this.map;
        }

    }
}
