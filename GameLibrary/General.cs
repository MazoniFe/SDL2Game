using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDLC_.GameLibrary
{
    internal class General
    {
        public enum Images_path { APPLE, BANANA, ORANGE, UNDEFINED}
        public enum GameObjects {APPLE, BANANA, ORANGE, UNDEFINED}

        public static bool ObjectsAreEqual(Sprite current, Sprite previous)
        {
            if (current.GetTexture() != previous.GetTexture())
            {
                return false;
            }

            return true;
        }
    }
}
