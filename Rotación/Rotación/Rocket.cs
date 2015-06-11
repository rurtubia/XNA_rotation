using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rotación
{
    class Rocket
    {
        public Texture2D texturaRocket;
        public Vector2 posicionRocket, velocidadRocket, origenRocket;
        public bool isVisible;

        public Rocket(Texture2D nuevaTextura)
        {
            texturaRocket = nuevaTextura;
            isVisible = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texturaRocket, posicionRocket, null,
                Color.White, 0f, origenRocket, 1f, SpriteEffects.None, 0);
        }
    }
}
