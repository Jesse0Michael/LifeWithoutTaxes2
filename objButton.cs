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
    class objButton :interactableGameObject
    {
        private String text;
        private SpriteFont arial;
        private Vector2 fontPosition;

        public objButton(Vector2 position, String text) : base (position, "Texture/buttonDown")
        {
            this.text = text;
        }
        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            arial = content.Load<SpriteFont>("Fonts/fontArial");
        }
        public override void Update(GameTime time)
        {
            base.Update(time);
            this.fontPosition = this.position - this.arial.MeasureString(text) / 2;
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            spriteBatch.DrawString(arial, text, fontPosition, tint, rotation, Vector2.Zero, scale, SpriteEffects.None, zIndex + 0.01f);
        }

    }
}
