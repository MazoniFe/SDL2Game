using SDLC_.GameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDLC_.GameLibrary
{
    // Classe que gerencia as animações no jogo.
    internal class Animations
    {
        // Método para obter as animações com base no tipo de objeto.
        public static List<Animation> GetAnimations(Resources.GameObjects obj)
        {
            return GetAnimationsFromGameObject(obj);
        }

        // Método privado para obter as animações com base no tipo de objeto.
        private static List<Animation> GetAnimationsFromGameObject(Resources.GameObjects obj)
        {
            List<Animation> anim = new List<Animation>();
            switch (obj)
            {
                case Resources.GameObjects.PLAYER:
                    anim.Add(new Animation(obj, Resources.Animation_State.LEFT));
                    anim.Add(new Animation(obj, Resources.Animation_State.BOTTOM));
                    break;

            }
            return anim;
        }

        // Método para obter um dicionário de animações com base no tipo de objeto.
        public static Dictionary<General.Inputs, Animation> GetAnimationDictionary(Resources.GameObjects obj)
        {
            Dictionary<General.Inputs, Animation> animationDictionary = new Dictionary<General.Inputs, Animation>();
            switch (obj)
            {
                case Resources.GameObjects.PLAYER:
                    animationDictionary[General.Inputs.TOP] = new Animation(obj, Resources.Animation_State.TOP);
                    animationDictionary[General.Inputs.LEFT] = new Animation(obj, Resources.Animation_State.LEFT);
                    animationDictionary[General.Inputs.BOTTOM] = new Animation(obj, Resources.Animation_State.BOTTOM);
                    animationDictionary[General.Inputs.RIGHT] = new Animation(obj, Resources.Animation_State.RIGHT);
                    animationDictionary[General.Inputs.NULL] = new Animation(obj, Resources.Animation_State.NULL);
                    break;
            }
            return animationDictionary;
        }
    }
}
