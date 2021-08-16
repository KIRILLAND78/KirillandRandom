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
            //Projectile.position.Y -= 80;
            Projectile.Name = "Sky Rapier";
            Projectile.width = 58;
            Projectile.height = 10;
            Projectile.timeLeft = 12;
            Projectile.penetrate = 9999;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.alpha = 255;
            DrawOriginOffsetY = -4;

            DrawOriginOffsetX = -4;
            Player owner = Main.player[Projectile.owner];
            
            lastplpos = owner.Center;// + MathHelper.ToRadians(-45f);
        }
        
        public override void AI()
        {
            
            Player owner = Main.player[Projectile.owner];
            if (first)
            {

                Projectile.rotation = Projectile.velocity.ToRotation();
                lastplpos = owner.Center;

                Projectile.alpha = 200;
                first = false;
            }


            Projectile.velocity *= 1.15f;

            Projectile.position += owner.Center - lastplpos;

            lastplpos = owner.Center;


            //if (Projectile.velocity.Y > 16f)
            //{
            //    Projectile.velocity.Y = 16f;
            //}






        }


    }
}
