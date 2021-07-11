using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platformer.Event;

namespace Platformer.Objects
{
    public class BaseGameObject
    {
        protected Texture2D _texture;

        private Vector2 position;

        public int zIndex;

        public virtual void OnNotify(Events eventType) 
        { 
        
        }

        public void Render(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(_texture, Vector2.One, Color.White);
        }
    }
}