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

        private Texture2D contentTest;
        private Vector2 contentPosition;
        private float contentMoveSpeed;

        private MeshRenderer mr;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            contentPosition = new Vector2(
                _graphics.PreferredBackBufferWidth / 2f,
                _graphics.PreferredBackBufferHeight / 2f);
            contentMoveSpeed = 100f;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            contentTest = Content.Load<Texture2D>("images/contentTester");

            camera = new Camera(GraphicsDevice, Window);
            camera.Position = new Vector3(0, 0, 0);
            camera.LookAtDirection = Vector3.Forward;

            mr = new MeshRenderer(camera, GraphicsDevice, _graphics);
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            float updatedContentMoveSpeed = contentMoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            KeyboardState kstate = Keyboard.GetState();

            /*if (kstate.IsKeyDown(Keys.Up)) camera.MoveForward(gameTime);
            if (kstate.IsKeyDown(Keys.Down)) camera.MoveBackward(gameTime);
            if (kstate.IsKeyDown(Keys.Left)) camera.MoveLeft(gameTime);
            if (kstate.IsKeyDown(Keys.Right)) camera.MoveRight(gameTime);*/

            HandleScreenEdges();

            camera.Update(gameTime);

            base.Update(gameTime);
        }

        private void HandleScreenEdges()
        {
            /* Sides of the screen. */
            if (contentPosition.X > _graphics.PreferredBackBufferWidth - contentTest.Width / 2)
                contentPosition.X = _graphics.PreferredBackBufferWidth - contentTest.Width / 2;
            else if (contentPosition.X < contentTest.Width / 2)
                contentPosition.X = contentTest.Width / 2;

            /* Top and bottom of the screen. */
            if (contentPosition.Y > _graphics.PreferredBackBufferHeight - contentTest.Height / 2)
                contentPosition.Y = _graphics.PreferredBackBufferHeight - contentTest.Height / 2;
            else if (contentPosition.Y < contentTest.Height / 2)
                contentPosition.Y = contentTest.Height / 2;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: camera.View);
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}