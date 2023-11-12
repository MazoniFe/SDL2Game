using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SDLC_.GameLibrary
{
    internal class Player : GameObject
    {
        public Player(string name,Resources.GameObjects obj, Vector2 position) : base(obj, position)
        {
            this.name = name;
        }
    }
}
