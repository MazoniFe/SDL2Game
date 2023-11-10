using SDL2;
using System;
namespace SDLC
{
    using SDLC_.GameLibrary;
    using System.Numerics;

    class Program
    {
        public static World world = new World();
        public static List<GameObject> gameObjects = new List<GameObject>(); static void Main(string[] args)
        {
            View.Init();
            GameObject player = new GameObject(Resources.GameObjects.PLAYER, new Vector2(250, 250));
            gameObjects.Add(player);
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
                            var currentPos = player.GetPosition();
                            if (e.key.keysym.sym == SDL.SDL_Keycode.SDLK_w)
                            {
                                player.SetPosition(currentPos.X, currentPos.Y - 6);
                                player.SetCurrentAnimation(General.Inputs.TOP);
                            }
                            else if (e.key.keysym.sym == SDL.SDL_Keycode.SDLK_a)
                            {
                                player.SetPosition(currentPos.X - 6, currentPos.Y);
                                player.SetCurrentAnimation(General.Inputs.LEFT);
                            }
                            else if (e.key.keysym.sym == SDL.SDL_Keycode.SDLK_s)
                            {
                                player.SetPosition(currentPos.X, currentPos.Y + 6);
                                player.SetCurrentAnimation(General.Inputs.BOTTOM);
                            }
                            else if (e.key.keysym.sym == SDL.SDL_Keycode.SDLK_d)
                            {
                                player.SetPosition(currentPos.X + 6, currentPos.Y);
                                player.SetCurrentAnimation(General.Inputs.RIGHT);
                            }
                            break;
                    }
                }

                IntPtr keyState = SDL.SDL_GetKeyboardState(out int numKeys);

                bool nenhumaTeclaPressionada = true;

                for (int i = 0; i < numKeys; i++)
                {
                    byte keyStateValue = System.Runtime.InteropServices.Marshal.ReadByte(keyState, i);
                    if (keyStateValue != 0)
                    {
                        nenhumaTeclaPressionada = false;
                        break;
                    }
                }

                View.camera.Follow(player);


                if (nenhumaTeclaPressionada)
                {
                    player.SetCurrentAnimation(General.Inputs.NULL);
                }

                View.Update();
            }

            SDL.SDL_DestroyRenderer(View.renderer);
            SDL.SDL_DestroyWindow(View.window);
            SDL.SDL_Quit();
        }
    }
}