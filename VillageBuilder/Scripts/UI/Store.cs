using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace VillageBuilder
{
    public static class Store
    {
        public static BuildingToBuy SelectedBuilding { get; set; }
        public static Rectangle StoreCollider { get; private set; }

        private static BuildingToBuy[] _buildingsForSale;
        private static Scroll _scroll;
        private static Game1 _game;

        private static Rectangle _scrollHelperRect;
        private static Texture2D _scrollHelperTexture;

        private const float _countBuildingInScrollArea = 1.75f;
        private const int BuildingCount = 6;

        public static void Initialize(Game1 game, Rectangle storeCollider)
        {
            _game = game;

            var buildingSize = (int)(storeCollider.Size.X / _countBuildingInScrollArea);
            StoreCollider = storeCollider;

            _scroll = new Scroll(
                tapArea: storeCollider, 
                startPosition: new Vector2(0, 0),
                endPosition: new Vector2(buildingSize * BuildingCount - storeCollider.Width, 0));

            _scrollHelperTexture = Game1.ContentManager.Load<Texture2D>(@"Sprites/scroll");

            var scale = (double)storeCollider.Width / _scrollHelperTexture.Width;

            _scrollHelperRect = new Rectangle(
                storeCollider.X,
                (int)(storeCollider.Y + storeCollider.Height - _scrollHelperTexture.Height * scale),
                (int)(_scrollHelperTexture.Width * scale), 
                (int)(_scrollHelperTexture.Height * scale));

            CreateBuildingsForSale(InitializeCellsForSale(storeCollider, buildingSize).ToArray());
        }

        public static void Update(GameTime gameTime)
        {
            _scroll.Update(gameTime);

            foreach (var obj in _buildingsForSale)
                obj.Update(gameTime);

            if (_scroll.HaveTap())
                SelectedBuilding = null;
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.End();
            spriteBatch.Begin(transformMatrix: _scroll.GetTransformationMatrix());

            foreach (var obj in _buildingsForSale)
                obj.Draw(spriteBatch);

            spriteBatch.End();
            spriteBatch.Begin();

            spriteBatch.Draw(_scrollHelperTexture, _scrollHelperRect, Color.White);
        }

        private static void CreateBuildingsForSale(Rectangle[] cellsForSale)
        {
            _buildingsForSale = new[]
            {
                new BuildingToBuy(
                    building: new Forest(Game1.ContentManager.Load<Texture2D>(@"Sprites/Tiles/tileForest"), cellsForSale[0]),
                    textureInShop: Game1.ContentManager.Load<Texture2D>(@"Sprites/Tiles/tileForestBuy"),
                    scroll: _scroll),

                new BuildingToBuy(
                    building: new Mine(Game1.ContentManager.Load<Texture2D>(@"Sprites/Tiles/tileMine"), cellsForSale[1]),
                    textureInShop: Game1.ContentManager.Load<Texture2D>(@"Sprites/Tiles/tileMineBuy"),
                    scroll: _scroll),

                new BuildingToBuy(
                    building: new IronMining(Game1.ContentManager.Load<Texture2D>(@"Sprites/Tiles/tileIronMine"), cellsForSale[2]),
                    textureInShop: Game1.ContentManager.Load<Texture2D>(@"Sprites/Tiles/tileIronMineBuy"),
                    scroll: _scroll),

                new BuildingToBuy(
                    building: new VillageHouse(Game1.ContentManager.Load<Texture2D>(@"Sprites/Tiles/tileHouse"), cellsForSale[3]),
                    textureInShop: Game1.ContentManager.Load<Texture2D>(@"Sprites/Tiles/tileHouseBuy"),
                    scroll: _scroll),

                new BuildingToBuy(
                    building: new Chapel(Game1.ContentManager.Load<Texture2D>(@"Sprites/Tiles/tileChapel"), cellsForSale[4]),
                    textureInShop: Game1.ContentManager.Load<Texture2D>(@"Sprites/Tiles/tileChapelBuy"),
                    scroll: _scroll),

                new BuildingToBuy(
                    building: new Administration(_game, Game1.ContentManager.Load<Texture2D>(@"Sprites/Tiles/tileAdministration"), cellsForSale[5]),
                    textureInShop: Game1.ContentManager.Load<Texture2D>(@"Sprites/Tiles/tileAdministrationBuy"),
                    scroll: _scroll)
            };

            foreach (var obj in _buildingsForSale)
                obj.Initialize();
        }

        private static IEnumerable<Rectangle> InitializeCellsForSale(Rectangle storeCollider, int buildingSize)
        {
            for (int i = 0; i < BuildingCount; i++)
            {
                yield return new Rectangle(storeCollider.Location, new Point(buildingSize, buildingSize));

                storeCollider = new Rectangle(
                    new Point(storeCollider.Location.X + buildingSize, storeCollider.Location.Y),
                    storeCollider.Size);
            }
        }
    }
}