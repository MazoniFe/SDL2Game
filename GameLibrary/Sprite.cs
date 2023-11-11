using SDL2;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SDLC_.GameLibrary
{
    // Classe que representa um sprite no jogo.
    internal class Sprite
    {
        // Ponteiro para a textura do sprite.
        public IntPtr texture;

        // Dimensões da imagem do sprite.
        private int imageWidth, imageHeight;

        // Construtor que cria um sprite a partir do caminho da imagem.
        public Sprite(Resources.Images_path imagePath)
        {
            // Obtém a textura do sprite a partir do caminho da imagem.
            this.texture = GetTextureFromPath(GetPathFromEnum(imagePath));
        }

        // Método privado para obter a textura do sprite a partir do caminho da imagem.
        private IntPtr GetTextureFromPath(string path)
        {
            // Carrega a imagem usando o SDL_image.
            IntPtr image = SDL_image.IMG_Load(path);

            // Verifica se houve erro no carregamento da imagem.
            if (image == IntPtr.Zero)
            {
                Console.WriteLine("Erro ao carregar a imagem: " + SDL.SDL_GetError());
                return IntPtr.Zero;
            }

            // Cria uma textura a partir da superfície da imagem.
            IntPtr texture = SDL.SDL_CreateTextureFromSurface(View.renderer, image);

            // Libera a superfície da imagem, pois a textura já foi criada.
            SDL.SDL_FreeSurface(image);

            // Consulta as dimensões da textura.
            SDL.SDL_QueryTexture(texture, out _, out _, out imageWidth, out imageHeight);

            // Ajusta a textura para manter a proporção original da imagem.
            this.texture = GetTextureWithAspectRatio(texture, General.SPRITE_WIDTH, General.SPRITE_WIDTH, imageWidth, imageHeight);

            return texture;
        }

        // Método privado para obter o caminho da imagem a partir do enum correspondente.
        private string GetPathFromEnum(Resources.Images_path path)
        {
            // Lê o conteúdo do arquivo JSON que mapeia os caminhos das imagens.
            string jsonContent = File.ReadAllText(General.GetImageJSONPath() + "\\img_path.json");
            dynamic imagePaths = JObject.Parse(jsonContent);

            // Converte o enum para o formato do JSON.
            string enumValue = path.ToString();
            var parts = enumValue.Split('_');
            string newValueEnum = parts[0] + "-" + parts[1] + "_" + parts[2];

            // Retorna o caminho completo da imagem.
            return General.GetSpritesPath() + General.FindItemByPath(newValueEnum, imagePaths);
        }

        // Método privado para ajustar a textura mantendo a proporção original da imagem.
        private IntPtr GetTextureWithAspectRatio(IntPtr originalTexture, int desiredWidth, int desiredHeight, int imageWidth, int imageHeight)
        {
            // Calcula a proporção da imagem original.
            float imageAspectRatio = (float)imageWidth / imageHeight;
            int newWidth, newHeight;

            // Calcula as novas dimensões da textura para manter a proporção original.
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

            // Define retângulos para a cópia da textura, considerando as novas dimensões.
            SDL.SDL_Rect sourceRect = new SDL.SDL_Rect
            {
                x = 0,
                y = 0,
                w = imageWidth,
                h = imageHeight
            };

            SDL.SDL_Rect destRect = new SDL.SDL_Rect
            {
                x = 0,
                y = 0,
                w = newWidth,
                h = newHeight
            };

            // Copia a textura original para a nova textura com dimensões ajustadas.
            SDL.SDL_RenderCopy(View.renderer, originalTexture, ref sourceRect, ref destRect);

            return originalTexture;
        }

        // Método público para obter a textura do sprite.
        public IntPtr GetTexture()
        {
            return texture;
        }
    }
}
