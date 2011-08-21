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
    class Burglar
    {
        GraphicsDevice graphics;
        public Vector3 position;
        private float scale;
        private Model burglar1;
        private Model burglar2;
        private Model burglarS;
        private Model burglarS2;
        private Matrix viewMatrix;
        private Matrix projMatrix;
        private Matrix[] transforms;
        private Matrix[] transforms2;
        private Matrix[] transforms3;
        private Matrix[] transforms4;
        public bool visible;
        public static Random rand = new Random();
        private float Speed;
        TimeSpan timer;
        TimeSpan shockTimer;
        TimeSpan shotTimer;
        private bool switchM;
        private bool switchS;
        private int switchTime;
        public bool shocked;

        public static bool flash;
        BoundingSphere sphere;
        BoundingSphere sphere2;
        BoundingSphere sphere3;
        BoundingSphere sphere4;
        BoundingSphere sphere5;
        BoundingSphere sphere6;

        static VertexBuffer vertBuffer;
        static VertexDeclaration vertDecl;
        static BasicEffect effect;
        static int sphereResolution;



        public Burglar(GraphicsDevice graphics, Vector3 position, float scale, Matrix projMatrix, float Speed)
        {
            this.graphics = graphics;
            this.position = position;
            this.scale = scale;
            this.projMatrix = projMatrix;
            this.Speed = Speed;
            timer = new TimeSpan(0, 0, 0);
            visible = false;
            switchM = true;
            switchS = false;
            shocked = false;
            switchTime = 750;
            shockTimer = new TimeSpan(0, 0, 0, 0, 75);
            shotTimer = new TimeSpan(0, 0, 0, 0, 750);
            flash = false;

        }
        public virtual void LoadContent(ContentManager content)
        {
            this.burglar1 = content.Load<Model>("Model/Burglar1");
            this.transforms = new Matrix[this.burglar1.Bones.Count];
            this.burglar1.CopyAbsoluteBoneTransformsTo(this.transforms);
            this.burglar2 = content.Load<Model>("Model/Burglar2");
            this.transforms2 = new Matrix[this.burglar2.Bones.Count];
            this.burglar2.CopyAbsoluteBoneTransformsTo(this.transforms2);
            this.burglarS = content.Load<Model>("Model/BurglarS");
            this.transforms3 = new Matrix[this.burglarS.Bones.Count];
            this.burglarS.CopyAbsoluteBoneTransformsTo(this.transforms3);
            this.burglarS2 = content.Load<Model>("Model/BurglarS2");
            this.transforms4 = new Matrix[this.burglarS2.Bones.Count];
            this.burglarS2.CopyAbsoluteBoneTransformsTo(this.transforms4);
            //InitializeGraphics(graphics, 1000);

        }
        public virtual void Update(GameTime time)
        {

            if (shocked == false)
            {
                if (visible == true)
                {
                    if (this.position.Z >= -5)
                    {
                        switchTime = 250;

                        sGameFour.progress -= .04f;
                        flash = true;
                    }
                    else
                    {
                        switchTime = 500;

                        this.position.Z += Speed;

                    }


                    timer -= time.ElapsedGameTime;

                    if (timer.Milliseconds <= 0)
                    {
                        switchM = !switchM;

                        timer = new TimeSpan(0, 0, 0, 0, switchTime);
                    }

                }
            }
            else
            {
                if (this.position.Z > -499)
                {
                    shotTimer -= time.ElapsedGameTime;
                    shockTimer -= time.ElapsedGameTime;

                    if (shockTimer.Milliseconds <= 0)
                    {
                        switchS = !switchS;

                        shockTimer = new TimeSpan(0, 0, 0, 0, 75);
                    }

                    if (shotTimer.Milliseconds <= 0)
                    {
                        shocked = false;
                        this.position.Z = -500;
                        visible = false;

                        shotTimer = new TimeSpan(0, 0, 0, 0, 750);
                    }
                }
            }
            
            

        }


        public void rayCheck(Ray ray)
        {
               
                
                Nullable<float> result = ray.Intersects(sphere);
                Nullable<float> result2 = ray.Intersects(sphere2);
                Nullable<float> result3 = ray.Intersects(sphere3);
                Nullable<float> result4 = ray.Intersects(sphere4);
                Nullable<float> result5 = ray.Intersects(sphere5);
                Nullable<float> result6 = ray.Intersects(sphere6);

                if (result.HasValue == true || result2.HasValue == true || result3.HasValue == true ||
                    result4.HasValue == true || result5.HasValue == true || result6.HasValue == true)
                {
                    //visible = false;
                    shocked = true;
                    //Console.WriteLine("intersects");
                    flash = false;
                }


                //Console.WriteLine("checking");


        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            graphics.RenderState.CullMode = CullMode.None;
            //Console.WriteLine(position.ToString());
            viewMatrix = Matrix.CreateLookAt(new Vector3(sGameFour.camX, 20.0f, 30.0f), new Vector3(sGameFour.camX, 0.0f, -500.0f), new Vector3(0.0f, 1.0f, 0.0f));

            if (visible == true)
            {



                if (shocked == true)
                {

                    if (switchS)
                    {

                        foreach (ModelMesh mesh in this.burglarS.Meshes)
                        {

                            foreach (BasicEffect effect in mesh.Effects)
                            {

                                effect.EnableDefaultLighting();
                                effect.Projection = projMatrix;
                                effect.View = viewMatrix;
                                effect.World = transforms3[mesh.ParentBone.Index] * Matrix.CreateRotationY(MathHelper.ToRadians(0.0f))
                                                   * Matrix.CreateScale(this.scale) * Matrix.CreateTranslation(position);
                            }
                            mesh.Draw();
                        }

                    }
                    else
                    {

                        foreach (ModelMesh mesh in this.burglarS2.Meshes)
                        {

                            foreach (BasicEffect effect in mesh.Effects)
                            {

                                effect.EnableDefaultLighting();
                                effect.Projection = projMatrix;
                                effect.View = viewMatrix;
                                effect.World = transforms4[mesh.ParentBone.Index] * Matrix.CreateRotationY(MathHelper.ToRadians(0.0f))
                                                   * Matrix.CreateScale(this.scale) * Matrix.CreateTranslation(position);
                            }
                            mesh.Draw();
                        }
                    }

                }
                else if (switchM)
                {
                    
                    foreach (ModelMesh mesh in this.burglar1.Meshes)
                    {

                        foreach (BasicEffect effect in mesh.Effects)
                        {

                            effect.EnableDefaultLighting();
                            effect.Projection = projMatrix;
                            effect.View = viewMatrix;
                            effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateRotationY(MathHelper.ToRadians(0.0f))
                                               * Matrix.CreateScale(this.scale) * Matrix.CreateTranslation(position);
                            
                        }

                        sphere = mesh.BoundingSphere;
                        //sphere.Transform(transforms[mesh.ParentBone.Index]);
                        sphere.Radius = (mesh.BoundingSphere.Radius * this.scale * 3);

                        sphere.Center = new Vector3(this.position.X, this.position.Y+3, this.position.Z);
                        
                        
                        //Render(sphere, graphics, viewMatrix, projMatrix, Color.SaddleBrown);

                        sphere2 = mesh.BoundingSphere;

                        sphere2.Radius = (mesh.BoundingSphere.Radius * this.scale *3);

                        sphere2.Center = new Vector3(this.position.X, this.position.Y + 7, this.position.Z);
                        
                        //Render(sphere2, graphics, viewMatrix, projMatrix, Color.Blue);

                        sphere3 = mesh.BoundingSphere;

                        sphere3.Radius = (mesh.BoundingSphere.Radius * this.scale * 3);

                        sphere3.Center = new Vector3(this.position.X, this.position.Y +11, this.position.Z);

                        //Render(sphere3, graphics, viewMatrix, projMatrix, Color.Blue);

                        sphere4 = mesh.BoundingSphere;

                        sphere4.Radius = (mesh.BoundingSphere.Radius * this.scale * 3);

                        sphere4.Center = new Vector3(this.position.X, this.position.Y + 15, this.position.Z);

                        //Render(sphere4, graphics, viewMatrix, projMatrix, Color.Blue);
                        sphere5 = mesh.BoundingSphere;

                        sphere5.Radius = (mesh.BoundingSphere.Radius * this.scale * 3);

                        sphere5.Center = new Vector3(this.position.X, this.position.Y + 19, this.position.Z);

                        //Render(sphere5, graphics, viewMatrix, projMatrix, Color.Blue);


                        sphere6 = mesh.BoundingSphere;

                        sphere6.Radius = (mesh.BoundingSphere.Radius * this.scale * 3);

                        sphere6.Center = new Vector3(this.position.X, this.position.Y + 22, this.position.Z);
                        
                        //Render(sphere6, graphics, viewMatrix, projMatrix, Color.Green);



                        mesh.Draw();
                    }

                }
                else
                {

                    foreach (ModelMesh mesh in this.burglar2.Meshes)
                    {

                        foreach (BasicEffect effect in mesh.Effects)
                        {

                            effect.EnableDefaultLighting();
                            effect.Projection = projMatrix;
                            effect.View = viewMatrix;
                            effect.World = transforms2[mesh.ParentBone.Index] * Matrix.CreateRotationY(MathHelper.ToRadians(0.0f))
                                               * Matrix.CreateScale(this.scale) * Matrix.CreateTranslation(position);
                            
                        }

                        sphere = mesh.BoundingSphere;
                        //sphere.Transform(transforms[mesh.ParentBone.Index]);
                        sphere.Radius = (mesh.BoundingSphere.Radius * this.scale * 3);

                        sphere.Center = new Vector3(this.position.X, this.position.Y + 3, this.position.Z);


                        //Render(sphere, graphics, viewMatrix, projMatrix, Color.SaddleBrown);

                        sphere2 = mesh.BoundingSphere;

                        sphere2.Radius = (mesh.BoundingSphere.Radius * this.scale * 3);

                        sphere2.Center = new Vector3(this.position.X, this.position.Y + 7, this.position.Z);

                        //Render(sphere2, graphics, viewMatrix, projMatrix, Color.Blue);

                        sphere3 = mesh.BoundingSphere;

                        sphere3.Radius = (mesh.BoundingSphere.Radius * this.scale * 3);

                        sphere3.Center = new Vector3(this.position.X, this.position.Y + 11, this.position.Z);

                        //Render(sphere3, graphics, viewMatrix, projMatrix, Color.Blue);

                        sphere4 = mesh.BoundingSphere;

                        sphere4.Radius = (mesh.BoundingSphere.Radius * this.scale * 3);

                        sphere4.Center = new Vector3(this.position.X, this.position.Y + 15, this.position.Z);

                        //Render(sphere4, graphics, viewMatrix, projMatrix, Color.Blue);
                        sphere5 = mesh.BoundingSphere;

                        sphere5.Radius = (mesh.BoundingSphere.Radius * this.scale * 3);

                        sphere5.Center = new Vector3(this.position.X, this.position.Y + 19, this.position.Z);

                        //Render(sphere5, graphics, viewMatrix, projMatrix, Color.Blue);


                        sphere6 = mesh.BoundingSphere;

                        sphere6.Radius = (mesh.BoundingSphere.Radius * this.scale * 3);

                        sphere6.Center = new Vector3(this.position.X, this.position.Y + 22, this.position.Z);

                        //Render(sphere6, graphics, viewMatrix, projMatrix, Color.Green);



                        mesh.Draw();
                    }
                }




            }
        }
        public static void InitializeGraphics(GraphicsDevice graphicsDevice, int sphereResolutioon)
        {
            sphereResolution = sphereResolutioon;

            vertDecl = new VertexDeclaration(graphicsDevice, VertexPositionColor.VertexElements);
            effect = new BasicEffect(graphicsDevice, null);
            effect.LightingEnabled = false;
            effect.VertexColorEnabled = false;

            VertexPositionColor[] verts = new VertexPositionColor[(sphereResolution + 1) * 3];

            int index = 0;

            float step = MathHelper.TwoPi / (float)sphereResolution;

            //create the loop on the XY plane first
            for (float a = 0f; a <= MathHelper.TwoPi; a += step)
            {
                verts[index++] = new VertexPositionColor(
                    new Vector3((float)Math.Cos(a), (float)Math.Sin(a), 0f),
                    Color.White);
            }

            //next on the XZ plane
            for (float a = 0f; a <= MathHelper.TwoPi; a += step)
            {
                verts[index++] = new VertexPositionColor(
                    new Vector3((float)Math.Cos(a), 0f, (float)Math.Sin(a)),
                    Color.White);
            }

            //finally on the YZ plane
            for (float a = 0f; a <= MathHelper.TwoPi; a += step)
            {
                verts[index++] = new VertexPositionColor(
                    new Vector3(0f, (float)Math.Cos(a), (float)Math.Sin(a)),
                    Color.White);
            }

            vertBuffer = new VertexBuffer(
                graphicsDevice,
                verts.Length * VertexPositionColor.SizeInBytes,
                BufferUsage.None);
            vertBuffer.SetData(verts);
        }
        public static void Render(
           BoundingSphere sphere,
           GraphicsDevice graphicsDevice,
           Matrix view,
           Matrix projection,
           Color color)
        {
            if (vertBuffer == null)
                InitializeGraphics(graphicsDevice, 30);

            graphicsDevice.VertexDeclaration = vertDecl;
            graphicsDevice.Vertices[0].SetSource(
                  vertBuffer,
                  0,
                  VertexPositionColor.SizeInBytes);

            effect.World =

                  Matrix.CreateScale(sphere.Radius) *
                  Matrix.CreateTranslation(sphere.Center);
            effect.View = view;
            effect.Projection = projection;
            effect.DiffuseColor = color.ToVector3();

            effect.Begin();
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Begin();

                //render each circle individually
                graphicsDevice.DrawPrimitives(
                      PrimitiveType.LineStrip,
                      0,
                      sphereResolution);
                graphicsDevice.DrawPrimitives(
                      PrimitiveType.LineStrip,
                      sphereResolution + 1,
                      sphereResolution);
                graphicsDevice.DrawPrimitives(
                      PrimitiveType.LineStrip,
                      (sphereResolution + 1) * 2,
                      sphereResolution);

                pass.End();
            }
            effect.End();
        }

    }

}