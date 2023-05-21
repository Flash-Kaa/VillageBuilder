using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace VillageBuilder
{
    public class Camera
    {
        public bool Works { get; set; }

        public Vector2 Position;
        public float Scale;

        private bool _leftPressed;

        private Point _lastPos;
        private float _lastScroll;

        private Vector2 start;
        private Vector2 end;

        private const float MinScale = 0.4f;
        private const float MaxScale = 1f;

        public Camera(Vector2 startPosition, Vector2 endPosition, float startScale, bool works = false)
        {
            Works = works;
            Position = startPosition;
            Scale = startScale;

            start = startPosition;
            end = endPosition;

            _leftPressed = false;

            var mouseState = Mouse.GetState();
            _lastScroll = mouseState.ScrollWheelValue;
            _lastPos = mouseState.Position;
        }

        public void Update(GameTime gameTime)
        {
            if (Works)
                return;

            var mouseState = Mouse.GetState();

            if(mouseState.LeftButton == ButtonState.Pressed && !_leftPressed)
                _leftPressed = true;
            else if (mouseState.LeftButton != ButtonState.Pressed && _leftPressed)
                _leftPressed = false;

            if (_leftPressed && mouseState.Position - _lastPos != new Point(0, 0))
                Position += (_lastPos - mouseState.Position).ToVector2();

            if (_leftPressed)
                _lastPos = mouseState.Position;

            Scale += (mouseState.ScrollWheelValue - _lastScroll )/ 500;

            if (Scale < MinScale)
                Scale = MinScale;
            else if (Scale > MaxScale)
                Scale = MaxScale;

            if (Position.X < start.X)
                Position.X = start.X;
            else if (Position.X > end.X - Game1.Graphics.PreferredBackBufferWidth / Scale)
                Position.X = end.X - Game1.Graphics.PreferredBackBufferWidth / Scale;

            if (Position.Y < start.Y)
                Position.Y = start.Y;
            else if(Position.Y > end.Y - Game1.Graphics.PreferredBackBufferHeight / Scale)
                Position.Y = end.Y - Game1.Graphics.PreferredBackBufferHeight / Scale;

            _lastScroll = mouseState.ScrollWheelValue;
            _lastPos = mouseState.Position;
        }

        public Matrix GetTransformationMatrix()
        {
            return Matrix.CreateTranslation(new Vector3(-Position, 0f)) *
                   Matrix.CreateScale(new Vector3(Scale, Scale, 1f));
        }
    }
}