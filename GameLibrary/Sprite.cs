using SDL2;
using SDLC;

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
            string pathValue = "";
            switch(path) {

                //PLAYER
                case Resources.Images_path.PLAYER_TOP_1:
                    pathValue = "C:\\Users\\lipem\\source\\repos\\SDLC#\\SDLC#\\Sprites\\Character\\TOP_1.png";
                    break;
                case Resources.Images_path.PLAYER_TOP_2:
                    pathValue = "C:\\Users\\lipem\\source\\repos\\SDLC#\\SDLC#\\Sprites\\Character\\TOP_2.png";
                    break;
                case Resources.Images_path.PLAYER_TOP_3:
                    pathValue = "C:\\Users\\lipem\\source\\repos\\SDLC#\\SDLC#\\Sprites\\Character\\TOP_3.png";
                    break;
                case Resources.Images_path.PLAYER_TOP_4:
                    pathValue = "C:\\Users\\lipem\\source\\repos\\SDLC#\\SDLC#\\Sprites\\Character\\TOP_4.png";
                    break;
                case Resources.Images_path.PLAYER_LEFT_1:
                    pathValue = "C:\\Users\\lipem\\source\\repos\\SDLC#\\SDLC#\\Sprites\\Character\\LEFT_1.png";
                    break;
                case Resources.Images_path.PLAYER_LEFT_2:
                    pathValue = "C:\\Users\\lipem\\source\\repos\\SDLC#\\SDLC#\\Sprites\\Character\\LEFT_2.png";
                    break;
                case Resources.Images_path.PLAYER_LEFT_3:
                    pathValue = "C:\\Users\\lipem\\source\\repos\\SDLC#\\SDLC#\\Sprites\\Character\\LEFT_3.png";
                    break;
                case Resources.Images_path.PLAYER_LEFT_4:
                    pathValue = "C:\\Users\\lipem\\source\\repos\\SDLC#\\SDLC#\\Sprites\\Character\\LEFT_4.png";
                    break;
                case Resources.Images_path.PLAYER_BOTTOM_1:
                    pathValue = "C:\\Users\\lipem\\source\\repos\\SDLC#\\SDLC#\\Sprites\\Character\\BOTTOM_1.png";
                    break;
                case Resources.Images_path.PLAYER_BOTTOM_2:
                    pathValue = "C:\\Users\\lipem\\source\\repos\\SDLC#\\SDLC#\\Sprites\\Character\\BOTTOM_2.png";
                    break;
                case Resources.Images_path.PLAYER_BOTTOM_3:
                    pathValue = "C:\\Users\\lipem\\source\\repos\\SDLC#\\SDLC#\\Sprites\\Character\\BOTTOM_3.png";
                    break;
                case Resources.Images_path.PLAYER_BOTTOM_4:
                    pathValue = "C:\\Users\\lipem\\source\\repos\\SDLC#\\SDLC#\\Sprites\\Character\\BOTTOM_4.png";
                    break;
                case Resources.Images_path.PLAYER_RIGHT_1:
                    pathValue = "C:\\Users\\lipem\\source\\repos\\SDLC#\\SDLC#\\Sprites\\Character\\RIGHT_1.png";
                    break;
                case Resources.Images_path.PLAYER_RIGHT_2:
                    pathValue = "C:\\Users\\lipem\\source\\repos\\SDLC#\\SDLC#\\Sprites\\Character\\RIGHT_2.png";
                    break;
                case Resources.Images_path.PLAYER_RIGHT_3:
                    pathValue = "C:\\Users\\lipem\\source\\repos\\SDLC#\\SDLC#\\Sprites\\Character\\RIGHT_3.png";
                    break;
                case Resources.Images_path.PLAYER_RIGHT_4:
                    pathValue = "C:\\Users\\lipem\\source\\repos\\SDLC#\\SDLC#\\Sprites\\Character\\RIGHT_4.png";
                    break;

                //teste
                case Resources.Images_path.ORANGE:
                    pathValue = "C:\\Users\\lipem\\source\\repos\\SDLC#\\SDLC#\\Sprites\\orange.png";
                    break;
                case Resources.Images_path.BANANA:
                    pathValue = "C:\\Users\\lipem\\source\\repos\\SDLC#\\SDLC#\\Sprites\\banana.png";
                    break;
                case Resources.Images_path.APPLE:
                    pathValue = "C:\\Users\\lipem\\source\\repos\\SDLC#\\SDLC#\\Sprites\\apple.png";
                    break;
                default:
                    pathValue = "C:\\Users\\lipem\\source\\repos\\SDLC#\\SDLC#\\Sprites\\undefined.png";
                    break;
            }
            return pathValue;
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
