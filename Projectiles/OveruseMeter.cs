using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;



namespace KirillandRandom.Projectiles
{
    public class OveruseMeter : ModProjectile
    {
        public Item Sword;
        public bool first = true;
        public override void SetDefaults()
        {
            projectile.Name = "OVMETER(how? just how?)";
            projectile.width = 12;
            projectile.height = 12;
            projectile.timeLeft = 720000;
            projectile.penetrate = 1;
            projectile.friendly = false;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.ranged = true;
            projectile.aiStyle = 0;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            Texture2D texture = Main.projectileTexture[projectile.type];
            int frameHeight = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int startY = frameHeight * projectile.frame;
            Rectangle sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);
            Vector2 origin = sourceRectangle.Size() / 2f;
            Color drawColor = new Color(255, 255, 255);
            Main.spriteBatch.Draw(texture,
            projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY),
            sourceRectangle, drawColor, projectile.rotation, origin, projectile.scale, spriteEffects, 0f);
            return false;
        }
        public override void AI()
        {
            Player owner = Main.player[projectile.owner];
            projectile.position.X = owner.Center.X-7; //- 41;

            projectile.position.Y = owner.Center.Y+42;


            if (first)
            {
                Sword = owner.HeldItem;
                first = false;
            }

            if (owner.HeldItem != Sword)
            {
                projectile.Kill();
            }



        }
    }
}

