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
        private Sprite sprite = new Sprite(General.Images_path.UNDEFINED);
        private Vector2 position;

        public GameObject(General.GameObjects name, Vector2 position)
        {
            switch (name)
            {
                case General.GameObjects.APPLE:
                    this.sprite = new Sprite(General.Images_path.APPLE);
                    this.name = "APPLE";
                    break;
                case General.GameObjects.ORANGE:
                    this.sprite = new Sprite(General.Images_path.ORANGE);
                    this.name = "ORANGE";
                    break;
                case General.GameObjects.BANANA:
                    this.sprite = new Sprite(General.Images_path.BANANA);
                    this.name = "BANANA";
                    break;
                default:
                    this.sprite = new Sprite(General.Images_path.UNDEFINED);
                    this.name = "UNDEFINED";
                    break;
            }
            this.position = position;
        }

        public string GetName()
        {
            return this.name;
        }

        public Sprite GetSprite()
        {
            return this.sprite;
        }

        public Vector2 GetPosition()
        {
            return this.position;
        }

        public void SetPosition (float x, float y)
        {
            this.position = new Vector2(x, y);
        }

    }
}
