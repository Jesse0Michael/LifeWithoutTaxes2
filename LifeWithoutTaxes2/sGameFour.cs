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

        private float camX;

        private Texture2D shadow;

        VertexBuffer vertexBG = null;
        VertexBuffer vertexHouse = null;
        IndexBuffer indexBuffer = null;
        VertexDeclaration vertexDec = null;

        KeyboardState keyState;
        KeyboardState previousKeyState;

        bool left;
        bool right;

        float progress;
        Texture2D BarB;

        private Vector3 position;

        int currentKey;
        static TimeSpan keyTime;

        Texture2D backGround;
        Texture2D House;

        Random rand = new Random();

        public sGameFour(mouseHelp mouse, keyboardHelp keyboard, GraphicsDevice GraphicsDevice)
            : base(mouse, keyboard, GraphicsDevice)
        {


            this.gameObjects = new List<gameObject>();
            this.obstacleList = new List<Obstacle>();
            this.gameObjects.Add(new gameObject(new Vector2(750, 300), "Texture/Bar"));

            this.gameState = stateGame.game4;

            resetScene();


        }
        public void resetScene()
        {
            camX = 0;
            
            right = false;
            left = false;

            //20
            progress = 55.0f;
            position = new Vector3(camX, 0, 0);
            

            worldMatrix = Matrix.Identity;
            projMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f),
                                                                     GraphicsDevice.DisplayMode.AspectRatio,
                                                                     1.0f,
                                                                     3000.0f);

            keyTime = new TimeSpan(0, 0, 0, 1, 0);
            prepareScene();

            keyState = Keyboard.GetState();

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
            base.LoadContent(content);
            effect1 = content.Load<Effect>("Effect1");
            backGround = content.Load<Texture2D>("Texture/GameTwoBkg2");
            BarB = content.Load<Texture2D>("Texture/BarFill Blue");
            House = content.Load<Texture2D>("Texture/BarFill Blue");
            
        }

        public override void Update(GameTime time)
        {
            base.Update(time);

            progress -= .01f;
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

            keyState = Keyboard.GetState();

            keyTime -= time.ElapsedGameTime;

            if (keyTime.Milliseconds <= 0)
            {

                //currentKey = Convert.ToInt32((float)rand.NextDouble() * 3);
                int nextTime = Convert.ToInt32((float)rand.NextDouble() * 500 + 750);
                keyTime = new TimeSpan(0, 0, 0, 0, nextTime);

            }



            
            if (keyState.IsKeyDown(Keys.Right))
            {
                right = true;
                
                left = false;
                
            }
            else if (keyState.IsKeyDown(Keys.Left))
            {
                left = true;
                
                right = false;
                
            }


            if (left == true && camX >= -4025)
            {
                camX -= 2.0f;


            }
            else if (right == true && camX <= 4025)
            {
                camX += 2.0f;

            }



        }
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            viewMatrix = Matrix.CreateLookAt(new Vector3(camX, 500.0f, 0), new Vector3(camX, 0.0f, -500.0f), new Vector3(0.0f, 1.0f, 0.0f));


            groundTile();
            shadowTile();

            

            effect1.Parameters["World"].SetValue(worldMatrix);
            effect1.Parameters["View"].SetValue(viewMatrix);
            effect1.Parameters["Projection"].SetValue(projMatrix);
            effect1.CurrentTechnique = effect1.Techniques["Technique1"];
            GraphicsDevice.VertexDeclaration = vertexDec;





            spriteBatch.Draw(BarB, new Vector2(778, 75), new Rectangle(0, 0, 55, Convert.ToInt32(progress) * 7), Color.White, MathHelper.ToRadians(180.0f), new Vector2(0, 440), 1.0f, SpriteEffects.None, 0.1f);

            drawThings();



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

            verts[0] = new VertexPositionColorTexture(new Vector3(camX - 50 - sin / 2 - 25, 1, camZ - 300 - sin / 2 - 50), Color.Black, new Vector2(1, 1));
            verts[1] = new VertexPositionColorTexture(new Vector3(camX + 50 + sin / 2 + 25, 1, camZ - 300 - sin / 2 - 50), Color.Black, new Vector2(0, 1));
            verts[2] = new VertexPositionColorTexture(new Vector3(camX - 50 - sin / 2 - 25, 1, camZ - 250 + sin / 2 + 50), Color.Black, new Vector2(1, 0));
            verts[3] = new VertexPositionColorTexture(new Vector3(camX + 50 + sin / 2 + 25, 1, camZ - 250 + sin / 2 + 50), Color.Black, new Vector2(0, 0));

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