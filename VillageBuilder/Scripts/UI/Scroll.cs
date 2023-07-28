using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace VillageBuilder
{
    public class Scroll
    {
        public Vector2 Position { get; private set; }

        private bool _leftPressed;
        private Point _lastPos;

        private readonly Vector2 _start;
        private readonly Vector2 _end;
        private readonly Rectangle _tapArea;

        public Scroll(Rectangle tapArea, Vector2 startPosition, Vector2 endPosition)
        {
            Position = startPosition;
            _start = startPosition;
            _end = endPosition;
            _tapArea = tapArea;

            _leftPressed = false;
            _lastPos = Mouse.GetState().Position;
        }

        public void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();

            if(_tapArea.Contains(mouseState.Position) &&  mouseState.LeftButton == ButtonState.Pressed && !_leftPressed)
                _leftPressed = true;
            else if (mouseState.LeftButton != ButtonState.Pressed && _leftPressed)
                _leftPressed = false;

            if (_leftPressed)
            {
                Position += (_lastPos - mouseState.Position).ToVector2();

                if (Position.X < _start.X)
                    Position = new Vector2(_start.X, Position.Y);
                else if (Position.X > _end.X)
                    Position = new Vector2(_end.X, Position.Y);

                if (Position.Y < _start.Y)
                    Position = new Vector2(Position.X, _start.Y);
                else if (Position.Y > _end.Y)
                    Position = new Vector2(Position.X, _end.Y);
            }
            
            _lastPos = mouseState.Position;
        }

        public Matrix GetTransformationMatrix()
            => Matrix.CreateTranslation(new Vector3(-Position, 0f));

        public bool HaveTap()
            => _leftPressed;
    }
}