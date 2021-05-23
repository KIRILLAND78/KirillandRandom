﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;



namespace KirillandRandom.Projectiles
{
    public class SkyRapier : ModProjectile
    {
        //Random rnd = new Random();
        public Vector2 lastplpos;
        bool first = true;
        //private int vasya;

        public override void SetDefaults()
        {
            //projectile.position.Y -= 80;
            projectile.Name = "Sky Rapier";
            projectile.width = 70;
            projectile.height = 70;
            projectile.timeLeft = 13;
            projectile.penetrate = 9999;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.ranged = false;
            projectile.melee = true;


            //projectile.netUpdate2 = true;
            //projectile.netUpdate = true;//this thing is pretty unreliable. and by unreliable i mean those sometimes don't work. at all.
            //projectile.netSpam = 6;//fix desync asap
            //projectile.netImportant = true;//i dunno what those do, i just slapped them wothout thinking twice. Remove if it lags.
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {if (projectile.timeLeft >= 4) {
                int DDustID = Dust.NewDust(target.Center, 2, 2, 226, projectile.velocity.X * 0.4f, projectile.velocity.Y * 0.4f, 100, default, 0.8f); //Spawns dust
                Main.dust[DDustID].noGravity = true;
                Main.dust[DDustID].velocity = 0.8f * Main.dust[DDustID].velocity.RotatedByRandom(MathHelper.ToRadians(2));
            }
            base.OnHitNPC(target, damage, knockback, crit);
        }
        public override bool? CanHitNPC(NPC target)
        {
            Vector2 Vel = projectile.velocity;
            Vel.Normalize();

            Rectangle test = new Rectangle((int)projectile.Center.X + ((int)Vel.X * -21) - 4, (int)projectile.Center.Y + ((int)Vel.Y * -21) - 4, 8, 8);
            Rectangle test1 = new Rectangle((int)projectile.Center.X + ((int)Vel.X * -1) - 4, (int)projectile.Center.Y + ((int)Vel.Y * -1) - 4, 8, 8);
            Rectangle test2 = new Rectangle((int)projectile.Center.X + (int)(Vel.X * 19) - 4, (int)projectile.Center.Y + (int)(Vel.Y * 19) - 4, 8, 8);
            Rectangle test3 = new Rectangle((int)projectile.Center.X + (int)(Vel.X * 39) - 5, (int)projectile.Center.Y + (int)(Vel.Y * 39) - 5, 10, 10);

            Player player = Main.player[projectile.owner];
            if ((((!target.friendly || (target.type == NPCID.Guide && projectile.owner < 255 && player.killGuide) || (target.type == NPCID.Clothier && projectile.owner < 255 && player.killClothier)))))
            {
                if ((test2.Intersects(target.Hitbox))|| (test3.Intersects(target.Hitbox)) || (test.Intersects(target.Hitbox))|| (test1.Intersects(target.Hitbox)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public override void AI()
        {

            Player owner = Main.player[projectile.owner];

            projectile.velocity *= 1.2f;

            if (first)
            {

                Vector2 PlayerPos = owner.Center;
                //float angle;

                Vector2 AimFor = 150 * Vector2.Normalize(projectile.velocity) + PlayerPos;

                Vector2 Diff2 = AimFor - projectile.Center;

                //if (Diff2.X >= 0)
                //{
                //    angle = (float)Math.Atan(Diff2.Y / Diff2.X);
                //}
                //else
                //{
                //    angle = (float)Math.Atan(Diff2.Y / Diff2.X);

                //}
                //projectile.velocity;

                projectile.velocity.X = Diff2.X;
                projectile.velocity.Y = Diff2.Y;
                projectile.velocity.Normalize();
                projectile.velocity *= 3f;

                projectile.light = 0.4f;
                lastplpos = owner.Center;
                projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(-45f);

                if (owner.whoAmI == Main.myPlayer)
                {
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("SkyRapier2"), 0, 0, owner.whoAmI);
                }

                first = false;
            }
            else
            {
                projectile.netUpdate = true;

            }


            projectile.position += owner.Center - lastplpos;

            lastplpos = owner.Center;

            int DDustID = Dust.NewDust(projectile.Center-new Vector2(8,4), 0, 0, 226, projectile.velocity.X * 0.4f, projectile.velocity.Y * 0.4f, 100, default, 0.2f); //Spawns dust
            Main.dust[DDustID].noGravity = true;
            Main.dust[DDustID].velocity = 1.1f*Main.dust[DDustID].velocity.RotatedByRandom(MathHelper.ToRadians(10));

            if (projectile.timeLeft < 4)
            {
                projectile.alpha += 60;
                if (projectile.timeLeft ==3)
                {
                    projectile.velocity *= -0.3f;
                };

            }

            //if (projectile.velocity.Y > 16f)
            //{
            //    projectile.velocity.Y = 16f;
            //}





        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            // By using ModifyDamageHitbox, we can allow the flames to damage enemies in a larger area than normal without colliding with tiles.
            // Here we adjust the damage hitbox. We adjust the normal 6x6 hitbox and make it 66x66 while moving it left and up to keep it centered.
            int size = 500;
            hitbox.X -= size;
            hitbox.Y -= size;
            hitbox.Width += size * 2;
            hitbox.Height += size * 2;
        }

        //pls someone help im tired and this is code nightmare please don't look at this.
        //i am tired
        //i am tired
        //i am tired
        //i don't like making custom hitboxes AT ALL
        //i am tired
        //i am tired
        //i am tired




        //public override void PostAI()
        //{
        //    Vector2 Vel = projectile.velocity;
        //    Vel.Normalize();
        //    Rectangle test = new Rectangle((int)projectile.Center.X + (int)Vel.X * -16, (int)projectile.Center.Y + (int)Vel.Y * -16, 1,1);
        //    Rectangle test2 = new Rectangle((int)projectile.Center.X+ (int)Vel.X*14, (int)projectile.Center.Y + (int)Vel.Y * 14,1,1);
        //    Rectangle test3 = new Rectangle((int)projectile.Center.X + (int)Vel.X * 46, (int)projectile.Center.Y + (int)Vel.Y * 46, 1, 1);
        //    if (projectile.owner == Main.myPlayer && projectile.damage > 0)
        //    {
        //        Player player = Main.player[projectile.owner];
        //        for (int k = 0; k < 200; k++)
        //        {
        //            NPC curNPC = Main.npc[k];
        //            if ((((!curNPC.friendly || (curNPC.type == 22 && projectile.owner < 255 && player.killGuide) || (curNPC.type == 54 && projectile.owner < 255 && player.killClothier)))))
        //            {
        //                if ((test3.Intersects(curNPC.Hitbox)) || (test.Intersects(curNPC.Hitbox)) || (test2.Intersects(curNPC.Hitbox)))
        //                {
        //                    vasya = projectile.damage - 17 + rnd.Next(34);
        //                    curNPC.StrikeNPC(vasya, projectile.knockBack, (int)projectile.velocity.ToRotation());
        //                    player.addDPS(vasya);
        //            } }
        //        }
        //        if (Main.LocalPlayer.hostile)
        //        {
        //            for (int l = 0; l < 255; l++)
        //            {
        //                Player subPlayer = Main.player[l];
        //                if (l != projectile.owner && subPlayer.active && !subPlayer.dead && !subPlayer.immune && subPlayer.hostile && projectile.playerImmune[l] <= 0 && (Main.LocalPlayer.team == 0 || Main.LocalPlayer.team != subPlayer.team))
        //                {
        //                    if ((test3.Intersects(subPlayer.Hitbox)) || (test.Intersects(subPlayer.Hitbox)) || (test2.Intersects(subPlayer.Hitbox)))
        //                    {
        //                        subPlayer.HurtOld(projectile.damage, (int)projectile.knockBack);
        //                    }
        //                }
        //            }
        //        }
        //    }


        //    base.PostAI();
        //}







    }
}
