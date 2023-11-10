using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace SDLC_.GameLibrary
{
    internal class General
    {
        public enum Inputs { TOP, LEFT, RIGHT, BOTTOM, NULL }
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

        public static string FindItemByPath(string path, JObject jsonObject)
        {
            string[] parts = path.Split('-');

            if (parts.Length < 2)
            {
                return "Caminho inválido";
            }

            string parent = parts[0];
            string childPath = parts[1];

            if (jsonObject[parent] is JObject parentObject)
            {
                if (parentObject[childPath] != null)
                {
                    return parentObject[childPath].ToString();
                }
                else
                {
                    return "Item não encontrado";
                }
            }
            else
            {
                return "Parent não encontrado";
            }
        }

    }
}
