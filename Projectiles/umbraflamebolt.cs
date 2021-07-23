﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

using Terraria.ID;


namespace KirillandRandom.Projectiles
{
    public class UmbraFlameBolt : ModProjectile
    {
        public int bonusDamage = 0;
        Item Book;
        public int mode = 2;
        private int first=1;
        private bool backup_update=false;

        public override void SetDefaults()
        {

            projectile.Name = "Umbra Flame";
            projectile.width = 12;
            projectile.height = 12;
            projectile.timeLeft = 7200;
            projectile.penetrate = 99999;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.magic = true;
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


            if (owner.dead == true)
            {
                projectile.Kill();
            }







            int DDustID = Dust.NewDust(projectile.position - new Vector2(2f, 2f), projectile.width + 4, projectile.height + 4, 17, projectile.velocity.X * 0.4f, projectile.velocity.Y * 0.4f, 100, default, 1.1f); //Spawns dust
            Main.dust[DDustID].noGravity = true;


            if (mode == 1)
            {
                if (first == 0)
                {
                    projectile.penetrate = 1;
                    projectile.friendly = true;
                    projectile.tileCollide = true;




                    projectile.damage +=bonusDamage;
                    projectile.light = 0.6f;
                    if (Main.myPlayer == projectile.owner)
                    {
                        var shootToX = Main.MouseWorld.X - projectile.Center.X;//обоже.
                        var shootToY = Main.MouseWorld.Y - projectile.Center.Y;//обоже.
                    float distance = (float)Math.Sqrt((shootToX * shootToX + shootToY * shootToY));
                    shootToX *= 15.0f/ distance;
                    shootToY *= 15.0f/ distance;
                    projectile.velocity.X = shootToX;//обоже.
                    projectile.velocity.Y = shootToY;//обоже.
                        projectile.netUpdate = true;
                    }
                    first = 2;
                }
            }
            else{

                Player p = Main.player[projectile.owner];
                if ((first != 1)&&(backup_update))
                {

                    projectile.netUpdate = true;


                }
                if (first == 1)
                {
                    Book = p.HeldItem;
                    if (Main.myPlayer == projectile.owner)
                    {if (projectile.ai[0] < 4)
                        {
                            projectile.ai[1] = p.GetModPlayer<MPlayer>().angle + 120 * projectile.ai[0];
                        }
                        else
                        {
                            projectile.ai[1] = -p.GetModPlayer<MPlayer>().angle + 72 * projectile.ai[0];
                            if (projectile.ai[0] >8)
                            {

                                projectile.ai[1] = p.GetModPlayer<MPlayer>().angle + 45 * projectile.ai[0];

                            }
                        }

                        projectile.netUpdate = true;
                    }
                    first = 0;
                }
                    projectile.alpha = 64;

                double deg = (double)projectile.ai[1];
                double rad = deg * (Math.PI / 180);
                double dist = 32;
                if (projectile.ai[0] >3){
                    dist = 60;
                }
                if (projectile.ai[0] > 8){
                    dist = 88;
                }

                bonusDamage = owner.GetModPlayer<MPlayer>().flames_summoned == 3 ? 16 : 0;

                bonusDamage = owner.GetModPlayer<MPlayer>().flames_summoned == 3 ? 32 : bonusDamage;

                projectile.position.X = p.Center.X - (int)(Math.Cos(rad) * dist) - projectile.width / 2;
                projectile.position.Y = p.Center.Y - (int)(Math.Sin(rad) * dist) - projectile.height / 2;
                if (projectile.ai[0] < 9 && projectile.ai[0] > 3)
                {
                    projectile.ai[1] -= 4f;
                }
                else
                {
                    projectile.ai[1] += 4f;
                }

                

                if (owner.HeldItem != Book)
                {
                    projectile.Kill();
                }
                if ((p.controlUseItem)&&(p.altFunctionUse!=2))
                {
                    mode = 1;
                }

            }


        }
    }
}