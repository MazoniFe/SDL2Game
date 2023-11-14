using SDL2;
using SDLC;
using System;
using System.Numerics;

namespace SDLC_.GameLibrary
{
    internal class View
    {
        public static IntPtr window;
        public static IntPtr renderer;
        public static bool running = false;
        public static bool isMaximized = false;
        public static int WINDOW_WIDTH = 1280;
        public static int WINDOW_HEIGHT = 720;
        private static double lastFrameTime = SDL.SDL_GetTicks();
        public static double currentFrameTime;
        public static double deltaTime;
        public static IntPtr font;
        const string TITLE = "Game TITLE";
        public static Camera camera = new Camera(Vector2.Zero);

        public static void Init()
        {
            InitializeSDL();
            CreateWindowAndRenderer();
            InitializeSDLImage();
            InitializeTTF();
            LoadFont();
            running = true;
        }

        private static void InitializeSDL()
        {
            if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO) < 0)
            {
                Console.WriteLine($"Houve um problema ao inicializar o SDL. {SDL.SDL_GetError()}");
            }
        }

        private static void CreateWindowAndRenderer()
        {
            window = SDL.SDL_CreateWindow(TITLE, SDL.SDL_WINDOWPOS_UNDEFINED, SDL.SDL_WINDOWPOS_UNDEFINED, WINDOW_WIDTH, WINDOW_HEIGHT, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN | SDL.SDL_WindowFlags.SDL_WINDOW_MAXIMIZED);

            if (window == IntPtr.Zero)
            {
                Console.WriteLine($"Houve um problema ao criar a janela. {SDL.SDL_GetError()}");
            }

            renderer = SDL.SDL_CreateRenderer(window,
                                                -1,
                                                SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED |
                                                SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);

            if (renderer == IntPtr.Zero)
            {
                Console.WriteLine($"Houve um problema ao criar o renderizador. {SDL.SDL_GetError()}");
            }
        }

        private static void InitializeSDLImage()
        {
            if (SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG) == 0)
            {
                Console.WriteLine($"Houve um problema ao inicializar o SDL2_Image {SDL_image.IMG_GetError()}");
            }
        }

        private static void InitializeTTF()
        {
            SDL_ttf.TTF_Init();
        }

        private static void LoadFont()
        {
            font = SDL_ttf.TTF_OpenFont(General.GetFontsPath() + "\\RobotoMedium.ttf", 14);

            if (font == IntPtr.Zero)
            {
                Console.WriteLine("Erro ao carregar a fonte TTF.");
            }
        }

        public static void Update()
        {
            UpdateTime();
            SetBackgroundColor();
            ClearRenderer();
            RenderWorld();
            RenderGameObjects();
            RenderEntities();
            PresentRenderer();
        }

        private static void UpdateTime()
        {
            currentFrameTime = SDL.SDL_GetTicks();
            deltaTime = (currentFrameTime - lastFrameTime) / 1000.0;
            lastFrameTime = currentFrameTime;
        }

        private static void SetBackgroundColor()
        {
            if (SDL.SDL_SetRenderDrawColor(View.renderer, 135, 206, 235, 255) < 0)
            {
                Console.WriteLine($"Houve um problema ao definir a cor de fundo. {SDL.SDL_GetError()}");
            }
        }

        private static void ClearRenderer()
        {
            if (SDL.SDL_RenderClear(View.renderer) < 0)
            {
                Console.WriteLine($"Houve um problema ao limpar a superfície de renderização. {SDL.SDL_GetError()}");
            }
        }

        private static void PresentRenderer()
        {
            SDL.SDL_RenderPresent(View.renderer);
        }

        public static void RenderGameObjects()
        {
            foreach (GameObject obj in Program.gameObjects)
            {
                UpdateObjectPosition(obj);
                RenderObjectTexture(obj);
            }
        }

        public static void RenderEntities()
        {
            foreach (Entity entity in Program.entities)
            {
                UpdateObjectPosition(entity);
                RenderObjectTexture(entity);
                RenderEntityHealth(entity);
                RenderObjectText(entity);
            }
        }

        private static void UpdateObjectPosition(GameObject obj)
        {
            obj.UpdatePosition();
            obj.UpdateAnimation(deltaTime);
        }

        private static void RenderObjectOutline(GameObject obj)
        {
            Vector2 adjustedPosition = View.camera.GetAdjustedPosition(obj.GetPosition());
            SDL.SDL_SetRenderDrawColor(renderer, 0, 255, 0, 255);
            SDL.SDL_Rect outlineRect = new SDL.SDL_Rect
            {
                x = (int)adjustedPosition.X - 1,
                y = (int)adjustedPosition.Y - 1,
                w = General.SPRITE_WIDTH + 2,
                h = General.SPRITE_HEIGHT + 2
            };
            SDL.SDL_RenderDrawRect(renderer, ref outlineRect);
        }

        private static void RenderObjectTexture(GameObject obj)
        {
            Vector2 adjustedPosition = View.camera.GetAdjustedPosition(obj.GetPosition());
            SDL.SDL_Rect destRect = new SDL.SDL_Rect
            {
                x = (int)adjustedPosition.X,
                y = (int)adjustedPosition.Y,
                w = General.SPRITE_WIDTH,
                h = General.SPRITE_HEIGHT
            };
            SDL.SDL_RenderCopy(renderer, obj.GetCurrentAnimation().GetCurrentFrameTexture(), IntPtr.Zero, ref destRect);
        }


        private static void RenderObjectText(GameObject obj)
        {
            string text = obj.GetName();

            if (text != null && obj is Player)
            {
                IntPtr textSurface = SDL_ttf.TTF_RenderText_Blended(font, text, new SDL.SDL_Color { r = 255, g = 255, b = 255, a = 255 });
                IntPtr textTexture = SDL.SDL_CreateTextureFromSurface(renderer, textSurface);
                int textWidth, textHeight;
                SDL_ttf.TTF_SizeText(font, text, out textWidth, out textHeight);

                Vector2 adjustedPosition = View.camera.GetAdjustedPosition(obj.GetPosition());

                SDL.SDL_Rect textDestRect = new SDL.SDL_Rect
                {
                    x = (int)adjustedPosition.X + (General.SPRITE_WIDTH - textWidth) / 2,
                    y = (int)adjustedPosition.Y - 30,
                    w = textWidth,
                    h = textHeight
                };

                SDL.SDL_RenderCopy(renderer, textTexture, IntPtr.Zero, ref textDestRect);

                SDL.SDL_FreeSurface(textSurface);
                SDL.SDL_DestroyTexture(textTexture);
            }
        }


        private static void RenderEntityHealth(Entity obj)
        {
            int maxHealth = obj.GetMaxHealth();  // Defina a quantidade máxima de vida do personagem
            int currentHealth = obj.GetCurrentHealth();  // Substitua por seu método real para obter a vida do personagem

            // Defina as dimensões do retângulo da barra de vida
            float barWidth = General.SPRITE_WIDTH * 0.85f;
            int barHeight = 7;
            int barPadding = 2;

            // Calcule a largura proporcional à vida atual
            int currentBarWidth = (int)((float)currentHealth / maxHealth * barWidth);

            Vector2 adjustedPosition = View.camera.GetAdjustedPosition(obj.GetPosition());
            int barX = (int)adjustedPosition.X + (General.SPRITE_WIDTH - currentBarWidth) / 2;

            // Desenhe o fundo da barra de vida
            SDL.SDL_SetRenderDrawColor(renderer, 255, 0, 0, 255);
            SDL.SDL_Rect backgroundRect = new SDL.SDL_Rect
            {
                x = barX,
                y = (int)adjustedPosition.Y - barHeight - barPadding,
                w = (int)barWidth,
                h = barHeight
            };
            SDL.SDL_RenderFillRect(renderer, ref backgroundRect);

            // Desenhe a barra de vida atual
            SDL.SDL_SetRenderDrawColor(renderer, 0, 255, 0, 255);
            SDL.SDL_Rect healthRect = new SDL.SDL_Rect
            {
                x = barX,
                y = (int)adjustedPosition.Y - barHeight - barPadding,
                w = currentBarWidth,
                h = barHeight
            };
            SDL.SDL_RenderFillRect(renderer, ref healthRect);
        }

        public static void RenderWorld()
        {
            World_Tile[,] map = Program.world.GetMap();

            for (int i = 0; i < World.HEIGHT; i++)
            {
                for (int j = 0; j < World.WIDTH; j++)
                {
                    RenderTile(map, i, j);
                }
            }
        }

        private static void RenderTile(World_Tile[,] map, int i, int j)
        {
            World_Tile tile = map[i, j];

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

            SDL.SDL_Rect destRect = new SDL.SDL_Rect
            {
                x = (int)adjustedPosition.X,
                y = (int)adjustedPosition.Y,
                w = General.TILE_WIDTH,
                h = General.TILE_HEIGHT
            };
            SDL.SDL_RenderCopy(renderer, tile.GetSprite().GetTexture(), IntPtr.Zero, ref destRect);
            //Program.world.GetWorldTile(new Vector2(i, j)).RemoveObject();
        }
    }
}
