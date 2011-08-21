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
    class sGameOne : interactableScene
    {
        Effect effect1;

        VertexBuffer vertexBufferStreet1 = null;
        VertexBuffer vertexBufferStreet2 = null;
        VertexBuffer vertexBufferStreet3 = null;
        VertexBuffer vertexBufferGrass = null;
        IndexBuffer indexBuffer = null;
        IndexBuffer indexBufferGrass = null;
        VertexBuffer vertexBufferSky = null;
        VertexDeclaration vertexDec = null;

        Model model;
        Texture2D carTexture;
        Matrix[] transforms;
        BoundingSphere playerSphere;
        float rotation;

        Texture2D Street;
        Texture2D Sky;
        Texture2D Grass;
        Texture2D BarB;
        Texture2D BarR;
        Texture2D redFlash;
        Color screenFade = Color.White;

        float streetZ;
        float streetZ2;
        float streetZ3;
        public static float camX;
        float skyX;
        static int progress;
        static int damage;
        int drawDamage;

        Matrix worldMatrix;
        Matrix viewMatrix;
        Matrix projMatrix;

        Obstacle oTrafficCone;
        Obstacle oTrashCan;
        Obstacle oTire;
        Obstacle oPotHole;
        Obstacle oLog;
        Obstacle oBarricade;

        Obstacle oCarBlack;
        Obstacle oCarBlue;
        Obstacle oCarGreen;
        Obstacle oCarPurple;
        Obstacle oCarRed;
        Obstacle oCarWhite;

        static TimeSpan timer;
        static TimeSpan obstacleTime;
        static TimeSpan carTime;

        KeyboardState keyState;
        KeyboardState previousKeyState;

        public static Random rand = new Random();
        BoundingBox bounding;

        public sGameOne(mouseHelp mouse, keyboardHelp keyboard, GraphicsDevice GraphicsDevice)
            : base(mouse, keyboard, GraphicsDevice)
        {
            

            this.gameObjects = new List<gameObject>();
            this.burglarList = new List<Burglar>();
            this.gameObjects.Add(new gameObject(new Vector2(750, 300), "Texture/Bar"));
            this.gameObjects.Add(new gameObject(new Vector2(50, 300), "Texture/Bar"));

            this.gameState = stateGame.game1;

            worldMatrix = Matrix.Identity;

            prepareScene();

            screenFade.A = 0;

            keyState = Keyboard.GetState();
            previousKeyState = keyState;

            bounding = new BoundingBox();

            streetZ = -2000;
            streetZ2 = -6000;
            streetZ3 = -10000;
            camX = 0;
            skyX = 0;
            progress = 0;
            damage = 0;
            drawDamage = 0;

            rotation = 0.0f;


            projMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f),
                                                                     GraphicsDevice.DisplayMode.AspectRatio,
                                                                     1.0f,
                                                                     8000.0f);

            timer = new TimeSpan(0, 0, 0);
            obstacleTime = new TimeSpan(0, 0, 1);
            carTime = new TimeSpan(0, 0, 2);

            this.obstacleList = new List<Obstacle>();

        }
        public override void LoadContent(ContentManager content)
        {
            loadObstacles();
            base.LoadContent(content);
            effect1 = content.Load<Effect>("Effect1");
            Street = content.Load<Texture2D>("Texture/DrivingOneStreet2");
            Sky = content.Load<Texture2D>("Texture/DrivingOneSky21");
            Grass = content.Load<Texture2D>("Texture/DrivingOneGrass2");
            BarB = content.Load<Texture2D>("Texture/BarFill Blue");
            BarR = content.Load<Texture2D>("Texture/BarFill Red");
            redFlash = content.Load<Texture2D>("Texture/RedFlash");
            model = content.Load<Model>("Model/carRedStripe");
            carTexture = content.Load<Texture2D>("Model/texture");
            transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(this.transforms);

        }

        public void loadObstacles()
        {
            oTrafficCone = new Obstacle(GraphicsDevice, "Model/TrafficCone", new Vector3(0, 0, -12500), 50.0f, 0.0f, projMatrix, false, false, 25.0f);
            oTrashCan = new Obstacle(GraphicsDevice, "Model/TrashCan", new Vector3(0, 0, -12500), 50.0f, 90.0f, projMatrix, false, false, 25.0f);
            oTire = new Obstacle(GraphicsDevice, "Model/tire", new Vector3(0, 0, -12500), 100.0f, 0.0f, projMatrix, false, false, 25.0f);
            oPotHole = new Obstacle(GraphicsDevice, "Model/potHole", new Vector3(0, 0, -12500), 150.0f, 0.0f, projMatrix, false, false, 25.0f);
            oLog = new Obstacle(GraphicsDevice, "Model/Log", new Vector3(0, 0, -12500), 100.0f, 90.0f, projMatrix, false, false, 25.0f);
            oBarricade = new Obstacle(GraphicsDevice, "Model/Barricade", new Vector3(0, 0, -12500), 100.0f, 0.0f, projMatrix, false, false, 25.0f);

            oCarBlack = new Obstacle(GraphicsDevice, "Model/carBlack", new Vector3(0, 0, -12500), 100.0f, 0.0f, projMatrix, true, false, 25.0f);
            oCarBlue = new Obstacle(GraphicsDevice, "Model/carBlue", new Vector3(0, 0, -12500), 100.0f, 0.0f, projMatrix, true, false, 25.0f);
            oCarGreen = new Obstacle(GraphicsDevice, "Model/carGreen", new Vector3(0, 0, -12500), 100.0f, 0.0f, projMatrix, true, false, 25.0f);
            oCarPurple = new Obstacle(GraphicsDevice, "Model/carPurple", new Vector3(0, 0, -12500), 100.0f, 0.0f, projMatrix, true, false, 25.0f);
            oCarRed = new Obstacle(GraphicsDevice, "Model/carRed", new Vector3(0, 0, -12500), 100.0f, 0.0f, projMatrix, true, false, 25.0f);
            oCarWhite = new Obstacle(GraphicsDevice, "Model/carWhite", new Vector3(0, 0, -12500), 100.0f, 0.0f, projMatrix, true, false, 25.0f);

            this.obstacleList.Add(oTrafficCone);
            this.obstacleList.Add(oTrashCan);
            this.obstacleList.Add(oTire);
            this.obstacleList.Add(oPotHole);
            this.obstacleList.Add(oLog);
            this.obstacleList.Add(oBarricade);

            this.obstacleList.Add(oCarBlack);
            this.obstacleList.Add(oCarBlue);
            this.obstacleList.Add(oCarGreen);
            this.obstacleList.Add(oCarPurple);
            this.obstacleList.Add(oCarRed);
            this.obstacleList.Add(oCarWhite);




        }
        public override void Update(GameTime time)
        {
            base.Update(time);

            timer += time.ElapsedGameTime;

            streetZ += 25.0f;
            streetZ2 += 25.0f;
            streetZ3 += 25.0f;

            progress = (int)timer.TotalSeconds;


            if (progress >= 63)
            //63
            {
                resetScene();
                this.sceneControl = sceneControler.goTo;
                this.gotoState = stateGame.game1Win;
                this.endScene = true;
            }
            else
                if (damage >= 10)
                {
                    resetScene();
                    this.sceneControl = sceneControler.goTo;
                    this.gotoState = stateGame.game1Fail;
                    this.endScene = true;

                }


            if ((keyboard.controlState[Controls.left] || keyboard.controlState[Controls.A])&& camX >= -2000)
            {
                if (camX <= -1950)
                {
                    rotation = 0.0f;
                }
                else if (rotation < 20.0f)
                {
                    rotation += 5.0f;
                }
                camX -= 25.0f;
                skyX -= 22.0f;
            }
            else if ((keyboard.controlState[Controls.right] || keyboard.controlState[Controls.D]) && camX <= 2000)
            {
                if (camX >= 1950)
                {
                    rotation = 0.0f;
                }
                else if (rotation > -20.0f)
                {
                    rotation -= 5.0f;
                }
                camX += 25.0f;
                skyX += 22.0f;
            }
            else
            {
                if (rotation < 0.0f)
                {
                    rotation += 5.0f;
                }
                else if (rotation > 0.0f)
                {
                    rotation -= 5.0f;
                }
            } 

            if (screenFade.A > 0)
            {
                screenFade.A = (byte)((float)screenFade.A - 25.0f);
            }


            obstacleManagement(time);



            bounding.Min = new Vector3(camX - 350, 0, -150);
            bounding.Max = new Vector3(camX + 350, 300, -151);

            collisionTime();


        }
        public void collisionTime()
        {
            for (int i = 0; i < this.obstacleList.Count; i++)
            {
                if (this.obstacleList[i].visible == true)
                {
                    if (this.obstacleList[i].boundingSphere.Intersects(playerSphere))
                    {
                        if (this.obstacleList[i].isCar == true)
                        {
                            damage += 2;
                        }
                        else if (this.obstacleList[i].isCar == false)
                        {
                            damage += 1;
                        }
                        this.obstacleList[i].position.Z = 3000;
                        this.obstacleList[i].boundingSphere.Center = this.obstacleList[i].position;
                        this.obstacleList[i].visible = false;
                        screenFade.A = 200;
                        break;
                    }
                }
            }

        }
        public void resetScene()
        {

            streetZ = -2000;
            streetZ2 = -6000;
            streetZ3 = -10000;
            camX = 0;
            skyX = 0;
            progress = 0;
            damage = 0;
            drawDamage = 0;
            screenFade.A = 0;

            timer = new TimeSpan(0, 0, 0);
            obstacleTime = new TimeSpan(0, 0, 1);
            carTime = new TimeSpan(0, 0, 2);

            for (int i = 0; i < this.obstacleList.Count; i++)
            {
                this.obstacleList[i].isRotate = false;
                this.obstacleList[i].visible = false;
                this.obstacleList[i].position.Z = -10500;
                this.obstacleList[i].boundingSphere.Center = this.obstacleList[i].position;
            }

        }
        public void obstacleManagement(GameTime time)
        {
            obstacleTime -= time.ElapsedGameTime;
            carTime -= time.ElapsedGameTime;

            if (obstacleTime.Milliseconds <= 0)
            {

                int index = (int)rand.Next(0, 12);

                isAnObstacle(index);


                obstacleTime = new TimeSpan(0, 0, 1);
            }
            if (carTime.Milliseconds <= 0)
            {

                int index = (int)rand.Next(0, 12);
                isACar(index);



                carTime = new TimeSpan(0, 0, 2);
            }
        }
        public void isACar(int index)
        {
            if (this.obstacleList[index].isCar == true)
            {

                this.obstacleList[index].visible = true;
                if (this.obstacleList[index].isRotate == true)
                {
                    this.obstacleList[index].rotation = 180.0f;
                }
                else if (this.obstacleList[index].isRotate == false)
                {
                    this.obstacleList[index].rotation = 0.0f;
                }
            }
            else
            {
                int newIndex = (int)rand.Next(0, 12);

                isACar(newIndex);
            }

        }
        public void isAnObstacle(int index)
        {
            if (this.obstacleList[index].isCar == false)
            {


                this.obstacleList[index].visible = true;
            }
            else
            {
                int newIndex = (int)rand.Next(0, 12);

                isAnObstacle(newIndex);
            }

        }
        public void streetTile1()
        {
            VertexPositionColorTexture[] verts = new VertexPositionColorTexture[4];

            verts[0] = new VertexPositionColorTexture(new Vector3(-2250, 0, streetZ - 2000), Color.Black, new Vector2(1, 1));
            verts[1] = new VertexPositionColorTexture(new Vector3(2250, 0, streetZ - 2000), Color.Black, new Vector2(0, 1));
            verts[2] = new VertexPositionColorTexture(new Vector3(-2250, 0, streetZ + 2000), Color.Black, new Vector2(1, 0));
            verts[3] = new VertexPositionColorTexture(new Vector3(2250, 0, streetZ + 2000), Color.Black, new Vector2(0, 0));

            vertexBufferStreet1 = new VertexBuffer(GraphicsDevice, VertexPositionColorTexture.SizeInBytes * 4, BufferUsage.WriteOnly);
            vertexBufferStreet1.SetData(verts);
            if (streetZ >= 2000)
                streetZ = streetZ3 - 4000;
        }
        public void streetTile2()
        {
            VertexPositionColorTexture[] verts = new VertexPositionColorTexture[4];

            verts[0] = new VertexPositionColorTexture(new Vector3(-2250, 0, streetZ2 - 2000), Color.Black, new Vector2(1, 1));
            verts[1] = new VertexPositionColorTexture(new Vector3(2250, 0, streetZ2 - 2000), Color.Black, new Vector2(0, 1));
            verts[2] = new VertexPositionColorTexture(new Vector3(-2250, 0, streetZ2 + 2000), Color.Black, new Vector2(1, 0));
            verts[3] = new VertexPositionColorTexture(new Vector3(2250, 0, streetZ2 + 2000), Color.Black, new Vector2(0, 0));

            vertexBufferStreet2 = new VertexBuffer(GraphicsDevice, VertexPositionColorTexture.SizeInBytes * 4, BufferUsage.WriteOnly);
            vertexBufferStreet2.SetData(verts);
            if (streetZ2 >= 2000)
                streetZ2 = streetZ - 4000;
        }
        public void streetTile3()
        {
            VertexPositionColorTexture[] verts = new VertexPositionColorTexture[4];

            verts[0] = new VertexPositionColorTexture(new Vector3(-2250, 0, streetZ3 - 2000), Color.Black, new Vector2(1, 1));
            verts[1] = new VertexPositionColorTexture(new Vector3(2250, 0, streetZ3 - 2000), Color.Black, new Vector2(0, 1));
            verts[2] = new VertexPositionColorTexture(new Vector3(-2250, 0, streetZ3 + 2000), Color.Black, new Vector2(1, 0));
            verts[3] = new VertexPositionColorTexture(new Vector3(2250, 0, streetZ3 + 2000), Color.Black, new Vector2(0, 0));

            vertexBufferStreet3 = new VertexBuffer(GraphicsDevice, VertexPositionColorTexture.SizeInBytes * 4, BufferUsage.WriteOnly);
            vertexBufferStreet3.SetData(verts);
            if (streetZ3 >= 2000)
                streetZ3 = streetZ2 - 4000;
        }
        public void grassTile()
        {
            VertexPositionColorTexture[] verts = new VertexPositionColorTexture[24];

            verts[0] = new VertexPositionColorTexture(new Vector3(-2250 - 4700, 0, streetZ - 2000), Color.Black, new Vector2(1, 1));
            verts[1] = new VertexPositionColorTexture(new Vector3(-2250, 0, streetZ - 2000), Color.Black, new Vector2(0, 1));
            verts[2] = new VertexPositionColorTexture(new Vector3(-2250 - 4700, 0, streetZ + 2000), Color.Black, new Vector2(1, 0));
            verts[3] = new VertexPositionColorTexture(new Vector3(-2250, 0, streetZ + 2000), Color.Black, new Vector2(0, 0));
            verts[4] = new VertexPositionColorTexture(new Vector3(2250, 0, streetZ - 2000), Color.Black, new Vector2(1, 1));
            verts[5] = new VertexPositionColorTexture(new Vector3(2250 + 4700, 0, streetZ - 2000), Color.Black, new Vector2(0, 1));
            verts[6] = new VertexPositionColorTexture(new Vector3(2250, 0, streetZ + 2000), Color.Black, new Vector2(1, 0));
            verts[7] = new VertexPositionColorTexture(new Vector3(2250 + 4700, 0, streetZ + 2000), Color.Black, new Vector2(0, 0));

            verts[8] = new VertexPositionColorTexture(new Vector3(-2250 - 4700, 0, streetZ2 - 2000), Color.Black, new Vector2(1, 1));
            verts[9] = new VertexPositionColorTexture(new Vector3(-2250, 0, streetZ2 - 2000), Color.Black, new Vector2(0, 1));
            verts[10] = new VertexPositionColorTexture(new Vector3(-2250 - 4700, 0, streetZ2 + 2000), Color.Black, new Vector2(1, 0));
            verts[11] = new VertexPositionColorTexture(new Vector3(-2250, 0, streetZ2 + 2000), Color.Black, new Vector2(0, 0));
            verts[12] = new VertexPositionColorTexture(new Vector3(2250, 0, streetZ2 - 2000), Color.Black, new Vector2(1, 1));
            verts[13] = new VertexPositionColorTexture(new Vector3(2250 + 4700, 0, streetZ2 - 2000), Color.Black, new Vector2(0, 1));
            verts[14] = new VertexPositionColorTexture(new Vector3(2250, 0, streetZ2 + 2000), Color.Black, new Vector2(1, 0));
            verts[15] = new VertexPositionColorTexture(new Vector3(2250 + 4700, 0, streetZ2 + 2000), Color.Black, new Vector2(0, 0));

            verts[16] = new VertexPositionColorTexture(new Vector3(-2250 - 4700, 0, streetZ3 - 2000), Color.Black, new Vector2(1, 1));
            verts[17] = new VertexPositionColorTexture(new Vector3(-2250, 0, streetZ3 - 2000), Color.Black, new Vector2(0, 1));
            verts[18] = new VertexPositionColorTexture(new Vector3(-2250 - 4700, 0, streetZ3 + 2000), Color.Black, new Vector2(1, 0));
            verts[19] = new VertexPositionColorTexture(new Vector3(-2250, 0, streetZ3 + 2000), Color.Black, new Vector2(0, 0));
            verts[20] = new VertexPositionColorTexture(new Vector3(2250, 0, streetZ3 - 2000), Color.Black, new Vector2(1, 1));
            verts[21] = new VertexPositionColorTexture(new Vector3(2250 + 4700, 0, streetZ3 - 2000), Color.Black, new Vector2(0, 1));
            verts[22] = new VertexPositionColorTexture(new Vector3(2250, 0, streetZ3 + 2000), Color.Black, new Vector2(1, 0));
            verts[23] = new VertexPositionColorTexture(new Vector3(2250 + 4700, 0, streetZ3 + 2000), Color.Black, new Vector2(0, 0));

            vertexBufferGrass = new VertexBuffer(GraphicsDevice, VertexPositionColorTexture.SizeInBytes * 24, BufferUsage.WriteOnly);
            vertexBufferGrass.SetData(verts);

        }
        public void skyTile()
        {
            VertexPositionColorTexture[] verts = new VertexPositionColorTexture[4];

            verts[0] = new VertexPositionColorTexture(new Vector3(skyX - 3000, 700, -4000), Color.Black, new Vector2(0, 0));
            verts[1] = new VertexPositionColorTexture(new Vector3(skyX + 3000, 700, -4000), Color.Black, new Vector2(1, 0));
            verts[2] = new VertexPositionColorTexture(new Vector3(skyX - 3000, 0, -4000), Color.Black, new Vector2(0, 1));
            verts[3] = new VertexPositionColorTexture(new Vector3(skyX + 3000, 0, -4000), Color.Black, new Vector2(1, 1));

            vertexBufferSky = new VertexBuffer(GraphicsDevice, VertexPositionColorTexture.SizeInBytes * 4, BufferUsage.WriteOnly);
            vertexBufferSky.SetData(verts);
        }
        public void prepareScene()
        {
            //now create the index buffer
            short[] indices = new short[6];
            indices[0] = 0;
            indices[1] = 1;
            indices[2] = 2;

            indices[3] = 2;
            indices[4] = 1;
            indices[5] = 3;

            indexBuffer = new IndexBuffer(GraphicsDevice, sizeof(short) * 6, BufferUsage.WriteOnly, IndexElementSize.SixteenBits);
            indexBuffer.SetData(indices);



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
            indexBufferGrass = new IndexBuffer(GraphicsDevice, sizeof(short) * 36, BufferUsage.WriteOnly, IndexElementSize.SixteenBits);
            indexBufferGrass.SetData<short>(index);

            vertexDec = new VertexDeclaration(GraphicsDevice, VertexPositionColorTexture.VertexElements);

        }
        public void drawStreetSky()
        {
            //set up our graphics device

            GraphicsDevice.Vertices[0].SetSource(vertexBufferStreet1, 0, VertexPositionColorTexture.SizeInBytes);
            GraphicsDevice.Indices = indexBuffer;
            effect1.Parameters["ColorMap"].SetValue(Street);

            effect1.Begin();
            foreach (EffectPass pass in effect1.CurrentTechnique.Passes)
            {
                pass.Begin();
                GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 4, 0, 2);
                pass.End();
            }
            effect1.End();

            GraphicsDevice.Vertices[0].SetSource(vertexBufferStreet2, 0, VertexPositionColorTexture.SizeInBytes);
            GraphicsDevice.Indices = indexBuffer;

            effect1.Parameters["ColorMap"].SetValue(Street);
            effect1.Begin();
            foreach (EffectPass pass in effect1.CurrentTechnique.Passes)
            {
                pass.Begin();
                GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 4, 0, 2);
                pass.End();
            }
            effect1.End();

            GraphicsDevice.Vertices[0].SetSource(vertexBufferStreet3, 0, VertexPositionColorTexture.SizeInBytes);
            GraphicsDevice.Indices = indexBuffer;

            effect1.Parameters["ColorMap"].SetValue(Street);
            effect1.Begin();
            foreach (EffectPass pass in effect1.CurrentTechnique.Passes)
            {
                pass.Begin();
                GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 4, 0, 2);
                pass.End();
            }
            effect1.End();



            effect1.Parameters["ColorMap"].SetValue(Grass);


            GraphicsDevice.Vertices[0].SetSource(vertexBufferGrass, 0, VertexPositionColorTexture.SizeInBytes);
            GraphicsDevice.Indices = indexBufferGrass;


            effect1.Begin();
            foreach (EffectPass pass in effect1.CurrentTechnique.Passes)
            {
                pass.Begin();
                GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 24, 0, 12);
                pass.End();
            }
            effect1.End();

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            //Console.WriteLine(camX);
            viewMatrix = Matrix.CreateLookAt(new Vector3(camX, 1600.0f, 1000.0f), new Vector3(camX, 250.0f, -1000.0f), new Vector3(0.0f, 1.0f, 0.0f));


            drawPlayer();

            streetTile1();
            streetTile2();
            streetTile3();
            grassTile();
            skyTile();


            //setup our effect
            effect1.Parameters["World"].SetValue(worldMatrix);
            effect1.Parameters["View"].SetValue(viewMatrix);
            effect1.Parameters["Projection"].SetValue(projMatrix);
            effect1.CurrentTechnique = effect1.Techniques["Technique1"];
            GraphicsDevice.VertexDeclaration = vertexDec;



            spriteBatch.Draw(BarB, new Vector2(778, 75), new Rectangle(0, 0, 55, progress * 7), Color.White, MathHelper.ToRadians(180.0f), new Vector2(0, 440), 1.0f, SpriteEffects.None, 0.1f);

            switch (damage)
            {
                case 0:
                    drawDamage = 0;
                    break;
                case 1:
                    drawDamage = 45;
                    break;
                case 2:
                    drawDamage = 90;
                    break;
                case 3:
                    drawDamage = 135;
                    break;
                case 4:
                    drawDamage = 180;
                    break;
                case 5:
                    drawDamage = 220;
                    break;
                case 6:
                    drawDamage = 265;
                    break;
                case 7:
                    drawDamage = 310;
                    break;
                case 8:
                    drawDamage = 350;
                    break;
                case 9:
                    drawDamage = 395;
                    break;
                case 10:
                    drawDamage = 440;
                    break;

            }

            spriteBatch.Draw(BarR, new Vector2(72, 77), new Rectangle(0, 0, 55, drawDamage), Color.White, MathHelper.ToRadians(180.0f), new Vector2(0, 440), 1.0f, SpriteEffects.None, 0.1f);



            spriteBatch.Draw(redFlash, new Rectangle(0, 0, 800, 600), new Rectangle(0, 0, 800, 600), screenFade, 0.0f, Vector2.Zero, SpriteEffects.None, .5f);


            drawStreetSky();




        }
        public void drawPlayer()
        {
            GraphicsDevice.RenderState.CullMode = CullMode.None;


            //Matrix newViewMatrix = Matrix.CreateLookAt(new Vector3(camX, 300.0f, 0.0f), new Vector3(camX, 0.0f, -1000.0f), new Vector3(0.0f, 1.0f, 0.0f));


            foreach (ModelMesh mesh in model.Meshes)
            {

                foreach (BasicEffect effect in mesh.Effects)
                {

                    effect.EnableDefaultLighting();
                    effect.Projection = projMatrix;
                    effect.View = viewMatrix;
                    effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateRotationY(MathHelper.ToRadians(rotation))
                                       * Matrix.CreateScale(100.0f) * Matrix.CreateTranslation(new Vector3(camX, 0.0f, -250.0f));
                    effect.Texture = carTexture;
                }
                playerSphere = mesh.BoundingSphere;
                playerSphere.Transform(transforms[mesh.ParentBone.Index]);
                playerSphere.Center = new Vector3(camX, 0.0f, -250.0f);
                playerSphere.Radius = (mesh.BoundingSphere.Radius * 250.0f);

                //Console.WriteLine(boundingSphere.Radius.ToString());
                //Render(boundingSphere, GraphicsDevice, viewMatrix, projMatrix, Color.SaddleBrown);
                mesh.Draw();
            }

        }


    }
}