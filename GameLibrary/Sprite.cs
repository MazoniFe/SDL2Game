using SDL2;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static SDLC_.GameLibrary.General;

namespace SDLC_.GameLibrary
{


    internal class Sprite
    {
        public IntPtr texture;
        private int imageWidth, imageHeight;

        public Sprite(Resources.Images_path imagePath)
        {
            this.texture = GetTextureFromPath(GetPathFromEnum(imagePath));
        }

        private IntPtr GetTextureFromPath(string path)
        {
            IntPtr image = SDL_image.IMG_Load(path);
            if (image == IntPtr.Zero)
            {
                Console.WriteLine("Erro ao carregar a imagem: " + SDL.SDL_GetError());
                return IntPtr.Zero;
            }
            IntPtr texture = SDL.SDL_CreateTextureFromSurface(View.renderer, image);
            SDL.SDL_FreeSurface(image);

            SDL.SDL_QueryTexture(texture, out _, out _, out imageWidth, out imageHeight);
            this.texture = GetTextureWithAspectRatio(texture, View.spriteWidth, View.spriteWidth, imageWidth, imageHeight);

            return texture;
        }

        private string GetPathFromEnum(Resources.Images_path path)
        {
            string jsonContent = File.ReadAllText(GetImageJSONPath() + "\\img_path.json");
            dynamic imagePaths = JObject.Parse(jsonContent);

            string enumValue = path.ToString();

            if (imagePaths[enumValue] != null)
            {
                return GetSpritesPath() + imagePaths[$"{enumValue}"];
            }
            else
            {
                return ""; 
            }
        }

        private IntPtr GetTextureWithAspectRatio(IntPtr originalTexture, int desiredWidth, int desiredHeight, int imageWidth, int imageHeight)
        {
            float imageAspectRatio = (float)imageWidth / imageHeight;
            int newWidth, newHeight;

            if (imageAspectRatio > 1.0f)
            {
                newWidth = desiredWidth;
                newHeight = (int)(newWidth / imageAspectRatio);
            }
            else
            {
                newHeight = desiredHeight;
                newWidth = (int)(newHeight * imageAspectRatio);
            }

            SDL.SDL_Rect sourceRect = new SDL.SDL_Rect
            {
                x = 0,
                y = 0,
                w = imageWidth,
                h = imageHeight
            };

            SDL.SDL_Rect destRect = new SDL.SDL_Rect
            {
                x = 0, // Defina as coordenadas x apropriadas
                y = 0, // Defina as coordenadas y apropriadas
                w = newWidth,
                h = newHeight
            };

            SDL.SDL_RenderCopy(View.renderer, originalTexture, ref sourceRect, ref destRect);

            return originalTexture;
        }

        public IntPtr GetTexture()
        {
            return texture;
        }
    }
}
