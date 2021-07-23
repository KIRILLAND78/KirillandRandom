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
            projectile.Name = "Nethersong";
            projectile.width = 2;
            projectile.height = 2;
            projectile.timeLeft = 14;
            projectile.penetrate = 999;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = true;
            projectile.ignoreWater = false;
            projectile.ranged = true;
            projectile.aiStyle = 0;
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
            if (projectile.wet)
            {
                projectile.Kill(); //This kills the projectile when touching water. However, since our projectile is a cursed flame, we will comment this so that it won't run it. If you want to test this, feel free to uncomment this.
            }
            if (timer >= 2)
            {
                if (rnd.Next(3) == 2)
                {

                    int DDustID2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, 0, 0, 50, default(Color), 6f); //Spawns dust
                    Main.dust[DDustID2].noGravity = true;
                    Main.dust[DDustID2].velocity = 0.9f * Main.dust[DDustID2].velocity;


                }
                int DDustID3 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, 0, 0, 50, default(Color), 1f); //Spawns dust

                int DDustID = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, 0, 0, 50, default(Color), 4f); //Spawns dust
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
