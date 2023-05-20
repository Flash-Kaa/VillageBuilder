using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VillageBuilder
{
    public class PlayMode : IGameMode
    {
        public static bool HaveStartedExecutingCommands { get; set; }
        public static Vector2 StandartRect;

        private bool _doFirstAfterPress;
        private Cell[,] _cells; // width, height

        // Для TextBox
        private SpriteFont _font;

        private const int CellsInWidth = 15;
        private const int CellsInHeight = 8;

        private const int CellsInWidthForUI = 4;

        private Resource[] resources;

        public PlayMode()
        {
            _doFirstAfterPress = true;
            HaveStartedExecutingCommands = false;

            StandartRect = new Vector2(
                Game1.Graphics.PreferredBackBufferWidth / CellsInWidth, 
                Game1.Graphics.PreferredBackBufferHeight / CellsInHeight);
        }


        public void Initialize()
        {
            UpdateLocationAndSize();

            _font = Game1.ContentManage.Load<SpriteFont>(@"Fonts/VlaShu");

            _cells = new Cell[CellsInWidth - CellsInWidthForUI, CellsInHeight];
            for (int i = 0; i < _cells.GetLength(0); i++)
            {
                for(int j= 0; j < _cells.GetLength(1); j++)
                {
                    _cells[i, j] = new Cell(StandartRect, new Vector2(i * StandartRect.X, j * StandartRect.Y));
                    _cells[i, j].Initialize();
                }
            }

            var widthRes =  _cells.GetLength(0) * StandartRect.X;

            var resourceRectSize = new Vector2(
                widthRes, 
                StandartRect.Y);

            var woodTexture = Game1.ContentManage.Load<Texture2D>(@"Sprites/wood");
            var ironTexture = Game1.ContentManage.Load<Texture2D>(@"Sprites/iron");
            var stoneTexture = Game1.ContentManage.Load<Texture2D>(@"Sprites/stone");

            resources = new[]
            {
                new Resource(ResourcesTypes.Wood, 20, new Rectangle(new Point((int)widthRes, 0), resourceRectSize.ToPoint()), woodTexture, _font),
                new Resource(ResourcesTypes.Stone, 0, new Rectangle(new Point((int)widthRes, (int)StandartRect.Y), resourceRectSize.ToPoint()), stoneTexture, _font),
                new Resource(ResourcesTypes.Iron, 0, new Rectangle(new Point((int)widthRes, (int)StandartRect.Y * 2), resourceRectSize.ToPoint()), ironTexture, _font)
            };
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Game1.Graphics.GraphicsDevice.Clear(Color.ForestGreen);


            for (int i = 0; i < _cells.GetLength(0); i++)
            {
                for (int j = 0; j < _cells.GetLength(1); j++)
                {
                    _cells[i, j].Draw(spriteBatch);
                }
            }

            foreach (var res in resources)
            {
                res.Draw(spriteBatch);
            }
        }

        public void UpdateLocationAndSize()
        {

        }
    }
}