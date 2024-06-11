using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;



namespace KirillandRandom.Projectiles
{
    public class LastFlameBolt : ModProjectile
    {
        public int bonusDamage = 0;
        Item Book;
        public int mode = 2;
        private int first = 1;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.PlayerHurtDamageIgnoresDifficultyScaling[Type] = true;
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Projectile.Name = "Last Flame";
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.timeLeft = 7200;
            Projectile.penetrate = 1;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.aiStyle = 0;

        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            // Vanilla explosions do less damage to Eater of Worlds in expert mode, so we will too.
            if (Main.expertMode)
            {
                if (target.type >= NPCID.EaterofWorldsHead && target.type <= NPCID.EaterofWorldsTail)
                {
                    modifiers.FinalDamage /= 5;
                }
            }
        }
        public void Blomb()
        {
            if (Projectile.timeLeft <= 2) return;
            Projectile.tileCollide = false; // This is important or the explosion will be in the wrong place if the bomb explodes on slopes.
            Projectile.alpha = 255; // Set to transparent. This projectile technically lives as transparent for about 3 frames

            Projectile.penetrate = 50;
            // Change the hitbox size, centered about the original projectile center. This makes the projectile damage enemies during the explosion.
            Projectile.Resize(80, 80);
            Projectile.timeLeft = 2;
            // Fire Dust spawn
            SoundEngine.PlaySound(SoundID.Item113, Projectile.position);
            for (int i = 0; i < 5; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.CorruptPlants, 0f, 0f, 100, default, 3f);
                dust.noGravity = true;
                dust.velocity *= 2f;
                dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.CorruptPlants, 0f, 0f, 100, default, 2f);
            }

            Projectile.velocity = new Vector2();
        }
        public override void OnKill(int timeLeft)
        {
            if (mode != 1)
            {
                Main.player[Projectile.owner].GetModPlayer<MPlayer>().flames_summoned -= 1;
            }
            base.OnKill(timeLeft);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Blomb();
            return false;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Blomb();
            base.OnHitNPC(target, hit, damageDone);
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(mode);
            base.SendExtraAI(writer);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            mode = reader.ReadInt32();
            base.ReceiveExtraAI(reader);
        }
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            if ((((Projectile.ai[1]+270)%360)>90) && (((Projectile.ai[1] + 270) % 360) < 270))
            behindNPCsAndTiles.Add(index);
            else overPlayers.Add(index);
            base.DrawBehind(index, behindNPCsAndTiles, behindNPCs, behindProjectiles, overPlayers, overWiresUI);
        }
        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];

            if (owner.dead == true)
            {
                Projectile.Kill();
            }
            if (Projectile.timeLeft % 4 == 0) {
            int DDustID = Dust.NewDust(Projectile.position - new Vector2(2f, 2f), Projectile.width + 4, Projectile.height + 4, DustID.CorruptPlants, Projectile.velocity.X * 0.4f, Projectile.velocity.Y * 0.4f, 100, default, 0.85f); //Spawns dust
            Main.dust[DDustID].noGravity = true;
            }

            if (mode == 1)
            {
                if (first == 0)
                {
                    Projectile.netUpdate = true;
                    Projectile.friendly = true;
                    Projectile.tileCollide = true;

                    Projectile.damage += bonusDamage;
                    Projectile.light = 0.6f;
                    if (Main.myPlayer == Projectile.owner && Projectile.ai[0] == 0f)
                    {
                        var vec = (Main.MouseWorld - Projectile.Center);
                        vec.Normalize();
                        Projectile.velocity = vec * 15f;
                    }
                    Projectile.netUpdate = true;
                    first = 2;
                }
            }
            else
            {

                Player p = Main.player[Projectile.owner];
                if (first == 1)
                {
                    Book = p.HeldItem;
                    if (Main.myPlayer == Projectile.owner && Projectile.ai[0] == 0f)
                    {
                        Projectile.ai[1] = p.GetModPlayer<MPlayer>().angle + 90 * p.GetModPlayer<MPlayer>().flames_summoned;
                        Projectile.ai[2] = p.GetModPlayer<MPlayer>().thisIsKindOfDumbToBeHonest;

                        Projectile.netUpdate = true;
                    }
                    first = 0;
                }
                Projectile.alpha = 64;

                float deg = Projectile.ai[1];
                float rad = MathHelper.ToRadians(deg);
                double dist = 42;

                Projectile.position.X = p.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
                Projectile.position.Y = p.Center.Y - (int)((Math.Sin(rad)*Math.Sin(MathHelper.ToRadians(Projectile.ai[2] / 1)+rad/3)) * dist/3) - Projectile.height / 2;

                Projectile.ai[1] += 1.2f;
                Projectile.ai[2]++;
                if (p.GetModPlayer<MPlayer>().flames_summoned * 5 > bonusDamage)
                {
                    bonusDamage = p.GetModPlayer<MPlayer>().flames_summoned * 5;
                }

                if (owner.HeldItem != Book)
                {
                    Projectile.Kill();
                }
                if ((p.controlUseItem) && (p.altFunctionUse == 2))
                {
                    mode = 1;
                }

            }


        }
    }
}
