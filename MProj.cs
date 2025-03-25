using KirillandRandom.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KirillandRandom
{
    internal class MProj : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public override void AI(Projectile projectile)
        {
            if (projectile.type == ProjectileID.BlandWhip)
            {
                projectile.hostile = true;
            }
            base.AI(projectile);
        }
        public override bool CanHitPlayer(Projectile projectile, Player target)
        {
            if ((target.whoAmI == projectile.owner) && (projectile.type == ProjectileID.BlandWhip))
            {
                return false;
            }
            return base.CanHitPlayer(projectile, target);
        }
        public override void ModifyHitPlayer(Projectile projectile, Player target, ref Player.HurtModifiers modifiers)
        {
            if ((!modifiers.PvP) && (projectile.type == ProjectileID.BlandWhip))
            {
                target.AddBuff(ModContent.BuffType<WorkFasterDamn>(), 220);
                modifiers.SourceDamage *= 0;
            }
            base.ModifyHitPlayer(projectile, target, ref modifiers);
        }
    }
}
