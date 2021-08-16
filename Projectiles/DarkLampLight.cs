using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

using Terraria.ID;


namespace KirillandRandom.Projectiles
{
    public class DarkLampLight : ModProjectile
    {
        Random rnd = new Random();
        public int timer = 0;
        public int distance = 0;
        public int hitcount = 0;

        public override void SetDefaults()
        {
            Projectile.Name = "DarkLamp";
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.timeLeft = 140;
            Projectile.penetrate = 9999;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.aiStyle = 0;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            hitcount += 1;
                damage -= ((hitcount) * 8);
            
            target.AddBuff(BuffID.OnFire, 200);
            base.ModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 200);
            base.OnHitPvp(target, damage, crit);
        }


        public override bool? CanHitNPC(NPC target)
        {
            Player Player = Main.player[Projectile.owner];
            if ((((!target.friendly || (target.type == 22 && Projectile.owner < 255 && Player.killGuide) || (target.type == 54 && Projectile.owner < 255 && Player.killClothier)))))
            {
                if (((target.Center-Player.Center).Length()<=distance)&& ((target.Center - Player.Center).Length() >= distance-60))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];
            if (owner.dead == true)
            {
                Projectile.Kill();
            }
            Projectile.position.X = (float)owner.Center.X;
            Projectile.position.Y = (float)owner.Center.Y;

            distance = (int)(600 * (Math.Sin(MathHelper.ToRadians(90 * timer / 160))));
            if (timer >= 5)
            {
                int[] DDustID= new int[12];
                for (int k = 0; k < 12; k++)
                {
                    float randi = (float)MathHelper.ToRadians(rnd.Next(360));

                    Vector2 randpos = new Vector2((distance * (float)Math.Cos(randi)), (distance * (float)Math.Sin(randi)));
                    DDustID[k] = Dust.NewDust(owner.Center + randpos, 0, 0, 6, 0, 0, 50, default(Color), 3f); //Spawns dust
                    Main.dust[DDustID[k]].noGravity = true;
                    Main.dust[DDustID[k]].velocity = 0.9f * Main.dust[DDustID[k]].velocity;


                }


                //int DDustID2 = Dust.NewDust(owner.Center + randpos, 0, 0, 6, 0, 0, 50, default(Color), 8f); //Spawns dust
                //Main.dust[DDustID2].noGravity = true;
                //Main.dust[DDustID2].velocity = 0.9f * Main.dust[DDustID2].velocity;
                

                //int DDustID = Dust.NewDust(owner.Center + randpos, 0, 0, 6, 0, 0, 50, default(Color), 8f); //Spawns dust
                //Main.dust[DDustID].noGravity = true;
                //Main.dust[DDustID].velocity = 0.9f * Main.dust[DDustID].velocity;
            }
            timer++;

        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            // By using ModifyDamageHitbox, we can allow the flames to damage enemies in a larger area than normal without colliding with tiles.
            // Here we adjust the damage hitbox. We adjust the normal 6x6 hitbox and make it 66x66 while moving it left and up to keep it centered.
            int size = 1000;
            hitbox.X -= size;
            hitbox.Y -= size;
            hitbox.Width += size * 2;
            hitbox.Height += size * 2;
        }

    }
}
