﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TTengine.Core;
using TTengine.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pixie1
{
    /// <summary>
    /// the level bitmap that scrolls around the player's view
    /// </summary>
    public class LevelBackground: Thing
    {
        /// <summary>
        /// background color is the non-passable color of the level borders and also the color
        /// of any transparent parts in the level bitmap.
        /// </summary>
        public Color BackgroundColor = Color.Black;

        /// <summary>
        /// foreground color is the default passable color of the level where Pixie walks, i.e.
        /// the most common color she walks on.
        /// </summary>
        public Color ForegroundColor = Color.White;

        SpriteBatch spriteBatch;

        public LevelBackground(string bitmapFileName)
            : base(bitmapFileName)
        {
            spriteBatch = new SpriteBatch(Screen.graphicsDevice);
        }
        
        public Color SamplePixel(Vector2 pos)
        {
            if (pos.X < 0f || pos.X > (Texture.Width - 1) ||
                pos.Y < 0f || pos.Y > (Texture.Height - 1))
            {
                return BackgroundColor;
            }
            int x = (int)Math.Round(pos.X);
            int y = (int)Math.Round(pos.Y);
            return textureData[x + y * Texture.Width];
        }

        public void SetPixel(Vector2 pos, Color color)
        {
            if (pos.X < 0f || pos.X > (Texture.Width - 1) ||
                pos.Y < 0f || pos.Y > (Texture.Height - 1))
            {
                return;
            }
            Color[] data = new Color[1];
            data[0] = color;
            int x = (int)Math.Round(pos.X);
            int y = (int)Math.Round(pos.Y);
            textureData[x + y * Texture.Width] = color;
            Texture.SetData<Color>(0, new Rectangle(x, y, 1, 1), data, 0, 1);
        }

        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);

            // update my on-screen position (shifting the big bitmap around to match middle point set)
            Motion.Position = Screen.Center - Motion.ScaleAbs * FromPixels(Position);            
        }

        protected override void OnDraw(ref DrawParams p)
        {
            if (Texture != null)
            {
                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);

                spriteBatch.Draw(Texture, DrawInfo.DrawPosition, null, DrawInfo.DrawColor,
                       Motion.RotateAbs, Vector2.Zero, DrawInfo.DrawScale, SpriteEffects.None, DrawInfo.LayerDepth);

                spriteBatch.End();
            }
            
        }

    }
}
