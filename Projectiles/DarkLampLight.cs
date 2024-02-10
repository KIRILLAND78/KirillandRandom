using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace KirillandRandom.Projectiles
{
    public class DarkLampLight : ModProjectile
    {
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
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.SourceDamage.Base -= ((hitcount) * 8);
            hitcount += 1;
            modifiers.HitDirectionOverride = (target.Center.X >= Main.player[Projectile.owner].Center.X).ToDirectionInt();
            target.AddBuff(BuffID.OnFire, 200);
            base.ModifyHitNPC(target, ref modifiers);
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.OnFire, 200);
            base.OnHitPlayer(target, info);
        }


        public override bool? CanHitNPC(NPC target)
        {
            Player Player = Main.player[Projectile.owner];
            if ((((!target.friendly || (target.type == 22 && Projectile.owner < 255 && Player.killGuide) || (target.type == 54 && Projectile.owner < 255 && Player.killClothier)))))
            {
                if (((target.Center - Player.Center).Length() <= distance) && ((target.Center - Player.Center).Length() >= distance - 60))
                {
                    return target.immune[Main.myPlayer] <= 0;
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
                int[] DDustID = new int[12];
                for (int k = 0; k < 12; k++)
                {
                    float randi = (float)MathHelper.ToRadians(Main.rand.Next(360));

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
            int size = 1000;
            hitbox.X -= size;
            hitbox.Y -= size;
            hitbox.Width += size * 2;
            hitbox.Height += size * 2;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadFlameRing();
            Main.EntitySpriteDraw(Terraria.GameContent.TextureAssets.FlameRing.Value, Projectile.Center - Main.screenPosition,
                new Rectangle(0, 0, 400, 400), Color.White * (0.55f - (distance * 0.5f / 600)), distance * 0.01f,
                new Vector2(400 * 0.5f, 400 * 0.5f), distance * 0.0057f, SpriteEffects.None, 0);
            Main.EntitySpriteDraw(Terraria.GameContent.TextureAssets.FlameRing.Value, Projectile.Center - Main.screenPosition,
                new Rectangle(0, 400, 400, 400), Color.White * (0.55f - (distance * 0.5f / 600)), distance * -0.008f,
                new Vector2(400 * 0.5f, 400 * 0.5f), distance * 0.0055f, SpriteEffects.None, 0);

            return base.PreDraw(ref lightColor);
        }

    }
}
