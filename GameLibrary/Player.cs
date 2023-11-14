using SDLC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SDLC_.GameLibrary
{
    internal class Player : Entity
    {
        private bool isLocal;
        public Player(string name,Resources.GameObjects obj, Vector2 position) : base(obj, position)
        {
            this.name = name;
            this.isLocal = false;
        }


        public void Move(General.DIRECTION direction)
        {
            if (!isLocal) return;
            SetPosition(direction);
        }
        public bool GetIsLocal()
        {
            return isLocal;
        }

        public void SetLocalPlayer()
        {
            isLocal = true;
        }


    }
}
