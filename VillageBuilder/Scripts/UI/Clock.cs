using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VillageBuilder
{
    public class Clock
    {
        public static int Time { get; private set; }
        public static int TotalTimeFromStart { get; private set; }

        public const int SecondsForPlayInHard = 400;
        private const int _littleTimeLeft = 30;

        private double _lastTimeUpd = 0;
        private readonly Rectangle _rect;
        private readonly SpriteFont _font;

        public Clock(Rectangle rect, SpriteFont font) 
        { 
            _rect = rect;
            _font = font;
            Time = 0;
            TotalTimeFromStart = 0;
        }

        public void Update(GameTime gameTime)
        {
            var currentTime = gameTime.TotalGameTime.TotalSeconds;

            if (currentTime - _lastTimeUpd < 1)
                return;

            TotalTimeFromStart++;
            Time = ++Time % 24;
            _lastTimeUpd = currentTime;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var writeTime = Settings.HardMode ? SecondsForPlayInHard - TotalTimeFromStart : Time;
            var color = 
                Settings.HardMode && SecondsForPlayInHard - TotalTimeFromStart < _littleTimeLeft 
                ? Color.Red : Color.Black;

            spriteBatch.DrawString(_font, "Time: " + writeTime.ToString(), 
                _rect.Location.ToVector2(), color, 0f, Vector2.Zero,
                _rect.Size.Y / _font.MeasureString("M").Y, SpriteEffects.None, 0f);
        }
    }
}