using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace VillageBuilder
{
    public class UI : IGameObject
    {
        private Rectangle _rectUI;
        private Resource[] _resources;
        private Texture2D _whiteMask;
        private Clock _clock;
        private Button _openShop;

        private Button[] _shop;

        private const float ResourceRect = 0.07f;

        private int Indent = (int)(Game1.Graphics.PreferredBackBufferHeight / 50);

        public UI(Rectangle rect, Resource[] resources)
        {
            _rectUI = rect;
            _resources = resources;
        }

        public void Initialize()
        {
            _whiteMask = Game1.ContentManage.Load<Texture2D>(@"Sprites\white_pixel");

            var rect = new Rectangle(_rectUI.Location + new Point(0, Indent), 
                new Point(_rectUI.Width, (int)(Game1.Graphics.PreferredBackBufferHeight * ResourceRect)));

            _clock = new Clock(rect, Game1.ContentManage.Load<SpriteFont>(@"Fonts/VlaShu"));

            foreach (var res in _resources)
            {
                rect.Location += new Point(0, Indent + rect.Size.Y);

                res.AddRect(rect);
            }

            var shopTexture = Game1.ContentManage.Load<Texture2D>(@"Sprites/shop");

            rect.Location += new Point(0, Indent + rect.Size.Y);
            rect.Size = new Point(rect.Width, rect.Width / 3);

            _openShop = new Button(shopTexture, rect, 
                _ =>
                {
                    PlayMode.OnBuildMode = !PlayMode.OnBuildMode;
                    PlayMode.Camera.Works = !PlayMode.Camera.Works;
                    PlayMode.SelectedTexture = null;
                });
            rect.Location += new Point(0, rect.Size.Y);
            rect.Size = new Point(rect.Width / 3, rect.Width / 3);

            _shop = new[] 
            { 
                new Button(
                    Game1.ContentManage.Load<Texture2D>(@"Sprites/Tiles/tileForestBuy"), 
                    rect,
                    _ => 
                    {
                        if(PlayMode.Resources[0].Count >= 20)
                        { 
                            PlayMode.SelectedTexture = Game1.ContentManage.Load<Texture2D>(@"Sprites/Tiles/tileForest");
                            PlayMode.Resources[0].Count -= 20;
                        }
                    })
            };
        }

        public void Update(GameTime gameTime)
        {
            _clock.Update(gameTime);
            _openShop.Update(gameTime);

            if(PlayMode.OnBuildMode)
                foreach (var b in _shop)
                    b.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_whiteMask, _rectUI, Color.White);

            _clock.Draw(spriteBatch);
            _openShop.Draw(spriteBatch);

            foreach (var res in _resources)
                res.Draw(spriteBatch);

            if (PlayMode.OnBuildMode)
                foreach (var b in _shop)
                    b.Draw(spriteBatch);
        }
    }
}
