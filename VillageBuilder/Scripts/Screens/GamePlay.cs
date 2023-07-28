using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VillageBuilder
{
    public class GamePlay
    {
        public const float ShareInWidthUI = 0.3f;

        private const int CellsInWidth = 15;
        private const int CellsInHeight = 14;

        private Storage _resourcesStorage;
        private SpriteFont _font;
        private Cell[,] _cells; // [width, height]
        private UI _ui;
        private Vector2 _cellRect;

        private readonly Game1 _game;

        public GamePlay(Game1 game)
        {
            _game = game;

            VillageHouse.ResetTotalCountToZero();
            Chapel.ResetTotalCountToZero();

            Initialize();
        }

        public void Initialize()
        {
            _font = Game1.ContentManager.Load<SpriteFont>(@"Fonts/VlaShu");

            _resourcesStorage = new Storage(_font);
            InitializeCells();
            InitializeUI();
        }

        public void Update(GameTime gameTime)
        {
            _ui.Update(gameTime);

            for (int i = 0; i < _cells.GetLength(0); i++)
                for (int j = 0; j < _cells.GetLength(1); j++)
                    _cells[i, j].Update(gameTime);

            if (Settings.HardMode && Clock.TotalTimeFromStart >= Clock.SecondsForPlayInHard)
            {
                _game.ClearGame();
                _game.OnMenu();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _ui.Draw(spriteBatch);

            for (int i = 0; i < _cells.GetLength(0); i++)
                for (int j = 0; j < _cells.GetLength(1); j++)
                    _cells[i, j].Draw(spriteBatch);
        }

        private void InitializeCells()
        {
            _cellRect = new Vector2(
                Game1.Graphics.PreferredBackBufferWidth * (1 - ShareInWidthUI) / CellsInWidth,
                Game1.Graphics.PreferredBackBufferHeight / CellsInHeight);

            _cells = new Cell[CellsInWidth, CellsInHeight];
            for (int i = 0; i < _cells.GetLength(0); i++)
            {
                for (int j = 0; j < _cells.GetLength(1); j++)
                {
                    _cells[i, j] = new Cell(_cellRect, new Vector2(i * _cellRect.X - i/3, j * _cellRect.Y - j/3));
                    _cells[i, j].Initialize();
                }
            }
        }


        private void InitializeUI()
        {
            _ui = new UI(_game,
                new Rectangle(
                    new Point((int)(Game1.Graphics.PreferredBackBufferWidth * (1 - ShareInWidthUI)) - CellsInWidth/3, 0),
                    new Point(
                        (int)(Game1.Graphics.PreferredBackBufferWidth * ShareInWidthUI) + CellsInWidth,
                        Game1.Graphics.PreferredBackBufferHeight)), Storage.Resources);

            _ui.Initialize();
        }
    }
}