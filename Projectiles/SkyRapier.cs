using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Graphics;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace KirillandRandom.Projectiles
{
    public class SkyRapier : ModProjectile
    {
        bool first = true;

        VertexStrip vertexStr = new();
        public override void SetDefaults()
        {
            Projectile.Name = "Brilliancy";
            Projectile.width = 70;
            Projectile.height = 70;
            Projectile.timeLeft = 13;
            Projectile.penetrate = 9999;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Melee;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            int DDustID = Dust.NewDust(target.Center, 2, 2, 226, Projectile.velocity.X * 0.4f, Projectile.velocity.Y * 0.4f, 100, default, 0.8f); //Spawns dust
            Main.dust[DDustID].noGravity = true;
            Main.dust[DDustID].velocity = 0.8f * Main.dust[DDustID].velocity.RotatedByRandom(MathHelper.ToRadians(2));
            target.immune[Projectile.owner] = 4;

        }
        public override bool? CanHitNPC(NPC target)
        {
            return base.CanHitNPC(target);

        }
        public override bool PreDraw(ref Color lightColor)
        {
            ;
            for (var i = 0; i < 2; i++)
            {
                Vector2 back = (Vector2.UnitX * -50f).RotatedBy(Projectile.rotation + MathHelper.ToRadians(45));
                var rot = MathHelper.ToRadians(Main.rand.NextFloat(-15, 15));
                Vector2 to = (Vector2.UnitX * Main.rand.NextFloat(2f, 20f)).RotatedBy(Projectile.rotation + MathHelper.ToRadians(45)).RotatedBy(rot);
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>("KirillandRandom/Projectiles/SkyRapier").Value,
                    Projectile.Center + back
                    + to
                    - Main.screenPosition,
                    null,
                    Color.White,
                    Projectile.rotation + rot,
                    new Vector2(0, 0),
                    Projectile.scale,
                    SpriteEffects.None
                    );
            }

            //
            for (var i = 0; i < 5; i++)
            {
                Vector2 back = (Vector2.UnitX * -50f).RotatedBy(Projectile.rotation + MathHelper.ToRadians(45));
                var rnd = Main.rand.NextFloat(-15, 15);
                var rot = MathHelper.ToRadians(rnd) - 0.1f;
                Vector2 to = (Vector2.UnitX * Main.rand.NextFloat(2f, 20f)).RotatedBy(Projectile.rotation + MathHelper.ToRadians(45)).RotatedBy(rot);
                var miscShaderData = GameShaders.Misc["EmpressBlade"];
                miscShaderData.UseImage0(ModContent.Request<Texture2D>("KirillandRandom/Visuals/testtrail"));
                miscShaderData.UseImage1(ModContent.Request<Texture2D>("KirillandRandom/Visuals/testtrail"));
                miscShaderData.UseImage2(ModContent.Request<Texture2D>("KirillandRandom/Visuals/testtrail"));
                int multiplier = 1;
                int num = 0;
                int num2 = 0;
                float index = 0.6f;
                miscShaderData.UseShaderSpecificData(new Vector4(multiplier, num, num2, index));
                miscShaderData.Apply();
                Main.graphics.GraphicsDevice.Textures[0] = ModContent.Request<Texture2D>("KirillandRandom/Visuals/white").Value;
                //Main.graphics.GraphicsDevice.Textures[0].GraphicsDevice.BlendState.AlphaSourceBlend= Blend.DestinationAlpha;
                var stSize = 3.6f - MathF.Abs(rnd) / 15;

                float[] mv1 = new float[5] { Projectile.rotation + rot, Projectile.rotation + rot, Projectile.rotation + rot, Projectile.rotation + rot, Projectile.rotation + rot };
                //float[] mv1 = new float[5] { Projectile.rotation + rot-0.2f, Projectile.rotation + rot - 0.2f, Projectile.rotation + rot - 0.2f, Projectile.rotation + rot - 0.2f, Projectile.rotation + rot-0.2f };
                Vector2 center = Projectile.Center + back + to + to - Vector2.UnitX.RotatedBy(Projectile.rotation) * 12;
                Vector2[] f1 = new Vector2[5] { center, center + to * stSize, center + to * stSize * 2, center + to * stSize * 3, center + to * stSize * 4 };
                vertexStr.PrepareStrip(f1, mv1,
                    ((float progress) =>
                    {
                        return Color.Aqua * ((progress * 4.5f) - 1);
                    }),
                    ((float progress) =>
                    {
                        return 40f * (0.5f - MathF.Abs(0.5f - progress));
                    }),
                    -Main.screenPosition, 5, includeBacksides: true);
                //vertexStr.PrepareStripWithProceduralPadding(a, Projectile.oldRot, StripColors, StripWidth, -Main.screenPosition + Projectile.Size / 2f);
                vertexStr.DrawTrail();
                //Main.pixelShader.CurrentTechnique.Passes[0].Apply();
            }
            //

            return false;
        }

        public override void AI()
        {

            Player owner = Main.player[Projectile.owner];


            Projectile.velocity = Vector2.Zero;
            if (first)
            {
                Projectile.velocity = Vector2.Zero;
                Projectile.light = 0.4f;
                first = false;
            }
            else
            {
                Projectile.netUpdate = true;

                owner.itemTime = 2;
                Projectile.timeLeft = 2;
                if ((Projectile.owner == Main.myPlayer) && (!Main.mouseLeft))
                {
                    Projectile.Kill();
                }
            }
            if (Projectile.owner == Main.myPlayer)
            {
                Projectile.ai[0] = (owner.Center).AngleTo(Main.MouseWorld) + MathHelper.ToRadians(-45f);
            }
            Projectile.rotation = Projectile.ai[0];
            if ((Projectile.rotation < MathHelper.ToRadians(45)) && (Projectile.rotation > MathHelper.ToRadians(-135))) owner.direction = 1;
            else owner.direction = -1;
            owner.itemRotation = Projectile.rotation + MathHelper.ToRadians(45) + MathHelper.ToRadians((owner.direction == 1) ? 0f : 180f) + Main._rand.NextFloat(-0.3f, 0.3f);
            Projectile.Center = owner.Center + new Vector2(30).RotatedBy(Projectile.rotation);

        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.HitDirectionOverride = (target.Center.X >= Main.player[Projectile.owner].Center.X).ToDirectionInt();
            base.ModifyHitNPC(target, ref modifiers);
        }
        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
        {
            modifiers.HitDirectionOverride = (target.Center.X >= Main.player[Projectile.owner].Center.X).ToDirectionInt();
            base.ModifyHitPlayer(target, ref modifiers);
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            /* //DAMN
            for (int i = -1; i<=1; i++)
            {
                for (int j = 0; j<4; j++)
                {
                    var _f = Projectile.Center+ Vector2.UnitX.RotatedBy(Projectile.rotation + (i * 0.1f))*j*5;
                    var f = new Rectangle((int)_f.X, (int)_f.Y, 1,1);
                }
            }
            */
            return targetHitbox.IntersectsConeSlowMoreAccurate(Main.player[Projectile.owner].Center, 140f, Projectile.rotation + 0.785f, 0.25f);

            return false;
            //return base.Colliding(projHitbox, targetHitbox);
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            int size = 500;
            hitbox.X -= size;
            hitbox.Y -= size;
            hitbox.Width += size * 2;
            hitbox.Height += size * 2;

        }

    }
}
