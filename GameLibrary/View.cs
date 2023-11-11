using SDL2;
using SDLC;
using System.Numerics;

namespace SDLC_.GameLibrary
{
    // Classe que gerencia a visualização do jogo.
    internal class View
    {
        // Ponteiro para a janela e renderizador do SDL.
        public static IntPtr window;
        public static IntPtr renderer;

        // Flags de controle do estado da aplicação.
        public static bool running = false;
        public static bool isMaximized = false;

        // Dimensões da janela.
        public static int WINDOW_WIDTH = 1280;
        public static int WINDOW_HEIGHT = 720;

        // Variáveis para controle de tempo e animação.
        private static double lastFrameTime = SDL.SDL_GetTicks();
        public static double animationTimer = 0.0;
        public static double currentFrameTime;
        public static double deltaTime;

        // Instância da câmera para controle de visualização.
        public static Camera camera = new Camera(Vector2.Zero);

        // Título da janela.
        const string TITLE = "Game TITLE";

        // Método de inicialização da visualização.
        public static void Init()
        {
            // Inicializa o SDL.
            if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO) < 0)
            {
                Console.WriteLine($"Houve um problema ao inicializar o SDL. {SDL.SDL_GetError()}");
            }

            // Cria a janela.
            window = SDL.SDL_CreateWindow(TITLE, SDL.SDL_WINDOWPOS_UNDEFINED, SDL.SDL_WINDOWPOS_UNDEFINED, WINDOW_WIDTH, WINDOW_HEIGHT, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN | SDL.SDL_WindowFlags.SDL_WINDOW_MAXIMIZED);

            if (window == IntPtr.Zero)
            {
                Console.WriteLine($"Houve um problema ao criar a janela. {SDL.SDL_GetError()}");
            }

            // Cria o renderizador.
            renderer = SDL.SDL_CreateRenderer(window,
                                                    -1,
                                                    SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED |
                                                    SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);

            if (renderer == IntPtr.Zero)
            {
                Console.WriteLine($"Houve um problema ao criar o renderizador. {SDL.SDL_GetError()}");
            }

            // Inicializa o SDL_image para uso com arquivos PNG.
            if (SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG) == 0)
            {
                Console.WriteLine($"Houve um problema ao inicializar o SDL2_Image {SDL_image.IMG_GetError()}");
            }

            // Sinaliza que a aplicação está em execução.
            running = true;
        }

        // Método de atualização da visualização.
        public static void Update()
        {
            // Atualiza o tempo e o delta de tempo.
            currentFrameTime = SDL.SDL_GetTicks();
            deltaTime = (currentFrameTime - lastFrameTime) / 1000.0;
            lastFrameTime = currentFrameTime;

            // Define a cor de fundo para azul celeste.
            if (SDL.SDL_SetRenderDrawColor(View.renderer, 135, 206, 235, 255) < 0)
            {
                Console.WriteLine($"Houve um problema ao definir a cor de fundo. {SDL.SDL_GetError()}");
            }

            // Limpa a superfície de renderização.
            if (SDL.SDL_RenderClear(View.renderer) < 0)
            {
                Console.WriteLine($"Houve um problema ao limpar a superfície de renderização. {SDL.SDL_GetError()}");
            }

            // Renderiza o mundo e os objetos do jogo.
            RenderWorld();
            RenderGameObjects(0.02);

            // Apresenta a superfície de renderização.
            SDL.SDL_RenderPresent(View.renderer);
        }

        // Método para renderizar os objetos do jogo.
        public static void RenderGameObjects(double deltaTime)
        {
            foreach (GameObject obj in Program.gameObjects)
            {
                // Atualiza a animação e a posição do objeto.
                obj.UpdateAnimation(deltaTime);
                obj.UpdatePosition();
                float objPosX = obj.GetPosition().X;
                float objPosY = obj.GetPosition().Y;

                //ADICIONA AO TILE A FLAG DE QUE HÁ UM OBJETO EM CIMA.
                Program.world.GetWorldTile(obj.GetWorldPosition()).AddObject();

                // Obtém a posição ajustada pela câmera.
                Vector2 adjustedPosition = View.camera.GetAdjustedPosition(obj.GetPosition());

                // Desenha um contorno ao redor do objeto com cor verde.
                SDL.SDL_SetRenderDrawColor(renderer, 0, 255, 0, 255);
                SDL.SDL_Rect outlineRect = new SDL.SDL_Rect
                {
                    x = (int)adjustedPosition.X - 1,
                    y = (int)adjustedPosition.Y - 1,
                    w = General.SPRITE_WIDTH + 2,
                    h = General.SPRITE_HEIGHT + 2
                };
                SDL.SDL_RenderDrawRect(renderer, ref outlineRect);

                // Renderiza o objeto.
                SDL.SDL_Rect destRect = new SDL.SDL_Rect
                {
                    x = (int)adjustedPosition.X,
                    y = (int)adjustedPosition.Y,
                    w = General.SPRITE_WIDTH,
                    h = General.SPRITE_HEIGHT
                };
                SDL.SDL_RenderCopy(renderer, obj.GetCurrentAnimation().GetCurrentFrameTexture(), IntPtr.Zero, ref destRect);
            }
        }

        // Método para renderizar o mundo.
        public static void RenderWorld()
        {
            // Obtém a matriz de tiles do mundo.
            World_Tile[,] map = Program.world.GetMap();

            for (int i = 0; i < World.HEIGHT; i++)
            {
                for (int j = 0; j < World.WIDTH; j++)
                {
                    World_Tile tile = map[i, j];
                    Program.world.GetWorldTile(new Vector2(i, j)).RemoveObject();

                    // Calcula a posição ajustada pela câmera.
                    Vector2 adjustedPosition = camera.GetAdjustedPosition(new Vector2(j * General.TILE_WIDTH, i * General.TILE_HEIGHT));
                    SDL.SDL_SetRenderDrawColor(renderer, 255, 255, 255, 255);
                    SDL.SDL_Rect outlineRect = new SDL.SDL_Rect
                    {
                        x = (int)adjustedPosition.X - 1,
                        y = (int)adjustedPosition.Y - 1,
                        w = General.TILE_WIDTH + 2,
                        h = General.TILE_HEIGHT + 2
                    };
                    SDL.SDL_RenderDrawRect(renderer, ref outlineRect);

                    // Renderiza o tile.
                    SDL.SDL_Rect destRect = new SDL.SDL_Rect
                    {
                        x = (int)adjustedPosition.X,
                        y = (int)adjustedPosition.Y,
                        w = General.TILE_WIDTH,
                        h = General.TILE_HEIGHT
                    };
                    SDL.SDL_RenderCopy(renderer, tile.GetSprite().GetTexture(), IntPtr.Zero, ref destRect);
                }
            }


        }

    }
}
