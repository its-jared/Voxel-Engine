using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Voxel_Engine.src.player;
using Voxel_Engine.src.render;

namespace Voxel_Engine
{
    public class Game1 : Game
    {
        public Camera camera; 

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private MeshRenderer mr;
        private Matrix worldMatrix, viewMatrix, projectionMatrix;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            worldMatrix = Matrix.Identity;
            viewMatrix = Matrix.CreateLookAt(new Vector3(0, 0, 50), Vector3.Zero, Vector3.Up);
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4,
                GraphicsDevice.Viewport.AspectRatio,
                1.0f, 300.0f);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            camera = new Camera(GraphicsDevice, Window);
            camera.World = Matrix.Identity;
            camera.Position = new Vector3(0, 0, 0);
            camera.LookAtDirection = Vector3.Forward;

            mr = new MeshRenderer(camera, GraphicsDevice, _graphics);
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            camera.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: camera.View);
            mr.DrawCube(Color.Red, worldMatrix, viewMatrix, projectionMatrix);;
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}