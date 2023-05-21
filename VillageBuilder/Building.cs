using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace VillageBuilder
{
    public class Building : IGameObject
    {
        private Texture2D _texture;
        private Rectangle _rect;
        Dictionary<ResourceType, int> _gives;

        public Building(Texture2D texture, Rectangle rect, Dictionary<ResourceType, int> gives)
        {
            _rect = rect;
            _texture = texture; 
        }

        public void Initialize()
        {
        }

        public void Update(GameTime gameTime)
        {
            foreach (var res in PlayMode.Resources)
            {
                if(_gives.ContainsKey(res.Type))
                    res.Count += _gives[res.Type];
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _rect, Color.White);
        }

        public void ChangeRect(Rectangle rect)
        {
            _rect = rect;
        }
    }
}
