using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;



namespace KirillandRandom.Projectiles
{
    public class lastflame : ModProjectile
    {
        Item Book;
        public int mode = 2;
        private int first=1;

        public override void SetDefaults()
        {

            projectile.Name = "Last Flame";
            projectile.width = 12;
            projectile.height = 12;
            projectile.timeLeft = 7200;
            projectile.penetrate = 1;
            projectile.friendly = false;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.ranged = true;
            projectile.aiStyle = 0;
        }
        public override void Kill(int timeLeft)
        {
            if (mode != 1)
            {
                Main.player[projectile.owner].GetModPlayer<MPlayer>().flames_summoned -= 1;

            }
            base.Kill(timeLeft);
        }
        public override void AI()
        {
            Player owner = Main.player[projectile.owner];



            if (mode == 1)
            {
                if (first == 0)
                {

                    projectile.friendly = true;
                    Player p = Main.player[projectile.owner];
                    projectile.tileCollide = true;

                    projectile.light = 0.6f;
                    var shootToX = Main.MouseWorld.X - projectile.Center.X;//обоже.
                    var shootToY = Main.MouseWorld.Y - projectile.Center.Y;//обоже.
                    float distance = (float)Math.Sqrt((shootToX * shootToX + shootToY * shootToY));
                    shootToX *= 15.0f/ distance;
                    shootToY *= 15.0f/ distance;
                    projectile.velocity.X = shootToX;//обоже.
                    projectile.velocity.Y = shootToY;//обоже.
                    first = 2;
                }
                //int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 4, projectile.height + 4, 36, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 0.75f); //Spawns dust
                //Main.dust[DustID].noGravity = true;
            }
            else{

                Player p = Main.player[projectile.owner];
                if (first == 1)
                {
                    Book = p.HeldItem;
                    projectile.ai[1] = p.GetModPlayer<MPlayer>().angle+ 120* p.GetModPlayer<MPlayer>().flames_summoned;
                    first = 0;
                }
                    projectile.alpha = 64;

                double deg = (double)projectile.ai[1];
                double rad = deg * (Math.PI / 180);
                double dist = 32; 

                projectile.position.X = p.Center.X - (int)(Math.Cos(rad) * dist) - projectile.width / 2;
                projectile.position.Y = p.Center.Y - (int)(Math.Sin(rad) * dist) - projectile.height / 2;

                projectile.ai[1] += 3f;
                if (owner.HeldItem != Book)
                {
                    projectile.Kill();//РАБОТАЕТ, ЮХУ!
                }
                if ((p.controlUseItem)&&(p.altFunctionUse!=2))
                {
                    mode = 1;
                }

            }


        }
    }
}
