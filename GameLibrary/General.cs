﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDLC_.GameLibrary
{
    internal class General
    {
        public enum Inputs { TOP, LEFT, RIGHT, BOTTOM }
        public static bool ObjectsAreEqual(Sprite current, Sprite previous)
        {
            if (current.GetTexture() != previous.GetTexture())
            {
                return false;
            }

            return true;
        }

        public static string GetSpritesPath()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "Sprites");
        }
        public static string GetImageJSONPath()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "GameLibrary", "DataFiles");
        }
    }
}
