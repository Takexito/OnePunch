using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace JustArc
{
    class SpriteAnimation
    {
        private Texture2D texture;
        private int width;
        private float frameTime;
        private float timeNow;
        private int maxFrameCountX = 3, maxFrameCountY = 1;
        private int frameX = 0, frameY = 0;
        private bool isTwo = false;
        private int height;
        public Vector2 position = Vector2.Zero;
        private Logic logic = new Logic();

        public SpriteAnimation(Texture2D texture, Vector2 position, int width, float frameTime)
        {

            this.texture = texture;
            this.width = width;
            this.frameTime = frameTime;
            this.position = position;
            maxFrameCountX = texture.Width / width;
            height = texture.Height;
            isTwo = false;
            
        }

        public SpriteAnimation(Texture2D texture, Vector2 position, int width, int height, float frameTime)
        {

            this.texture = texture;
            this.width = width;
            this.frameTime = frameTime;
            this.position = position;
            maxFrameCountX = texture.Width / width;
            maxFrameCountY = texture.Height / height;
            isTwo = true;
            this.height = height;
        }

        public void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            timeNow += delta;

            if (timeNow > frameTime)
            {
                int count = (int)(timeNow / frameTime);
                timeNow -= count * frameTime;
                timeNow = 0;
                frameX++;
                //узначь что зза херня процент-равно frameX %= maxFrameCountX;
                if (frameX >= maxFrameCountX)
                {
                    frameX = 0;
                    
                    Logic.hit = false;
                    Game1.crit = false;
                    if (isTwo)
                    {
                        frameY++;
                        frameY %= maxFrameCountY;
                    }
                }
                
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(texture, position, new Rectangle(frameX * width, frameY * height, width, height), Color.White);
        }
    }
}