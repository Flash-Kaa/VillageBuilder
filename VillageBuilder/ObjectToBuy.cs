using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VillageBuilder
{
    public class ObjectToBuy : IGameObject
    {
        public Building Building;
        private Texture2D _textureInShop;
        private Texture2D _textureOnMap;
        public Dictionary<ResourceType, int> _costs;
        private Dictionary<ResourceType, int> _gives;

        private Button _buyButton;
        private Rectangle _positionInShop;

        public ObjectToBuy(Texture2D textureInShop, Texture2D textureOnMap, 
            Dictionary<ResourceType, int> costs, Dictionary<ResourceType, int> gives, 
            Rectangle positionInShop)
        {
            _textureInShop = textureInShop;
            _textureOnMap = textureOnMap;
            _costs = costs;
            _gives = gives;
            _positionInShop = positionInShop;

            Building = new Building(_textureOnMap, new Rectangle(), _gives);
        }

        public void Initialize()
        {
            _buyButton = new Button(_textureInShop, _positionInShop,
                _ =>
            {
                if (PlayMode.Resources.All(
                    x => !_costs.ContainsKey(x.Type) 
                        || _costs.ContainsKey(x.Type) && _costs[x.Type] <= x.Count))
                {
                    PlayMode.obj = this;
                }
                else
                {
                    PlayMode.obj = null;
                }
            });
        }

        public void Update(GameTime gameTime)
        {
            _buyButton.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _buyButton.Draw(spriteBatch);
        }
    }
}
