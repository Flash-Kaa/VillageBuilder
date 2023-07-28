using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageBuilder
{
    public class Mine : Building
    {
        public override Dictionary<ResourceType, int> Gives { get; }
            = new Dictionary<ResourceType, int> { [ResourceType.Stone] = 5 };

        public override Dictionary<ResourceType, int> Cost { get; }
            = new Dictionary<ResourceType, int> { [ResourceType.Wood] = 100, [ResourceType.Stone] = 20 };

        public Mine(Texture2D texture, Rectangle rect) : base(texture, rect) { }

        public override Building Build(Rectangle newRectangle) => new Mine(Texture, newRectangle);
    }
}
