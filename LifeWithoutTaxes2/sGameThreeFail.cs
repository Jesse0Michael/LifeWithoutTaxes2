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
    class sGameThreeFail : interactableScene
    {
        private objButton conButt;

        public sGameThreeFail(mouseHelp mouse, keyboardHelp keyboard, GraphicsDevice GraphicsDevice)
            : base(mouse, keyboard, GraphicsDevice)
        {


            this.gameObjects = new List<gameObject>();
            this.obstacleList = new List<Obstacle>();
            this.gameObjects.Add(new gameObject(new Vector2(400, 300), "MenuArt/DrivingTwoFail"));
            conButt = new objButton(new Vector2(400, 500), "Retry");
            conButt.zIndex = 0.5f;

            this.gameObjects.Add(conButt);
            this.gameState = stateGame.game3Fail;

        }


        public override void Update(GameTime time)
        {
            base.Update(time);


            if (this.mouse.rect.Intersects(conButt.rect))
            {
                if (this.mouse.mouseDown)
                {

                    this.sceneControl = sceneControler.previous;
                    this.endScene = true;

                }
                conButt.tint = Color.Gray;
            }
            else
            {

                conButt.tint = Color.White;
            }



        }



    }
}