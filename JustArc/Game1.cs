using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

namespace JustArc
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
        const int SCREEN_WIDTH = 1280, SCREEN_HEIGHT = 720;
        
        int hp, dmg, score;
        float timeLeft;
        int index = 1;
        bool isDie, isNew, gameover, isMenu;
            public static bool crit;
        string word1, word2, word3, answer, result;

        GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
        Logic logic;
        Point playerSprFrame;
        Point playerSprSize;
        Point hpSprFrame;
        Texture2D player, hpBar, enemy, background, background1, lose, win, playerCrit;
        SpriteFont font;
        SpriteAnimation playerAnimation, enemyAnimation, playerHpAnimation, enemyHpAnimation, playerCritAnimation;
        Keys[] pressedkeys;
        Keys lastkey;

        public Game1 ()
		{
			graphics = new GraphicsDeviceManager (this);
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            Content.RootDirectory = "Content";
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize ()
		{
			// TODO: Add your initialization logic here
            
			logic = new Logic ();
            font = Content.Load<SpriteFont>("Font");
            word1 = logic.showWords(index);
            word2 = logic.showWords(index);
            word3 = logic.showWords(index);
            answer = "";
            hp = 100;
            hpSprFrame = new Point(0, 9);
            result = "";
            dmg = 2;
            crit = false;
            isNew = false;
            score = 0;
            timeLeft = 30;
            isMenu = true;

            base.Initialize ();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent ()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch (GraphicsDevice);
            player = Content.Load<Texture2D>("PlayerSheet");
            enemy = Content.Load<Texture2D>("Germman");
            hpBar = Content.Load<Texture2D>("hp");
            background1 = Content.Load<Texture2D>("Ring_1 (1)");
            background = Content.Load<Texture2D>("Ring1");
            win = Content.Load<Texture2D>("Win");
            lose = Content.Load<Texture2D>("Lose");
            playerCrit = Content.Load<Texture2D>("PowerHit");

            playerCritAnimation = new SpriteAnimation(playerCrit, new Vector2(400, 250), (playerCrit.Width / 4 )+5, 200);
            playerAnimation = new SpriteAnimation(player, new Vector2(400, 250), player.Width / 4, 35);
            enemyAnimation = new SpriteAnimation(enemy, new Vector2(680, 250), enemy.Width / 2, 100);
            playerHpAnimation = new SpriteAnimation(hpBar, new Vector2(280, 200), hpBar.Width, 100);
            enemyHpAnimation = new SpriteAnimation(hpBar, new Vector2(1000, 200), hpBar.Width, hpBar.Height / 10, 100);
            //TODO: use this.Content to load your game content here 
        }

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update (GameTime gameTime)
		{
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			// Exit() is obsolete on iOS
			#if !__IOS__ &&  !__TVOS__
			if (GamePad.GetState (PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState ().IsKeyDown (Keys.Escape))
				Exit ();
#endif
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timeLeft -= delta;
            if (crit) playerCritAnimation.Update(gameTime);
            if (Logic.hit) {
                playerAnimation.Update(gameTime);
                enemyAnimation.Update(gameTime);
            }

            if (hp < 2) isDie = true;
            CompareStr();
            HpChange(hp);

            if (timeLeft < 0) gameover = true;
            // TODO: Add your update logic here

                base.Update (gameTime);
		}

		
		protected override void Draw (GameTime gameTime)
		{
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();


            // playerSprFrame = logic.SpriteChange(gameTime,playerSprFrame, playerSprSize, 400);
            spriteBatch.Draw(background, new Vector2(120, 360), new Rectangle(0, 0, background.Width, background.Height),Color.White);
            // playerHpAnimation.Draw(gameTime, spriteBatch);
            enemyAnimation.Draw(gameTime, spriteBatch);
            if (crit)
            {
                spriteBatch.DrawString(font, "Crit: "+dmg.ToString(), new Vector2(750, 200), Color.Yellow);
                playerCritAnimation.Draw(gameTime, spriteBatch);
            }
            else playerAnimation.Draw(gameTime, spriteBatch);
            // enemyHpAnimation.Draw(gameTime, spriteBatch);  
            spriteBatch.Draw(background1, new Vector2(120, 366), new Rectangle(0, 0, background1.Width, background1.Height),Color.White);
  
            spriteBatch.DrawString(font, word1, new Vector2(600, 50), Color.White);
            spriteBatch.DrawString(font, word2, new Vector2(600, 75), Color.White);
            spriteBatch.DrawString(font, word3, new Vector2(600, 100), Color.White);
            spriteBatch.DrawString(font, answer, new Vector2(600, 150), Color.Red);
            spriteBatch.DrawString(font, "Hp: "+hp.ToString(), new Vector2(700, 200), Color.Red);
            spriteBatch.DrawString(font, "Score: " + score.ToString(), new Vector2(100, 100), Color.Black);
            if (!gameover) spriteBatch.DrawString(font, timeLeft.ToString(), new Vector2(600, 20), Color.Red);

           
            if (isDie)
            {
                score += 100;
                spriteBatch.Draw(win, new Vector2(400, 150), new Rectangle(0, 0, win.Width, win.Height), Color.White);
            }

            if (gameover) spriteBatch.Draw(lose, new Vector2(400, 150), new Rectangle(0, 0, lose.Width, lose.Height), Color.White);
            spriteBatch.End();
			//TODO: Add your drawing code here
            
			base.Draw (gameTime);
		}

        public void CompareStr()
        {
            pressedkeys = Keyboard.GetState().GetPressedKeys();
            if (pressedkeys.Length > 0 && !lastkey.Equals(pressedkeys[0]))
            {
                
                lastkey = pressedkeys[0];
                answer += pressedkeys[0].ToString();
                // if (isNew) { isNew = false; answer = ""; }
                if (pressedkeys[0].Equals(Keys.Space))
                {
                    answer = answer.Remove(answer.Length - 5, 5);
                    if (answer.Equals(word1) || answer.Equals(word2) || answer.Equals(word3))
                    {
                        dmg = 2;
                        if (answer.Equals(word1)) { result += word1 + "."; }
                        else if (answer.Equals(word2)) { result += word2 + ".";}
                            else if (answer.Equals(word3)) { result += word3 + ".";}
                        if (index > 2)
                        {
                            switch (result)
                            {
                                case "HIT.HIS.DOG.": dmg = (int)(1.5 * 2) * 2; crit = true; result = ""; score += 1; break;
                                case "HIT.HIS.CAT.": dmg = (int)(1.5 * 2) * 2; crit = true; result = ""; score += 1; break;
                                case "HIT.HIS.MOM.": dmg = (int)(1.5 * 2) * 2; crit = true; result = ""; score += 1; break;
                                case "HIT.HIS.DAD.": dmg = (int)(1.5 * 2) * 2; crit = true; result = ""; score += 1; break;
                                case "HIT.HERE.DOG.": dmg = (int)(1.5 * 2) * 2; crit = true; result = ""; score += 1; break;
                                case "HIT.HERE.CAT.": dmg = (int)(1.5 * 2) * 2; crit = true; result = ""; score += 1; break;
                                case "HIT.HERE.MOM.": dmg = (int)(1.5 * 2) * 2; crit = true; result = ""; score += 1; break;
                                case "HIT.HERE.DAD.": dmg = (int)(1.5 * 2) * 2; crit = true; result = ""; score += 1; break;
                                case "HIT.YOUR.DOG.": dmg = (int)(1.5 * 2) * 3; crit = true; result = ""; score += 2; break;
                                case "HIT.YOUR.CAT.": dmg = (int)(1.5 * 2) * 3; crit = true; result = ""; score += 2; break;
                                case "HIT.YOUR.MOM.": dmg = (int)(1.5 * 2) * 3; crit = true; result = ""; score += 2; break;
                                case "HIT.YOUR.DAD.": dmg = (int)(1.5 * 2) * 3; crit = true; result = ""; score += 2; break;
                                case "HIT.THEIR.DOG.": dmg = (int)(1.5 * 2) * 4; crit = true; result = ""; score += 3; break;
                                case "HIT.THEIR.CAT.": dmg = (int)(1.5 * 2) * 4; crit = true; result = ""; score += 3; break;
                                case "HIT.THEIR.MOM.": dmg = (int)(1.5 * 2) * 4; crit = true; result = ""; score += 3; break;
                                case "HIT.THEIR.DAD.": dmg = (int)(1.5 * 2) * 4; crit = true; result = ""; score += 3; break;
                                case "BANG.HIS.DOG.": dmg = 2 * 2 * 2; crit = true; result = ""; score += 4; break;
                                case "BANG.HIS.CAT.": dmg = 2 * 2 * 2; crit = true; result = ""; score += 4; break;
                                case "BANG.HIS.MOM.": dmg = 2 * 2 * 2; crit = true; result = ""; score += 4; break;
                                case "BANG.HIS.DAD.": dmg = 2 * 2 * 2; crit = true; result = ""; score += 4; break;
                                case "BANG.HERE.DOG.": dmg = 2 * 3 * 2; crit = true; result = ""; score += 5; break;
                                case "BANG.HERE.CAT.": dmg = 2 * 3 * 2; crit = true; result = ""; score += 5; break;
                                case "BANG.HERE.MOM.": dmg = 2 * 3 * 2; crit = true; result = ""; score += 5; break;
                                case "BANG.HERE.DAD.": dmg = 2 * 3 * 2; crit = true; result = ""; score += 5; break;
                                case "BANG.YOUR.DOG.": dmg = 2 * 3 * 2; crit = true; result = ""; score += 5; break;
                                case "BANG.YOUR.CAT.": dmg = 2 * 3 * 2; crit = true; result = ""; score += 5; break;
                                case "BANG.YOUR.MOM.": dmg = 2 * 3 * 2; crit = true; result = ""; score += 5; break;
                                case "BANG.YOUR.DAD.": dmg = 2 * 3 * 2; crit = true; result = ""; score += 5; break;
                                case "BANG.THEIR.DOG.": dmg = 2 * 4 * 2; crit = true; result = ""; score += 6; break;
                                case "BANG.THEIR.CAT.": dmg = 2 * 4 * 2; crit = true; result = ""; score += 6; break;
                                case "BANG.THEIR.MOM.": dmg = 2 * 4 * 2; crit = true; result = ""; score += 6; break;
                                case "BANG.THEIR.DAD.": dmg = 2 * 4 * 2; crit = true; result = ""; score += 6; break;
                                case "TOUCH.HIS.DOG.": dmg = 3 * 2 * 2; crit = true; result = ""; score += 7; break;
                                case "TOUCH.HIS.CAT.": dmg = 3 * 2 * 2; crit = true; result = ""; score += 7; break;
                                case "TOUCH.HIS.MOM.": dmg = 3 * 2 * 2; crit = true; result = ""; score += 7; break;
                                case "TOUCH.HIS.DAD.": dmg = 3 * 2 * 2; crit = true; result = ""; score += 7; break;
                                case "TOUCH.HERE.DOG.": dmg = 3 * 3 * 2; crit = true; result = ""; score += 8; break;
                                case "TOUCH.HERE.CAT.": dmg = 3 * 3 * 2; crit = true; result = ""; score += 8; break;
                                case "TOUCH.HERE.MOM.": dmg = 3 * 3 * 2; crit = true; result = ""; score += 8; break;
                                case "TOUCH.HERE.DAD.": dmg = 3 * 3 * 2; crit = true; result = ""; score += 8; break;
                                case "TOUCH.YOUR.DOG.": dmg = 3 * 3 * 2; crit = true; result = ""; score += 8; break;
                                case "TOUCH.YOUR.CAT.": dmg = 3 * 3 * 2; crit = true; result = ""; score += 8; break;
                                case "TOUCH.YOUR.MOM.": dmg = 3 * 3 * 2; crit = true; result = ""; score += 8; break;
                                case "TOUCH.YOUR.DAD.": dmg = 3 * 3 * 2; crit = true; result = ""; score += 8; break;
                                case "TOUCH.THEIR.DOG.": dmg = 3 * 4 * 2; crit = true; result = ""; score += 9; break;
                                case "TOUCH.THEIR.CAT.": dmg = 3 * 4 * 2; crit = true; result = ""; score += 9; break;
                                case "TOUCH.THEIR.MOM.": dmg = 3 * 4 * 2; crit = true; result = ""; score += 9; break;
                                case "TOUCH.THEIR.DAD.": dmg = 3 * 4 * 2; crit = true; result = ""; score += 9; break;
                                case "BLOWUP.HIS.DOG.": dmg = 4 * 2 * 2; crit = true; result = ""; score += 10; break;
                                case "BLOWUP.HIS.CAT.": dmg = 4 * 2 * 2; crit = true; result = ""; score += 10; break;
                                case "BLOWUP.HIS.MOM.": dmg = 4 * 2 * 2; crit = true; result = ""; score += 10; break;
                                case "BLOWUP.HIS.DAD.": dmg = 4 * 2 * 2; crit = true; result = ""; score += 10; break;
                                case "BLOWUP.HERE.DOG.": dmg = 4 * 3 * 2; crit = true; result = ""; score += 10; break;
                                case "BLOWUP.HERE.CAT.": dmg = 4 * 3 * 2; crit = true; result = ""; score += 10; break;
                                case "BLOWUP.HERE.MOM.": dmg = 4 * 3 * 2; crit = true; result = ""; score += 10; break;
                                case "BLOWUP.HERE.DAD.": dmg = 4 * 3 * 2; crit = true; result = ""; score += 10; break;
                                case "BLOWUP.YOUR.DOG.": dmg = 4 * 3 * 2; crit = true; result = ""; score += 10; break;
                                case "BLOWUP.YOUR.CAT.": dmg = 4 * 3 * 2; crit = true; result = ""; score += 10; break;
                                case "BLOWUP.YOUR.MOM.": dmg = 4 * 3 * 2; crit = true; result = ""; score += 10; break;
                                case "BLOWUP.YOUR.DAD.": dmg = 4 * 3 * 2; crit = true; result = ""; score += 10; break;
                                case "BLOWUP.THEIR.DOG.": dmg = 4 * 4 * 2; crit = true; result = ""; score += 11; break;
                                case "BLOWUP.THEIR.CAT.": dmg = 4 * 4 * 2; crit = true; result = ""; score += 11; break;
                                case "BLOWUP.THEIR.MOM.": dmg = 4 * 4 * 2; crit = true; result = ""; score += 11; break;
                                case "BLOWUP.THEIR.DAD.": dmg = 4 * 4 * 2; crit = true; result = ""; score += 11; break;
                            }
                            index = 0;
                        }

                        index++;
                        word1 = logic.showWords(index);
                        word2 = logic.showWords(index);
                        word3 = logic.showWords(index);
                        hp -= dmg;
                        Logic.hit = true;
                        answer = "";
                        lastkey = Keys.Back;
                        isNew = true;
                    }
                    answer = "";
                }
                if (pressedkeys[0].Equals(Keys.Back)) answer = "";
            }
            
        }
        public void HpChange(int hp)
        {
            switch (hp)
            {
                case 10: hpSprFrame.Y = 0; break;
                case 9: hpSprFrame.Y = 1; break;
                case 8: hpSprFrame.Y = 2; break;
                case 7: hpSprFrame.Y = 3; break;
                case 6: hpSprFrame.Y = 4; break;
                case 5: hpSprFrame.Y = 5; break;
                case 4: hpSprFrame.Y = 6; break;
                case 3: hpSprFrame.Y = 7; break;
                case 2: hpSprFrame.Y = 8; break;
                case 1: hpSprFrame.Y = 9; break;
                case 0: hpSprFrame.Y = 10; break;
            }
        }

    }
}