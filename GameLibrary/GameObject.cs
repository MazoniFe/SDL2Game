using SDL2;
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
        protected string name = "";
        public static double animationSpeed = 0.5f;
        public float moveSpeed = 2.5f;
        private Vector2 position, targetPosition, worldPosition;
        public bool canMove = true;
        public bool isMoving = false;

        private double animationTimer = 0.0;
        private Dictionary<(General.DIRECTION, Resources.Animation_State), Animation> animationDictionary = new Dictionary<(General.DIRECTION, Resources.Animation_State), Animation>();
        private Animation currentAnimation = new Animation(Resources.GameObjects.UNDEFINED, Resources.Animation_State.IDLE, General.DIRECTION.BOTTOM);

        private General.DIRECTION direction;

        // Construtor que inicializa um objeto com um nome e posição.
        public GameObject(Resources.GameObjects name, Vector2 position)
        {
            // Obtém o dicionário de animações e a lista de animações para o objeto.
            this.animationDictionary = Animations.GetAnimationDictionary(name);
            // Define a animação inicial como a animação de movimento para baixo.
            this.currentAnimation = new Animation(name, Resources.Animation_State.IDLE, direction);
            // Inicializa as posições do objeto no mundo.
            this.position = position;
            this.worldPosition = World.PixelToMatrixPosition((int)position.X, (int)position.Y);
            this.targetPosition = worldPosition;
            this.direction = General.DIRECTION.BOTTOM;
            Program.world.GetWorldTile(this.worldPosition).AddObject();
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
            if (state == Resources.Animation_State.IDLE && this.isMoving == true) return;
            var newAnimation = animationDictionary[(direction, state)];
            if (currentAnimation != newAnimation)
            {
                this.currentAnimation = animationDictionary[(direction, state)];
            }
        }

        // Método para definir a posição alvo com base na entrada do jogador.
        protected void SetPosition(General.DIRECTION key)
        {
            if (!canMove) return;
            var currentX = (int)worldPosition.X;
            var currentY = (int)worldPosition.Y;

            switch (key)
            {
                case General.DIRECTION.TOP:
                    targetPosition = new Vector2(currentX, currentY - 1);
                    direction = key;
                    SetCurrentAnimation(key, Resources.Animation_State.WALK);
                    break;
                case General.DIRECTION.LEFT:
                    targetPosition = new Vector2(currentX - 1, currentY);
                    direction = key;
                    SetCurrentAnimation(key, Resources.Animation_State.WALK);
                    break;
                case General.DIRECTION.BOTTOM:
                    targetPosition = new Vector2(currentX, currentY + 1);
                    direction = key;
                    SetCurrentAnimation(key, Resources.Animation_State.WALK);
                    break;
                case General.DIRECTION.RIGHT:
                    targetPosition = new Vector2(currentX + 1, currentY);
                    direction = key;
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
            animationTimer += deltaTime;
            int frameCount = currentAnimation.GetFrameCount();

            if (frameCount > 0 && animationTimer >= animationSpeed)
            {
                int currentFrameIndex = currentAnimation.GetCurrentFrameIndex();
                currentFrameIndex = (currentFrameIndex + 1) % frameCount;
                currentAnimation.SetCurrentFrameIndex(currentFrameIndex);
                animationTimer = 0.0;
            }
        }

        // Método para atualizar a posição do objeto.
        public void UpdatePosition()
        {
            if (Program.world.GetWorldTile(targetPosition).HasObject())
            {
                SetCurrentAnimation(this.direction, Resources.Animation_State.IDLE);
                canMove = true;
                return;
            }

            float step = moveSpeed * (float)View.deltaTime;
            this.worldPosition = General.MoveTowards(this.worldPosition, targetPosition, step);

            if (Vector2.Distance(this.worldPosition, targetPosition) == 0f)
            {
                SetCurrentAnimation(direction, Resources.Animation_State.IDLE);
                canMove = true;
            }
            else canMove = false;
            this.position = World.GetPosition(worldPosition.X, worldPosition.Y);
        }


        public Vector2 GetWorldPosition()
        {
            return this.worldPosition;
        }
    }
}
