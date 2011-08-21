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

    class gameOneControl
    {

        private GraphicsDevice graphics;
        private SpriteBatch spriteBatch;
        private ContentManager Content;
        private Effect effect1;

        VertexBuffer vertexBuffer = null;
        IndexBuffer indexBuffer = null;
        VertexBuffer vertexBuffer2 = null;
        IndexBuffer indexBuffer2 = null;
        VertexDeclaration vertexDec = null;

        Texture2D Street;
        Texture2D Sky;

        float streetZ = -1000;
        float camX = 0;

        public gameOneControl(ContentManager Content, GraphicsDevice graphics, SpriteBatch spriteBatch, Effect effect1)
        {
            this.Content = Content;
            this.graphics = graphics;
            this.spriteBatch = spriteBatch;
            this.effect1 = effect1;


            Street = Content.Load<Texture2D>("Texture/DrivingOneStreet");
            Sky = Content.Load<Texture2D>("Texture/DrivingOneSky");

            prepareScene();
        }
        


        public void Update()
        {

            
            streetZ += 1.0f;

            //KeyboardState keys = Keyboard.GetState();

            //if (keys.IsKeyDown(Keys.Left))
            //{
            //    camX -= .1f;
            //}
            //if (keys.IsKeyDown(Keys.Right))
            //{
            //    camX += .1f;
            //}


        }


        public void prepareScene()
        {
            VertexPositionColorTexture[] verts = new VertexPositionColorTexture[4];

            verts[0] = new VertexPositionColorTexture(new Vector3(-2400, 0, streetZ), Color.Black, new Vector2(0, 0));
            verts[1] = new VertexPositionColorTexture(new Vector3(2400, 0, streetZ), Color.Black, new Vector2(0, 1));
            verts[2] = new VertexPositionColorTexture(new Vector3(-2400, 0, streetZ+1000), Color.Black, new Vector2(1, 0));
            verts[3] = new VertexPositionColorTexture(new Vector3(2400, 0, streetZ+1000), Color.Black, new Vector2(1, 1));

            vertexBuffer = new VertexBuffer(graphics, VertexPositionColorTexture.SizeInBytes * 4, BufferUsage.WriteOnly);
            vertexBuffer.SetData(verts);

            //now create the index buffer
            short[] indices = new short[6];
            indices[0] = 0;
            indices[1] = 1;
            indices[2] = 2;

            indices[3] = 2;
            indices[4] = 1;
            indices[5] = 3;

            indexBuffer = new IndexBuffer(graphics, sizeof(short) * 6, BufferUsage.WriteOnly, IndexElementSize.SixteenBits);
            indexBuffer.SetData(indices);





            VertexPositionColorTexture[] verts2 = new VertexPositionColorTexture[4];

            verts2[0] = new VertexPositionColorTexture(new Vector3(camX - 2400, 600, -1000), Color.Black, new Vector2(0, 0));
            verts2[1] = new VertexPositionColorTexture(new Vector3(camX + 2400, 600, -1000), Color.Black, new Vector2(0, 1));
            verts2[2] = new VertexPositionColorTexture(new Vector3(camX - 2400, 0, -1000), Color.Black, new Vector2(1, 0));
            verts2[3] = new VertexPositionColorTexture(new Vector3(camX + 2400, 0, -1000), Color.Black, new Vector2(1, 1));

            vertexBuffer2 = new VertexBuffer(graphics, VertexPositionColorTexture.SizeInBytes * 4, BufferUsage.WriteOnly);
            vertexBuffer2.SetData(verts2);

            //now create the index buffer
            short[] indices2 = new short[6];
            indices2[0] = 0;
            indices2[1] = 1;
            indices2[2] = 2;

            indices2[3] = 2;
            indices2[4] = 1;
            indices2[5] = 3;

            indexBuffer2 = new IndexBuffer(graphics, sizeof(short) * 6, BufferUsage.WriteOnly, IndexElementSize.SixteenBits);
            indexBuffer2.SetData(indices);



            vertexDec = new VertexDeclaration(graphics, VertexPositionColorTexture.VertexElements);
            
        }

        public void Draw()
        {

            Matrix modelMatrix = Matrix.Identity;
            Matrix viewMatrix = Matrix.CreateLookAt(new Vector3(camX, 30, 0), new Vector3(0, 0,  - 1000),  new Vector3(0, 1, 0));
            Matrix projMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f),
                                                                     graphics.DisplayMode.AspectRatio,
                                                                     1.0f,
                                                                     1000.0f);
            //setup our effect
            effect1.Parameters["World"].SetValue(modelMatrix);
            effect1.Parameters["View"].SetValue(viewMatrix);
            effect1.Parameters["Projection"].SetValue(projMatrix);
            effect1.CurrentTechnique = effect1.Techniques["Technique1"];
            effect1.Parameters["ColorMap"].SetValue(Street);


            //set up our graphics device
            graphics.VertexDeclaration = vertexDec;
            graphics.Vertices[0].SetSource(vertexBuffer, 0, VertexPositionColorTexture.SizeInBytes);
            graphics.Indices = indexBuffer;


            effect1.Begin();
            foreach (EffectPass pass in effect1.CurrentTechnique.Passes)
            {
                pass.Begin();
                graphics.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 4, 0, 2);
                pass.End();
            }
            effect1.End();




            effect1.Parameters["ColorMap"].SetValue(Sky);


            graphics.Vertices[0].SetSource(vertexBuffer2, 0, VertexPositionColorTexture.SizeInBytes);
            graphics.Indices = indexBuffer2;


            effect1.Begin();
            foreach (EffectPass pass in effect1.CurrentTechnique.Passes)
            {
                pass.Begin();
                graphics.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 4, 0, 2);
                pass.End();
            }
            effect1.End();

        }
    }
}
