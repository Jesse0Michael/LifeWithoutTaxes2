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


    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private sceneManager sceneManager;
        private keyboardHelp keyboard;
        private mouseHelp mouse;

        
        
        

        public static int gameState;


        public Game1()
        {

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Window.Title = "Life Without Taxes 2";
            gameState = 0;
            keyboard = new keyboardHelp();
            mouse = new mouseHelp();
            mouse.zIndex = 0.9f;

            sTitleScreen sTitleScreen = new sTitleScreen(mouse, keyboard, GraphicsDevice);
            sGameIntro sGameIntro = new sGameIntro(mouse, keyboard, GraphicsDevice);
            sGameOneIntro sGameOneIntro = new sGameOneIntro(mouse, keyboard, GraphicsDevice);
            sGameOne sGameOne = new sGameOne(mouse, keyboard, GraphicsDevice);
            sGameOneWin sGameOneWin = new sGameOneWin(mouse, keyboard, GraphicsDevice);
            sGameOneFail sGameOneFail = new sGameOneFail(mouse, keyboard, GraphicsDevice);
            sGameTwoIntro sGameTwoIntro = new sGameTwoIntro(mouse, keyboard, GraphicsDevice);
            sGameTwo sGameTwo = new sGameTwo(mouse, keyboard, GraphicsDevice);
            sGameThree sGameThree = new sGameThree(mouse, keyboard, GraphicsDevice);
            sGameTwoWin sGameTwoWin = new sGameTwoWin(mouse, keyboard, GraphicsDevice);
            sGameTwoFail sGameTwoFail = new sGameTwoFail(mouse, keyboard, GraphicsDevice);
            sGameCredits sGameCredits = new sGameCredits(mouse, keyboard, GraphicsDevice);
            sGameOutro sGameOutro = new sGameOutro(mouse, keyboard, GraphicsDevice);
            sGameThreeFail sGameThreeFail = new sGameThreeFail(mouse, keyboard, GraphicsDevice);
            sGameFour sGameFour = new sGameFour(mouse, keyboard, GraphicsDevice);
            sGameFourFail sGameFourFail = new sGameFourFail(mouse, keyboard, GraphicsDevice);
            sGameFourIntro sGameFourIntro = new sGameFourIntro(mouse, keyboard, GraphicsDevice);
            sceneManager = new sceneManager(new List<scene> { sTitleScreen, sGameIntro, sGameOneIntro, sGameOne,
                                                              sGameOneFail, sGameOneWin, sGameTwoIntro, sGameTwoIntro, 
                                                              sGameTwoWin, sGameTwo, sGameTwoFail, sGameThree, 
                                                              sGameThreeFail, sGameFourIntro, sGameFour,
                                                              sGameFourFail, sGameOutro, sGameCredits});
            



            base.Initialize();
        }
        

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            sceneManager.LoadContent(Content);
            
        }


        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            sceneManager.Update(gameTime);

            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.FrontToBack, SaveStateMode.SaveState);
            sceneManager.Draw(spriteBatch);

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
