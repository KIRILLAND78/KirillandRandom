using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.Graphics;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace KirillandRandom.Projectiles
{
    public class UmbraFlameBolt : ModProjectile
    {
        int target=-1;
        public int bonusDamage = 0;
        Item Book;
        public int mode = 2;
        private int first = 1;
        private int orig_dmg = 0;
        private int draw_timer = 0;
        //drawing


        VertexStrip vertexStr = new();

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Projectile.light = 0.7f;
            Projectile.Name = "Umbra Flame";
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.timeLeft = 99999999;
            Projectile.penetrate = 99999;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.aiStyle = 0;

        }
        public override void OnKill(int timeLeft)
        {
            if (mode != 1)
            {
                Main.player[Projectile.owner].GetModPlayer<MPlayer>().flames_summoned -= 1;
            }
            base.OnKill(timeLeft);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            if (mode == 1)
                draw_timer++;
            draw_timer += 2;
            MiscShaderData miscShaderData = GameShaders.Misc["FlameLash"];
            miscShaderData.Apply();
            Main.graphics.GraphicsDevice.Textures[0] = ModContent.Request<Texture2D>("KirillandRandom/Visuals/white").Value;
            vertexStr.PrepareStrip(Projectile.oldPos, Projectile.oldRot,
                ((float progress) =>
                {
                    if (progress < 0.21f) return Color.Transparent;
                    Color result = Color.Lerp(Color.Aqua * 0.8f, Color.SlateGray * 0.1f, MathF.Pow(progress, 0.5f));
                    result.A /= 4;
                    return result;
                }),
                ((float progress) =>
                {
                    return MathHelper.Lerp(1f, 15f, Utils.GetLerpValue(0f, 1f, progress, clamped: true)) * Utils.GetLerpValue(0f, 0.07f, progress, clamped: true);
                }),

                -Main.screenPosition + Projectile.Size / 2f, Projectile.oldPos.Length, includeBacksides: true);
            //vertexStr.PrepareStripWithProceduralPadding(a, Projectile.oldRot, StripColors, StripWidth, -Main.screenPosition + Projectile.Size / 2f);
            vertexStr.DrawTrail();

            miscShaderData = GameShaders.Misc["LightDisc"];
            miscShaderData.Apply();
            Main.graphics.GraphicsDevice.Textures[0] = ModContent.Request<Texture2D>("KirillandRandom/Visuals/white").Value;
            var stSize = 3;
            float[] mv1 = new float[5] { draw_timer / 12f + 0, draw_timer / 12f + 0, draw_timer / 12f + 0, draw_timer / 12f + 0, draw_timer / 12f + 0 };
            float[] mv2 = new float[5] { draw_timer / 12f + 1.57f, draw_timer / 12f + 1.57f, draw_timer / 12f + 1.57f, draw_timer / 12f + 1.57f, draw_timer / 12f + 1.57f };



            var rt = Vector2.UnitX.RotatedBy(mv1[0]);
            var rt2 = Vector2.UnitX.RotatedBy(mv2[0]);
            Vector2[] f1 = new Vector2[5] { Projectile.position - rt * (stSize * 2), Projectile.position - rt * stSize, Projectile.position, Projectile.position + rt * stSize, Projectile.position + (rt * stSize * 3) };
            Vector2[] f2 = new Vector2[5] { Projectile.position - rt2 * (stSize * 2), Projectile.position - rt2 * stSize, Projectile.position, Projectile.position + rt2 * stSize, Projectile.position + rt2 * (stSize * 3) };


            vertexStr.PrepareStrip(f1, mv1,
                ((float progress) =>
                {
                    return Color.Aqua * 0.55f;
                }),
                ((float progress) =>
                {
                    return 5f * (0.5f - MathF.Abs(0.5f - progress));
                }),
                -Main.screenPosition + Projectile.Size / 2f, 5, includeBacksides: true);
            //vertexStr.PrepareStripWithProceduralPadding(a, Projectile.oldRot, StripColors, StripWidth, -Main.screenPosition + Projectile.Size / 2f);
            vertexStr.DrawTrail();

            vertexStr.PrepareStrip(f2, mv2,
                ((float progress) =>
                {
                    return Color.Aqua * 0.55f;
                }),
                ((float progress) =>
                {
                    return 5f * (0.5f - MathF.Abs(0.5f - progress));
                }),

                -Main.screenPosition + Projectile.Size / 2f, 5, includeBacksides: true);
            //vertexStr.PrepareStripWithProceduralPadding(a, Projectile.oldRot, StripColors, StripWidth, -Main.screenPosition + Projectile.Size / 2f);
            vertexStr.DrawTrail();
            return false;
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(mode);
            writer.Write(target);
            base.SendExtraAI(writer);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            mode = reader.ReadInt32();
            target = reader.ReadInt32();
            base.ReceiveExtraAI(reader);
        }
        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];


            if (owner.dead == true)
            {
                Projectile.Kill();
            }


            if (mode == 1)
            {
                Projectile.netUpdate = true;
                if (first == 0)
                {
                    Projectile.netUpdate = true;
                    Projectile.penetrate = 1;
                    Projectile.friendly = true;
                    Projectile.tileCollide = true;

                    Projectile.timeLeft = 800;
                    Projectile.light = 0.6f;
                    if (Main.myPlayer == Projectile.owner)
                    {
                        var vec = (Main.MouseWorld - Projectile.Center);
                        vec.Normalize();
                        Projectile.velocity = vec * 15f;
                    }
                    first = 2;
                }
                if ((Projectile.owner == Main.myPlayer) || (Main.netMode == NetmodeID.SinglePlayer))
                {
                    if ((target != -1) && !Main.npc[target].active)
                    {
                        target = -1;
                        Projectile.netUpdate = true;
                    }
                    if (target != -1)
                    {
                        float angle = ((-Projectile.Center + Main.npc[target].Center).ToRotation() + ((-Projectile.Center + Main.npc[target].Center).ToRotation() < 0 ? MathHelper.TwoPi : 0) - Projectile.velocity.ToRotation() - (Projectile.velocity.ToRotation() < 0 ? MathHelper.TwoPi : 0) * -1) % MathHelper.TwoPi;
                        angle = angle > MathHelper.Pi ? -(MathHelper.TwoPi - angle) : angle;
                        Projectile.velocity = Projectile.velocity.RotatedBy(Math.Clamp(angle, (-MathHelper.Pi / 40), (MathHelper.Pi / 40)));

                    }
                    else
                    {
                        int buffer = -1;
                        float g = 0;
                        for (int i = 0; i < Main.maxNPCs; i++)
                        {
                            if (Vector2.Distance(Main.npc[i].Center, Projectile.Center) <= 200 && Main.npc[i].active && !Main.npc[i].friendly && Main.npc[i].type != NPCID.TargetDummy)
                                if (g < Vector2.Distance(Main.npc[i].Center, Projectile.Center)) buffer = i;

                        }
                        if (buffer >= 0) {
                        target = buffer;
                        Projectile.netUpdate = true;
                        }
                    }
                }
            }
            else
            {

                Player p = Main.player[Projectile.owner];
                if (first == 1)
                {

                    orig_dmg = Projectile.damage;
                    Book = p.HeldItem;
                    if (Main.myPlayer == Projectile.owner)
                    {
                        if (Projectile.ai[0] < 4)
                        {
                            Projectile.ai[1] = p.GetModPlayer<MPlayer>().angle + 120 * Projectile.ai[0];
                        }
                        else
                        {
                            Projectile.ai[1] = -p.GetModPlayer<MPlayer>().angle + 72 * Projectile.ai[0];
                            if (Projectile.ai[0] > 8)
                            {

                                Projectile.ai[1] = p.GetModPlayer<MPlayer>().angle + 45 * Projectile.ai[0];

                            }
                        }

                        Projectile.netUpdate = true;
                    }
                }
                Projectile.alpha = 64;


                double deg = (double)Projectile.ai[1];
                double rad = deg * (Math.PI / 180);
                double dist = 32;
                if (Projectile.ai[0] > 3)
                {
                    dist = 60;
                }
                if (Projectile.ai[0] > 8)
                {
                    dist = 88;
                }
                bonusDamage = owner.GetModPlayer<MPlayer>().flames_summoned == 8 ? 32 : 16;

                bonusDamage = owner.GetModPlayer<MPlayer>().flames_summoned == 16 ? 48 : bonusDamage;

                Projectile.damage = orig_dmg + bonusDamage;

                Projectile.position.X = p.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
                Projectile.position.Y = p.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;
                if (Projectile.ai[0] < 9 && Projectile.ai[0] > 3)
                {
                    Projectile.ai[1] -= 4f;
                }
                else
                {
                    Projectile.ai[1] += 4f;
                }

                if (owner.HeldItem != Book)
                {
                    Projectile.Kill();
                }
                if ((p.controlUseItem) && (p.altFunctionUse == 2))
                {
                    mode = 1;
                }
                if (first == 1)
                {
                    first = 0;
                    Dust.NewDustDirect(Projectile.Center, 1, 1, DustID.Frost, default, default, default, Color.Aqua).noGravity = true;
                }
            }
        }
    }
}
