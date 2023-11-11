using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDLC_.GameLibrary
{
    // Classe que representa um tile no mundo do jogo.
    internal class World_Tile
    {
        // Sprite associado a este tile.
        readonly Sprite Sprite;

        // Objeto do jogo associado a este tile (pode ser nulo).
        readonly GameObject? gameObject;

        // Construtor para criar um World_Tile apenas com um Sprite.
        public World_Tile(Sprite sprite)
        {
            this.Sprite = sprite;
        }

        // Construtor para criar um World_Tile com um GameObject.
        public World_Tile(GameObject obj)
        {
            // Inicializa o GameObject associado.
            this.gameObject = obj;

            // Obtém o Sprite da animação atual do GameObject.
            this.Sprite = gameObject.GetCurrentAnimation().GetCurrentFrameSprite();
        }

        // Método para obter o Sprite deste World_Tile.
        public Sprite GetSprite()
        {
            // Retorna o Sprite associado a este tile.
            return this.Sprite;
        }
    }
}
