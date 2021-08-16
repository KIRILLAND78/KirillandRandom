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

            Projectile.Name = "nebulavortex";
            Projectile.width = 60;
            Projectile.height = 60;
            Projectile.timeLeft = 240;
            Projectile.penetrate = 9999;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.aiStyle = 0;
        }
        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];


            if (owner.dead == true)
            {
                Projectile.Kill();
            }


            int DDustID = Dust.NewDust(Projectile.position - new Vector2(2f, 2f), Projectile.width + 4, Projectile.height + 4, 17, Projectile.velocity.X * 0.4f, Projectile.velocity.Y * 0.4f, 100, default(Color), 1.1f); //Spawns dust
            Main.dust[DDustID].noGravity = true;



            

                Player p = Main.player[Projectile.owner];

                if (first == 1)
                {
                    Projectile.ai[1] = p.GetModPlayer<MPlayer>().angle + 90 * p.GetModPlayer<MPlayer>().flames_summoned;
                    first = 0;
                }
                Projectile.alpha = 0;

                double deg = (double)Projectile.ai[1];
                double rad = deg * (Math.PI / 180);
                double dist = 32;
            Projectile.rotation = Projectile.ai[1];
            Projectile.ai[1] -= 0.1f;
            Projectile.velocity = new Vector2(0,0);



        }
    }
}
