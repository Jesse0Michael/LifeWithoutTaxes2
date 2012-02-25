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
    class sTitleScreen : interactableScene
    {
        private objButton startGame;
        private objButton credits;

        public sTitleScreen(mouseHelp mouse, keyboardHelp keyboard, GraphicsDevice GraphicsDevice) : base(mouse, keyboard, GraphicsDevice)
        {


            this.gameObjects = new List<gameObject>();
            this.obstacleList = new List<Obstacle>();
            this.gameObjects.Add(new gameObject(new Vector2(400,300), "MenuArt/titleScreen"));
            startGame = new objButton(new Vector2(200, 500), "Start");
            credits = new objButton(new Vector2(555, 500), "Credits");
            startGame.zIndex = 0.5f;
            credits.zIndex = 0.5f;

            this.gameObjects.Add(startGame);
            this.gameObjects.Add(credits);
            this.gameState = stateGame.mainMenu;

        }


        public override void Update(GameTime time)
        {
            base.Update(time);


            if (this.mouse.rect.Intersects(startGame.rect))
            {
                if (this.mouse.mouseDown)
                {
                    this.sceneControl = sceneControler.goTo;
                    this.gotoState = stateGame.gameIntro;
                    this.endScene = true;
                }
                startGame.tint = Color.Gray;
            }
            else
            {

                startGame.tint = Color.White;
            }

            if (this.mouse.rect.Intersects(credits.rect))
            {
                if (this.mouse.mouseDown)
                {
                    this.sceneControl = sceneControler.goTo;
                    this.gotoState = stateGame.credits;
                    this.endScene = true;
                }
                credits.tint = Color.Gray;
            }
            else
            {


                credits.tint = Color.White;
            } 


        }



    }
}