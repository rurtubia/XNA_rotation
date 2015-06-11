using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Rotación
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D Textura;
        Rectangle Rectangulo;

        Vector2 Origen;
        Vector2 Posicion;
        Vector2 Posicion2;

        float rotacion;

        Vector2 velocidad;
        const float velocidadTangencial = 5f;
        float friccion = 0.1f;

        List<Rocket> rockets = new List<Rocket>();

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize() {
            base.Initialize();
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Textura = Content.Load<Texture2D>("nave");
            Posicion = new Vector2(250,200);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            Rectangulo = new Rectangle((int)Posicion.X, (int)Posicion.Y, 
                                        Textura.Width, Textura.Height);
            Posicion = velocidad + Posicion;
            
            Posicion2 = Posicion * 2;

            Origen = new Vector2(Rectangulo.Width/2, Rectangulo.Height/2);

            if (Keyboard.GetState().IsKeyDown(Keys.Right)) rotacion += 0.2f;
            if (Keyboard.GetState().IsKeyDown(Keys.Left)) rotacion -= 0.2f;
            if (Keyboard.GetState().IsKeyDown(Keys.Up)) {
                velocidad.X = (float)Math.Cos(rotacion)*velocidadTangencial;
                //coseno del ángulo = velocidad tangencial
                velocidad.Y = (float)Math.Sin(rotacion)*velocidadTangencial;
            }
            else if (velocidad != Vector2.Zero) {
                float i = velocidad.X;
                float j = velocidad.Y;

                velocidad.X = i -= friccion * i;
                velocidad.Y = j -= friccion * j;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                Disparo();
                UpdateRockets();
            
            base.Update(gameTime);
        }

        public void UpdateRockets() 
        {
            foreach (Rocket rocket in rockets)
            {
                rocket.posicionRocket += rocket.velocidadRocket;
                if (Vector2.Distance(rocket.posicionRocket, Posicion) > 700)
                {
                    rocket.isVisible = false;
                }
            }
            for (int i = 0; i < rockets.Count; i++)
            {
                if (!rockets[i].isVisible)
                {
                    rockets.RemoveAt(i);
                    i--;
                }
            }
        }

        public void Disparo()
        {
            Rocket nuevoRocket = new Rocket(Content.Load<Texture2D>("bala"));
            nuevoRocket.velocidadRocket =
                new Vector2((float)Math.Cos(rotacion), (float)Math.Sin(rotacion)) * 5f + velocidad;
            nuevoRocket.posicionRocket = Posicion + nuevoRocket.velocidadRocket;
            nuevoRocket.isVisible = true;

            if (rockets.Count < 10)
            {
                rockets.Add(nuevoRocket);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(Textura, Posicion, null, 
                                Color.White, rotacion, Origen, 
                                1f, SpriteEffects.None, 0);
            foreach (Rocket rocket in rockets)
                rocket.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
