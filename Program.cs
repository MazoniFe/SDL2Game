using SDL2;
using System;
using System.Numerics;
using SDLC_.GameLibrary;

namespace SDLC
{
    class Program
    {
        public static World world = new World();
        public static List<GameObject> gameObjects = new List<GameObject>(); static void Main(string[] args)
        {
            View.Init();
            GameObject player = new GameObject(Resources.GameObjects.PLAYER, World.GetPosition(25,25));
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
                        case SDL.SDL_EventType.SDL_WINDOWEVENT:
                            if (e.window.windowEvent == SDL.SDL_WindowEventID.SDL_WINDOWEVENT_RESIZED)
                            {
                                // Atualize o tamanho da janela
                                View.WINDOW_WIDTH = e.window.data1;
                                View.WINDOW_HEIGHT = e.window.data2;
                            }
                            break;
                        case SDL.SDL_EventType.SDL_KEYDOWN:
                            var currentPos = player.GetPosition();
                            if (e.key.keysym.sym == SDL.SDL_Keycode.SDLK_w)
                            {
                                player.SetPosition(General.DIRECTION.TOP);
                            }
                            else if (e.key.keysym.sym == SDL.SDL_Keycode.SDLK_a)
                            {
                                player.SetPosition(General.DIRECTION.LEFT);
                            }
                            else if (e.key.keysym.sym == SDL.SDL_Keycode.SDLK_s)
                            {
                                player.SetPosition(General.DIRECTION.BOTTOM);
                            }
                            else if (e.key.keysym.sym == SDL.SDL_Keycode.SDLK_d)
                            {
                                player.SetPosition(General.DIRECTION.RIGHT);
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
                    if (player.canMove)
                    {
                        //player.SetCurrentAnimation(General.direction, Resources.Animation_State.IDLE);
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