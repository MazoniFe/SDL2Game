using SDL2;
using SDLC;

namespace SDLC_.GameLibrary
{
    internal class View
    {
        public static IntPtr window;
        public static IntPtr renderer;
        public static bool running = false;

        public static int spriteWidth = 64;
        public static int spriteHeight = 64;

        private static double lastFrameTime = SDL.SDL_GetTicks();
        public static double animationTimer = 0.0;
        public static double currentFrameTime;
        public static double deltaTime;


        const string TITLE = "Game TITLE";

        public static void Init()
        {
            if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO) < 0)
            {
                Console.WriteLine($"There was an issue initilizing SDL. {SDL.SDL_GetError()}");
            }

            // Create a new window given a title, size, and passes it a flag indicating it should be shown.
            window = SDL.SDL_CreateWindow(TITLE, SDL.SDL_WINDOWPOS_UNDEFINED, SDL.SDL_WINDOWPOS_UNDEFINED, 640, 480, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

            if (window == IntPtr.Zero)
            {
                Console.WriteLine($"There was an issue creating the window. {SDL.SDL_GetError()}");
            }

            // Creates a new SDL hardware renderer using the default graphics device with VSYNC enabled.
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

            RenderGameObjects(deltaTime);


            // Switches out the currently presented render surface with the one we just did work on.
            SDL.SDL_RenderPresent(View.renderer);
        }
        public static void RenderGameObjects(double deltaTime)
        {
            foreach (GameObject obj in Program.gameObjects)
            {
                obj.UpdateAnimation(deltaTime);
                SDL.SDL_Rect destRect = new SDL.SDL_Rect
                {
                    x = (int)obj.GetPosition().X, // Defina as coordenadas x apropriadas
                    y = (int)obj.GetPosition().Y, // Defina as coordenadas y apropriadas
                    w = spriteWidth,  // Largura do sprite
                    h = spriteHeight   // Altura do sprite
                };
                SDL.SDL_RenderCopy(renderer, obj.GetCurrentAnimation().GetCurrentFrameTexture(), IntPtr.Zero, ref destRect);

            }

            SDL.SDL_RenderPresent(renderer);
        }
    }
}
