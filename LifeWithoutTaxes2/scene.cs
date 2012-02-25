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
    class scene
    {
        public List<gameObject> gameObjects;
        public List<Obstacle> obstacleList;
        public stateGame gameState;
        public Boolean endScene;
        public sceneControler sceneControl;
        public stateGame gotoState;

        public scene()
        {

        }

        public scene(List<gameObject> gameObjects, List<Obstacle> obstacleList, stateGame gameState)
        {
            this.gameObjects = gameObjects;
            this.obstacleList = obstacleList;
            this.gameState = gameState;
            this.sceneControl = sceneControler.next;


        }

        public scene(List<gameObject> gameObjects, List<Obstacle> obstacleList, stateGame gameState, sceneControler sceneControl)
        {
            this.gameObjects = gameObjects;
            this.obstacleList = obstacleList;
            this.gameState = gameState;
            if((sceneControl == sceneControler.next) || (sceneControl == sceneControler.previous))
            {
                this.sceneControl = sceneControl;
            }
            else
            {
                this.sceneControl = sceneControler.next;
            }
        }

        public scene(List<gameObject> gameObjects, List<Obstacle> obstacleList, stateGame gameState, sceneControler sceneControl, stateGame gotoState)
        {
            this.gameObjects = gameObjects;
            this.obstacleList = obstacleList;
            this.gameState = gameState;
            this.sceneControl = sceneControl;
            this.gotoState = gotoState;
        }
        public virtual void LoadContent(ContentManager content)
        {
           foreach(gameObject gameObject in gameObjects)
           {
               gameObject.LoadContent(content);
           } 
            
           foreach(Obstacle Obstacle in obstacleList)
           {
               Obstacle.LoadContent(content);
           }

        }


        public virtual void Update(GameTime time)
        {
            foreach (gameObject gameObject in gameObjects)
            {
                gameObject.Update(time);
            }

            foreach (Obstacle Obstacle in obstacleList)
            {
                Obstacle.Update(time);
            }

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

            foreach (gameObject gameObject in gameObjects)
            {
                gameObject.Draw(spriteBatch);
            }

            foreach (Obstacle Obstacle in obstacleList)
            {
                Obstacle.Draw(spriteBatch);
            }

        }
        


    }
}
