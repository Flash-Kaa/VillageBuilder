using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace VillageBuilder
{
    internal class IronMining : Building
    {
        public override Dictionary<ResourceType, int> Gives { get; }
            = new Dictionary<ResourceType, int> { [ResourceType.Iron] = 5, [ResourceType.Stone] = 5 };

        public override Dictionary<ResourceType, int> Cost { get; }
            = new Dictionary<ResourceType, int> { [ResourceType.Wood] = 250, [ResourceType.Stone] = 350 };

        public IronMining(Texture2D texture, Rectangle rect) : base(texture, rect) { }

        public override Building Build(Rectangle newRectangle) => new IronMining(Texture, newRectangle);
    }
}
