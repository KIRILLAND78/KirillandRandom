using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;



namespace KirillandRandom.Projectiles
{
    public class LastFlameBook : ModProjectile
    {
        public override void Kill(int timeLeft)
        {

            Main.player[projectile.owner].GetModPlayer<MPlayer>().BookCreated = false;
            base.Kill(timeLeft);
        }

        private int angle=1;
        public Item Book;
        public bool first = true;
        public override void SetDefaults()
        {
            projectile.light = 0.3f;
            projectile.Name = "Last Flame Book(how? just how?)";
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
        public override void AI()
        {
            Player owner = Main.player[projectile.owner];
            if (owner.dead == true)
            {
                projectile.Kill();
            }
            if (owner.direction == 1)
            {
                projectile.position.X= owner.Center.X - 43;
            }
            else
            {
                projectile.position.X = owner.Center.X + 13;
            }

            projectile.position.Y = (float)(owner.Center.Y - 40f+10*Math.Sin(angle*Math.PI/128f));


            angle += 1;
            if (first)
            {
                Book = owner.HeldItem;
                first = false;
            }



            if (owner.HeldItem != Book)
                {
                projectile.Kill();//РАБОТАЕТ, ЮХУ!

                Main.player[projectile.owner].GetModPlayer<MPlayer>().BookCreated = false;
            }
            


        }
    }
}
