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

    class keyboardHelp
    {

        public Dictionary<Controls, bool> controlState;
        public Dictionary<Keys, Controls> keyboardControlScheme;
        private KeyboardState keyboardState;

        public keyboardHelp()
        {

                    this.keyboardControlScheme = new Dictionary<Keys, Controls>();
                    this.keyboardControlScheme.Add(Keys.Up, Controls.up);
                    this.keyboardControlScheme.Add(Keys.Down, Controls.down);
                    this.keyboardControlScheme.Add(Keys.Left, Controls.left);
                    this.keyboardControlScheme.Add(Keys.Right, Controls.right);
                    this.keyboardControlScheme.Add(Keys.D, Controls.D);
                    this.keyboardControlScheme.Add(Keys.A, Controls.A);
                    this.keyboardControlScheme.Add(Keys.W, Controls.W);
                    this.keyboardControlScheme.Add(Keys.S, Controls.S);
                   
                
            

            this.controlState = new Dictionary<Controls, bool>();
            this.controlState.Add(Controls.up, false);
            this.controlState.Add(Controls.down, false);
            this.controlState.Add(Controls.left, false);
            this.controlState.Add(Controls.right, false);
            this.controlState.Add(Controls.D, false);
            this.controlState.Add(Controls.A, false);
            this.controlState.Add(Controls.W, false);
            this.controlState.Add(Controls.S, false);

        }

        public void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            foreach (Keys key in keyboardControlScheme.Keys)
            {
                if (keyboardState.IsKeyDown(key))
                {
                    Controls control = keyboardControlScheme[key];
                    if (controlState.ContainsKey(control))
                    {
                        controlState[control] = true;
                    }
                }
                else if (keyboardState.IsKeyUp(key))
                {
                    Controls control = keyboardControlScheme[key];
                    if (controlState.ContainsKey(control))
                    {
                        controlState[control] = false;
                    }
                }
            }
        }
            

    }
}