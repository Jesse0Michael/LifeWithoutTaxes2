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
    class sGameFour : interactableScene
    {
        Effect effect1;

        Matrix worldMatrix;
        Matrix viewMatrix;
        Matrix projMatrix;

        public static float camX;


        VertexBuffer vertexBG = null;
        VertexBuffer vertexBufferGrass = null;
        IndexBuffer indexBuffer = null;
        VertexDeclaration vertexDec = null;

        Texture2D Grass;
        Texture2D Trees;

        Burglar Burglar1;
        Burglar Burglar2;
        Burglar Burglar3;
        Burglar Burglar4;
        Burglar Burglar5;

        bool left;
        bool right;

        public static float progress;
        Texture2D BarB;
        Texture2D Bar;

        private Vector3 position;

        static TimeSpan burglarTime;

        Texture2D backGround;
        Model House;
        Matrix[] transforms;

        Color screenFade = Color.White;
        Texture2D redFlash;

        Random rand = new Random();

        Texture2D cross;
        Texture2D readyGun;
        Texture2D reloadingGun;
        Boolean gunReady;
        VertexBuffer vb;
        TimeSpan reloading;
        int reloadingTime;


        public sGameFour(mouseHelp mouse, keyboardHelp keyboard, GraphicsDevice GraphicsDevice)
            : base(mouse, keyboard, GraphicsDevice)
        {


            this.gameObjects = new List<gameObject>();
            this.obstacleList = new List<Obstacle>();
            this.burglarList = new List<Burglar>();
           // this.gameObjects.Add(new gameObject(new Vector2(750, 300), "Texture/Bar"));

            this.gameState = stateGame.game4;

            resetScene();

            
            

        }
        public void resetScene()
        {
            camX = 0;
            
            right = false;
            left = false;

            

            //20
            progress = 20.0f;
            position = new Vector3(camX, 0, 0);

            for (int i = 0; i < this.burglarList.Count; i++)
            {
                this.burglarList[i].visible = false;
                this.burglarList[i].position.Z = -500;
                
                Burglar.flash = false;
            }

            screenFade.A = 0;

            gunReady = true;

            worldMatrix = Matrix.Identity;
            projMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f),
                                                                     GraphicsDevice.DisplayMode.AspectRatio,
                                                                     1.0f,
                                                                     3000.0f);

            burglarTime = new TimeSpan(0, 0, 0, 1, 0);
            reloadingTime = 900;
            reloading = new TimeSpan(0, 0, 0, 0, reloadingTime);
            prepareScene();


        }
        public void prepareScene()
        {
            //now create the index buffer
            short[] index = new short[36];
            short i;
            for (i = 0; i < 6; ++i)
            {
                short startIndex = (short)(i * 6);
                short startVertex = (short)(i * 4);
                index[startIndex + 0] = (short)(startVertex + 0);
                index[startIndex + 1] = (short)(startVertex + 1);
                index[startIndex + 2] = (short)(startVertex + 2);
                index[startIndex + 3] = (short)(startVertex + 2);
                index[startIndex + 4] = (short)(startVertex + 1);
                index[startIndex + 5] = (short)(startVertex + 3);
            }


            indexBuffer = new IndexBuffer(GraphicsDevice, sizeof(short) * 36, BufferUsage.WriteOnly, IndexElementSize.SixteenBits);
            indexBuffer.SetData(index);


            vertexDec = new VertexDeclaration(GraphicsDevice, VertexPositionColorTexture.VertexElements);

        }
        public override void LoadContent(ContentManager content)
        {

            Burglar1 = new Burglar(GraphicsDevice, new Vector3(-110, 0, -500), 1.5f, projMatrix, .85f);
            Burglar2 = new Burglar(GraphicsDevice, new Vector3(-55, 0, -500), 1.5f, projMatrix, .85f);
            Burglar3 = new Burglar(GraphicsDevice, new Vector3(0, 0, -500), 1.5f, projMatrix, .85f);
            Burglar4 = new Burglar(GraphicsDevice, new Vector3(55, 0, -500), 1.5f, projMatrix, .85f);
            Burglar5 = new Burglar(GraphicsDevice, new Vector3(110, 0, -500), 1.5f, projMatrix, .85f);

            this.burglarList.Add(Burglar1);
            this.burglarList.Add(Burglar2);
            this.burglarList.Add(Burglar3);
            this.burglarList.Add(Burglar4);
            this.burglarList.Add(Burglar5);

            base.LoadContent(content);
            effect1 = content.Load<Effect>("Effect1");
            backGround = content.Load<Texture2D>("Texture/GameTwoBkg2");
            BarB = content.Load<Texture2D>("Texture/BarFill Blue");
            Bar = content.Load<Texture2D>("Texture/Bar");
            House = content.Load<Model>("Model/wall");
            transforms = new Matrix[House.Bones.Count];
            House.CopyAbsoluteBoneTransformsTo(transforms);
            Grass = content.Load<Texture2D>("Texture/nightGrass");
            Trees = content.Load<Texture2D>("Texture/nightTrees");
            redFlash = content.Load<Texture2D>("Texture/redFlash");
            cross = content.Load<Texture2D>("Texture/crossHair");
            readyGun = content.Load<Texture2D>("Texture/readyGun");
            reloadingGun = content.Load<Texture2D>("Texture/reloadingGun");
            
            
        }
        public void burglarManagement(GameTime time)
        {
            burglarTime -= time.ElapsedGameTime;

            if (burglarTime.Milliseconds <= 0)
            {

                int index = (int)rand.Next(0, 5);

                if (this.burglarList[index].shocked == false)
                {
                    this.burglarList[index].visible = true;
                }
                


                burglarTime = new TimeSpan(0, 0, 1);
            }
        }
        public override void Update(GameTime time)
        {
            base.Update(time);

            

            progress += .01f;
            position.X = camX;

            

            if (progress >= 63)
            {
                resetScene();
                this.sceneControl = sceneControler.goTo;
                this.gotoState = stateGame.gameOutro;
                this.endScene = true;
            }
            else if (progress <= 0)
            {
                resetScene();
                this.sceneControl = sceneControler.goTo;
                this.gotoState = stateGame.game4Fail;
                this.endScene = true;
            }


            if (screenFade.A > 0)
            {
                screenFade.A = (byte)((float)screenFade.A - 25.0f);
            }

            if (screenFade.A <= 0)
            {
                if (Burglar.flash == true)
                {
                    screenFade.A = 200;
                }
            }

            burglarManagement(time);
            gunControl(time);




            if (keyboard.controlState[Controls.right] || keyboard.controlState[Controls.D])
            {
                right = true;
                
                left = false;
                
            }
            else if (keyboard.controlState[Controls.left] || keyboard.controlState[Controls.A])
            {
                left = true;

                right = false;

            }
            else
            {
                right = false;

                left = false;

            }


            if (left == true && camX >= -110.0f)
            {
                camX -= 1.0f;


            }
            else if (right == true && camX <= 110.0f)
            {
                camX += 1.0f;

            }



        }
        public void hitCheck()
        {
            int mouseX = this.mouse.mState.X;
            int mouseY = this.mouse.mState.Y;
            Vector3 nearsource = new Vector3((float)mouseX, (float)mouseY, 0f);
            Vector3 farsource = new Vector3((float)mouseX, (float)mouseY, 1f);

            Matrix world = Matrix.CreateTranslation(0, 0, 0);

            Vector3 nearPoint = GraphicsDevice.Viewport.Unproject(nearsource, projMatrix, viewMatrix, worldMatrix);

            Vector3 farPoint = GraphicsDevice.Viewport.Unproject(farsource, projMatrix, viewMatrix, worldMatrix);

            Vector3 direction = farPoint - nearPoint;
            direction.Normalize();
            Ray pickRay = new Ray(nearPoint, direction);


            VertexPositionColor[] v = new VertexPositionColor[2];
            v[0] = new VertexPositionColor(pickRay.Position, Color.Blue);
            v[1] = new VertexPositionColor(pickRay.Position * (pickRay.Direction * 1000), Color.Red);

            vb = new VertexBuffer(GraphicsDevice, VertexPositionColor.SizeInBytes * 2, BufferUsage.WriteOnly);
            vb.SetData(v);

            


            for (int i = 0; i < this.burglarList.Count; i++)
            {
                this.burglarList[i].rayCheck(pickRay);
            }

            
        }
        public void gunControl(GameTime time)
        {

            if (this.mouse.mouseDown)
            {
                if (gunReady == true)
                {
                    hitCheck();
                    gunReady = false;
                }

            }

            if (gunReady == false)
            {
                reloading -= time.ElapsedGameTime;

                if(reloading.Milliseconds <= 0)
                {
                    gunReady = true;
                    reloading = new TimeSpan(0, 0, 0, 0, reloadingTime);
                }
            }



        }
        public void grassTile()
        {
            VertexPositionColorTexture[] verts = new VertexPositionColorTexture[4];

            verts[0] = new VertexPositionColorTexture(new Vector3(-400, 0, -400), Color.Black, new Vector2(1, 1));
            verts[1] = new VertexPositionColorTexture(new Vector3(400, 0, -400), Color.Black, new Vector2(0, 1));
            verts[2] = new VertexPositionColorTexture(new Vector3(-200, 0, 400), Color.Black, new Vector2(1, 0));
            verts[3] = new VertexPositionColorTexture(new Vector3(200, 0, 400), Color.Black, new Vector2(0, 0));

            vertexBufferGrass = new VertexBuffer(GraphicsDevice, VertexPositionColorTexture.SizeInBytes * 4, BufferUsage.WriteOnly);
            vertexBufferGrass.SetData(verts);
        }
        public void treeTile()
        {
            VertexPositionColorTexture[] verts = new VertexPositionColorTexture[4];

            verts[0] = new VertexPositionColorTexture(new Vector3(-400, 400, -400), Color.Black, new Vector2(1, 0));
            verts[1] = new VertexPositionColorTexture(new Vector3(400, 400, -400), Color.Black, new Vector2(0, 0));
            verts[2] = new VertexPositionColorTexture(new Vector3(-400, 0, -400), Color.Black, new Vector2(1, 1));
            verts[3] = new VertexPositionColorTexture(new Vector3(400, 0, -400), Color.Black, new Vector2(0, 1));

            vertexBG = new VertexBuffer(GraphicsDevice, VertexPositionColorTexture.SizeInBytes * 4, BufferUsage.WriteOnly);
            vertexBG.SetData(verts);
        }
        public void drawStuff()
        {
            //set up our graphics device

            GraphicsDevice.Vertices[0].SetSource(vertexBufferGrass, 0, VertexPositionColorTexture.SizeInBytes);
            GraphicsDevice.Indices = indexBuffer;
            effect1.Parameters["ColorMap"].SetValue(Grass);

            effect1.Begin();
            foreach (EffectPass pass in effect1.CurrentTechnique.Passes)
            {
                pass.Begin();
                GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 4, 0, 2);
                pass.End();
            }
            effect1.End();


            GraphicsDevice.Vertices[0].SetSource(vertexBG, 0, VertexPositionColorTexture.SizeInBytes);
            GraphicsDevice.Indices = indexBuffer;
            effect1.Parameters["ColorMap"].SetValue(Trees);

            effect1.Begin();
            foreach (EffectPass pass in effect1.CurrentTechnique.Passes)
            {
                pass.Begin();
                GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 4, 0, 2);
                pass.End();
            }
            effect1.End();

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            viewMatrix = Matrix.CreateLookAt(new Vector3(camX, 20.0f, 30.0f), new Vector3(camX, 0.0f, -500.0f), new Vector3(0.0f, 1.0f, 0.0f));

            grassTile();
            treeTile();
            

            effect1.Parameters["World"].SetValue(worldMatrix);
            effect1.Parameters["View"].SetValue(viewMatrix);
            effect1.Parameters["Projection"].SetValue(projMatrix);
            effect1.CurrentTechnique = effect1.Techniques["Technique1"];
            GraphicsDevice.VertexDeclaration = vertexDec;


            drawStuff();



            foreach (ModelMesh mesh in House.Meshes)
            {

                foreach (BasicEffect effect in mesh.Effects)
                {

                    effect.EnableDefaultLighting();
                    effect.Projection = projMatrix;
                    effect.View = viewMatrix;
                    effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateRotationY(MathHelper.ToRadians(0.0f))
                                       * Matrix.CreateScale(5.0f);// *Matrix.CreateTranslation(position);
                }

                mesh.Draw();
            }



            spriteBatch.Draw(BarB, new Vector2(778, 75), new Rectangle(0, 0, 55, Convert.ToInt32(progress) * 7), Color.White, MathHelper.ToRadians(180.0f), new Vector2(0, 440), 1.0f, SpriteEffects.None, 0.1f);


            spriteBatch.Draw(Bar, new Vector2(716, 475), new Rectangle(0, 0, 100, 800), Color.White, MathHelper.ToRadians(0.0f), new Vector2(0, 440), 1.0f, SpriteEffects.None, 0.1f);



            
            if (Burglar.flash == true)
            {
                spriteBatch.Draw(redFlash, new Rectangle(0, 0, 800, 600), new Rectangle(0, 0, 800, 600), screenFade, 0.0f, Vector2.Zero, SpriteEffects.None, .5f);
            }

            spriteBatch.Draw(cross, new Vector2(mouse.position.X - 20, mouse.position.Y - 20), new Rectangle(0, 0, 30, 30), Color.White, MathHelper.ToRadians(0.0f), new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.2f);


            if (gunReady == true)
            {
                spriteBatch.Draw(readyGun, new Vector2(-5, 425), new Rectangle(0, 0, 300, 250), Color.White, MathHelper.ToRadians(0.0f), new Vector2(0, 0), 0.7f, SpriteEffects.None, 0.1f);

            }
            else if (gunReady == false)
            {
                spriteBatch.Draw(reloadingGun, new Vector2(-5, 425), new Rectangle(0, 0, 300, 250), Color.White, MathHelper.ToRadians(0.0f), new Vector2(0, 0), 0.7f, SpriteEffects.None, 0.1f);
            }

            
        }

        
    }
}