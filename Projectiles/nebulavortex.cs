using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

using Terraria.ID;


namespace KirillandRandom.Projectiles
{
    public class nebulavortex : ModProjectile
    {
        private int first = 1;

        public override void SetDefaults()
        {

            projectile.Name = "nebulavortex";
            projectile.width = 60;
            projectile.height = 60;
            projectile.timeLeft = 240;
            projectile.penetrate = 9999;
            projectile.friendly = false;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.ranged = true;
            projectile.aiStyle = 0;
        }
        public override void AI()
        {
            Player owner = Main.player[projectile.owner];


            if (owner.dead == true)
            {
                projectile.Kill();
            }


            int DDustID = Dust.NewDust(projectile.position - new Vector2(2f, 2f), projectile.width + 4, projectile.height + 4, 17, projectile.velocity.X * 0.4f, projectile.velocity.Y * 0.4f, 100, default(Color), 1.1f); //Spawns dust
            Main.dust[DDustID].noGravity = true;



            

                Player p = Main.player[projectile.owner];

                if (first == 1)
                {
                    projectile.ai[1] = p.GetModPlayer<MPlayer>().angle + 90 * p.GetModPlayer<MPlayer>().flames_summoned;
                    first = 0;
                }
                projectile.alpha = 0;

                double deg = (double)projectile.ai[1];
                double rad = deg * (Math.PI / 180);
                double dist = 32;
            projectile.rotation = projectile.ai[1];
            projectile.ai[1] -= 0.1f;
            projectile.velocity = new Vector2(0,0);



        }
    }
}
