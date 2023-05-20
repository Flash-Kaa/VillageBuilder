using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageBuilder
{
    public class Cell : IGameObject
    {
        public Rectangle Rect { get; private set; }

        private Texture2D _texture;

        public Cell(Vector2 size, Vector2 position)
        {
            Rect = new Rectangle(position.ToPoint(), size.ToPoint());
        }

        public void Initialize()
        {
            _texture = Game1.ContentManage.Load<Texture2D>(@"Sprites/Tile");
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Rect, Color.White);
        }
    }
}
