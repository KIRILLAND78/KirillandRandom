using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;



namespace KirillandRandom.Projectiles
{
    public class SkyRapier : ModProjectile
    {
        public Vector2 lastplpos;
        bool first = true;
        public override void SetDefaults()
        {
            //projectile.position.Y -= 80;
            projectile.Name = "Flashin' Speed";
            projectile.width = 60;
            projectile.height = 60;
            projectile.timeLeft = 12;
            projectile.penetrate = 9999;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.ranged = false;
            projectile.melee = true;
        }
        
       public override void AI()
       {

            Random rnd = new Random();
            Player owner = Main.player[projectile.owner];

            projectile.velocity *= 1.2f;

            if (first){

                Vector2 MousePos = new Vector2(Main.MouseWorld.X, Main.MouseWorld.Y);
                Vector2 PlayerPos = owner.Center;
                //float angle;

                Vector2 AimFor = 150 * Vector2.Normalize(projectile.velocity)+ PlayerPos;
                Vector2 Diff = MousePos - PlayerPos;

                Vector2 DiffRand = Diff.RotatedByRandom(MathHelper.ToRadians(45));
                
                projectile.Center += (32 * Vector2.Normalize(DiffRand));

                Vector2 Diff2 = AimFor - projectile.Center;

                //if (Diff2.X >= 0)
                //{
                //    angle = (float)Math.Atan(Diff2.Y / Diff2.X);
                //}
                //else
                //{
                //    angle = (float)Math.Atan(Diff2.Y / Diff2.X);

                //}
                //projectile.velocity;

                projectile.velocity.X = Diff2.X;
                projectile.velocity.Y = Diff2.Y;
                projectile.velocity.Normalize();
                projectile.velocity *= 3f;

                projectile.light = 0.4f;
                lastplpos = owner.Center;
                projectile.rotation = projectile.velocity.ToRotation() +MathHelper.ToRadians(-45f);
                first = false;
            }
            projectile.position += owner.Center - lastplpos;

            lastplpos = owner.Center;

            int DDustID = Dust.NewDust(projectile.Center, 4, 4, 226, projectile.velocity.X * 0.4f, projectile.velocity.Y * 0.4f, 100, default(Color), 0.2f); //Spawns dust
            Main.dust[DDustID].noGravity = true;
            Main.dust[DDustID].velocity = 1.1f*Main.dust[DDustID].velocity.RotatedByRandom(MathHelper.ToRadians(10));

            if (projectile.timeLeft < 4)
            {
                projectile.alpha += 60;
                if (projectile.timeLeft ==3)
                {
                    projectile.velocity *= -0.3f;
                };

            }

            //if (projectile.velocity.Y > 16f)
            //{
            //    projectile.velocity.Y = 16f;
            //}
            // Since our sprite has an orientation, we need to adjust rotation to compensate for the draw flipping.






        }
        

    }
}
