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
        // Método para obter um dicionário de animações com base no tipo de objeto.
        public static Dictionary<(General.DIRECTION, Resources.Animation_State), Animation> GetAnimationDictionary(Resources.GameObjects obj)
        {
            Dictionary<(General.DIRECTION, Resources.Animation_State), Animation> animationDictionary = new Dictionary<(General.DIRECTION, Resources.Animation_State), Animation>();
            //TOP
            animationDictionary[(General.DIRECTION.TOP, Resources.Animation_State.WALK)] = new Animation(obj, Resources.Animation_State.WALK, General.DIRECTION.TOP);
            animationDictionary[(General.DIRECTION.TOP, Resources.Animation_State.IDLE)] = new Animation(obj, Resources.Animation_State.IDLE, General.DIRECTION.TOP);
            //LEFT
            animationDictionary[(General.DIRECTION.LEFT, Resources.Animation_State.WALK)] = new Animation(obj, Resources.Animation_State.WALK, General.DIRECTION.LEFT);
            animationDictionary[(General.DIRECTION.LEFT, Resources.Animation_State.IDLE)] = new Animation(obj, Resources.Animation_State.IDLE, General.DIRECTION.LEFT);
            
            //BOTTOM
            animationDictionary[(General.DIRECTION.BOTTOM, Resources.Animation_State.WALK)] = new Animation(obj, Resources.Animation_State.WALK, General.DIRECTION.BOTTOM);
            animationDictionary[(General.DIRECTION.BOTTOM, Resources.Animation_State.IDLE)] = new Animation(obj, Resources.Animation_State.IDLE, General.DIRECTION.BOTTOM);

            //RIGHT
            animationDictionary[(General.DIRECTION.RIGHT, Resources.Animation_State.WALK)] = new Animation(obj, Resources.Animation_State.WALK, General.DIRECTION.RIGHT);
            animationDictionary[(General.DIRECTION.RIGHT, Resources.Animation_State.IDLE)] = new Animation(obj, Resources.Animation_State.IDLE, General.DIRECTION.RIGHT);
            return animationDictionary;
        }
    }
}
