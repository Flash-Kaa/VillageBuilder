using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VillageBuilder
{
    public interface IGameMode : IGameObject
    {
        void UpdateLocationAndSize();
    }
}