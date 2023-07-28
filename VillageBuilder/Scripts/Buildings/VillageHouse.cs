using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace VillageBuilder
{
    public class VillageHouse : Building
    {
        public override Dictionary<ResourceType, int> Cost { get; } 
            = new Dictionary<ResourceType, int> { [ResourceType.Wood] = 1000, [ResourceType.Stone] = 500 };

        public static int TotalCount { get; private set; }
        public const float Coefficient = 0.05f;

        public VillageHouse(Texture2D texture, Rectangle rect) : base(texture, rect) { }

        public override Building Build(Rectangle newRectangle)
        {
            TotalCount++;
            return new VillageHouse(Texture, newRectangle);
        }

        public static void ResetTotalCountToZero()
            => TotalCount = 0;
    }
}
