using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDLC_.GameLibrary
{
    public class Resources
    {
        public enum Images_path { 
            //objects
            FRUITS_APPLE, BANANA, ORANGE, UNDEFINED,
            //GOKU
            GOKU_IDLETOP_1, GOKU_IDLELEFT_1, GOKU_IDLELEFT_2, GOKU_IDLERIGHT_1, GOKU_IDLERIGHT_2, GOKU_IDLEBOTTOM_1, GOKU_IDLEBOTTOM_2,
            GOKU_TOP_1, GOKU_TOP_2, GOKU_TOP_3, GOKU_TOP_4,
            GOKU_LEFT_1, GOKU_LEFT_2, GOKU_LEFT_3, GOKU_LEFT_4,
            GOKU_BOTTOM_1, GOKU_BOTTOM_2, GOKU_BOTTOM_3, GOKU_BOTTOM_4,
            GOKU_RIGHT_1, GOKU_RIGHT_2, GOKU_RIGHT_3, GOKU_RIGHT_4,

            //ENVIRONMENT
            ENV_GRASS_1, ENV_GROUND_1

        }
        public enum Player_Image_Path { TOP, LEFT, RIGHT, BOTTOM }
        public enum GameObjects { APPLE, BANANA, ORANGE, PLAYER, UNDEFINED }
        public enum Animation_State { IDLE, LEFT, RIGHT, TOP, BOTTOM, NULL }
    }
}
