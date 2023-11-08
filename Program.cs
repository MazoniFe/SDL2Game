using SDL2;
using System;

namespace SDLC
{
    using SDL2;
    using SDLC_.GameLibrary;
    using System.Numerics;

    class Program
    {
        public static List<GameObject> gameObjects = new List<GameObject>();

        static void Main(string[] args)
        {
            View.Init();

            GameObject apple = new GameObject(General.GameObjects.ORANGE, new Vector2(50,50));
            gameObjects.Add(apple);
            while (View.running)
            {

                while (SDL.SDL_PollEvent(out SDL.SDL_Event e) == 1)
                {
                    switch (e.type)
                    {
                        case SDL.SDL_EventType.SDL_QUIT:
                            View.running = false;
                            break;
                        case SDL.SDL_EventType.SDL_KEYDOWN:
                            var currentPos = apple.GetPosition();
                            if (e.key.keysym.sym == SDL.SDL_Keycode.SDLK_w)
                            {
                                apple.SetPosition(currentPos.X, currentPos.Y - 6);
                            }
                            else if (e.key.keysym.sym == SDL.SDL_Keycode.SDLK_a)
                            {
                                apple.SetPosition(currentPos.X - 6, currentPos.Y);
                            }
                            else if (e.key.keysym.sym == SDL.SDL_Keycode.SDLK_s)
                            {
                                apple.SetPosition(currentPos.X, currentPos.Y + 6);
                            }
                            else if (e.key.keysym.sym == SDL.SDL_Keycode.SDLK_d)
                            {
                                apple.SetPosition(currentPos.X + 6, currentPos.Y);
                            }
                            break;
                    }
                }
                View.Update();
            }

            SDL.SDL_DestroyRenderer(View.renderer);
            SDL.SDL_DestroyWindow(View.window);
            SDL.SDL_Quit();
        }
    }
}