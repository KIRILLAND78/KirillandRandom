using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace KirillandRandom
{
    class ItemDrawPlayer : ModPlayer
    {
        public int timer;
        public bool flyingItemDraw;
        public Asset<Texture2D> flyingItemAsset;
        public Item itemLookForForDrawing;
        public override void ResetEffects()
        {
            if (Player.HeldItem != itemLookForForDrawing)
            {
                flyingItemDraw = false;
                flyingItemAsset = null;
            }
        }

        public override void Initialize()
        {
            flyingItemDraw = false;
            flyingItemAsset = null;
            base.Initialize();
        }
        public override void PostUpdate()
        {
            timer++;
            base.PostUpdate();
        }

        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (drawInfo.shadow == 0f)
            {
                if ((flyingItemDraw) && (flyingItemAsset != null))
                {
                    Vector2 pos = new Vector2(
                        Player.Center.X + ((Player.direction == 1) ? -45f : 15f),
                        (float)(Player.Center.Y - 40f + 10 * Math.Sin(timer * Math.PI / 128f))
                        );
                    Main.EntitySpriteDraw(flyingItemAsset.Value, pos - Main.screenPosition, null, Color.White, 0, Vector2.Zero, 1, (Player.direction != 1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
                }
            }
            base.DrawEffects(drawInfo, ref r, ref g, ref b, ref a, ref fullBright);
        }
    }

}

