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

namespace Jump
{
    /// <summary>
    /// Questo è il tipo principale per il gioco
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont punteggio;
        Map mappa;
        Player player;
        public static bool GameOver = false;

        int lastSec = DateTime.Now.Second;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Elemento.content = Content;
        }

        /// <summary>
        /// Consente al gioco di eseguire tutte le operazioni di inizializzazione necessarie prima di iniziare l'esecuzione.
        /// È possibile richiedere qualunque servizio necessario e caricare eventuali
        /// contenuti non grafici correlati. Quando si chiama base.Initialize, tutti i componenti vengono enumerati
        /// e inizializzati.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: aggiungere qui la logica di inizializzazione

            mappa = new Map(

                // this is how the map start
                new int[,]{
                    {0, 0, 0, 0, 0, 0, 2, 1},
                    {0, 0, 0, 0, 0, 0, 2, 1},
                    {0, 0, 0, 0, 0, 0, 2, 1},
                    {0, 0, 0, 0, 0, 0, 2, 1},
                    {0, 0, 0, 0, 0, 0, 2, 1},
                    {0, 0, 0, 0, 0, 0, 2, 1},
                    {0, 0, 0, 0, 0, 0, 2, 1},
                    {0, 0, 0, 0, 0, 0, 2, 1},
                    {0, 0, 0, 0, 0, 0, 2, 1},
                    {0, 0, 0, 0, 0, 0, 2, 1},
                });

            player = new Player();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent verrà chiamato una volta per gioco e costituisce il punto in cui caricare
        /// tutto il contenuto.
        /// </summary>
        protected override void LoadContent()
        {
            // Creare un nuovo SpriteBatch, che potrà essere utilizzato per disegnare trame.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            punteggio = Content.Load<SpriteFont>("Punteggio");

            // TODO: utilizzare this.content per caricare qui il contenuto del gioco
        }

        /// <summary>
        /// UnloadContent verrà chiamato una volta per gioco e costituisce il punto in cui scaricare
        /// tutto il contenuto.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: scaricare qui tutto il contenuto non gestito da ContentManager
        }

        /// <summary>
        /// Consente al gioco di eseguire la logica per, ad esempio, aggiornare il mondo,
        /// controllare l'esistenza di conflitti, raccogliere l'input e riprodurre l'audio.
        /// </summary>
        /// <param name="gameTime">Fornisce uno snapshot dei valori di temporizzazione.</param>
        protected override void Update(GameTime gameTime)
        {
            // Consente di uscire dal gioco
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (GameOver)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Y))
                {
                    GameOver = false;

                    mappa.Clear();
                    player.Reset();

                }
                else if (Keyboard.GetState().IsKeyDown(Keys.X))
                {
                    this.Exit();
                }
            }
            else
            {
                mappa.Update(graphics.GraphicsDevice.Viewport.X);
                player.Update(graphics.GraphicsDevice.Viewport.Y, mappa.yFinish);

                foreach (Elemento re in mappa.elementi)
                    if (re.type > 0)
                        player.Intersect(re.rect);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Viene chiamato quando il gioco deve disegnarsi.
        /// </summary>
        /// <param name="gameTime">Fornisce uno snapshot dei valori di temporizzazione.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (lastSec != DateTime.Now.Second)
            {
                player.Score++;
            }

            lastSec = DateTime.Now.Second;

            GraphicsDevice.Clear(Color.CornflowerBlue);
          
            spriteBatch.Begin();
            player.Draw(spriteBatch);

            if (GameOver)
            {
                spriteBatch.DrawString(punteggio, "GameOver - Premi Y per riniziare, X per uscire", new Vector2(10, 0), Color.White);
            }
            else
            {
                spriteBatch.DrawString(punteggio, "Punteggio: " + player.Score, new Vector2(10, 0), Color.White);
            }

            mappa.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
