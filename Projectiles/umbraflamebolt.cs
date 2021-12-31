using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using KirillandRandom.Dusts;


namespace KirillandRandom.Projectiles
{
    public class UmbraFlameBolt : ModProjectile
    {
        public int bonusDamage = 0;
        Item Book;
        public int mode = 2;
        private int first=1;
        private bool backup_update=false;
        private int orig_dmg = 0;




        public override void SetDefaults()
        {

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


            int DDustID = Dust.NewDust(Projectile.Center-new Vector2(2,2), 4 , 4 , ModContent.DustType<Umbra_smoke>(), 0, 0, 100, default, 1f); //Spawns dust
            Main.dust[DDustID].noGravity = true;


            if (mode == 1)
            {
                Projectile.netUpdate = true;
                if (first == 0)
                {
                    Projectile.penetrate = 1;
                    Projectile.friendly = true;
                    Projectile.tileCollide = true;




                    Projectile.light = 0.6f;
                    if (Main.myPlayer == Projectile.owner)
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
                    orig_dmg = Projectile.damage;
                    Book = p.HeldItem;
                    if (Main.myPlayer == Projectile.owner)
                    {if (Projectile.ai[0] < 4)
                        {
                            Projectile.ai[1] = p.GetModPlayer<MPlayer>().angle + 120 * Projectile.ai[0];
                        }
                        else
                        {
                            Projectile.ai[1] = -p.GetModPlayer<MPlayer>().angle + 72 * Projectile.ai[0];
                            if (Projectile.ai[0] >8)
                            {

                                Projectile.ai[1] = p.GetModPlayer<MPlayer>().angle + 45 * Projectile.ai[0];

                            }
                        }

                        Projectile.netUpdate = true;
                    }
                    first = 0;
                }
                    Projectile.alpha = 64;


                double deg = (double)Projectile.ai[1];
                double rad = deg * (Math.PI / 180);
                double dist = 32;
                if (Projectile.ai[0] >3){
                    dist = 60;
                }
                if (Projectile.ai[0] > 8){
                    dist = 88;
                }

                bonusDamage = owner.GetModPlayer<MPlayer>().flames_summoned == 8 ? 32 : 16;

                bonusDamage = owner.GetModPlayer<MPlayer>().flames_summoned == 16 ? 48 : bonusDamage;

                Projectile.damage =orig_dmg+bonusDamage;

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
                if ((p.controlUseItem)&&(p.altFunctionUse==2))
                {
                    mode = 1;
                }

            }


        }
    }
}
