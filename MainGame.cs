using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1.Effects;
using SharpDX.DXGI;
using System.Diagnostics.Tracing;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Platformer.State;
using Platformer.Event;

namespace Platformer
{
    public class MainGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private RenderTarget2D _renderTarget;
        private Rectangle _renderScaleRectangle;
        private BaseGameState _currentGameState;

        private const int DESIGNED_RESOLUTION_WIDTH = 1280;
        private const int DESIGNED_RESOLUTION_HEIGHT = 720;
        private const float DESIGN_RESOLUTION_ASPECT_RATIO = DESIGNED_RESOLUTION_WIDTH / (float)DESIGNED_RESOLUTION_HEIGHT;

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);


            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            graphics.IsFullScreen = true;

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _renderTarget = new RenderTarget2D(graphics.GraphicsDevice, DESIGNED_RESOLUTION_WIDTH, DESIGNED_RESOLUTION_HEIGHT, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);

            _renderScaleRectangle = GetScaleRectangle();

            base.Initialize();
        }

        private Rectangle GetScaleRectangle()
        {
            var variance = 0.5;
            var actualAspectRatio = Window.ClientBounds.Width / (float)Window.ClientBounds.Height;

            Rectangle scaleRectangle;

            if (actualAspectRatio <= DESIGN_RESOLUTION_ASPECT_RATIO)
            {
                var presentHeight = (int)(Window.ClientBounds.Width / DESIGN_RESOLUTION_ASPECT_RATIO + variance);
                var barHeight = (Window.ClientBounds.Height - presentHeight) / 2;

                scaleRectangle = new Rectangle(0, barHeight, Window.ClientBounds.Width, presentHeight);
            }
            else
            {
                var presentWidth = (int)(Window.ClientBounds.Height * DESIGN_RESOLUTION_ASPECT_RATIO + variance);
                var barWidth = (Window.ClientBounds.Width - presentWidth) / 2;

                scaleRectangle = new Rectangle(barWidth, 0, presentWidth, Window.ClientBounds.Height);
            }

            return scaleRectangle;

        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            SwitchGameState(new SplashState());
        }

        protected override void UnloadContent()
        {
            _currentGameState?.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            _currentGameState.HandleInput();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(_renderTarget);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            _currentGameState.Render(spriteBatch);
            spriteBatch.End();

            graphics.GraphicsDevice.SetRenderTarget(null);
            graphics.GraphicsDevice.Clear(ClearOptions.Target, Color.Black, 1.0f, 0);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque);
            spriteBatch.Draw(_renderTarget, _renderScaleRectangle, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void SwitchGameState(BaseGameState gameState)
        {
            if(_currentGameState != null)
            {
                _currentGameState.OnStateSwitched -= OnStateSwitched;
                _currentGameState.OnEventNotification -= OnEventNotification;
                _currentGameState.UnloadContent();
            }

            _currentGameState?.UnloadContent();
            _currentGameState = gameState;
            _currentGameState.Initialise(Content);
            _currentGameState.LoadContent(Content);
            _currentGameState.OnStateSwitched += OnStateSwitched;
            _currentGameState.OnEventNotification += OnEventNotification;
        }

        private void OnStateSwitched(object sender, BaseGameState e)
        {
            SwitchGameState(e);
        }

        private void OnEventNotification(object sender, Events e)
        {
            switch(e)
            {
                case Events.GAME_QUIT:
                    Exit();
                    break;
            }

        }
    }


}
