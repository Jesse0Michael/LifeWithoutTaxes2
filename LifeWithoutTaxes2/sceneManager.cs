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
    class sceneManager
    {
        public List<scene> scenes;
        public int sceneIndex;
        public Boolean exit;
        public Song sound1;
        public Song sound2;
        public Song sound3;
        public Song sound4;
        public Song sound5;

        public sceneManager(List<scene> scenes)
        {
            this.scenes = scenes;
            this.sceneIndex = 0;
            exit = false;
            

        }
        public virtual void LoadContent(ContentManager content)
        {
            sound1 = content.Load<Song>("Sound/Frost Waltz");
            sound2 = content.Load<Song>("Sound/Killing Time");
            sound3 = content.Load<Song>("Sound/Scheming Weasel faster");
            sound4 = content.Load<Song>("Sound/Kick Shock");
            sound5 = content.Load<Song>("Sound/Faster Does It");

            foreach (scene scene in scenes)
            {
                scene.LoadContent(content);
            }

            DJ();
        }


        public virtual void Update(GameTime time)
        {
            scenes[sceneIndex].Update(time);
            if (scenes[sceneIndex].endScene)
            {
                if (scenes[sceneIndex].sceneControl == sceneControler.next)
                {
                    nextScene();
                }
                else if (scenes[sceneIndex].sceneControl == sceneControler.previous)
                {
                    prevScene();
                }
                else if (scenes[sceneIndex].sceneControl == sceneControler.goTo)
                {
                    goToScene(scenes[sceneIndex].gotoState);
                    DJ();
                }
                else if (scenes[sceneIndex].sceneControl == sceneControler.reset)
                {
                    resetGame();
                    DJ();
                }
                else if (scenes[sceneIndex].sceneControl == sceneControler.exit)
                {
                    exit = true;
                }


                
             

            }

        }
        public void DJ()
        {
            if (sceneIndex == 0)
            {
                MediaPlayer.Play(sound1);
            }
            else if (sceneIndex == 3)
            {
                MediaPlayer.Play(sound2);
            }
            else if (sceneIndex == 9)
            {
                MediaPlayer.Play(sound3);
            }
            else if (sceneIndex == 11)
            {
                MediaPlayer.Play(sound4);
            }
            else if (sceneIndex == 14)
            {
                MediaPlayer.Play(sound5);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

            scenes[sceneIndex].Draw(spriteBatch);

        }
        private void nextScene()
        {
            if (sceneIndex < scenes.Count)
            {
                sceneIndex++;
            }
            else
            {
                sceneIndex = 0;
            }

        }
        private void prevScene()
        {
            if (sceneIndex > 0)
            {
                sceneIndex--;
                scenes[sceneIndex].endScene = false;

            }
            else
            {
                sceneIndex = scenes.Count - 1;
            }

        }
        private void goToScene(stateGame gameState)
        {
            for (int i = 0; i < scenes.Count; i++)
            {
                if (scenes[i].gameState == gameState)
                {
                    scenes[i].endScene = false;
                    sceneIndex = i;
                    break;
                }
            }


        }
        public void resetGame()
        {
            for(int i = 0; i < scenes.Count; i++)
            {
                scenes[i].endScene = false;
            }
            goToScene(stateGame.mainMenu);
            //this.sceneControl = sceneControler.goTo;
            //this.gotoState = stateGame.mainMenu;

        }

    }
}
