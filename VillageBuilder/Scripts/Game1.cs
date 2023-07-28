using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace VillageBuilder
{
    public class Game1 : Game
    {
        public static GraphicsDeviceManager Graphics { get; private set; }
        public static ContentManager ContentManager { get; private set; }
        public static SpriteBatch SpriteBatch { get; private set; }

        private bool _onMenu = true;
        private Menu _menu;

        private bool _onSettings = false;
        private Settings _settings;

        private bool _haveWin = false;
        private Win _win;

        private GamePlay _game;

        public Game1()
        {
            Graphics = new GraphicsDeviceManager(this);
 
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            ContentManager = Content;
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            _settings = new Settings(this);
            ClearGame();
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                _onMenu = true;
                _onSettings = false;
                _haveWin = false;
            }

            if (_haveWin)
                _win.Update(gameTime);
            else if(_onSettings)
                _settings.Update(gameTime);
            else if (_onMenu)
                _menu.Update(gameTime);
            else
                _game.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            SpriteBatch.Begin();
            if (_haveWin)
                _win?.Draw(SpriteBatch);
            else if (_onSettings)
                _settings?.Draw(SpriteBatch);
            else if (_onMenu)
                _menu?.Draw(SpriteBatch);
            else
                _game?.Draw(SpriteBatch);

            SpriteBatch.End();

            base.Draw(gameTime);
        }

        public void ClearGame()
        {
            _game = new GamePlay(this);
            _menu = new Menu(this);
            _win = new Win(this);
        }

        public void OffMenu()
            => _onMenu = false;

        public void OnMenu()
            => _onMenu = true;

        public void OffSettings()
            => _onSettings = false;

        public void OnSettings()
            => _onSettings = true;

        public void HaveWin()
            => _haveWin = true;

        public void NotHaveWin()
            => _haveWin = false;
    }
}