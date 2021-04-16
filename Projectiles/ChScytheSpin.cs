using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using KirillandRandom.NPCs;
using Terraria.ID;


namespace KirillandRandom.Projectiles
{
    public class ChScytheSpin : ModProjectile
    {
        public bool todelete = false;
        public double rad;
        public double deg;
        public double rad2;
        private int first = 1;
        public override void SetDefaults()
        {

            projectile.light = 0.3f;
            projectile.damage = 80;
            projectile.Name = "ChScythe";
            projectile.width = 60;
            projectile.height = 60;
            projectile.timeLeft = 20;
            projectile.penetrate = 999;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.ranged = false;
            projectile.aiStyle = -1;
        }




        public override void Kill(int timeLeft)
        {
            base.Kill(timeLeft);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
            {
                target.AddBuff(ModContent.BuffType<Buffs.stacking_charge>(), 300);//СИНИЙ ОГОНЬ СЮДА. ИЛИ ВООБЩЕ ЧТО-ТО ДРУГОЕ.
                                                                                  //временно дебафф заряда, пока он не решит, какое оружие он хочет
                if (target.GetGlobalNPC<MNPC>().charge_e < 5)
                {
                    target.GetGlobalNPC<MNPC>().charge_e += 1;
                }
            }
       
        public override void AI()
        {
            if (todelete) {
                projectile.Kill();//нужно для того, чтобы коса не мелькала в последнем кадре атаки и переходила в следующий цикл плавно.
            }
            Player owner = Main.player[projectile.owner];
            if (owner.dead == true)
            {
                projectile.Kill();
            }

            //int DDustID = Dust.NewDust(projectile.position - new Vector2(2f, 2f), projectile.width + 4, projectile.height + 4, 17, projectile.velocity.X * 0.4f, projectile.velocity.Y * 0.4f, 100, default(Color), 1.1f); //Spawns dust
            //Main.dust[DDustID].noGravity = true;



                Player p = Main.player[projectile.owner];
                if (first == 1)
                {
                    first = 0;
                }
                projectile.alpha = 0;
            if (projectile.ai[0] == 1f){
                deg = (double)projectile.ai[1] + 225;
                rad = deg * (Math.PI / 180);
                if (owner.direction == 1)
                {
                    if (deg >= (225 + 180-6)) { todelete = true; }
                    rad2 = (deg - 45) * (Math.PI / 180);
                }
                else
                {
                    if (deg <= (45+6)) { todelete=true; }
                    rad2 = (deg - 135) * (Math.PI / 180);

                }
                projectile.rotation = (float)rad2;
            }
            else
            {
                deg = (double)projectile.ai[1] + 45;
                rad = deg * (Math.PI / 180);

                if (owner.direction == 1)
                {
                    if (deg >= 225-6) { todelete = true; }
                    rad2 = (deg - 45) * (Math.PI / 180);
                }else{
                    if (deg <= 45-180+6) { todelete = true; }
                    rad2 = (deg-135) * (Math.PI / 180);
                }
                projectile.rotation = (float)rad2;





            }

            double dist = 24;

            projectile.position.X = p.Center.X - (int)(Math.Cos(rad) * dist) - projectile.width / 2;
            projectile.position.Y = p.Center.Y - (int)(Math.Sin(rad) * dist) - projectile.height / 2;
            if (owner.direction == 1)
            {
                projectile.ai[1] += 9f;
                //projectile.spriteDirection = 1;
            }
            else
            {
                projectile.ai[1] -= 9f;

                projectile.spriteDirection = -1;
            }

            


        }
    }
}
