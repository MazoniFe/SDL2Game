using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace SDLC_.GameLibrary
{
    // Classe de utilidades gerais para o jogo.
    internal class General
    {
        // Enumeração para as entradas do jogador.
        public enum DIRECTION { TOP, LEFT, RIGHT, BOTTOM, NULL}

        // Dimensões dos tiles e sprites no jogo.
        public static int TILE_WIDTH = 64;
        public static int TILE_HEIGHT = 64;
        public static int SPRITE_HEIGHT = 64;
        public static int SPRITE_WIDTH = 64;
        public static DIRECTION direction = DIRECTION.BOTTOM;

        // Método para verificar se dois sprites são iguais.
        public static bool ObjectsAreEqual(Sprite current, Sprite previous)
        {
            // Compara as texturas dos sprites.
            if (current.GetTexture() != previous.GetTexture())
            {
                return false;
            }

            return true;
        }

        // Método para obter o caminho completo para a pasta de sprites.
        public static string GetSpritesPath()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "Sprites");
        }

        // Método para obter o caminho completo para o arquivo JSON de caminhos de imagens.
        public static string GetImageJSONPath()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "GameLibrary", "DataFiles");
        }

        // Método para encontrar um item em um objeto JSON com base no caminho especificado.
        public static string FindItemByPath(string path, JObject jsonObject)
        {
            // Divide o caminho em partes.
            string[] parts = path.Split('-');

            // Verifica se o caminho é válido.
            if (parts.Length < 2)
            {
                return "Caminho inválido";
            }

            // Obtém o nome do objeto pai e o caminho do objeto filho.
            string parent = parts[0];
            string childPath = parts[1];

            // Verifica se o objeto pai é um objeto JSON.
            if (jsonObject[parent] is JObject parentObject)
            {
                // Verifica se o objeto filho existe no objeto pai.
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

        // Método para mover um ponto em direção a outro ponto com uma distância máxima delta.
        public static Vector2 MoveTowards(Vector2 current, Vector2 target, float maxDistanceDelta)
        {
            // Calcula o vetor direção e a magnitude.
            Vector2 vector = target - current;
            float magnitude = vector.Length();

            // Verifica se a magnitude é menor ou igual à distância máxima delta ou muito pequena.
            if (magnitude <= maxDistanceDelta || magnitude < float.Epsilon)
            {
                return target;
            }

            // Move o ponto em direção ao alvo com uma distância máxima delta.
            return current + vector / magnitude * maxDistanceDelta;
        }
    }
}
