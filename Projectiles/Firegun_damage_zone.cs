using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

using Terraria.ID;


namespace KirillandRandom.Projectiles
{
    public class Firegun_damage_zone : ModProjectile
    {
        Random rnd = new Random();
        public int timer = 0;

        public override void SetDefaults()
        {
            Projectile.Name = "Nethersong";
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.timeLeft = 14;
            Projectile.penetrate = 999;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.aiStyle = 0;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 60);
            base.OnHitNPC(target, damage, knockback, crit);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 60);
            base.OnHitPvp(target, damage, crit);
        }

        public override void AI()
        {
            if (Projectile.wet)
            {
                Projectile.Kill(); //This kills the Projectile when touching water. However, since our Projectile is a cursed flame, we will comment this so that it won't run it. If you want to test this, feel free to uncomment this.
            }
            if (timer >= 2)
            {
                if (rnd.Next(3) == 2)
                {

                    int DDustID2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6, 0, 0, 50, default(Color), 6f); //Spawns dust
                    Main.dust[DDustID2].noGravity = true;
                    Main.dust[DDustID2].velocity = 0.9f * Main.dust[DDustID2].velocity;


                }
                int DDustID3 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6, 0, 0, 50, default(Color), 1f); //Spawns dust

                int DDustID = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6, 0, 0, 50, default(Color), 4f); //Spawns dust
                    Main.dust[DDustID].noGravity = true;
                    Main.dust[DDustID].velocity = 0.9f * Main.dust[DDustID].velocity;
                } 
            timer++;




        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            // By using ModifyDamageHitbox, we can allow the flames to damage enemies in a larger area than normal without colliding with tiles.
            // Here we adjust the damage hitbox. We adjust the normal 6x6 hitbox and make it 66x66 while moving it left and up to keep it centered.
            int size = 40;
            hitbox.X -= size;
            hitbox.Y -= size;
            hitbox.Width += size * 2;
            hitbox.Height += size * 2;
        }
    }
}
