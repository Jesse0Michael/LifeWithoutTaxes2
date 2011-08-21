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
    class sGameTwo : interactableScene
    {
        Effect effect1;

        Matrix worldMatrix;
        Matrix viewMatrix;
        Matrix projMatrix;

        private float camX;
        private float camZ;

        private Model modelDown;
        private Model modelUp;
        private float jump;
        private float deltaY;
        private float Y2;
        private float sin;
        private bool jumping;
        private float rotation;
        private Matrix[] transforms1;
        private Matrix[] transforms2;

        private Texture2D shadow;

        VertexBuffer vertexBufferGround = null;
        VertexBuffer vertexShadow = null;
        IndexBuffer indexBuffer = null;
        VertexDeclaration vertexDec = null;
        Texture2D Ground;

        KeyboardState keyState;
        KeyboardState previousKeyState;
        Random rand = new Random();

        bool up;
        bool down;
        bool left;
        bool right;

        float progress;
        Texture2D BarB;
        Texture2D Arrow;

        private Vector3 position;

        int currentKey;
        static TimeSpan keyTime;


        public sGameTwo(mouseHelp mouse, keyboardHelp keyboard, GraphicsDevice GraphicsDevice)
            : base(mouse, keyboard, GraphicsDevice)
        {


            this.gameObjects = new List<gameObject>();
            this.obstacleList = new List<Obstacle>();
            this.burglarList = new List<Burglar>();
            this.gameObjects.Add(new gameObject(new Vector2(750, 300), "Texture/Bar"));

            this.gameState = stateGame.game2;

            resetScene();
            

        }
        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            effect1 = content.Load<Effect>("Effect1");
            Ground = content.Load<Texture2D>("Texture/GameTwoBkg2");
            BarB = content.Load<Texture2D>("Texture/BarFill Blue");
            Arrow = content.Load<Texture2D>("Texture/arrowKey");
            modelDown = content.Load<Model>("Model/kangarooDown");
            transforms1 = new Matrix[modelDown.Bones.Count];
            modelDown.CopyAbsoluteBoneTransformsTo(transforms1);
            modelUp = content.Load<Model>("Model/kangarooUp");
            transforms2 = new Matrix[modelUp.Bones.Count];
            modelUp.CopyAbsoluteBoneTransformsTo(transforms2);
            shadow = content.Load<Texture2D>("Texture/shadow");
        }

        public override void Update(GameTime time)
        {
            base.Update(time);

            progress -= .01f;
            position.X = camX;
            position.Z = camZ-300;
            Y2 = position.Y;
            sin = (float)(Math.Sin(MathHelper.ToRadians(jump)) * 100);
            position.Y = sin + 105;

            deltaY = position.Y - Y2;
            jump += 5.0f;
            jumping = (deltaY > 0);

            if (progress >= 63)
            {
                resetScene();
                this.sceneControl = sceneControler.goTo;
                this.gotoState = stateGame.game2Win;
                this.endScene = true;
            }
            else if (progress <= 0)
            {
                resetScene();
                this.sceneControl = sceneControler.goTo;
                this.gotoState = stateGame.game2Fail;
                this.endScene = true;
            }

            keyState = Keyboard.GetState();

            keyTime -= time.ElapsedGameTime;

            if (keyTime.Milliseconds <= 0)
            {
                
                currentKey = Convert.ToInt32((float)rand.NextDouble() * 3);
                int nextTime = Convert.ToInt32((float)rand.NextDouble() * 500 + 750);
                keyTime = new TimeSpan(0,0,0,0,nextTime);

            }
            
            

            // 0 = up, 1 = down, 2 = right, 3 = left
            if (((previousKeyState.IsKeyUp(Keys.Up)) && (keyState.IsKeyDown(Keys.Up)))  || ((previousKeyState.IsKeyUp(Keys.W)) && (keyState.IsKeyDown(Keys.W))))
            {
                up = true;
                down = false;
                left = false;
                right = false;
                if (currentKey == 0)
                {
                    progress += 1.0f;
                }
                else
                {
                    progress -= 2.0f;
                }

            }
            else if (((previousKeyState.IsKeyUp(Keys.Down)) && (keyState.IsKeyDown(Keys.Down)))  || ((previousKeyState.IsKeyUp(Keys.S)) && (keyState.IsKeyDown(Keys.S))))
            {
                down = true;
                up = false;
                left = false;
                right = false;
                if (currentKey == 1)
                {
                    progress += 1.0f;
                }
                else
                {
                    progress -= 2.0f;
                }
            }
            else if (((previousKeyState.IsKeyUp(Keys.Right)) && (keyState.IsKeyDown(Keys.Right)))  || ((previousKeyState.IsKeyUp(Keys.D)) && (keyState.IsKeyDown(Keys.D))))
            {
                right = true;
                down = false;
                up = false;
                left = false;
                if (currentKey == 2)
                {
                    progress += 1.0f;
                }
                else
                {
                    progress -= 2.0f;
                }
            }
            else if (((previousKeyState.IsKeyUp(Keys.Left)) && (keyState.IsKeyDown(Keys.Left))) || ((previousKeyState.IsKeyUp(Keys.A)) && (keyState.IsKeyDown(Keys.A))))
            {
                left = true;
                down = false;
                up = false;
                right = false;
                if (currentKey == 3)
                {
                    progress += 1.0f;
                }
                else
                {
                    progress -= 2.0f;
                }
            }


            if (left == true && camX >= -4025)
            {
                camX -= 2.0f;
                
                if (rotation > 270.0f)
                    rotation -= 5.0f;
                else if (rotation < 270.0f)
                    rotation += 5.0f;

            }
            else if (right == true && camX <= 4025)
            {
                camX += 2.0f;
                if (rotation > 90.0f)
                    rotation -= 5.0f;
                else if (rotation < 90.0f)
                    rotation += 5.0f;
            }
            else if (up == true && camZ >= -3325)
            {
                camZ -= 2.0f;
                if (rotation > 180.0f)
                    rotation -= 5.0f;
                else if (rotation < 180.0f)
                    rotation += 5.0f;
            }
            else if (down == true && camZ <= 2400)
            {
                camZ += 2.0f;
                if (rotation > 0.0f)
                    rotation -= 5.0f;
                else if (rotation < 0.0f)
                    rotation += 5.0f;
            }





            previousKeyState = keyState;


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
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            viewMatrix = Matrix.CreateLookAt(new Vector3(camX, 700.0f, camZ+500), new Vector3(camX, 0.0f, -500.0f + camZ), new Vector3(0.0f, 1.0f, 0.0f));

            
            groundTile();
            shadowTile();

            if (jumping)
            {
                foreach (ModelMesh mesh in this.modelUp.Meshes)
                {

                    foreach (BasicEffect effect in mesh.Effects)
                    {

                        effect.EnableDefaultLighting();
                        effect.Projection = projMatrix;
                        effect.View = viewMatrix;
                        effect.World = transforms2[mesh.ParentBone.Index] * Matrix.CreateRotationY(MathHelper.ToRadians(rotation))
                                           * Matrix.CreateScale(10.5f) * Matrix.CreateTranslation(position);
                    }

                    mesh.Draw();
                }
            }
            else
            {
                foreach (ModelMesh mesh in this.modelDown.Meshes)
                {

                    foreach (BasicEffect effect in mesh.Effects)
                    {


                        effect.EnableDefaultLighting();
                        effect.Projection = projMatrix;
                        effect.View = viewMatrix;
                        effect.World = transforms1[mesh.ParentBone.Index] * Matrix.CreateRotationY(MathHelper.ToRadians(rotation))
                                           * Matrix.CreateScale(10.5f) * Matrix.CreateTranslation(position);
                    }

                    mesh.Draw();
                }

            }

            effect1.Parameters["World"].SetValue(worldMatrix);
            effect1.Parameters["View"].SetValue(viewMatrix);
            effect1.Parameters["Projection"].SetValue(projMatrix);
            effect1.CurrentTechnique = effect1.Techniques["Technique1"];
            GraphicsDevice.VertexDeclaration = vertexDec;



            


            spriteBatch.Draw(BarB, new Vector2(778, 75), new Rectangle(0, 0, 55, Convert.ToInt32(progress) * 7), Color.White, MathHelper.ToRadians(180.0f), new Vector2(0, 440), 1.0f, SpriteEffects.None, 0.1f);

            drawThings();
            drawCurrentKey(spriteBatch);

            

        }
        public void resetScene()
        {
            camX = 0;
            camZ = 0;
            up = false;
            down = false;
            right = false;
            left = false;
            
            //20
            progress = 20.0f;
            position = new Vector3(0, 0, 0);
            jump = 0;
            deltaY = 0;
            sin = 0;
            Y2 = 0;
            rotation = 0;
            jumping = false;

            worldMatrix = Matrix.Identity;
            projMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f),
                                                                     GraphicsDevice.DisplayMode.AspectRatio,
                                                                     1.0f,
                                                                     3000.0f);

            keyTime = new TimeSpan(0, 0, 0, 1, 0);
            prepareScene();

            keyState = Keyboard.GetState();
            previousKeyState = keyState;

        }
        public void drawCurrentKey(SpriteBatch spriteBatch)
        {
            // 0 = up, 1 = down, 2 = right, 3 = left
            if (currentKey == 0)
            {
                spriteBatch.Draw(Arrow, new Vector2(325, 40), new Rectangle(0, 0, Arrow.Width, Arrow.Height), Color.White, MathHelper.ToRadians(0.0f), new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.1f);

            }
            else if (currentKey == 1)
            {
                spriteBatch.Draw(Arrow, new Vector2(425, 550), new Rectangle(0, 0, Arrow.Width, Arrow.Height), Color.White, MathHelper.ToRadians(180.0f), new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.1f);

            }
            else if (currentKey == 2)
            {
                spriteBatch.Draw(Arrow, new Vector2(700, 250), new Rectangle(0, 0, Arrow.Width, Arrow.Height), Color.White, MathHelper.ToRadians(90.0f), new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.1f);

            }
            else if (currentKey == 3)
            {
                spriteBatch.Draw(Arrow, new Vector2(40, 350), new Rectangle(0, 0, Arrow.Width, Arrow.Height), Color.White, MathHelper.ToRadians(270.0f), new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.1f);

            }

        }
        public void groundTile()
        {
            VertexPositionColorTexture[] verts = new VertexPositionColorTexture[24];

            verts[0] = new VertexPositionColorTexture(new Vector3(-3000, 0, -3000), Color.Black, new Vector2(1, 1));
            verts[1] = new VertexPositionColorTexture(new Vector3(3000, 0, -3000), Color.Black, new Vector2(0, 1));
            verts[2] = new VertexPositionColorTexture(new Vector3(-3000, 0, 3000), Color.Black, new Vector2(1, 0));
            verts[3] = new VertexPositionColorTexture(new Vector3(3000, 0, 3000), Color.Black, new Vector2(0, 0));

            verts[4] = new VertexPositionColorTexture(new Vector3(-3000, 0, -6000), Color.Black, new Vector2(1, 1));
            verts[5] = new VertexPositionColorTexture(new Vector3(3000, 0, -6000), Color.Black, new Vector2(0, 1));
            verts[6] = new VertexPositionColorTexture(new Vector3(-3000, 0, -3000), Color.Black, new Vector2(1, 0));
            verts[7] = new VertexPositionColorTexture(new Vector3(3000, 0, -3000), Color.Black, new Vector2(0, 0));

            verts[8] = new VertexPositionColorTexture(new Vector3(3000, 0, -3000), Color.Black, new Vector2(1, 1));
            verts[9] = new VertexPositionColorTexture(new Vector3(6000, 0, -3000), Color.Black, new Vector2(0, 1));
            verts[10] = new VertexPositionColorTexture(new Vector3(3000, 0, 3000), Color.Black, new Vector2(1, 0));
            verts[11] = new VertexPositionColorTexture(new Vector3(6000, 0, 3000), Color.Black, new Vector2(0, 0));

            verts[12] = new VertexPositionColorTexture(new Vector3(-6000, 0, -3000), Color.Black, new Vector2(1, 1));
            verts[13] = new VertexPositionColorTexture(new Vector3(-3000, 0, -3000), Color.Black, new Vector2(0, 1));
            verts[14] = new VertexPositionColorTexture(new Vector3(-6000, 0, 3000), Color.Black, new Vector2(1, 0));
            verts[15] = new VertexPositionColorTexture(new Vector3(-3000, 0, 3000), Color.Black, new Vector2(0, 0));

            verts[16] = new VertexPositionColorTexture(new Vector3(3000, 0, -6000), Color.Black, new Vector2(1, 1));
            verts[17] = new VertexPositionColorTexture(new Vector3(6000, 0, -6000), Color.Black, new Vector2(0, 1));
            verts[18] = new VertexPositionColorTexture(new Vector3(3000, 0, -3000), Color.Black, new Vector2(1, 0));
            verts[19] = new VertexPositionColorTexture(new Vector3(6000, 0, -3000), Color.Black, new Vector2(0, 0));

            verts[20] = new VertexPositionColorTexture(new Vector3(-6000, 0, -6000), Color.Black, new Vector2(1, 1));
            verts[21] = new VertexPositionColorTexture(new Vector3(-3000, 0, -6000), Color.Black, new Vector2(0, 1));
            verts[22] = new VertexPositionColorTexture(new Vector3(-6000, 0, -3000), Color.Black, new Vector2(1, 0));
            verts[23] = new VertexPositionColorTexture(new Vector3(-3000, 0, -3000), Color.Black, new Vector2(0, 0));

            vertexBufferGround = new VertexBuffer(GraphicsDevice, VertexPositionColorTexture.SizeInBytes * 24, BufferUsage.WriteOnly);
            vertexBufferGround.SetData(verts);
        }
        public void shadowTile()
        {
            VertexPositionColorTexture[] verts = new VertexPositionColorTexture[4];

            verts[0] = new VertexPositionColorTexture(new Vector3(camX - 50 -sin/2 -25, 1, camZ - 300-sin/2-50), Color.Black, new Vector2(1, 1));
            verts[1] = new VertexPositionColorTexture(new Vector3(camX + 50+sin/2+25, 1, camZ - 300-sin/2-50), Color.Black, new Vector2(0, 1));
            verts[2] = new VertexPositionColorTexture(new Vector3(camX - 50-sin/2-25, 1, camZ-250+sin/2+50), Color.Black, new Vector2(1, 0));
            verts[3] = new VertexPositionColorTexture(new Vector3(camX + 50+sin/2+25, 1, camZ-250+sin/2+50), Color.Black, new Vector2(0, 0));

            vertexShadow = new VertexBuffer(GraphicsDevice, VertexPositionColorTexture.SizeInBytes * 4, BufferUsage.WriteOnly);
            vertexShadow.SetData(verts);
        }
        public void drawThings()
        {

            GraphicsDevice.Vertices[0].SetSource(vertexBufferGround, 0, VertexPositionColorTexture.SizeInBytes);
            GraphicsDevice.Indices = indexBuffer;
            effect1.Parameters["ColorMap"].SetValue(Ground);
            

            effect1.Begin();
            foreach (EffectPass pass in effect1.CurrentTechnique.Passes)
            {
                pass.Begin();
                
                
                GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 24, 0, 12);
                pass.End();
            }
            effect1.End();

            GraphicsDevice.Vertices[0].SetSource(vertexShadow, 0, VertexPositionColorTexture.SizeInBytes);
            GraphicsDevice.Indices = indexBuffer;
            effect1.Parameters["ColorMap"].SetValue(shadow);

            effect1.Begin();

            foreach (EffectPass pass in effect1.CurrentTechnique.Passes)
            {
                pass.Begin();

                GraphicsDevice.RenderState.AlphaBlendEnable = true;
                GraphicsDevice.RenderState.DepthBufferWriteEnable = true;
                GraphicsDevice.RenderState.DepthBufferEnable = true;
                GraphicsDevice.RenderState.SourceBlend = Blend.SourceAlpha;
                GraphicsDevice.RenderState.DestinationBlend = Blend.InverseSourceAlpha; 
                GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 4, 0, 2);
                pass.End();
            }
            effect1.End();
            GraphicsDevice.RenderState.AlphaBlendEnable = false;

        }




    }
}