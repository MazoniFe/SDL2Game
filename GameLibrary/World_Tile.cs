using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDLC_.GameLibrary
{
    internal class World_Tile
    {
        readonly Sprite Sprite;
        readonly GameObject? gameObject;

        public World_Tile(Sprite sprite)
        {
            this.Sprite = sprite;
        }
        public World_Tile(GameObject obj)
        {
            this.gameObject = obj;
            this.Sprite = gameObject.GetCurrentAnimation().GetCurrentFrameSprite();
        }

        public Sprite GetSprite()
        {
            return this.Sprite;
        }
    }

}
