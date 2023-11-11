using SDLC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SDLC_.GameLibrary
{
    // Classe que representa um tile no mundo do jogo.
    internal class World_Tile
    {
        private bool hasObject = false;
        // Sprite associado a este tile.
        private Sprite Sprite;

        // Construtor para criar um World_Tile apenas com um Sprite.
        public World_Tile(Sprite sprite)
        {
            this.Sprite = sprite;
        }


        // Método para obter o Sprite deste World_Tile.
        public Sprite GetSprite()
        {
            // Retorna o Sprite associado a este tile.
            return this.Sprite;
        }

        public bool HasObject()
        {
            return hasObject;
        }

        public void AddObject() { this.hasObject = true; }
        public void RemoveObject() { this.hasObject = false; }


    }
}
