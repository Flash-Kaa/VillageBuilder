using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace VillageBuilder
{
    public class Button
    {
        private Rectangle _clickableButtonCollider;
        private Rectangle _drawButtonCollider;
        private Color _color;
        private bool _isPressed;
        
        private readonly Texture2D _texture;
        private readonly Action<object> _click;

        public Button(Texture2D sprite, Rectangle buttonCollider, Action<object> onClick)
        {
            _texture = sprite;
            _clickableButtonCollider = buttonCollider;
            _drawButtonCollider = buttonCollider;
            _click = onClick;

            _color = Color.White;
        }

        public void Update(GameTime gameTime)
        {
            if (EnterButton() && ! _isPressed && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                _color = Color.Gray;
                _isPressed = true;
            }
            else if (_isPressed && Mouse.GetState().LeftButton == ButtonState.Released && EnterButton())
            {
                _click(null);
                _color = Color.White;
                _isPressed = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _drawButtonCollider, _color);
        }

        public bool EnterButton()
            => Mouse.GetState().X <= _clickableButtonCollider.Location.X + _clickableButtonCollider.Width
                && Mouse.GetState().Y <= _clickableButtonCollider.Location.Y + _clickableButtonCollider.Height
                && Mouse.GetState().Y >= _clickableButtonCollider.Location.Y
                && Mouse.GetState().X >= _clickableButtonCollider.Location.X;

        public void UpdateCollider(Rectangle newCollider)
        {
            _drawButtonCollider = newCollider;
            _clickableButtonCollider = newCollider;
        }

        public void UpdateClickableCollider(Rectangle newCollider)
            => _clickableButtonCollider = newCollider;
    }
}