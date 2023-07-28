using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageBuilder
{
    public class Storage
    {
        public static Resource[] Resources { get; private set; }

        public Storage(SpriteFont font)
        {
            var woodTexture = Game1.ContentManager.Load<Texture2D>(@"Sprites/wood");
            var ironTexture = Game1.ContentManager.Load<Texture2D>(@"Sprites/iron");
            var stoneTexture = Game1.ContentManager.Load<Texture2D>(@"Sprites/stone");

            Resources = new[]
            {
                new Resource(ResourceType.Wood, 20, woodTexture, font),
                new Resource(ResourceType.Stone, 20, stoneTexture, font),
                new Resource(ResourceType.Iron, 0, ironTexture, font)
            };
        }
    }
}
