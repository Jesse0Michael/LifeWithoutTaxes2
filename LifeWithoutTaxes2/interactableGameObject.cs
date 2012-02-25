using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
namespace LifeWithoutTaxes2
{
    class interactableGameObject : gameObject
    {
        

        public interactableGameObject(Vector2 position, String fileName) : base (position, fileName)
        {
            

        }


        public override void Update(GameTime time)
        {
            this.rect.X = (int)this.position.X;
            this.rect.Y = (int)this.position.Y;
            this.rect.X = (int)(this.position.X - (this.rect.Width / 2));
            this.rect.Y = (int)(this.position.Y - (this.rect.Height / 2));


        }



    }
}