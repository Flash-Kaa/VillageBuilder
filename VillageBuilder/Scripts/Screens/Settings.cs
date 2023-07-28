using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace VillageBuilder
{
    public class Settings
    {
        public static bool HardMode { get; private set; }

        private Texture2D _background;
        private Rectangle _bgRect;

        private Rectangle _hardModeButtonCollide;
        private Button _hardModeOffButton;
        private Button _hardModeOnButton;
        private Button _currentHardButton;

        private Rectangle _screenButtonCollide;
        private Button _fullScreenButton;
        private Button _windowScreenButton;
        private Button _currentScreenButton;

        private Rectangle _backButtonCollider;
        private Button _backButton;

        private Texture2D[] _screenResolutionTextures;
        private List<Button> _screenResolutionButtons;

        private readonly Game1 _game;

        private const string _srTextureFileLocation = "Sprites/ScreenResolution/button";

        public Settings(Game1 game) 
        {
            HardMode = false;
            _game = game;
            Initialize();
        }

        public void Initialize()
        {
            _bgRect = new Rectangle(0, 0, Game1.Graphics.PreferredBackBufferWidth, Game1.Graphics.PreferredBackBufferHeight);
            _background = Game1.ContentManager.Load<Texture2D>(@"Sprites/bgSettings");

            CreateBackButton();
            CreateHardModeButton();
            CreateScreenButton();
            CreateScreenResolutionUpdateButtons();
        }

        public void Update(GameTime gameTime) 
        {
            _screenResolutionButtons.ForEach(button => button.Update(gameTime));
            _currentHardButton.Update(gameTime);
            _currentScreenButton.Update(gameTime);
            _backButton.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_background, _bgRect, Color.White);
            _screenResolutionButtons.ForEach(button => button.Draw(spriteBatch));
            _currentHardButton.Draw(spriteBatch);
            _currentScreenButton.Draw(spriteBatch);
            _backButton.Draw(spriteBatch);
        }

        private void CreateBackButton()
        {
            var size = new Point(Game1.Graphics.PreferredBackBufferWidth / 5, Game1.Graphics.PreferredBackBufferHeight / 8);
            var location = new Point(Game1.Graphics.PreferredBackBufferWidth / 2 - size.X / 2, Game1.Graphics.PreferredBackBufferHeight * 3 / 4 - size.Y / 2);

            _backButtonCollider = new Rectangle(location, size);

            _backButton = new Button(Game1.ContentManager.Load<Texture2D>(@"Sprites/back"), _backButtonCollider,
                _ => _game.OffSettings());
        }

        private void CreateHardModeButton()
        {
            _hardModeButtonCollide = new Rectangle(
                Game1.Graphics.PreferredBackBufferWidth * 3 / 4, Game1.Graphics.PreferredBackBufferHeight *17 / 32,
                Game1.Graphics.PreferredBackBufferWidth / 10, Game1.Graphics.PreferredBackBufferHeight / 10);

            _hardModeOffButton = new Button(Game1.ContentManager.Load<Texture2D>(@"Sprites/hardOFF"), _hardModeButtonCollide,
                _ =>
                {
                    HardMode = true;
                    _currentHardButton = _hardModeOnButton;
                    _game.ClearGame();
                });

            _hardModeOnButton = new Button(Game1.ContentManager.Load<Texture2D>(@"Sprites/hardON"), _hardModeButtonCollide,
                _ =>
                {
                    HardMode = false;
                    _currentHardButton = _hardModeOffButton;
                    _game.ClearGame();
                });

            _currentHardButton = _hardModeOffButton;
        }

        private void CreateScreenButton()
        {
            _screenButtonCollide = new Rectangle(
                Game1.Graphics.PreferredBackBufferWidth * 3 / 4, Game1.Graphics.PreferredBackBufferHeight * 11 / 32,
                Game1.Graphics.PreferredBackBufferWidth / 10, Game1.Graphics.PreferredBackBufferHeight / 10);

            _fullScreenButton = new Button(Game1.ContentManager.Load<Texture2D>(@"Sprites/fullScreen"), _screenButtonCollide,
                _ =>
                {
                    Game1.Graphics.ToggleFullScreen();
                    Game1.Graphics.ApplyChanges();

                    _currentScreenButton = _windowScreenButton;
                });

            _windowScreenButton = new Button(Game1.ContentManager.Load<Texture2D>(@"Sprites/window"), _screenButtonCollide,
                _ =>
                {
                    Game1.Graphics.ToggleFullScreen();
                    Game1.Graphics.ApplyChanges();

                    _currentScreenButton = _fullScreenButton;
                });

            _currentScreenButton = _windowScreenButton;
        }

        private void CreateScreenResolutionUpdateButtons()
        {
            _screenResolutionTextures = new[]
            {
                Game1.ContentManager.Load<Texture2D>(_srTextureFileLocation + "1280x720"),
                Game1.ContentManager.Load<Texture2D>(_srTextureFileLocation + "1440x900"),
                Game1.ContentManager.Load<Texture2D>(_srTextureFileLocation + "1600x900"),
                Game1.ContentManager.Load<Texture2D>(_srTextureFileLocation + "1920x1080")
            };

            var buttonsSize = new Point(
                Game1.Graphics.PreferredBackBufferWidth / 5,
                Game1.Graphics.PreferredBackBufferHeight / 8);

            var indent = Game1.Graphics.PreferredBackBufferHeight / 40;

            var location = new Point(
                Game1.Graphics.PreferredBackBufferWidth / 8,
                Game1.Graphics.PreferredBackBufferHeight *4/ 7
                    - (indent + buttonsSize.Y) * ((_screenResolutionTextures.Length + 1) / 2));

            _screenResolutionButtons = new List<Button>();

            foreach (var srTetureButton in _screenResolutionTextures)
            {
                var screenResolution = srTetureButton.Name
                    .Remove(0, _srTextureFileLocation.Length)
                    .Split('x')
                    .Select(x => int.Parse(x))
                    .ToArray();

                var newLocation = location;

                _screenResolutionButtons.Add(new Button(
                    srTetureButton,
                    new Rectangle(newLocation, buttonsSize),
                    _ =>
                    {
                        Game1.Graphics.PreferredBackBufferWidth = screenResolution[0];
                        Game1.Graphics.PreferredBackBufferHeight = screenResolution[1];
                        Game1.Graphics.ApplyChanges();

                        UpdateLocationAndSize();
                        _game.ClearGame();
                    }
                    ));

                location.Y += indent + buttonsSize.Y;
            }
        }

        private void UpdateLocationAndSize()
        {
            UpdLocationAndSizeSRButton();
            UpdLocationAndSizeBackground();
            UpdLocationAndSizeHardModeButton();
            UpdLocationAndSizeScreenButton();
            UpdLocationAndSizeBackButton();
        }

        private void UpdLocationAndSizeSRButton()
        {
            var srButtonsSize = new Point(
                Game1.Graphics.PreferredBackBufferWidth / 5,
                Game1.Graphics.PreferredBackBufferHeight / 8);

            var srIndent = Game1.Graphics.PreferredBackBufferHeight / 40;

            var srLocation = new Point(
                Game1.Graphics.PreferredBackBufferWidth / 8,
                Game1.Graphics.PreferredBackBufferHeight * 4 / 7
                    - (srIndent + srButtonsSize.Y) * ((_screenResolutionTextures.Length + 1) / 2));

            for (int i = 0; i < _screenResolutionButtons.Count; i++)
            {
                var newLocation = srLocation;

                _screenResolutionButtons[i].UpdateCollider(new Rectangle(newLocation, srButtonsSize));

                srLocation.Y += srIndent + srButtonsSize.Y;
            }
        }

        private void UpdLocationAndSizeBackground()
        {
            _bgRect = new Rectangle(0, 0, Game1.Graphics.PreferredBackBufferWidth, Game1.Graphics.PreferredBackBufferHeight);
        }

        private void UpdLocationAndSizeHardModeButton()
        {
            _hardModeButtonCollide = new Rectangle(
               Game1.Graphics.PreferredBackBufferWidth * 3 / 4, Game1.Graphics.PreferredBackBufferHeight * 17 / 32,
               Game1.Graphics.PreferredBackBufferWidth / 10, Game1.Graphics.PreferredBackBufferHeight / 10);

            _hardModeOnButton.UpdateCollider(_hardModeButtonCollide);
            _hardModeOffButton.UpdateCollider(_hardModeButtonCollide);
        }

        private void UpdLocationAndSizeScreenButton()
        {
            _screenButtonCollide = new Rectangle(
                Game1.Graphics.PreferredBackBufferWidth * 3 / 4, Game1.Graphics.PreferredBackBufferHeight * 11 / 32,
                Game1.Graphics.PreferredBackBufferWidth / 10, Game1.Graphics.PreferredBackBufferHeight / 10);

            _fullScreenButton.UpdateCollider(_screenButtonCollide);
            _windowScreenButton.UpdateCollider(_screenButtonCollide);
        }

        private void UpdLocationAndSizeBackButton()
        {
            var size = new Point(Game1.Graphics.PreferredBackBufferWidth / 5, Game1.Graphics.PreferredBackBufferHeight / 8);
            var location = new Point(Game1.Graphics.PreferredBackBufferWidth / 2 - size.X / 2, Game1.Graphics.PreferredBackBufferHeight * 3 / 4 - size.Y / 2);

            _backButtonCollider = new Rectangle(location, size);
            _backButton.UpdateCollider(_backButtonCollider);
        }
    }
}