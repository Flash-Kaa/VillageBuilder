using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageBuilder
{
    internal class Clock : IGameObject
    {
        public static int Time;
        private Rectangle _rect;
        private SpriteFont _font;

        public Clock(Rectangle rect, SpriteFont font) 
        { 
            _rect = rect;
            _font = font;
        }


        public void Initialize()
        {
        }

        public void Update(GameTime gameTime)
        {
            Time = (int)gameTime.TotalGameTime.TotalSeconds % 24;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, "Time: " + Time.ToString(), 
                _rect.Location.ToVector2(), Color.Black, 0f, Vector2.Zero,
                _rect.Size.Y / _font.MeasureString("M").Y, SpriteEffects.None, 0f);
        }
    }
}
