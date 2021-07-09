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

            projectile.Name = "ElderStaffProjectile";
            projectile.width = 60;
            projectile.height = 60;
            projectile.timeLeft = 70;
            projectile.penetrate = 1;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.magic = true;
            projectile.aiStyle = 0;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 40; i++)
            {
                int ADust = Dust.NewDust(projectile.Center - new Vector2(30, 30), 60, 60, 61, 0, 0, 0, default, 1.6f);

            }
            if (projectile.owner == Main.myPlayer)
            {if (first>=2)
                {

                    Main.PlaySound(2, projectile.position,8);
                    Main.player[projectile.owner].position = projectile.Center - new Vector2(0, 21);
                    
                }


            }
            base.Kill(timeLeft);
        }
        public override void AI()
        {
            Player owner = Main.player[projectile.owner];

            first++;
            if (first == 1)
            {
                projectile.tileCollide = true;
            }
            //if (owner.dead == true)
            //{
            //    projectile.Kill();
            //}


            projectile.velocity *= 0.975f;
            Vector2 Dustt = new Vector2(1, 1);

            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(-45f);
            Dustt = new Vector2((float)Math.Cos(projectile.velocity.ToRotation() + MathHelper.ToRadians(15f)), (float)Math.Sin(projectile.velocity.ToRotation() + MathHelper.ToRadians(15f)));

            int GreenFl = Dust.NewDust(projectile.Center+new Vector2(-4,-4)+Dustt*20f,2,2, 61,0, 0, 0, default, 1.2f);
            //Main.NewText(Convert.ToString(projectile.velocity.ToRotation()));

            

                Player p = Main.player[projectile.owner];
            
                double deg = (double)projectile.ai[1];
                double rad = deg * (Math.PI / 180);
                double dist = 32;


        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 40;
            height = 40;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough);
        }
    }
}
