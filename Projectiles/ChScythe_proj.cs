using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

using KirillandRandom.NPCs;
using Terraria.ID;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;


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

        public override void Kill(int timeLeft)
        {
            Main.player[Main.myPlayer].itemAnimation = 0;
            Main.player[Main.myPlayer].itemTime = 2;
            base.Kill(timeLeft);
        }

        public override void SetDefaults()
        {

            Projectile.DamageType = DamageClass.Melee;
            Projectile.light = 0.4f;
            Projectile.damage = 80;
            Projectile.Name = "Spark";
            Projectile.width = 80;
            Projectile.height = 80;
            Projectile.timeLeft = 65;
            Projectile.penetrate = 999;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.aiStyle = -1;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            knockback = 12;
            damage += (50 * target.GetGlobalNPC<MNPC>().charge_e);
            //target.GetGlobalNPC<MNPC>().charge_e = 0;
            base.ModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
        }
        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];
            if (owner.dead == true)
            {
                Projectile.Kill();
            }

                //int DDustID = Dust.NewDust(Projectile.position - new Vector2(2f, 2f), Projectile.width + 4, Projectile.height + 4, 17, Projectile.velocity.X * 0.4f, Projectile.velocity.Y * 0.4f, 100, default(Color), 1.1f); //Spawns dust
                //Main.dust[DDustID].noGravity = true;
                if (first == 1)
                {
                //Shader
                //Filters.Scene.Activate("nihil");
                first = 0;
            }
            acceleration = 0;
            if (Projectile.timeLeft < 55)
            {
                if ((owner.Center - Projectile.Center).Length() <= 50)
                {
                   Projectile.Kill();
                }
                acceleration = 2;
                Projectile.velocity += Vector2.Normalize(owner.Center - Projectile.Center) * acceleration;

                if (Projectile.timeLeft < 25)
                {
                    Projectile.damage = 0;
                    Projectile.velocity = Vector2.Normalize(owner.Center - Projectile.Center) * 35;
                }
            }

            double rad2 = (Projectile.ai[0]) * (Math.PI / 180);
            Projectile.rotation = (float)rad2;


            Projectile.ai[0] += 14f;


        }
    }
}
