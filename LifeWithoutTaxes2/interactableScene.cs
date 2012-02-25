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
    class interactableScene : scene
    {
        public mouseHelp mouse;
        public keyboardHelp keyboard;
        public GraphicsDevice GraphicsDevice;


        public interactableScene(mouseHelp mouse, keyboardHelp keyboard, GraphicsDevice GraphicsDevice)
        {
            this.keyboard = keyboard;
            this.mouse = mouse;
            this.GraphicsDevice = GraphicsDevice;
        }
        public interactableScene(List<gameObject> gameObjects, List<Obstacle> obstacleList, stateGame gameState, mouseHelp mouse, keyboardHelp keyboard)
            : base(gameObjects, obstacleList, gameState)
        {
            this.keyboard = keyboard;
            this.mouse = mouse;


        }

        public interactableScene(List<gameObject> gameObjects, List<Obstacle> obstacleList, stateGame gameState, sceneControler sceneControl, mouseHelp mouse, keyboardHelp keyboard)
            : base(gameObjects, obstacleList, gameState, sceneControl)
        {
            this.keyboard = keyboard;
            this.mouse = mouse;
            

        }

        public interactableScene(List<gameObject> gameObjects, List<Obstacle> obstacleList, stateGame gameState, sceneControler sceneControl, stateGame gotoState, mouseHelp mouse, keyboardHelp keyboard)
            : base(gameObjects, obstacleList, gameState, sceneControl, gotoState)
        {
            this.keyboard = keyboard;
            this.mouse = mouse;


        }
        public override void LoadContent(ContentManager content)
        {
            mouse.LoadContent(content);

            base.LoadContent(content);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            mouse.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }

        public override void Update(GameTime time)
        {
            base.Update(time);
            this.mouse.Update(time);
            this.keyboard.Update(time);

        }
        

    }
}
