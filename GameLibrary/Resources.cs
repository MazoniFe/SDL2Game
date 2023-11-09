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
            APPLE, BANANA, ORANGE, UNDEFINED,
            //PLAYER
            PLAYER_TOP_1, PLAYER_TOP_2, PLAYER_TOP_3, PLAYER_TOP_4,
            PLAYER_LEFT_1, PLAYER_LEFT_2, PLAYER_LEFT_3, PLAYER_LEFT_4, 
            PLAYER_BOTTOM_1, PLAYER_BOTTOM_2, PLAYER_BOTTOM_3, PLAYER_BOTTOM_4, 
            PLAYER_RIGHT_1, PLAYER_RIGHT_2, PLAYER_RIGHT_3, PLAYER_RIGHT_4
        }
        public enum Player_Image_Path { TOP, LEFT, RIGHT, BOTTOM }
        public enum GameObjects { APPLE, BANANA, ORANGE, PLAYER, UNDEFINED }
        public enum Animation_State { IDLE, LEFT, RIGHT, TOP, BOTTOM }
    }
}
