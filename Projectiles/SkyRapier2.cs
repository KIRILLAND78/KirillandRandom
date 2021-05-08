using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;



namespace KirillandRandom.Projectiles
{
    public class SkyRapier2 : ModProjectile
    {
        public Vector2 lastplpos;
        bool first = true;
        public override void SetDefaults()
        {
            //projectile.position.Y -= 80;
            projectile.Name = "Sky Rapier";
            projectile.width = 58;
            projectile.height = 10;
            projectile.timeLeft = 12;
            projectile.penetrate = 9999;
            projectile.friendly = false;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.ranged = false;
            projectile.melee = true;
            projectile.alpha = 255;
            drawOriginOffsetY = -4;

            drawOriginOffsetX = -4;
            Player owner = Main.player[projectile.owner];
            
            lastplpos = owner.Center;// + MathHelper.ToRadians(-45f);
        }
        
        public override void AI()
        {
            Player owner = Main.player[projectile.owner];
            if (first)
            {

                projectile.rotation = projectile.velocity.ToRotation();
                lastplpos = owner.Center;

                projectile.alpha = 200;
                first = false;
            }


            projectile.velocity *= 1.15f;

            projectile.position += owner.Center - lastplpos;

            lastplpos = owner.Center;


            //if (projectile.velocity.Y > 16f)
            //{
            //    projectile.velocity.Y = 16f;
            //}






        }


    }
}
