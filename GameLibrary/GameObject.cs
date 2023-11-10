using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SDLC_.GameLibrary
{
    internal class GameObject
    {
        private string name = "";
        public static double animationSpeed = 0.2;
        private Vector2 position;
        private List<Animation> animations = new List<Animation>();
        private Dictionary<General.Inputs, Animation> animationDictionary = new Dictionary<General.Inputs, Animation>();
        private Animation currentAnimation = new Animation(Resources.GameObjects.UNDEFINED, Resources.Animation_State.IDLE);

        public GameObject(Resources.GameObjects name, Vector2 position)
        {

            this.animationDictionary = Animations.GetAnimationDictionary(name);
            this.animations = Animations.GetAnimations(name);
            this.currentAnimation = new Animation(name, Resources.Animation_State.BOTTOM);
            this.position = position;

            switch (name)
            {
                case Resources.GameObjects.PLAYER:
                    this.name = "PLAYER";
                    break;
            }
        }

        public string GetName()
        {
            return this.name;
        }

        public Vector2 GetPosition()
        {
            return this.position;
        }

        public Animation GetCurrentAnimation()
        {
            return this.currentAnimation;
        }

        public void SetCurrentAnimation(General.Inputs input)
        {
            this.currentAnimation = animationDictionary[input];
        }

        public void SetPosition (float x, float y)
        {
            this.position = new Vector2(x, y);
        }

        public void UpdateAnimation(double deltaTime)
        {
            View.animationTimer += deltaTime;

            if (View.animationTimer >= animationSpeed)
            {
                int currentFrameIndex = currentAnimation.GetCurrentFrameIndex();
                currentFrameIndex = (currentFrameIndex + 1) % currentAnimation.GetFrameCount();
                currentAnimation.SetCurrentFrameIndex(currentFrameIndex);
                View.animationTimer = 0.0;
            }
        }

    }
}
