﻿using System;
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
    class mouseHelp : interactableGameObject
    {
        public MouseState mState;
        public Boolean mouseDown;
        public MouseState oldMouse;

        public mouseHelp() : base(Vector2.Zero, "Texture/pointer")
        {
            mState = Mouse.GetState();

            this.position = new Vector2(mState.X, mState.Y);

            


        }
        public override void Update(GameTime time)
        {

            this.mState = Mouse.GetState();
            this.mouseDown = ((mState.LeftButton == ButtonState.Pressed) && (oldMouse.LeftButton == ButtonState.Released));
            this.position.X = this.mState.X;
            this.position.Y = this.mState.Y;

            

            base.Update(time);

            /*if (crossHair)
            {
                Console.WriteLine("yes");
                this.texture = "Texture/crossHair";
            }
            else
            {
                this.fileName = "Texture/pointer";
            }*/

            oldMouse = mState;
        }


    }
}
