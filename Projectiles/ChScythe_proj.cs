using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

using KirillandRandom.NPCs;
using Terraria.ID;


namespace KirillandRandom.Projectiles
{
    public class ChScythe : ModProjectile
    {
        public float origx;
        public float origy;
        public double rad;
        public double deg;
        private int first = 1;
        private float acceleration;

        public override void SetDefaults()
        {

            projectile.melee = true;
            projectile.light = 0.4f;
            projectile.damage = 90;
            projectile.Name = "ChScythe";
            projectile.width = 80;
            projectile.height = 80;
            projectile.timeLeft = 65;
            projectile.penetrate = 999;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.ranged = false;
            projectile.aiStyle = -1;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            knockback = 12;
            damage += (50 * target.GetGlobalNPC<MNPC>().charge_e);
            target.GetGlobalNPC<MNPC>().charge_e = 0;
            base.ModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
        }
        public override void AI()
        {
            Player owner = Main.player[projectile.owner];
            if (owner.dead == true)
            {
                projectile.Kill();
            }

            //int DDustID = Dust.NewDust(projectile.position - new Vector2(2f, 2f), projectile.width + 4, projectile.height + 4, 17, projectile.velocity.X * 0.4f, projectile.velocity.Y * 0.4f, 100, default(Color), 1.1f); //Spawns dust
            //Main.dust[DDustID].noGravity = true;



            Player p = Main.player[projectile.owner];
            if (first == 1)
            {
                first = 0;
            }
            acceleration = 0;
            if (projectile.timeLeft < 55)
            {
                if ((p.Center - projectile.Center).Length() < 20)
                {
                    projectile.Kill();
                }
                acceleration = 1;

                if (projectile.timeLeft < 25)
                {
                    projectile.damage = 0;
                    acceleration = 3;
                }
            }
            projectile.velocity += Vector2.Normalize(p.Center-projectile.Center) * acceleration;
            if ((projectile.velocity.Length() > 35f)&& (projectile.timeLeft>24))
            {
                projectile.velocity = Vector2.Normalize(projectile.velocity) * 35;
            }

            double rad2 = (projectile.ai[0]) * (Math.PI / 180);
            projectile.rotation = (float)rad2;


            projectile.ai[0] += 14f;


        }
    }
}
