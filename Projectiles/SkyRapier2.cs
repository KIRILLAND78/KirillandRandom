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
            projectile.Name = "Flashin' Speed";
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
            projectile.alpha = 200;
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
                first = false;
            }


            projectile.velocity *= 1.15f;

            projectile.position += owner.Center - lastplpos;

            lastplpos = owner.Center;


            //if (projectile.velocity.Y > 16f)
            //{
            //    projectile.velocity.Y = 16f;
            //}
            // Since our sprite has an orientation, we need to adjust rotation to compensate for the draw flipping.






        }


    }
}
