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
            Projectile.DamageType = DamageClass.Melee;
            Player Player = Main.player[Projectile.owner];
            Projectile.light = 0.3f;
            Projectile.damage = 80;
            Projectile.Name = "Spark";
            Projectile.width = 118;
            Projectile.height = 118;
            Projectile.penetrate = 999;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.aiStyle = -1;
            Projectile.knockBack = 5;
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
            Player owner = Main.player[Projectile.owner];
            if (owner.dead == true)
            {
                Projectile.Kill();
            }
            if (Main.myPlayer == Projectile.owner)
            {
                if (!Main.mouseRight)
                {
                    Projectile.Kill();
                }
            }
                //int DDustID = Dust.NewDust(Projectile.position - new Vector2(2f, 2f), Projectile.width + 4, Projectile.height + 4, 17, Projectile.velocity.X * 0.4f, Projectile.velocity.Y * 0.4f, 100, default(Color), 1.1f); //Spawns dust
                //Main.dust[DDustID].noGravity = true;



                Player p = Main.player[Projectile.owner];
                if (first == 1)
            {
                direct = p.direction;
                first = 0;
                }
                deg = (double)Projectile.ai[0] + 45;
                rad = deg * (Math.PI / 180);
                Projectile.rotation = MathHelper.ToRadians(Projectile.ai[0]);
            if (Projectile.ai[0]%180 == 0)
            {
                //p.heldProj = Projectile.whoAmI;
                Projectile.timeLeft = 22;
                p.itemTime = 20; // Set Item time to 2 frames while we are used
                p.itemAnimation = 20; // Set Item animation time to 2 frames while we are used
            }


            Projectile.Center = p.Center;
            Projectile.Center = p.Center;
            if (direct== 1)
            {
                p.direction = 1;
                Projectile.ai[0] += 9f;
                //Projectile.spriteDirection = 1;
            }
            else
            {
                p.direction = -1;
                Projectile.ai[0] -= 9f;

                Projectile.spriteDirection = -1;
            }

        }
    }
}
