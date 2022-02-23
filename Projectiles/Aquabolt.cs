using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

using Terraria.ID;


namespace KirillandRandom.Projectiles
{
    public class Aquabolt : ModProjectile
    {
        NPC target;
        public bool playedSound = false;
        public override void SetDefaults()
        {

            Projectile.Name = "Aqua bolt";
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.timeLeft = 7200;
            Projectile.penetrate = 1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.aiStyle = 0;
            Projectile.alpha = 30;
        }
        public override void AI()
        {
            if (!playedSound)
            {
                Terraria.Audio.SoundEngine.PlaySound(new Terraria.Audio.LegacySoundStyle(SoundID.Splash, 1), Projectile.Center);
                playedSound = true;
            }
            Player owner = Main.player[Projectile.owner];
            int DDustID = Dust.NewDust(Projectile.position - new Vector2(2f, 2f), Projectile.width + 4, Projectile.height + 4, DustID.BlueTorch, Projectile.velocity.X * 0.4f, Projectile.velocity.Y * 0.4f, 100, default, 1.1f); //Spawns dust
            Main.dust[DDustID].noGravity = true;
            if (Projectile.ai[0]== 5)
            {
                Projectile.tileCollide = false;
                if ((Main.netMode == NetmodeID.Server) || (Main.netMode == NetmodeID.SinglePlayer))
                {
                    if (target != null && !target.active)
                    {
                        target = null;
                    }
                    if (target != null)
                    {
                        float angle = ((-Projectile.Center + target.Center).ToRotation() + ((-Projectile.Center + target.Center).ToRotation() < 0 ? MathHelper.TwoPi : 0) - Projectile.velocity.ToRotation() - (Projectile.velocity.ToRotation() < 0 ? MathHelper.TwoPi : 0) * -1) % MathHelper.TwoPi;
                        angle = angle > MathHelper.Pi ? -(MathHelper.TwoPi - angle) : angle;
                        Projectile.velocity = Projectile.velocity.RotatedBy(Math.Clamp(angle, (-MathHelper.Pi / 80), (MathHelper.Pi / 80)));

                    }
                    else
                    {
                        NPC buffer = null;
                        float g = 0;
                        for (int i = 0; i < Main.maxNPCs; i++)
                        {
                            if (Vector2.Distance(Main.npc[i].Center, Projectile.Center) <= 200 && Main.npc[i].active && !Main.npc[i].friendly && Main.npc[i].type != NPCID.TargetDummy)
                                if (g < Vector2.Distance(Main.npc[i].Center, Projectile.Center)) buffer = Main.npc[i];

                        }
                        target = buffer;
                    }
                }
            }


        }
    }
}
