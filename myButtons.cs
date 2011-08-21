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

    class myButtons
    {
        private String Identity;
        private String Name;
        private int xloc;
        private int yloc;
        private int xOffset;
        private int yOffset;
        private SpriteFont fontArial;
        private Texture2D buttonUp;
        private Texture2D buttonDown;
        private Rectangle location;
        private MouseState mouse;
        private MouseState oldMouse;

        private GraphicsDevice graphics;
        private SpriteBatch spriteBatch;
        private ContentManager Content;
        
        public myButtons(String id, String name, int xloc, int yloc, int xOffset, int yOffset, ContentManager Content,
                         GraphicsDevice graphics, SpriteBatch spriteBatch)
        {
            this.Content = Content;
            this.graphics = graphics;
            this.spriteBatch = spriteBatch;


            Identity = id;
            Name = name;
            this.xloc = xloc;
            this.yloc = yloc;
            this.xOffset = xOffset;
            this.yOffset = yOffset;


            fontArial = Content.Load<SpriteFont>("Fonts/fontArial");
            buttonUp = Content.Load<Texture2D>("Texture/buttonUp");
            buttonDown = Content.Load<Texture2D>("Texture/buttonDown");


            location = new Rectangle(xloc, yloc, buttonUp.Width, buttonUp.Height); 


            
        }

        public void updateButton()
        {
           // mouse = Mouse.GetState();
            
            if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
            {
                if (location.Contains(new Point(mouse.X, mouse.Y)))
                {
                    switch (Identity)
                    {

                        case "Credit":
                            Game1.gameState = 1;
                            

                            break;
                        case "Return":
                            Game1.gameState = 0;

                            break;
                        case "Start":
                            Game1.gameState = 2;

                                     break;
                        case "Continue":
                                     Game1.gameState ++;

                                     break;

                      

                    }
                    
                }
            }


            
            oldMouse = mouse;
            
            
        }


        public void drawButton()
        {
            //mouse = Mouse.GetState();
            if (location.Contains(new Point(mouse.X, mouse.Y)))
            {
                
                spriteBatch.Draw(buttonUp, new Vector2(xloc, yloc), Color.White);
            }
            else
            {

                spriteBatch.Draw(buttonDown, new Vector2(xloc, yloc), Color.White);
            }
            

            spriteBatch.DrawString(fontArial, Name, new Vector2(xloc + xOffset, yloc + yOffset), Color.Black,
                                   0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.001f);


        }
    }
}
