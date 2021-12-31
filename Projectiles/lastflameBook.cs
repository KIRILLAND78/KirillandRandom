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

            Main.player[Projectile.owner].GetModPlayer<MPlayer>().BookCreated = false;
            base.Kill(timeLeft);
        }

        private int angle=1;
        public Item Book;
        public bool first = true;
        public override void SetDefaults()
        {
            Projectile.light = 0.3f;
            Projectile.Name = "Last Flame Book(how? just how did you that!?)";
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.timeLeft = 7200;
            Projectile.penetrate = 1;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.aiStyle = 0;
        }
        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];
            if (owner.dead == true)
            {
                Projectile.Kill();
            }
            if (owner.direction == 1)
            {
                Projectile.position.X= owner.Center.X - 43;
            }
            else
            {
                Projectile.position.X = owner.Center.X + 13;
            }

            Projectile.position.Y = (float)(owner.Center.Y - 40f+10*Math.Sin(angle*Math.PI/128f));


            angle += 1;
            if (first)
            {
                Book = owner.HeldItem;
                first = false;
            }



            if (owner.HeldItem != Book)
                {
                Projectile.Kill();//РАБОТАЕТ, ЮХУ!

                Main.player[Projectile.owner].GetModPlayer<MPlayer>().BookCreated = false;
            }
            


        }
    }
}
