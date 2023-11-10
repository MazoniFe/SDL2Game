using SDL2;
using SDLC;
using System.Numerics;

namespace SDLC_.GameLibrary
{
    internal class View
    {
        public static IntPtr window;
        public static IntPtr renderer;
        public static bool running = false;

        public static int WINDOW_WIDTH = 800;
        public static int WINDOW_HEIGHT = 800;

        public static int spriteWidth = 64;
        public static int spriteHeight = 64;

        private static double lastFrameTime = SDL.SDL_GetTicks();
        public static double animationTimer = 0.0;
        public static double currentFrameTime;
        public static double deltaTime;


        public static Camera camera = new Camera(Vector2.Zero);

        const string TITLE = "Game TITLE";

        public static void Init()
        {

            if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO) < 0)
            {
                Console.WriteLine($"There was an issue initilizing SDL. {SDL.SDL_GetError()}");
            }


            window = SDL.SDL_CreateWindow(TITLE, SDL.SDL_WINDOWPOS_UNDEFINED, SDL.SDL_WINDOWPOS_UNDEFINED, WINDOW_WIDTH, WINDOW_HEIGHT, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

            if (window == IntPtr.Zero)
            {
                Console.WriteLine($"There was an issue creating the window. {SDL.SDL_GetError()}");
            }


            renderer = SDL.SDL_CreateRenderer(window,
                                                    -1,
                                                    SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED |
                                                    SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);

            if (renderer == IntPtr.Zero)
            {
                Console.WriteLine($"There was an issue creating the renderer. {SDL.SDL_GetError()}");
            }

            // Initilizes SDL_image for use with png files.
            if (SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG) == 0)
            {
                Console.WriteLine($"There was an issue initilizing SDL2_Image {SDL_image.IMG_GetError()}");
            }
            running = true;
        }

        public static void Update()
        {
            currentFrameTime = SDL.SDL_GetTicks();
            deltaTime = (currentFrameTime - lastFrameTime) / 1000.0;
            lastFrameTime = currentFrameTime;

            if (SDL.SDL_SetRenderDrawColor(View.renderer, 135, 206, 235, 255) < 0)
            {
                Console.WriteLine($"There was an issue with setting the render draw color. {SDL.SDL_GetError()}");
            }

            // Clears the current render surface.
            if (SDL.SDL_RenderClear(View.renderer) < 0)
            {
                Console.WriteLine($"There was an issue with clearing the render surface. {SDL.SDL_GetError()}");
            }

            RenderWorld();
            RenderGameObjects(0.02);


            SDL.SDL_RenderPresent(View.renderer);
        }
        public static void RenderGameObjects(double deltaTime)
        {
            foreach (GameObject obj in Program.gameObjects)
            {
                obj.UpdateAnimation(deltaTime);

                Vector2 adjustedPosition = View.camera.GetAdjustedPosition(obj.GetPosition());

                SDL.SDL_Rect destRect = new SDL.SDL_Rect
                {
                    x = (int)adjustedPosition.X,
                    y = (int)adjustedPosition.Y,
                    w = spriteWidth,
                    h = spriteHeight
                };

                SDL.SDL_RenderCopy(renderer, obj.GetCurrentAnimation().GetCurrentFrameTexture(), IntPtr.Zero, ref destRect);
            }
        }

        public static void RenderWorld()
        {
            World_Tile[,] map = Program.world.GetMap();
            int tileWidth = 48;
            int tileHeight = 48;

            for (int i = 0; i < World.HEIGHT; i++)
            {
                for (int j = 0; j < World.WIDTH; j++)
                {
                    World_Tile tile = map[i, j];

                    Vector2 adjustedPosition = View.camera.GetAdjustedPosition(new Vector2(j * tileWidth, i * tileHeight));

                    SDL.SDL_Rect destRect = new SDL.SDL_Rect
                    {
                        x = (int)adjustedPosition.X,
                        y = (int)adjustedPosition.Y,
                        w = tileWidth,
                        h = tileHeight
                    };

                    SDL.SDL_RenderCopy(renderer, tile.GetSprite().GetTexture(), IntPtr.Zero, ref destRect);
                }
            }
        }

    }
}
