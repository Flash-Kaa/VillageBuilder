using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VillageBuilder
{
    public class Menu
    {
        private Texture2D _background;
        private Rectangle _bgRect;

        private Button _playButton;
        private Button _settingButton;
        private Button _exitButton;

        private readonly Game1 _game;

        public Menu(Game1 game)
        {
            _game = game;
            Initialize();
        }

        public void Initialize()
        {
            _bgRect = new Rectangle(0, 0, 
                Game1.Graphics.PreferredBackBufferWidth, Game1.Graphics.PreferredBackBufferHeight);

            var playButtonSize = new Point(
                Game1.Graphics.PreferredBackBufferWidth / 4, Game1.Graphics.PreferredBackBufferWidth / 10);
            var playButtonPosition = new Point(
                Game1.Graphics.PreferredBackBufferWidth / 2, Game1.Graphics.PreferredBackBufferHeight * 7/ 15);

            var settingButtonSize = playButtonSize;
            var settingButtonPosition = new Point(playButtonPosition.X,
                playButtonPosition.Y + playButtonSize.Y + Game1.Graphics.PreferredBackBufferHeight / 30);

            var exitButtonSize = playButtonSize;
            var exitButtonPosition = new Point(playButtonPosition.X,
                settingButtonPosition.Y + settingButtonSize.Y + Game1.Graphics.PreferredBackBufferHeight / 30);

            var playButtonCollider = new Rectangle(
                playButtonPosition.X - playButtonSize.X/2,
                playButtonPosition.Y - playButtonSize.Y/2, 
                playButtonSize.X, 
                playButtonSize.Y);

            var settingButtonCollider = new Rectangle(
                settingButtonPosition.X - settingButtonSize.X / 2,
                settingButtonPosition.Y - settingButtonSize.Y / 2,
                settingButtonSize.X,
                settingButtonSize.Y);

            var exitButtonCollider = new Rectangle(
                exitButtonPosition.X - exitButtonSize.X / 2,
                exitButtonPosition.Y - exitButtonSize.Y / 2,
                exitButtonSize.X,
                exitButtonSize.Y);

            _background = Game1.ContentManager.Load<Texture2D>(@"Sprites/bgMenu");

            _playButton = new Button(
                Game1.ContentManager.Load<Texture2D>(@"Sprites/play"), 
                playButtonCollider,
                _ => _game.OffMenu());

            _settingButton = new Button(
                Game1.ContentManager.Load<Texture2D>(@"Sprites/settings"), 
                settingButtonCollider,
                _ => _game.OnSettings());

            _exitButton = new Button(
                Game1.ContentManager.Load<Texture2D>(@"Sprites/exit"), 
                exitButtonCollider,
                _ => _game.Exit());
        }

        public void Update(GameTime gameTime)
        {
            _playButton.Update(gameTime);
            _settingButton.Update(gameTime);
            _exitButton.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_background, _bgRect, Color.White);
            _playButton.Draw(spriteBatch);
            _settingButton.Draw(spriteBatch);
            _exitButton.Draw(spriteBatch);
        }
    }
}