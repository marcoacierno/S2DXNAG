using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Jump
{
    class Player
    {
        private Texture2D texture;
        private Rectangle rect;

        private Vector2 position = new Vector2(10, 0);
        private Vector2 velocity = new Vector2();

        private int times;
        public int Score = 0;

        public Player()
        {
            // since i have content element inside Elemento class..
            texture = Elemento.content.Load<Texture2D>("player");
        }

        public void Reset()
        {
            position = new Vector2(10, 0);
            velocity = new Vector2();

            Score = 0;
        }

        public void Draw(SpriteBatch sprite)
        {
            sprite.Draw(texture, rect, Color.White);
        }

        public void Update(int Y, int Yfinish)
        {
            if (position.Y < Y)
            {
                velocity.Y += 1.0f;
                //position.Y = 0;
            }
            else if (position.Y > Yfinish)
            {
                // game over
                Game1.GameOver = true;
            }

            position += velocity;

            if (velocity.Y < 10)
                velocity.Y += 0.4f;

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (times < 2)
                {
                    position.Y -= 5f;
                    velocity.Y -= 9f;

                    times++;
                }
            }

            rect = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void Intersect(Rectangle r1)
        {
            if (rect.IsTouchTop(r1))
            {
                velocity.Y = 0;
                times = 0;
            }
        }
    }

    // by oyyou91
    static class ColRectangle
    {
        // i have used a class because mb later i need more methods
        public static bool IsTouchTop(this Rectangle r1, Rectangle r2)
        {
            return (r1.Bottom >= r2.Top &&
                r1.Bottom <= r2.Top + (r2.Height / 2) &&
                r1.Right >= r2.Left + (r2.Width / 5) &&
                r1.Left <= r2.Right - (r2.Width / 5));
        }
    }
}
