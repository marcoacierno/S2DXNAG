using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Jump
{
    class Elemento
    {
        private Texture2D texture;
        public Rectangle rect;
        public static ContentManager content;
        public int type;

        public Elemento(int type, Rectangle newrect)
        {
            this.type = type;

            if (type > 0)
                texture = content.Load<Texture2D>("tile" + type);

            rect = newrect;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (type > 0)
                spriteBatch.Draw(texture, rect, Color.White);
        }

        public bool Update(int X)
        {
            rect.X -= 3;

            return (rect.X+Map.obj_size <= X);
        }
    }
}
