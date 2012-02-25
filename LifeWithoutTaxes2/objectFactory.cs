using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    class objectFactory
    {
        Effect Effect1;

        private GraphicsDevice graphics;
        private SpriteBatch spriteBatch;
        private ContentManager Content;
        

        public objectFactory(ContentManager Content, GraphicsDevice graphics, SpriteBatch spriteBatch)
        {
            this.Content = Content;
            this.graphics = graphics;
            this.spriteBatch = spriteBatch;

            Effect1 = Content.Load<Effect>("Effect1");

        }

        public void createMainMenu()
        {

            Texture2D mainMenu = Content.Load<Texture2D>("MenuArt/titleScreen");


            spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.FrontToBack, SaveStateMode.SaveState);


            spriteBatch.Draw(mainMenu, Vector2.Zero, Color.White);



            spriteBatch.End();

        }
        public void createCredits()
        {
            Texture2D Credits = Content.Load<Texture2D>("MenuArt/DrivingOneIntro");


            spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.FrontToBack, SaveStateMode.SaveState);


            spriteBatch.Draw(Credits, Vector2.Zero, Color.White);



            spriteBatch.End();

        }
        public void createGameIntro()
        {
            Texture2D Screen = Content.Load<Texture2D>("MenuArt/gameIntro");


            spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.FrontToBack, SaveStateMode.SaveState);


            spriteBatch.Draw(Screen, Vector2.Zero, Color.White);



            spriteBatch.End();

        }
        public void createGame1Intro()
        {
            Texture2D Screen = Content.Load<Texture2D>("MenuArt/DrivingOneIntro");


            spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.FrontToBack, SaveStateMode.SaveState);


            spriteBatch.Draw(Screen, Vector2.Zero, Color.White);



            spriteBatch.End();

        }
        public void createGame1()
        {
            gameOneControl controller = new gameOneControl(Content, graphics, spriteBatch, Effect1);
            controller.Update();
            controller.Draw();



        }





    }
}
