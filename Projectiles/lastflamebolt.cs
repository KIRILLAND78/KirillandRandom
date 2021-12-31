using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

using Terraria.ID;


namespace KirillandRandom.Projectiles
{
    public class LastFlameBolt : ModProjectile
    {
        public int bonusDamage = 0;
        Item Book;
        public int mode = 2;
        private int first=1;
        private bool backup_update=false;

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
        public override void Kill(int timeLeft)
        {
            if (mode != 1)
            {
                Main.player[Projectile.owner].GetModPlayer<MPlayer>().flames_summoned -= 1;

            }
            base.Kill(timeLeft);
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

                    Projectile.damage +=bonusDamage;
                    Projectile.light = 0.6f;
                    if (Main.myPlayer == Projectile.owner && Projectile.ai[0] == 0f)
                    {
                        var shootToX = Main.MouseWorld.X - Projectile.Center.X;//обоже.
                        var shootToY = Main.MouseWorld.Y - Projectile.Center.Y;//обоже.
                    float distance = (float)Math.Sqrt((shootToX * shootToX + shootToY * shootToY));
                    shootToX *= 15.0f/ distance;
                    shootToY *= 15.0f/ distance;
                    Projectile.velocity.X = shootToX;//обоже.
                    Projectile.velocity.Y = shootToY;//обоже.
                    }
                    first = 2;
                }
            }
            else{

                Player p = Main.player[Projectile.owner];
                if ((first != 1)&&(backup_update))
                {

                    Projectile.netUpdate = true;


                }
                if (first == 1)
                {
                    Book = p.HeldItem;
                    if (Main.myPlayer == Projectile.owner && Projectile.ai[0] == 0f)
                    {
                        Projectile.ai[1] = p.GetModPlayer<MPlayer>().angle+ 90* p.GetModPlayer<MPlayer>().flames_summoned;

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
                    Projectile.Kill();//РАБОТАЕТ, ЮХУ!
                }
                if ((p.controlUseItem)&&(p.altFunctionUse==2))
                {

                    mode = 1;
                }

            }


        }
    }
}
