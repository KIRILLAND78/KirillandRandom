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
        private float shootToX;
        private float distance;
        private float shootToY;

        public override void SetDefaults()
        {

            projectile.light = 0.4f;
            projectile.damage = 90;
            projectile.Name = "ChScythe";
            projectile.width = 80;
            projectile.height = 80;
            projectile.timeLeft = 60;
            projectile.penetrate = 999;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.ranged = false;
            projectile.aiStyle = -1;
            projectile.scale = 1;
        }
        public override void Kill(int timeLeft)
        {
            base.Kill(timeLeft);
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {

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
                origx = projectile.position.X;
                origy = projectile.position.Y;
                shootToX = Main.MouseWorld.X - projectile.Center.X;//обоже.
                shootToY = Main.MouseWorld.Y - projectile.Center.Y;//обоже.
                distance = (float)Math.Sqrt((shootToX * shootToX + shootToY * shootToY));
                first = 0;
            }
            if (projectile.timeLeft <= 30)
            {
                projectile.damage = 90;
            }
            else
            {
                projectile.damage = 0;
            }
            projectile.position.X = origx;
            projectile.position.Y = origy;

            projectile.alpha = 0;
                deg = (double)projectile.ai[1];
                double deg2= projectile.ai[0];
                rad = (deg-90) * (Math.PI / 180);
                double rad2 = (deg2+30) * (Math.PI / 180);
            projectile.rotation = (float)rad2 +projectile.velocity.ToRotation();
            
            float fshootToX = shootToX * (float)Math.Cos(rad)* 180.0f / distance;
            float fshootToY = shootToY * (float)Math.Cos(rad)* 180.0f  / distance;
            projectile.velocity.X = fshootToX;//help me.
            projectile.velocity.Y = fshootToY;

            //double dist = 24;

            projectile.position.X = p.Center.X + (projectile.position.X - origx)-40;//what am i doing with my life
            projectile.position.Y = p.Center.Y + (projectile.position.Y - origy)-20;
            //projectile.position.X = p.Center.X - (int)(Math.Cos(rad) * dist) - projectile.width / 2;
            //projectile.position.Y += p.Center.Y - (int)(Math.Sin(rad) * dist) - projectile.height / 2;
            projectile.ai[1] += 3f;
            projectile.ai[0] += 1f;


        }
    }
}
