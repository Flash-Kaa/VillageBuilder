using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;

namespace VillageBuilder
{
    public class MenuMode : IGameMode
    {
        private Button _startButton;
        private List<Button> _screenResolutionButtons = new List<Button>();

        private Texture2D _startButtonTexture;
        private Texture2D[] _screenResolutionTextures;

        private string _srTextureFileLocation = "Sprites/screen resolution/srbutton";

        public MenuMode()
        {
        }

        public void UpdateLocationAndSize()
        {
            #region[Screen Resolution Buttons]
            var srButtonsSize = new Point(
                Game1.Graphics.PreferredBackBufferWidth / 5,
                Game1.Graphics.PreferredBackBufferHeight / 10);

            var srIndent = Game1.Graphics.PreferredBackBufferHeight / 40;

            var srLocation = new Point(
                Game1.Graphics.PreferredBackBufferWidth / 20,
                Game1.Graphics.PreferredBackBufferHeight / 2 - (srIndent + srButtonsSize.Y) * _screenResolutionTextures.Length / 2);

            for (int i = 0; i < _screenResolutionButtons.Count; i++)
            {
                var newLocation = srLocation;

                _screenResolutionButtons[i].ButtonCollider = new Rectangle(newLocation, srButtonsSize);

                srLocation.Y += srIndent + srButtonsSize.Y;
            }
            #endregion

            #region[Start Button]
            CreateStartButton();
            #endregion
        }

        public void Update(GameTime gameTime)
        {
            _startButton.Update(gameTime);
            _screenResolutionButtons.ForEach(x => x.Update(gameTime));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Game1.Graphics.GraphicsDevice.Clear(Color.DarkGreen);
            spriteBatch.Begin();

            _startButton.Draw(spriteBatch);
            _screenResolutionButtons.ForEach(x => x.Draw(spriteBatch));

            spriteBatch.End();
        }

        private void CreateStartButton()
        {
            var sizeStartButtonRect = new Point(
                    Game1.Graphics.PreferredBackBufferWidth / 3,
                    Game1.Graphics.PreferredBackBufferHeight / 5);

            _startButton = new Button(
                    _startButtonTexture,
                    new Rectangle(
                        location: new Point(
                            Game1.Graphics.PreferredBackBufferWidth / 2 - sizeStartButtonRect.X / 2,
                            Game1.Graphics.PreferredBackBufferHeight / 2 - sizeStartButtonRect.Y / 2),
                        size: sizeStartButtonRect),
                    _ => Game1.CurrentScene = Scene.Play);
        }

        private void CreateScreenResolutionUpdateButtons()
        {
            var buttonsSize = new Point(
                Game1.Graphics.PreferredBackBufferWidth / 5,
                Game1.Graphics.PreferredBackBufferHeight / 10);

            var indent = Game1.Graphics.PreferredBackBufferHeight / 40;

            var location = new Point(
                Game1.Graphics.PreferredBackBufferWidth / 20,
                Game1.Graphics.PreferredBackBufferHeight / 2 - (indent + buttonsSize.Y) * _screenResolutionTextures.Length / 2);

            foreach(var srTetureButton in _screenResolutionTextures)
            {
                var screenResolution = srTetureButton.Name
                    .Remove(0, _srTextureFileLocation.Length)
                    .Split('x')
                    .Select(x => int.Parse(x))
                    .ToArray();

                var newLocation = location;

                _screenResolutionButtons.Add( new Button(
                    srTetureButton,
                    new Rectangle(newLocation, buttonsSize),
                    _ =>
                    {
                        Game1.Graphics.IsFullScreen = false;
                        Game1.Graphics.PreferredBackBufferWidth = screenResolution[0];
                        Game1.Graphics.PreferredBackBufferHeight = screenResolution[1];
                        Game1.Graphics.IsFullScreen = true;
                        Game1.Graphics.ApplyChanges();

                        UpdateLocationAndSize();
                    }
                    ));

                location.Y += indent + buttonsSize.Y;
            }
        }

        public void Initialize()
        {
            _startButtonTexture = Game1.ContentManage.Load<Texture2D>(@"Sprites/MyPixelButton");

            _screenResolutionTextures = new[]
            {
                Game1.ContentManage.Load<Texture2D>(_srTextureFileLocation + "1280x720"),
                Game1.ContentManage.Load<Texture2D>(_srTextureFileLocation + "1440x900"),
                Game1.ContentManage.Load<Texture2D>(_srTextureFileLocation + "1600x900"),
                Game1.ContentManage.Load<Texture2D>(_srTextureFileLocation + "1920x1080")
            };

            CreateStartButton();
            CreateScreenResolutionUpdateButtons();
        }
    }
}
