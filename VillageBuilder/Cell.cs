using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageBuilder
{
    public class Cell : IGameObject
    {
        public Rectangle Rect;
        public Building Building { get; set; }


        private Texture2D _buildTexture;
        private Texture2D _texture;

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
            _texture = Game1.ContentManage.Load<Texture2D>(@"Sprites/Tile");
            _buildTexture = Game1.ContentManage.Load<Texture2D>(@"Sprites/TileForBuild");
        }

        public void Update(GameTime gameTime)
        {
            if (!PlayMode.OnBuildMode)
                return;

            var mouseState = Mouse.GetState();


            if (PlayMode.obj != null
                && mouseState.LeftButton == ButtonState.Pressed
                && Rect.Intersects(
                    new Rectangle(
                    new Point(
                        (int)((mouseState.Position.X + PlayMode.Camera.Position.X) / PlayMode.Camera.Scale),
                        (int)((mouseState.Position.Y + PlayMode.Camera.Position.Y) / PlayMode.Camera.Scale)
                        ),
                    new Point(0, 0)))
                && PlayMode.Resources.All(
                    x => !PlayMode.obj._costs.ContainsKey(x.Type)
                        || PlayMode.obj._costs.ContainsKey(x.Type) && PlayMode.obj._costs[x.Type] <= x.Count))
            {
                foreach (var res in PlayMode.Resources)
                {
                    if (PlayMode.obj._costs.ContainsKey(res.Type))
                        res.Count -= PlayMode.obj._costs[res.Type];
                }
                Building = PlayMode.obj.Building;
                Building.ChangeRect(Rect);
                PlayMode.SelectedTexture = null;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Building is null)
            {
                if(PlayMode.OnBuildMode)
                    spriteBatch.Draw(_buildTexture, Rect, Color.White);
                else 
                    spriteBatch.Draw(_texture, Rect, Color.White);
            }
            else
            {
                Building.Draw(spriteBatch);
            }

        }
    }
}
