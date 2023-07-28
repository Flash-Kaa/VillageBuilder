using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace VillageBuilder
{
    public class Chapel : Building
    {
        public override Dictionary<ResourceType, int> Cost { get; } 
            = Storage.Resources.ToDictionary(x => x.Type, _ => 2000);

        public static int TotalCount { get; private set; }
        public const float AddWorkTime = 1;

        public Chapel(Texture2D texture, Rectangle rect) : base(texture, rect) { }

        public override Building Build(Rectangle newRectangle)
        {
            TotalCount++;
            return new Chapel(Texture, newRectangle);
        }

        public static void ResetTotalCountToZero()
            => TotalCount = 0;
    }
}
