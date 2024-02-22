using KirillandRandom.NPCs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace KirillandRandom.Projectiles
{
    public class ChScytheSpin : ModProjectile
    {
        private int first = 1;
        private int direct;
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Melee;
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
            Projectile.DamageType = DamageClass.MeleeNoSpeed;
            Projectile.aiStyle = -1;
            Projectile.knockBack = 5;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
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
            Player p = Main.player[Projectile.owner];
            if (first == 1)
            {
                direct = p.direction;
                first = 0;
            }
            Projectile.rotation = MathHelper.ToRadians(Projectile.ai[0] * p.direction);
            if (Projectile.ai[0] % 130 == 0)
            {
                Projectile.timeLeft = 22;
                p.itemTime = 20;
                p.itemAnimation = 20;
            }
            if ((Projectile.ai[0] % 520 == 0) && (Projectile.ai[0] >= 250))
            {
                for (int j = 0; j < 20; j++)
                {
                    var f = Dust.NewDust(Projectile.Center + (new Vector2(1, 0) * 400).RotatedBy(MathHelper.ToRadians(18f * j)), 1, 1, DustID.Electric);
                    Main.dust[f].noGravity = true;
                    Main.dust[f].noLight = true;
                }
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    if ((!Main.npc[i].friendly) && ((Main.npc[i].Center - Projectile.Center).Length() < 400))
                    {

                        Main.npc[i].AddBuff(ModContent.BuffType<Buffs.stacking_charge>(), 300);
                        if (Main.npc[i].GetGlobalNPC<MNPC>().charge_e < 5)
                        {
                            Main.npc[i].GetGlobalNPC<MNPC>().charge_e += 1;
                        }
                    }

                }
            }

            Projectile.Center = p.Center;
            if (direct == 1)
            {
                p.direction = 1;
                Projectile.spriteDirection = 1;
            }
            else
            {
                p.direction = -1;
                Projectile.spriteDirection = -1;
            }
            Projectile.ai[0] += 13f;

        }
    }
}
