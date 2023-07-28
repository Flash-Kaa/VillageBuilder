using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace VillageBuilder
{
    public abstract class Building
    {
        public Rectangle Rect { get; private set; }
        public virtual Dictionary<ResourceType, int> Gives { get; }
        public abstract Dictionary<ResourceType, int> Cost { get; }

        protected Texture2D Texture { get; }

        private int _timeToStartWork = 6;
        private int _timeToEndWork = 22;
        private int _lastTimeUpd = -1;

        public Building(Texture2D texture, Rectangle rect)
        {
            Rect = rect;
            this.Texture = texture;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (Clock.Time >= _timeToStartWork - Chapel.TotalCount * Chapel.AddWorkTime
                && Clock.Time <= _timeToEndWork && _lastTimeUpd != Clock.Time
                && Gives != null)
            {
                foreach (var res in Storage.Resources)
                {
                    if (Gives.ContainsKey(res.Type))
                        res.Count += Gives[res.Type] 
                            + (int)(VillageHouse.TotalCount * VillageHouse.Coefficient * Gives[res.Type]);
                }

                _lastTimeUpd = Clock.Time;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Rect, Color.White);
        }

        public abstract Building Build(Rectangle newRectangle);
    }
}
