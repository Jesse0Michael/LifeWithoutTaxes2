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
    class gameObject
    {
        public Texture2D texture;
        public Vector2 position;
        public String fileName;
        public Rectangle rect;
        public Color tint;
        public float zIndex;
        public float scale;
        public float rotation;

        public gameObject(Vector2 position, String fileName)
        {
            this.position = position;
            this.fileName = fileName;
            this.tint = Color.White;

            scale = 1.0f;
            rotation = 0.0f;
            zIndex = 0.1f;

            this.rect = new Rectangle((int)this.position.X, (int)this.position.Y, 0, 0);

        }
        public virtual void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(fileName);

            this.rect.Width = texture.Width;
            this.rect.Height = texture.Height;
            this.rect.X = (int)(this.position.X - (this.rect.Width / 2));
            this.rect.Y = (int)(this.position.Y - (this.rect.Height/2));

        }


        public virtual void Update(GameTime time)
        {


        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, new Vector2 (this.rect.X, this.rect.Y), null, this.tint, this.rotation, Vector2.Zero, this.scale, SpriteEffects.None, this.zIndex);


        }


    }
}
