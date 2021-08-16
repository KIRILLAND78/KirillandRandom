using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

using Terraria.ID;


namespace KirillandRandom.Projectiles
{
    public class ElderStaffProjectile : ModProjectile
    {
        private int first = 0;

        public override void SetDefaults()
        {
            Projectile.Name = "ElderStaffProjectile";
            Projectile.width = 60;
            Projectile.height = 60;
            Projectile.timeLeft = 70;
            Projectile.penetrate = 1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.aiStyle = 0;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                int ADust = Dust.NewDust(Projectile.Center - new Vector2(30, 30), 60, 60, 60, 0, 0, 0, default, 1.6f);
                ADust = Dust.NewDust(Projectile.Center - new Vector2(30, 30), 60, 60, 59, 0, 0, 0, default, 1.6f);
                ADust = Dust.NewDust(Projectile.Center - new Vector2(30, 30), 60, 60, 61, 0, 0, 0, default, 1.6f);
                ADust = Dust.NewDust(Projectile.Center - new Vector2(30, 30), 60, 60, 62, 0, 0, 0, default, 1.6f);
                ADust = Dust.NewDust(Projectile.Center - new Vector2(30, 30), 60, 60, 64, 0, 0, 0, default, 1.6f);

            }
            if (Projectile.owner == Main.myPlayer)
            {if (first>=3)
                {
                    Main.player[Projectile.owner].teleporting = true;
                    Main.player[Projectile.owner].teleportTime = 2;

                    Terraria.Audio.SoundEngine.PlaySound(2, Projectile.position,8);
                    Main.player[Projectile.owner].Teleport(Projectile.Center - new Vector2(0, 21), 6, 1);
                    
                }


            }
            base.Kill(timeLeft);
        }
        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];

            first++;
            if (first == 2)
            {
                Projectile.tileCollide = true;
            }
            //if (owner.dead == true)
            //{
            //    Projectile.Kill();
            //}


            Projectile.velocity *= 0.975f;
            Vector2 Dustt = new Vector2(1, 1);

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(-45f);
            Dustt = new Vector2((float)Math.Cos(Projectile.velocity.ToRotation() + MathHelper.ToRadians(15f)), (float)Math.Sin(Projectile.velocity.ToRotation() + MathHelper.ToRadians(15f)));
            if (Projectile.timeLeft % 3 == 0) { 
            int GreenFl = Dust.NewDust(Projectile.Center + new Vector2(-4, -4) + Dustt * 20f, 2, 2, 61, 0, 0, 0, default, 1f);
            GreenFl = Dust.NewDust(Projectile.Center + new Vector2(-4, -4) + Dustt * 20f, 2, 2, 64, 0, 0, 0, default, 1f);
            GreenFl = Dust.NewDust(Projectile.Center + new Vector2(-4, -4) + Dustt * 20f, 2, 2, 62, 0, 0, 0, default, 1f);
            GreenFl = Dust.NewDust(Projectile.Center + new Vector2(-4, -4) + Dustt * 20f, 2, 2, 60, 0, 0, 0, default, 1f);
            GreenFl = Dust.NewDust(Projectile.Center + new Vector2(-4, -4) + Dustt * 20f, 2, 2, 59, 0, 0, 0, default, 1f);
            //Main.NewText(Convert.ToString(Projectile.velocity.ToRotation()));

        }

            Player p = Main.player[Projectile.owner];
            
                double deg = (double)Projectile.ai[1];
                double rad = deg * (Math.PI / 180);
                double dist = 32;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 20;
            height = 20;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough);
        }
    }
}
