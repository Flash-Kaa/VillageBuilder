using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace VillageBuilder
{
    public class Game1 : Game
    {
        public static Dictionary<Scene, IGameMode> Scenes { get; private set; }
        public static GraphicsDeviceManager Graphics { get; private set; }
        public static  SpriteBatch SpriteBatch;
        public static ContentManager ContentManage;

        public static Scene CurrentScene { get; set; } = Scene.Menu;


        private Scene _previous = Scene.Menu;

        public Game1()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            ContentManage = Content;
        }

        protected override void Initialize()
        {
            Scenes = new Dictionary<Scene, IGameMode>
            { [Scene.Menu] = new MenuMode() };
            Scenes[Scene.Menu].Initialize();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (CurrentScene != _previous)
            {
                if (CurrentScene == Scene.Play)
                    Scenes[CurrentScene] = new PlayMode();

                _previous = CurrentScene;
                Scenes[CurrentScene].Initialize();
            }

            Scenes[CurrentScene].Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if(Scenes.ContainsKey(CurrentScene))
                Scenes[CurrentScene].Draw(SpriteBatch);


            base.Draw(gameTime);
        }
    }

    public enum Scene
    {
        Menu,
        Play
    }
}