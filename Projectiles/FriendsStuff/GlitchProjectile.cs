using KirillandRandom.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace KirillandRandom.Projectiles.FriendsStuff
{
    internal class GlitchProjectile : ModProjectile
    {
        public override string Texture => "KirillandRandom/Visuals/1";
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.width = 70;
            Projectile.height = 70;
            Projectile.penetrate = 20;
            Projectile.timeLeft = 30;
            base.SetDefaults();
        }
        public override bool? CanDamage()
        {
            if (Projectile.timeLeft > 15) return false;
            return base.CanDamage();
        }
        public override void AI()
        {
            if (Projectile.timeLeft % 5 == 0)
                Dust.NewDustDirect(Projectile.position, 70, 70, ModContent.DustType<BinaryDust>());
            if ((Projectile.timeLeft < 12) && (Projectile.timeLeft > 2))
            {

                Dust.NewDustDirect(Projectile.position - new Vector2(10, 10), 90, 90, ModContent.DustType<BinaryDust>());
            }
            base.AI();
        }
        public override bool PreDraw(ref Color lightColor)
        {
            return false;
        }

    }
}
