﻿using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace KirillandRandom.Projectiles
{
    public class Firegun_damage_zone : ModProjectile
    {
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
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 60);
            base.OnHitNPC(target, hit, damageDone);
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.OnFire, 60);
            base.OnHitPlayer(target, info);
        }

        public override void AI()
        {
            if (Projectile.wet)
            {
                Projectile.Kill();
            }
            if (timer >= 2)
            {
                if (Main.rand.Next(3) == 2)
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
