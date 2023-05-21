using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace VillageBuilder
{
    public class PlayMode : IGameMode
    {
        public static ObjectToBuy obj;


        public static Vector2 StandartRect;
        public static Camera Camera;
        public static bool OnBuildMode = false;
        public static Texture2D SelectedTexture;

        private const int CellsInWidth = 30;
        private const int CellsInHeight = 16;
        private const float ShareInWidthUI = 0.25f;

        public static Resource[] Resources;
        private SpriteFont _font;
        private Cell[,] _cells; // width, height
        private UI _ui;

        private Vector2 _start;
        private Vector2 _end;

        public PlayMode()
        {
            var fieldSize = new Vector2(
                Game1.Graphics.PreferredBackBufferWidth * 3f, 
                Game1.Graphics.PreferredBackBufferHeight * 3f);

            _start = new Vector2(0,0);
            _end = fieldSize + 
                new Vector2(Game1.Graphics.PreferredBackBufferWidth * ShareInWidthUI, 0);

            StandartRect = new Vector2(
                fieldSize.X / CellsInWidth,
                fieldSize.Y / CellsInHeight);

            Camera = new Camera(_start, _end, 1);
        }

        public void Initialize()
        {
            _font = Game1.ContentManage.Load<SpriteFont>(@"Fonts/VlaShu");

            _cells = new Cell[CellsInWidth, CellsInHeight];
            for (int i = 0; i < _cells.GetLength(0); i++)
            {
                for(int j= 0; j < _cells.GetLength(1); j++)
                {
                    _cells[i, j] = new Cell(StandartRect, new Vector2(i * StandartRect.X, j * StandartRect.Y));
                    _cells[i, j].Initialize();
                }
            }

            var widthRes = Game1.Graphics.PreferredBackBufferWidth * (1 -ShareInWidthUI);

            var resourceRectSize = new Vector2(
                widthRes, 
                StandartRect.Y/2);

            var woodTexture = Game1.ContentManage.Load<Texture2D>(@"Sprites/wood");
            var ironTexture = Game1.ContentManage.Load<Texture2D>(@"Sprites/iron");
            var stoneTexture = Game1.ContentManage.Load<Texture2D>(@"Sprites/stone");

            Resources = new[]
            {
                new Resource(ResourceType.Wood, 20, woodTexture, _font),
                new Resource(ResourceType.Stone, 1000000, stoneTexture, _font),
                new Resource(ResourceType.Iron, 0, ironTexture, _font)
            };

            _ui = new UI(new Rectangle(new Point((int)widthRes, 0), 
                new Point(
                    (int)(Game1.Graphics.PreferredBackBufferWidth * ShareInWidthUI), 
                    Game1.Graphics.PreferredBackBufferHeight)), Resources);

            _ui.Initialize();
        }

        public void Update(GameTime gameTime)
        {
            Camera.Update(gameTime);
            _ui.Update(gameTime);


            for (int i = 0; i < _cells.GetLength(0); i++)
            {
                for (int j = 0; j < _cells.GetLength(1); j++)
                {
                    _cells[i, j].Update(gameTime);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Game1.Graphics.GraphicsDevice.Clear(Color.ForestGreen);

            spriteBatch.Begin(transformMatrix: Camera.GetTransformationMatrix());

            for (int i = 0; i < _cells.GetLength(0); i++)
            {
                for (int j = 0; j < _cells.GetLength(1); j++)
                {
                    _cells[i, j].Draw(spriteBatch);
                }
            }

            spriteBatch.End();

            spriteBatch.Begin();

            _ui.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}