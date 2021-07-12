using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

using Terraria.ID;


namespace KirillandRandom.Projectiles
{
    public class Hex : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.Name = "Hex";
            projectile.width = 100;
            projectile.height = 100;
            projectile.timeLeft = 30;
            projectile.penetrate = 50;
            projectile.friendly = false;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.magic = true;
            projectile.aiStyle = 0;

            projectile.alpha = 255;

        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            damage = 0;
            target.AddBuff(ModContent.BuffType<Buffs.Hexed>(), 600);
            base.OnHitPlayer(target, damage, crit);
        }


        public override void AI()
        {
            if ((projectile.owner == Main.myPlayer) && (projectile.timeLeft == 29))
            {
                projectile.position = new Vector2(Main.MouseWorld.X - 50, Main.MouseWorld.Y - 50);
                projectile.netUpdate = true;
            }
            if (projectile.timeLeft == 25){
                for (int i = 0; i < 256; i++)
                {

                    projectile.alpha = 0;
                    if ((Main.player[i].Center - projectile.Center).Length() <= 60)
                    {
                        Main.player[i].AddBuff(ModContent.BuffType<Buffs.Hexed>(), 600);
                    }
                }
            }
            
            Player owner = Main.player[projectile.owner];


            if (owner.dead == true)
            {
                projectile.Kill();
            }
            projectile.alpha += 10;


            projectile.velocity = new Vector2(0, 0);

        }
    }
}
