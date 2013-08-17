using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Jump
{
    /// <summary>
    /// Mappa di gioco
    /// </summary>
    class Map
    {
        /// <summary>
        /// Rappresenta gli elementi della mappa
        /// </summary>
        public List<Elemento> elementi = new List<Elemento>();

        /// <summary>
        /// Map size values
        /// </summary>
        private int width;
        /// <summary>
        /// Map size values - 
        /// </summary>
        private int height;
        /// <summary>
        /// grandezza degli oggetti
        /// </summary>
        public const int obj_size = 64;
        /// <summary>
        /// Indica l'X dell'ultimo rettangolo
        /// </summary>
        public int xFinish;
        /// <summary>
        /// Indica l'Y dell'ultimo rettangolo
        /// </summary>
        public int yFinish;
        /// <summary>
        /// rappresenza la lunghezza massima dell'index 0 della mappa passata nel costruttore
        /// </summary>
        private int maxlen;
        /// <summary>
        /// Copia della mappa originale
        /// </summary>
        private int[,] mappa_originale;

        /// <summary>
        /// Crea la mappa di gioco
        /// </summary>
        /// <param name="mappa">int[,] che rappresenta il contenuto della mappa</param>
        /// <param name="obj_size">la grandezza di ogni rettangolo</param>
        public Map(int[,] mappa)
        {
            InitMap(mappa);
            mappa_originale = mappa;
        }

        public void Clear()
        {
            elementi.Clear();
            InitMap(mappa_originale);
        }

        private void InitMap(int[,] mappa)
        {
            int k = mappa.GetLength(1);
            int y = maxlen = mappa.GetLength(0);

            for (int i = 0; i < k; ++i)
            {
                for (int x = 0; x < y; ++x)
                {
                    int type = mappa[x, i];

                    /// 0 rappresenta un elemento del cielo, quindi non esiste
                    xFinish = x * obj_size;
                    yFinish = i * obj_size;

                    elementi.Add(new Elemento(type, new Rectangle(xFinish, yFinish, obj_size, obj_size)));

                    width = (i + 1) * obj_size;
                    height = (x + 1) * obj_size;
                }
            }
        }

        /// <summary>
        /// Si occupa di disegnare gli elementi a schermo
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Elemento elemento in elementi)
            {
                elemento.Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Update della mappa di gioco, ad ogni aggiornamento si aggiorna un blocco della mappa
        /// </summary>
        public void Update(int X)
        {
            Random rand = new Random();

            for(int i = 0; i < elementi.Count; ++i)
            {
                if (elementi[i].Update(X))
                {
                    elementi.RemoveAt(i);

                    //
                    xFinish += obj_size;

                    int num = rand.Next(3);

                    if (num == 2 || num == 1)
                    {
                        elementi.Add(new Elemento(1, new Rectangle(xFinish, yFinish, obj_size, obj_size)));
                        elementi.Add(new Elemento(2, new Rectangle(xFinish, yFinish - obj_size, obj_size, obj_size)));
                    }

                    if (xFinish >= Int32.MaxValue - obj_size)
                        xFinish = maxlen * obj_size;

                }
            }
        }
    }
}
