using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace VillageBuilder
{
    public class Cell
    {
        public Rectangle Rect { get; }
        public Building Building { get; private set; }

        private Texture2D _buildTexture;

        public Cell(Vector2 size, Vector2 position)
        {
            Rect = new Rectangle(position.ToPoint(), size.ToPoint());
        }

        public Cell(Rectangle rect)
        {
            Rect = rect;
        }

        public void Initialize()
        {
            _buildTexture = Game1.ContentManager.Load<Texture2D>(@"Sprites/TileForBuild");
        }

        public void Update(GameTime gameTime)
        {
            if (Building != null)
                Building.Update(gameTime);

            var mouseState = Mouse.GetState();

            var mouseRect = new Rectangle(
                        new Point(mouseState.Position.X, mouseState.Position.Y),
                        new Point(1, 1));

            if (mouseState.X <= Game1.Graphics.PreferredBackBufferWidth * (1 - GamePlay.ShareInWidthUI)
                && Building == null && Store.SelectedBuilding != null
                && mouseState.LeftButton == ButtonState.Pressed
                && Rect.Intersects(mouseRect)
                && Storage.Resources.All(
                    x => !Store.SelectedBuilding.Building.Cost.ContainsKey(x.Type)
                        || Store.SelectedBuilding.Building.Cost.ContainsKey(x.Type) 
                        && Store.SelectedBuilding.Building.Cost[x.Type] <= x.Count))
            {
                foreach (var res in Storage.Resources)
                {
                    if (Store.SelectedBuilding.Building.Cost.ContainsKey(res.Type))
                        res.Count -= Store.SelectedBuilding.Building.Cost[res.Type];
                }

                Building = Store.SelectedBuilding.Building.Build(Rect);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Building is null)
                spriteBatch.Draw(_buildTexture, Rect, Color.White);
            else
                Building.Draw(spriteBatch);
        }
    }
}
