using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageBuilder
{
    public class Resource : IGameObject
    {
        private int _count;
        public int Count
        {
            get => _count;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Count");

                _count = value;
            }
        }

        public ResourceType Type { get; }
        private Rectangle _rect;
        private Texture2D _texture;
        private SpriteFont _font;

        private Rectangle _rectSprite;
        private Rectangle _rectText;

        public Resource(ResourceType type, int count, Texture2D texture, SpriteFont font)
        {
            Type = type;
            Count = count;
            _texture = texture;
            _font = font;
        }

        public void Initialize()
        {
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _rectSprite, Color.White);
            spriteBatch.DrawString(_font, Count.ToString(), _rectText.Location.ToVector2(), Color.Black, 0f, Vector2.Zero, _rectText.Size.Y / _font.MeasureString("M").Y, SpriteEffects.None, 0f);
        }

        public void AddRect(Rectangle rect)
        {
            _rect = rect;

            _rectSprite = new Rectangle(rect.Location, new Point(rect.Size.Y, rect.Size.Y));
            _rectText = new Rectangle(rect.Location + new Point(rect.Size.Y, 0), new Point(rect.Size.X - rect.Size.Y, rect.Size.Y));
        }
    }
}
