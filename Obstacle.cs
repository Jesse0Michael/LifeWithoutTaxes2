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
    class Obstacle
    {
        GraphicsDevice graphics;
        private String fileName;
        public Vector3 position;
        private float scale;
        public float rotation;
        private Model model;
        private Matrix viewMatrix;
        private Matrix projMatrix;
        private Matrix[] transforms;
        private float xLoc;
        public bool visible;
        public bool isCar;
        public static Random rand = new Random();
        public bool isRotate;
        private float Speed;
        static TimeSpan timer;
        private bool swerveLeft;
        private bool swerveRight;
        public BoundingSphere boundingSphere;
        private Texture2D carTexture;

        static VertexBuffer vertBuffer;
        static VertexDeclaration vertDecl;
        static BasicEffect effect;
        static int sphereResolution;



        public Obstacle(GraphicsDevice graphics, String fileName, Vector3 position, float scale, float rotation, Matrix projMatrix, 
                                bool isCar, bool isRotate, float Speed)
        {
            this.graphics = graphics;
            this.fileName = fileName;
            this.position = position;
            this.scale = scale;
            this.rotation = rotation;
            this.projMatrix = projMatrix;
            this.isCar = isCar;
            this.isRotate = isRotate;
            this.Speed = Speed;
            timer = new TimeSpan(0, 0, 2);
            swerveLeft = false;
            swerveRight = false;

            visible = false;

            boundingSphere = new BoundingSphere();

            boundingSphere.Center = new Vector3(0, 0, 125000);
            
            if (isCar == true)
            {
                int rotate = (int)rand.Next(0, 2);

                if (rotate == 0)
                {
                    this.isRotate = false;
                    rotation = 0.0f;
                    this.xLoc = (float)rand.NextDouble() * 2025.0f;
                    this.position.X = xLoc;
                }
                else if (rotate == 1)
                {
                    this.isRotate = true;
                    rotation = 180.0f;
                    this.xLoc = (float)rand.NextDouble() * 2025.0f - 2025;
                    this.position.X = xLoc;
                }

            }
            else
            {
                int rotate = (int)rand.Next(0, 2);

                if (rotate == 0)
                {
                    this.isRotate = false;
                    rotation += 0.0f;
                }
                else if (rotate == 1)
                {
                    this.isRotate = true;
                    rotation += 180.0f;
                }
                this.xLoc = (float)rand.NextDouble() * 4050.0f - 2025;
                this.position.X = xLoc;
            }

            
            
        }

        public virtual void LoadContent(ContentManager content)
        {
            this.model = content.Load<Model>(fileName);
            this.transforms = new Matrix[this.model.Bones.Count];
            this.model.CopyAbsoluteBoneTransformsTo(this.transforms);
            this.carTexture = content.Load<Texture2D>("Model/texture");
            //InitializeGraphics(graphics, 1000);

        }
        public virtual void Update(GameTime time)
        {

           
           if (visible == true)
           {
               
               
               if (isCar == true)
               {
                   timer -= time.ElapsedGameTime;

                   if (timer.Milliseconds <= 0)
                   {

                       int swerve = (int)rand.Next(0, 3);

                       if (swerve == 0)
                       {
                           swerveLeft = true;
                           swerveRight = false;

                       }
                       else if (swerve == 1)
                       {
                           swerveLeft = false;
                           swerveRight = false;
                       }
                       else if (swerve == 2)
                       {
                           swerveLeft = false;
                           swerveRight = true;
                       }

                       int random = (int)rand.Next(2, 4);
                       timer = new TimeSpan(0, 0, random);
                   }

                   if(swerveLeft == true )
                   {
                       if (isRotate == true && this.position.X >= -1950)
                       {
                           this.position.X -= 10.0f;
                       }
                       else if (isRotate == false && this.position.X >= -1950)
                       {
                           this.position.X -= 10.0f;
                       }
                   }
                   else if (swerveRight == true)
                   {
                       if (isRotate == true && this.position.X <= 1950)
                       {
                           this.position.X += 10.0f;
                       }
                       else if (isRotate == false && this.position.X <= 1950)
                       {
                           this.position.X += 10.0f;
                       }
                   }



                   if (isRotate == true)
                   {
                       
                       if (swerveLeft == true)
                       {
                           if (this.position.X <= -1950)
                           {
                               rotation = 180.0f;
                           }
                           else if (rotation > 160.0f)
                           {
                               rotation -= 5.0f;
                           }
                       }
                       else if (swerveRight == true)
                       {
                           if (this.position.X >= 1950)
                           {
                               rotation = 180.0f;
                           }
                           else if (rotation < 200.0f)
                           {
                               rotation += 5.0f;
                           }
                       }
                       else
                       {
                           if (rotation < 180.0f)
                           {
                               rotation += 5.0f;
                           }
                           else if (rotation > 180.0f)
                           {
                               rotation -= 5.0f;
                           }
                       }
                       
                           this.position.Z += Speed + 20.0f;
                   }
                   else if (isRotate == false)
                   {
                       if (swerveLeft == true)
                       {
                           if (this.position.X <= -1950)
                           {
                               rotation = 0.0f;
                           }
                           else if (rotation < 20.0f)
                           {
                               rotation += 5.0f;
                           }
                       }
                       else if (swerveRight == true)
                       {
                           if (this.position.X >= 1950)
                           {
                               rotation = 0.0f;
                           }
                           else if (rotation > -20.0f)
                           {
                               rotation -= 5.0f;
                           }
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
                       
                       this.position.Z += Speed - 10.0f;
                   }
                   
               }
               else
               {
                   this.position.Z += Speed;
               }

           }

           if (this.position.Z >= 500)
           {
               
               this.position.Z = -12500;
               if (isCar == true)
               {
                   int rotate = (int)rand.Next(0, 2);

                   if (rotate == 0)
                   {
                       this.isRotate = false;
                       rotation = 0.0f;
                       this.xLoc = (float)rand.NextDouble() * 2025.0f;
                       this.position.X = xLoc;
                   }
                   else if (rotate == 1)
                   {
                       this.isRotate = true;
                       rotation = 180.0f;
                       this.xLoc = (float)rand.NextDouble() * 2025.0f - 2025;
                       this.position.X = xLoc;
                   }

               }
               else
               {
                   int rotate = (int)rand.Next(0, 2);

                   if (rotate == 0)
                   {
                       this.isRotate = false;
                       rotation += 0.0f;
                   }
                   else if (rotate == 1)
                   {
                       this.isRotate = true;
                       rotation += 180.0f;
                   }
                   this.xLoc = (float)rand.NextDouble() * 4050.0f - 2025;
                   this.position.X = xLoc;
               }
               visible = false;
           }

        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            graphics.RenderState.CullMode = CullMode.None;
            //Console.WriteLine(position.ToString());
            if (visible == true)
            {
                if (Speed >= 40)
                {
                    this.viewMatrix = Matrix.CreateLookAt(new Vector3(sGameThree.camX, 1600.0f, 1000.0f), new Vector3(sGameThree.camX, 250.0f, -1000.0f), new Vector3(0.0f, 1.0f, 0.0f));
                }
                else
                {
                    this.viewMatrix = Matrix.CreateLookAt(new Vector3(sGameOne.camX, 1600.0f, 1000.0f), new Vector3(sGameOne.camX, 250.0f, -1000.0f), new Vector3(0.0f, 1.0f, 0.0f));
                }

                foreach (ModelMesh mesh in this.model.Meshes)
                {
                    
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        
                        effect.EnableDefaultLighting();
                        effect.Projection = projMatrix;
                        effect.View = viewMatrix;
                        effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateRotationY(MathHelper.ToRadians(rotation)) 
                                           * Matrix.CreateScale(this.scale) * Matrix.CreateTranslation(position) ;
                        effect.Texture = carTexture;
                    }
                    boundingSphere = mesh.BoundingSphere;
                    boundingSphere.Transform(transforms[mesh.ParentBone.Index]);
                    boundingSphere.Center = position;
                    if (isCar)
                        this.scale = this.scale * 3;

                    boundingSphere.Radius = (mesh.BoundingSphere.Radius * this.scale);
                    if (isCar)
                        this.scale = this.scale / 3;

                    //Console.WriteLine(boundingSphere.Radius.ToString());
                    //Render(boundingSphere, graphics, viewMatrix, projMatrix, Color.SaddleBrown);
                    mesh.Draw();
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