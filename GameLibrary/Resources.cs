using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDLC_.GameLibrary
{
    // Classe que gerencia os recursos do jogo.
    public class Resources
    {
        // Enumeração para os caminhos das imagens.
        public enum Images_path
        {
            // Objetos
            FRUITS_APPLE_1, FRUITS_BANANA, FRUITS_ORANGE, UNDEFINED,

            // Goku
            GOKU_IDLETOP_1, GOKU_IDLELEFT_1, GOKU_IDLELEFT_2, GOKU_IDLERIGHT_1, GOKU_IDLERIGHT_2, GOKU_IDLEBOTTOM_1, GOKU_IDLEBOTTOM_2,
            GOKU_TOP_1, GOKU_TOP_2, GOKU_TOP_3, GOKU_TOP_4,
            GOKU_LEFT_1, GOKU_LEFT_2, GOKU_LEFT_3, GOKU_LEFT_4,
            GOKU_BOTTOM_1, GOKU_BOTTOM_2, GOKU_BOTTOM_3, GOKU_BOTTOM_4,
            GOKU_RIGHT_1, GOKU_RIGHT_2, GOKU_RIGHT_3, GOKU_RIGHT_4,

            // Ambiente
            ENV_GRASS_1, ENV_GROUND_1
        }

        // Enumeração para os caminhos das imagens dos jogadores.
        public enum Player_Image_Path { TOP, LEFT, RIGHT, BOTTOM }

        // Enumeração para os tipos de objetos do jogo.
        public enum GameObjects { APPLE, BANANA, ORANGE, PLAYER, UNDEFINED }

        // Enumeração para os estados de animação.
        public enum Animation_State { IDLE, WALK }
    }
}
