using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace VillageBuilder
{
    public class UI
    {
        private Texture2D _whiteMask;
        private Texture2D _shopTexture;
        private Rectangle _shopRect;
        private Clock _clock;

        private readonly Rectangle _rectUI;
        private readonly Resource[] _resources;
        private readonly Game1 _game;
        private readonly int indent;

        private const float ResourceRect = 0.09f;

        public UI(Game1 game,Rectangle rect, Resource[] resources)
        {
            _game = game;
            _rectUI = rect;
            _resources = resources;

            indent = Game1.Graphics.PreferredBackBufferHeight / 100;
        }

        public void Initialize()
        {
            var linearLayout = new Rectangle(_rectUI.Location + new Point(0, indent), 
                new Point(_rectUI.Width, (int)(Game1.Graphics.PreferredBackBufferHeight * ResourceRect)));

            _clock = new Clock(linearLayout, Game1.ContentManager.Load<SpriteFont>(@"Fonts/VlaShu"));

            linearLayout.Location += new Point(0, indent + linearLayout.Size.Y);

            var rectsForResources = DivideRectForResources(linearLayout).ToArray();
            for (int i = 0; i < _resources.Length; i++)
                _resources[i].UpdRect(rectsForResources[i]);

            linearLayout.Y = indent + rectsForResources.Last().Location.Y + rectsForResources.Last().Height;
            linearLayout.Height = linearLayout.Width / 3;

            _shopRect = linearLayout;

            linearLayout.Y += linearLayout.Height;
            linearLayout.Size = new Point(
                Game1.Graphics.PreferredBackBufferWidth - linearLayout.Location.X,
                Game1.Graphics.PreferredBackBufferHeight - linearLayout.Location.Y);
            
            Store.Initialize(_game, linearLayout);
            _whiteMask = Game1.ContentManager.Load<Texture2D>(@"Sprites/white_pixel");
            _shopTexture = Game1.ContentManager.Load<Texture2D>(@"Sprites/shop");
        }

        public void Update(GameTime gameTime)
        {
            _clock.Update(gameTime);
            Store.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_whiteMask, _rectUI, Color.White);

            _clock.Draw(spriteBatch);
            foreach (var res in _resources)
                res.Draw(spriteBatch);

            spriteBatch.Draw(_shopTexture, _shopRect, Color.White);

            Store.Draw(spriteBatch);
        }

        private IEnumerable<Rectangle> DivideRectForResources(Rectangle rect)
        {
            for (int i = 0; i < _resources.Length; i++)
            {
                yield return new Rectangle(rect.Location, new Point(rect.Width / 2, rect.Height));

                rect.Y += rect.Height + indent;
            }
        }
    }
}
