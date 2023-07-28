using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageBuilder
{
    public class Administration : Building
    {
        public override Dictionary<ResourceType, int> Cost { get; } = Storage.Resources.ToDictionary(x => x.Type, _ => 100000);

        private readonly Game1 _game;

        public Administration(Game1 game, Texture2D texture, Rectangle rect) : base(texture, rect) 
        {
            _game = game;
        }

        public override void Update(GameTime gameTime)
        {
            _game.HaveWin();
            base.Update(gameTime);
        }

        public override Building Build(Rectangle newRectangle) => new Administration(_game, Texture, newRectangle);
    }
}
