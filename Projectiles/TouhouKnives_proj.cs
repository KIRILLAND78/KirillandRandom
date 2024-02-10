using KirillandRandom.Items;
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
    internal class TouhouKnives_proj : ModProjectile
    {
        private Color StripColors(float progressOnStrip)
        {
            if (progressOnStrip < 0.21f) return Color.Transparent;
            Color result = Color.Lerp((Projectile.ai[1] == 1 ? Color.GhostWhite : Color.White) * 0.6f, (Projectile.ai[1] == 1 ? Color.Red : Color.Blue), MathF.Pow(progressOnStrip, 2));
            result.A /= 4;
            return result;
        }
        private float StripWidth(float progressOnStrip)
        {
            return MathHelper.Lerp(26f, 32f, Utils.GetLerpValue(0f, 0.2f, progressOnStrip, clamped: true)) * Utils.GetLerpValue(0f, 0.07f, progressOnStrip, clamped: true);
        }
        //private float StripWidth(float progressOnStrip)
        //{
        //    if (progressOnStrip < 0.2f) return (MathF.Pow(1 + progressOnStrip*2, 2)-1)*6.25f*5;
        //    return 6f- (progressOnStrip-0.2f)*7.5f * 5;
        //}
        VertexStrip vertexStr = new VertexStrip();
        bool first = true;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.width = 100;
            Projectile.height = 100;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 90;
            //Projectile.damage = 20;
            Projectile.width = 30;
            Projectile.penetrate = 5;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.height = 30;
            base.SetDefaults();
        }
        public override void PostDraw(Color lightColor)
        {
            MiscShaderData miscShaderData = GameShaders.Misc["MagicMissile"];


            //RainbowRod
            //miscShaderData.UseSaturation(-2.8f);
            //miscShaderData.UseOpacity(4f);

            //FlameLash
            //float transitToDark = Utils.GetLerpValue(0f, 6f, 20f, clamped: true);
            //miscShaderData.UseSaturation(-2f);
            //miscShaderData.UseOpacity(MathHelper.Lerp(4f, 8f, transitToDark));

            //MagicMissile
            miscShaderData.UseSaturation(-2.8f);
            miscShaderData.UseOpacity(2f);

            miscShaderData.Apply();

            //Vector2[] a= Projectile.oldPos;
            //Projectile.oldPos[0] +=new Vector2(0,20);
            //Projectile.oldPos[1] = Projectile.position;
            //a[1] = Projectile.position;
            //a[2] = Projectile.position;
            Main.graphics.GraphicsDevice.Textures[0] = ModContent.Request<Texture2D>("KirillandRandom/Visuals/white").Value;
            vertexStr.PrepareStrip(Projectile.oldPos, Projectile.oldRot, StripColors, StripWidth, -Main.screenPosition + Projectile.Size / 2f, Projectile.oldPos.Length, includeBacksides: true);
            //vertexStr.PrepareStripWithProceduralPadding(a, Projectile.oldRot, StripColors, StripWidth, -Main.screenPosition + Projectile.Size / 2f);
            vertexStr.DrawTrail();
            //Main.pixelShader.CurrentTechnique.Passes[0].Apply();
            base.PostDraw(lightColor);
        }
        public override void OnKill(int timeLeft)
        {
            int DDustID = Dust.NewDust(Projectile.Center - new Vector2(2, 2), 4, 4, DustID.PurificationPowder, 0, 0, 100, default, 1f); //Spawns dust
            Main.dust[DDustID].noGravity = true;
            Main.dust[DDustID].noLight = true;
            base.OnKill(timeLeft);
        }
        public override void AI()
        {
            if (first)
            {
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45);
                if (Projectile.ai[1] == 1)
                    Projectile.penetrate = 1;
            }
            if (Projectile.ai[0] > 0)
            {
                Projectile.timeLeft++;
                Projectile.position -= Projectile.velocity;
                Projectile.ai[0]--;
            }

            first = false;
            Player owner = Main.player[Projectile.owner];

            if ((owner.itemAnimation > owner.itemAnimationMax - 1) && (Main.myPlayer==Projectile.owner) && (owner.controlUseItem) && (Projectile.ai[1] == 0) && (owner.HeldItem.type == ModContent.ItemType<TouhouKnives>()) && (owner.CheckMana(owner.GetManaCost(owner.HeldItem))) && (Projectile.timeLeft < 89) && (owner.altFunctionUse == 2))
            {
                Projectile.ai[1] = 1;
                Projectile.ai[0] = 10;
                Projectile.velocity *= 2;
                for (int i = 0; i < 6; i++)
                {
                    int DDustID = Dust.NewDust(Projectile.Center - new Vector2(2, 2) + new Vector2(20, 0).RotatedBy(MathHelper.TwoPi / 6 * i), 4, 4, DustID.PurificationPowder, 0, 0, 100, default, 1f); //Spawns dust
                    Main.dust[DDustID].noGravity = true;
                    Main.dust[DDustID].noLight = true;
                }
                for (int i = 0; i < 3; i++)
                {

                    Vector2 Pos = Projectile.position + new Vector2(250, 0).RotateRandom(MathHelper.TwoPi);
                    int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Pos, (-Pos + Projectile.position) / 4, Projectile.type, Projectile.damage, Projectile.knockBack, Projectile.owner, ai0: 15, ai1: 1);
                    Main.projectile[proj].timeLeft = Projectile.timeLeft;
                    Main.projectile[proj].damage = (int)(Main.projectile[proj].damage * 1.1f);
                    Main.projectile[proj].netUpdate = true;
                }
                Projectile.Kill();
            }
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            Projectile.timeLeft = reader.ReadInt32();
            base.ReceiveExtraAI(reader);
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(Projectile.timeLeft);
            base.SendExtraAI(writer);
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            if (Projectile.ai[1] == 1) return false;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }

    }
}
