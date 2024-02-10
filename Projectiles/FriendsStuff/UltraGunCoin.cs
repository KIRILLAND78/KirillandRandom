using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Graphics;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace KirillandRandom.Projectiles.FriendsStuff
{
    internal class UltraGunCoin : ModProjectile
    {
        int timer = 0;
        public override bool PreDraw(ref Color lightColor)
        {
            timer++;
            var miscShaderData = GameShaders.Misc["LightDisc"];
            miscShaderData.Apply();
            Main.graphics.GraphicsDevice.Textures[0] = ModContent.Request<Texture2D>("KirillandRandom/Visuals/white").Value;
            var stSize = 9 + MathF.Sin(timer / 6f) * 1.5f;
            var draw_timer = timer * 3.1f;
            float[] mv1 = new float[5] { draw_timer / 12f + 0, draw_timer / 12f + 0, draw_timer / 12f + 0, draw_timer / 12f + 0, draw_timer / 12f + 0 };
            float[] mv2 = new float[5] { draw_timer / 12f + 1.57f, draw_timer / 12f + 1.57f, draw_timer / 12f + 1.57f, draw_timer / 12f + 1.57f, draw_timer / 12f + 1.57f };



            var rt = Vector2.UnitX.RotatedBy(mv1[0]);
            var rt2 = Vector2.UnitX.RotatedBy(mv2[0]);
            Vector2[] f1 = new Vector2[5] { Projectile.position - rt * (stSize * 2), Projectile.position - rt * stSize, Projectile.position, Projectile.position + rt * stSize, Projectile.position + (rt * stSize * 3) };
            Vector2[] f2 = new Vector2[5] { Projectile.position - rt2 * (stSize * 2), Projectile.position - rt2 * stSize, Projectile.position, Projectile.position + rt2 * stSize, Projectile.position + rt2 * (stSize * 3) };

            var vertexStr = new VertexStrip();
            vertexStr.PrepareStrip(f1, mv1,
                ((float progress) =>
                {
                    return Color.Gold * 0.75f * (1 + MathF.Sin(timer / 8f + 3) * 0.2f) * (1 - MathF.Abs(progress * 2 - 1));
                }),
                ((float progress) =>
                {
                    return 8f * (0.5f - MathF.Abs(0.5f - progress));
                }),
                -Main.screenPosition + Projectile.Size / 2f, 5, includeBacksides: true);
            //vertexStr.PrepareStripWithProceduralPadding(a, Projectile.oldRot, StripColors, StripWidth, -Main.screenPosition + Projectile.Size / 2f);
            vertexStr.DrawTrail();

            vertexStr.PrepareStrip(f2, mv2,
                ((float progress) =>
                {
                    return Color.Gold * 0.75f * (1 + MathF.Sin(timer / 8f + 3) * 0.2f) * (1 - MathF.Abs(progress * 2 - 1));
                }),
                ((float progress) =>
                {
                    return 8f * (0.5f - MathF.Abs(0.5f - progress));
                }),

                -Main.screenPosition + Projectile.Size / 2f, 5, includeBacksides: true);
            //vertexStr.PrepareStripWithProceduralPadding(a, Projectile.oldRot, StripColors, StripWidth, -Main.screenPosition + Projectile.Size / 2f);
            vertexStr.DrawTrail();
            return false;
        }
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Projectile.timeLeft = 200;
            base.SetDefaults();
        }
        public override void AI()
        {
            Projectile.velocity /= 1.02f;
            if (Projectile.timeLeft < 185)
            {
                Projectile.velocity += new Vector2(0, 0.8f);
                if (Projectile.velocity.Y > 14)
                {
                    Projectile.velocity.Y = 14;
                }
            }
            base.AI();
        }
    }
}
