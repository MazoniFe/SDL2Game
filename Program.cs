using SDL2;
using System;
using System.Collections.Generic;
using System.Numerics;
using SDLC_.GameLibrary;

namespace SDLC
{
    class Program
    {
        public static World world = new World();
        public static List<GameObject> gameObjects = new List<GameObject>();
        public static List<Entity> entities = new List<Entity>();
        public static Player? localPlayer;

        // Definições de membros de classe...
        static void Main(string[] args)
        {
            View.Init();
            InitializeEntities();

            while (View.running)
            {
                HandleEvents();

                UpdateGameState();

                View.Update();
            }

            CleanUp();
        }

        // Inicializa os jogadores e entidades do jogo.
        static void InitializeEntities()
        {

            Player player1 = new Player("Ryukii", Resources.GameObjects.PLAYER, World.GetPosition(8, 23));
            Player player2 = new Player("Mage", Resources.GameObjects.PLAYER, World.GetPosition(15, 13));

            entities.Add(player1);
            entities.Add(player2);

            player2.SetLocalPlayer();

            localPlayer = General.GetLocalPlayer();
        }

        // Lida com eventos SDL, como teclas pressionadas e redimensionamento de janela.
        // Chama métodos específicos para tratar cada tipo de evento.
        static void HandleEvents()
        {
            while (SDL.SDL_PollEvent(out SDL.SDL_Event e) == 1)
            {
                switch (e.type)
                {
                    case SDL.SDL_EventType.SDL_QUIT:
                        View.running = false;
                        break;
                    case SDL.SDL_EventType.SDL_WINDOWEVENT:
                        HandleWindowEvent(e);
                        break;
                    case SDL.SDL_EventType.SDL_KEYDOWN:
                        HandleKeyDownEvent(e);
                        break;
                }
            }

            CheckKeyState();
        }

        // Lida com eventos de redimensionamento de janela.
        static void HandleWindowEvent(SDL.SDL_Event e)
        {
            if (e.window.windowEvent == SDL.SDL_WindowEventID.SDL_WINDOWEVENT_RESIZED)
            {
                // Atualize o tamanho da janela
                View.WINDOW_WIDTH = e.window.data1;
                View.WINDOW_HEIGHT = e.window.data2;
            }
        }

        // Lida com eventos de tecla pressionada, movendo o jogador local se necessário.
        static void HandleKeyDownEvent(SDL.SDL_Event e)
        {
            if (localPlayer != null)
            {
                if (e.key.keysym.sym == SDL.SDL_Keycode.SDLK_w)
                {
                    localPlayer.Move(General.DIRECTION.TOP);
                    localPlayer.isMoving = true;
                }
                else if (e.key.keysym.sym == SDL.SDL_Keycode.SDLK_a)
                {
                    localPlayer.Move(General.DIRECTION.LEFT);
                    localPlayer.isMoving = true;
                }
                else if (e.key.keysym.sym == SDL.SDL_Keycode.SDLK_s)
                {
                    localPlayer.Move(General.DIRECTION.BOTTOM);
                    localPlayer.isMoving = true;
                }
                else if (e.key.keysym.sym == SDL.SDL_Keycode.SDLK_d)
                {
                    localPlayer.Move(General.DIRECTION.RIGHT);
                    localPlayer.isMoving = true;
                }
            }
        }
        // Verifica o estado das teclas para determinar se o jogador local está se movendo.
        static void CheckKeyState()
        {
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

            if (localPlayer != null)
            {
                View.camera.Follow(localPlayer);
            }

            if (nenhumaTeclaPressionada && localPlayer != null)
            {
                localPlayer.isMoving = false;
            }
        }
        // Atualiza o estado do jogo
        static void UpdateGameState()
        {
            
        }
        // Realiza a limpeza necessária, como destruição de janelas e renderizadores SDL.
        static void CleanUp()
        {
            SDL.SDL_DestroyRenderer(View.renderer);
            SDL.SDL_DestroyWindow(View.window);
            SDL.SDL_Quit();
        }
    }
}
