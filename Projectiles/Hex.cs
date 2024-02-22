using KirillandRandom.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;


namespace KirillandRandom.Projectiles
{
    public class Hex : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.Name = "Hex";
            Projectile.width = 100;
            Projectile.height = 100;
            Projectile.timeLeft = 30;
            Projectile.penetrate = 50;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.aiStyle = 0;

            Projectile.alpha = 255;

        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            info.Damage = 1;
            target.AddBuff(ModContent.BuffType<Hexed>(), 600);
            base.OnHitPlayer(target, info);
        }


        public override void AI()
        {
            if ((Projectile.owner == Main.myPlayer) && (Projectile.timeLeft == 29))
            {
                Projectile.position = new Vector2(Main.MouseWorld.X - 50, Main.MouseWorld.Y - 50);
                Projectile.netUpdate = true;
            }
            if (Projectile.timeLeft == 25)
            {
                for (int i = 0; i < 256; i++)
                {
                    Projectile.alpha = 0;
                    if ((Main.player[i].Center - Projectile.Center).Length() <= 60)
                    {
                        Main.player[i].AddBuff(ModContent.BuffType<Hexed>(), 600);
                    }
                }
            }

            Player owner = Main.player[Projectile.owner];


            if (owner.dead == true)
            {
                Projectile.Kill();
            }
            Projectile.alpha += 10;


            Projectile.velocity = new Vector2(0, 0);

        }
    }
}
