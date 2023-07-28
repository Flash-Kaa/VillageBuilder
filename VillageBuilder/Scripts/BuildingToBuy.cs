using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace VillageBuilder
{
    public class BuildingToBuy
    {
        public Building Building { get; }

        private Button _buyButton;
        private Rectangle _positionInShop;
        private Rectangle _startPositionInShop;

        private readonly Scroll _scroll;
        private readonly Texture2D _textureInShop;

        public BuildingToBuy(Texture2D textureInShop, Building building, Scroll scroll)
        {
            _textureInShop = textureInShop;
            _positionInShop = building.Rect;
            _startPositionInShop = building.Rect;
            _scroll = scroll;
            Building = building;
        }

        public void Initialize()
        {
            _buyButton = new Button(_textureInShop, _positionInShop, _ => Store.SelectedBuilding = this );
        }

        public void Update(GameTime gameTime)
        {
            _positionInShop.Location = _startPositionInShop.Location - _scroll.Position.ToPoint();

            if (Store.StoreCollider.X > _positionInShop.X)
            {
                _positionInShop.Width = _startPositionInShop.Width + _positionInShop.X - Store.StoreCollider.X;
                _positionInShop.Location = Store.StoreCollider.Location;
            }
            else
            {
                _positionInShop.Width = _startPositionInShop.Width;
            }

            _buyButton.UpdateClickableCollider(_positionInShop);
            _buyButton.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _buyButton.Draw(spriteBatch);
        }
    }
}
