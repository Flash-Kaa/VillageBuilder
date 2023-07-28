using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VillageBuilder
{
    public class Win
    {
        private Texture2D _bg;
        private Rectangle _bgRect;

        private Rectangle _toMenuButtonRectangle;
        private Button _toMenuButton;

        public Win(Game1 game)
        {
            _bg = Game1.ContentManager.Load<Texture2D>(@"Sprites/bgWin");
            _bgRect = new Rectangle(0, 0,
                Game1.Graphics.PreferredBackBufferWidth, Game1.Graphics.PreferredBackBufferHeight);

            var size = new Point(
                Game1.Graphics.PreferredBackBufferWidth * 2/ 5,
                Game1.Graphics.PreferredBackBufferHeight / 5);

            var location = new Point(
                Game1.Graphics.PreferredBackBufferWidth / 2 - size.X / 2,
                Game1.Graphics.PreferredBackBufferHeight * 2 / 3 - size.Y / 2);

            _toMenuButtonRectangle = new Rectangle(location, size);
            _toMenuButton = new Button(
                Game1.ContentManager.Load<Texture2D>(@"Sprites/toMenu"),
                _toMenuButtonRectangle, 
                _ =>
                {
                    game.NotHaveWin();
                    game.OnMenu();
                });

        }

        public void Update(GameTime gameTime)
        {
            _toMenuButton.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_bg, _bgRect, Color.White);
            _toMenuButton.Draw(spriteBatch);
        }
    }
}
