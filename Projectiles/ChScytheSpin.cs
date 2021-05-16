using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using KirillandRandom.NPCs;
using Terraria.ID;


namespace KirillandRandom.Projectiles
{
    public class ChScytheSpin : ModProjectile
    {
        public bool todelete = false;
        public double rad;
        public double deg;
        public double rad2;
        private int first = 1;
        private int direct;
        public override void SetDefaults()
        {
            projectile.melee = true;
            Player player = Main.player[projectile.owner];
            projectile.light = 0.3f;
            projectile.damage = 80;
            projectile.Name = "ChScythe";
            projectile.width = 118;
            projectile.height = 118;
            projectile.penetrate = 999;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.ranged = false;
            projectile.aiStyle = -1;
            projectile.knockBack = 5;
        }
        



        public override void Kill(int timeLeft)
        {
            base.Kill(timeLeft);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
            {
                target.AddBuff(ModContent.BuffType<Buffs.stacking_charge>(), 300);
                if (target.GetGlobalNPC<MNPC>().charge_e < 5)
                {
                    target.GetGlobalNPC<MNPC>().charge_e += 1;
                }
            }
       
        public override void AI()
        {
            Player owner = Main.player[projectile.owner];
            if (owner.dead == true)
            {
                projectile.Kill();
            }
            if (!owner.channel)
            {
                projectile.Kill();
            }
                //int DDustID = Dust.NewDust(projectile.position - new Vector2(2f, 2f), projectile.width + 4, projectile.height + 4, 17, projectile.velocity.X * 0.4f, projectile.velocity.Y * 0.4f, 100, default(Color), 1.1f); //Spawns dust
                //Main.dust[DDustID].noGravity = true;



                Player p = Main.player[projectile.owner];
                if (first == 1)
            {
                direct = p.direction;
                first = 0;
                }
                deg = (double)projectile.ai[0] + 45;
                rad = deg * (Math.PI / 180);
                projectile.rotation = MathHelper.ToRadians(projectile.ai[0]);
            


            projectile.Center = p.Center;
            projectile.Center = p.Center;
            if (direct== 1)
            {
                p.direction = 1;
                projectile.ai[0] += 9f;
                //projectile.spriteDirection = 1;
            }
            else
            {
                p.direction = -1;
                projectile.ai[0] -= 9f;

                projectile.spriteDirection = -1;
            }

        }
    }
}
