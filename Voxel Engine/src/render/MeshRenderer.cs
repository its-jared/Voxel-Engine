using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void DrawCube(float width, float height, float depth, Microsoft.Xna.Framework.Color color1)
        {
            BasicEffect be = new BasicEffect(gd);
            VertexPositionColor[] verts = new VertexPositionColor[8];
            short[] indices = new short[36];
            VertexBuffer vb = new VertexBuffer(gd, typeof(VertexPositionColor), verts.Length, BufferUsage.WriteOnly);
            IndexBuffer ib = new IndexBuffer(gdm.GraphicsDevice, typeof(short), indices.Length, BufferUsage.WriteOnly);

            verts[0] = new VertexPositionColor(new Vector3(0, 0, 0), color1);
            verts[1] = new VertexPositionColor(new Vector3(width, 0, 0), color1);
            verts[2] = new VertexPositionColor(new Vector3(width, -height, 0), color1);
            verts[3] = new VertexPositionColor(new Vector3(0, -height, 0), color1);
            verts[4] = new VertexPositionColor(new Vector3(0, 0, depth), color1);
            verts[5] = new VertexPositionColor(new Vector3(width, 0, depth), color1);
            verts[6] = new VertexPositionColor(new Vector3(width, -height, depth), color1);
            verts[7] = new VertexPositionColor(new Vector3(0, -height, depth), color1);

            indices[0] = 0; indices[1] = 1; indices[2] = 2;
            indices[3] = 0; indices[4] = 3; indices[5] = 2;
            indices[6] = 4; indices[7] = 0; indices[8] = 3;
            indices[9] = 4; indices[10] = 7; indices[11] = 3;
            indices[12] = 3; indices[13] = 7; indices[14] = 6;
            indices[15] = 3; indices[16] = 6; indices[17] = 2;
            indices[18] = 1; indices[19] = 5; indices[20] = 6;
            indices[21] = 1; indices[22] = 5; indices[23] = 2;
            indices[24] = 4; indices[25] = 5; indices[26] = 6;
            indices[27] = 4; indices[28] = 7; indices[29] = 6;
            indices[30] = 0; indices[31] = 1; indices[32] = 5;
            indices[33] = 0; indices[34] = 4; indices[35] = 5;

            vb.SetData<VertexPositionColor>(verts);
            ib.SetData(indices);
        }
    }
}
