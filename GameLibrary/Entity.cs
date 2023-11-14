using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SDLC_.GameLibrary
{
    internal class Entity : GameObject
    {
        private int currentHealth;
        private int maxHealth;
        public Entity(Resources.GameObjects name, Vector2 position) : base(name, position)
        {
            this.maxHealth = 100;
            this.currentHealth = this.maxHealth;
        }

        public int GetCurrentHealth()
        {
            return this.currentHealth;
        }
        public int GetMaxHealth()
        {
            return this.maxHealth;
        }
    }
}
