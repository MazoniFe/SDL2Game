using SDLC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SDLC_.GameLibrary
{
    // Classe que representa um objeto no jogo.
    internal class GameObject
    {
        private string name = "";
        public static double animationSpeed = 0.5f;
        public float moveSpeed = 3;
        private Vector2 position, targetPosition, worldPosition;
        public bool canMove = true;
        private Dictionary<(General.DIRECTION, Resources.Animation_State), Animation> animationDictionary = new Dictionary<(General.DIRECTION, Resources.Animation_State), Animation>();
        private Animation currentAnimation = new Animation(Resources.GameObjects.UNDEFINED, Resources.Animation_State.IDLE, General.DIRECTION.BOTTOM);

        // Construtor que inicializa um objeto com um nome e posição.
        public GameObject(Resources.GameObjects name, Vector2 position)
        {
            // Obtém o dicionário de animações e a lista de animações para o objeto.
            this.animationDictionary = Animations.GetAnimationDictionary(name);

            // Define a animação inicial como a animação de movimento para baixo.
            this.currentAnimation = new Animation(name, Resources.Animation_State.IDLE, General.DIRECTION.BOTTOM);

            // Inicializa as posições do objeto no mundo.
            this.position = position;
            this.worldPosition = World.PixelToMatrixPosition((int)this.position.X, (int)this.position.Y);

            // Define o nome do objeto com base no tipo.
            switch (name)
            {
                case Resources.GameObjects.PLAYER:
                    this.name = "PLAYER";
                    break;
            }
        }

        // Método para obter o nome do objeto.
        public string GetName()
        {
            return this.name;
        }

        // Método para obter a posição do objeto.
        public Vector2 GetPosition()
        {
            return this.position;
        }

        // Método para obter a animação atual do objeto.
        public Animation GetCurrentAnimation()
        {
            return this.currentAnimation;
        }

        // Método para definir a animação atual com base na entrada do jogador.
        public void SetCurrentAnimation(General.DIRECTION direction, Resources.Animation_State state)
        {
            this.currentAnimation = animationDictionary[(direction, state)];
        }

        // Método para definir a posição alvo com base na entrada do jogador.
        public void SetPosition(General.DIRECTION key)
        {
            if (!canMove) return;

            var currentX = (int)worldPosition.X;
            var currentY = (int)worldPosition.Y;
            Program.world.GetWorldTile(this.GetWorldPosition()).RemoveObject();
            World_Tile tile;

            switch (key)
            {
                case General.DIRECTION.TOP:
                    targetPosition = new Vector2(currentX, currentY - 1);
                    tile = Program.world.GetWorldTile(targetPosition);
                    if (tile.HasObject()) return;
                    General.direction = General.DIRECTION.TOP;
                    SetCurrentAnimation(key, Resources.Animation_State.WALK);
                    break;
                case General.DIRECTION.LEFT:
                    targetPosition = new Vector2(currentX - 1, currentY);
                    tile = Program.world.GetWorldTile(targetPosition);
                    if (tile.HasObject()) return;
                    General.direction = General.DIRECTION.LEFT;
                    SetCurrentAnimation(key, Resources.Animation_State.WALK);
                    break;
                case General.DIRECTION.BOTTOM:
                    targetPosition = new Vector2(currentX, currentY + 1);
                    tile = Program.world.GetWorldTile(targetPosition);
                    if (tile.HasObject()) return;
                    General.direction = General.DIRECTION.BOTTOM;
                    SetCurrentAnimation(key, Resources.Animation_State.WALK);
                    break;
                case General.DIRECTION.RIGHT:
                    targetPosition = new Vector2(currentX + 1, currentY);
                    tile = Program.world.GetWorldTile(targetPosition);
                    if (tile.HasObject()) return;
                    General.direction = General.DIRECTION.RIGHT;
                    SetCurrentAnimation(key, Resources.Animation_State.WALK);
                    break;
                default:
                    break;
            }
            canMove = false;
        }

        // Método para atualizar a animação do objeto com base no tempo delta.
        public void UpdateAnimation(double deltaTime)
        {
            // Atualiza o temporizador de animação.
            View.animationTimer += deltaTime;
            int frameCount = currentAnimation.GetFrameCount();

            if(frameCount > 0.1f)
            {
                if (View.animationTimer >= animationSpeed)
                {
                    int currentFrameIndex = currentAnimation.GetCurrentFrameIndex();
                    currentFrameIndex = (currentFrameIndex + 1) % currentAnimation.GetFrameCount();
                    currentAnimation.SetCurrentFrameIndex(currentFrameIndex);
                    View.animationTimer = 0.0;
                }
            }

        }

        // Método para atualizar a posição do objeto.
        public void UpdatePosition()
        {
            if (!canMove)
            {
                // Calcula o passo com base na velocidade de movimento e no tempo delta.
                float step = moveSpeed * (float)View.deltaTime;

                // Move o objeto em direção à posição alvo.
                this.worldPosition = General.MoveTowards(this.worldPosition, targetPosition, step);

                // Verifica se o objeto atingiu a posição alvo.
                if (Vector2.Distance(this.worldPosition, targetPosition) < 0.01f)
                {
                    this.worldPosition = targetPosition;
                    canMove = true;
                    SetCurrentAnimation(General.direction, Resources.Animation_State.IDLE);
                }

                // Converte a posição no mundo para a posição em pixels.
                this.position = World.GetPosition(worldPosition.X, worldPosition.Y);
            }
        }
    
        public Vector2 GetWorldPosition()
        {
            return this.worldPosition;
        }
    }
}
