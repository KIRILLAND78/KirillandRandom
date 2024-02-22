using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.ModLoader;



namespace KirillandRandom.Projectiles
{
    public class LastFlameBolt : ModProjectile
    {
        public int bonusDamage = 0;
        Item Book;
        public int mode = 2;
        private int first = 1;

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
        public override void OnKill(int timeLeft)
        {
            if (mode != 1)
            {
                Main.player[Projectile.owner].GetModPlayer<MPlayer>().flames_summoned -= 1;

            }
            base.OnKill(timeLeft);
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
        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];

            if (owner.dead == true)
            {
                Projectile.Kill();
            }
            int DDustID = Dust.NewDust(Projectile.position - new Vector2(2f, 2f), Projectile.width + 4, Projectile.height + 4, 17, Projectile.velocity.X * 0.4f, Projectile.velocity.Y * 0.4f, 100, default, 1.1f); //Spawns dust
            Main.dust[DDustID].noGravity = true;

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

                        Projectile.netUpdate = true;
                    }
                    first = 0;
                }
                Projectile.alpha = 64;

                double deg = (double)Projectile.ai[1];
                double rad = deg * (Math.PI / 180);
                double dist = 32;

                Projectile.position.X = p.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
                Projectile.position.Y = p.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;

                Projectile.ai[1] += 3f;

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
