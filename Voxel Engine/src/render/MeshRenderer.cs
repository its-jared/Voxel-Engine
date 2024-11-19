using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Voxel_Engine.src.player;

namespace Voxel_Engine.src.render
{
    public class MeshRenderer
    {
        public Camera Camera
        {
            get { return camera; }
        }
        public GraphicsDevice GraphicsDevice
        {
            get { return gd; }
        }

        private Camera camera;
        private GraphicsDevice gd;
        private GraphicsDeviceManager gdm;

        public MeshRenderer(Camera camera, GraphicsDevice gd, GraphicsDeviceManager gdm)
        {
            this.camera = camera;
            this.gd = gd;
            this.gdm = gdm;
        }

        public void DrawMesh(Model mesh)
        {
            foreach (ModelMesh mMesh in mesh.Meshes)
            {
                foreach (BasicEffect effect in mMesh.Effects)
                {
                    effect.View = camera.View;
                    effect.World = camera.World;
                    effect.Projection = camera.Projection;
                    mMesh.Draw();
                }
            }
        }

        public void DrawCube(Color voxelColor, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix)
        {
            VertexPositionColor[] triverts = new VertexPositionColor[3];
            BasicEffect effect = new BasicEffect(gd);

            /* Init effects. */
            effect.World = worldMatrix;
            effect.View = viewMatrix;
            effect.Projection = projectionMatrix;

            effect.AmbientLightColor = new Vector3(0.1f, 0.1f, 0.1f);
            effect.DiffuseColor = new Vector3(1.0f, 1.0f, 1.0f);
            effect.SpecularColor = new Vector3(0.25f, 0.25f, 0.25f);
            effect.SpecularPower = 5.0f;
            effect.Alpha = 1.0f;

            effect.VertexColorEnabled = true;
            effect.EnableDefaultLighting();

            /* Init tris. */
            triverts[0].Position = new Vector3(0f, 0f, 0f);
            triverts[0].Color = voxelColor;
            triverts[1].Position = new Vector3(10f, 10f, 0f);
            triverts[1].Color = Color.Orange;
            triverts[2].Position = new Vector3(10f, 0f, -5f);
            triverts[2].Color = Color.Olive;

            /* Draw! */
            RasterizerState rs = new RasterizerState();
            rs.CullMode = CullMode.None;
            gd.RasterizerState = rs;

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                gd.DrawUserPrimitives(
                    PrimitiveType.TriangleList,
                    triverts,
                    0,
                    1,
                    VertexPositionColor.VertexDeclaration);
            }
        }
    }
}
