using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using KirillandRandom.Projectiles;



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
            //Projectile.position.Y -= 80;
            Projectile.Name = "Brilliancy";
            Projectile.width = 70;
            Projectile.height = 70;
            Projectile.timeLeft = 13;
            Projectile.penetrate = 9999;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Melee;

            //Projectile.netUpdate2 = true;
            //Projectile.netUpdate = true;//this thing is pretty unreliable. and by unreliable i mean those sometimes don't work. at all.
            //Projectile.netSpam = 6;//fix desync asap
            //Projectile.netImportant = true;//i dunno what those do, i just slapped them wothout thinking twice. Remove if it lags.
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {if (Projectile.timeLeft >= 4) {
                int DDustID = Dust.NewDust(target.Center, 2, 2, 226, Projectile.velocity.X * 0.4f, Projectile.velocity.Y * 0.4f, 100, default, 0.8f); //Spawns dust
                Main.dust[DDustID].noGravity = true;
                Main.dust[DDustID].velocity = 0.8f * Main.dust[DDustID].velocity.RotatedByRandom(MathHelper.ToRadians(2));
            }
            base.OnHitNPC(target, damage, knockback, crit);
        }
        public override bool? CanHitNPC(NPC target)
        {
            Vector2 Vel = Projectile.velocity;
            Vel.Normalize();

            Rectangle test = new Rectangle((int)Projectile.Center.X + ((int)Vel.X * -21) - 4, (int)Projectile.Center.Y + ((int)Vel.Y * -21) - 4, 8, 8);
            Rectangle test1 = new Rectangle((int)Projectile.Center.X + ((int)Vel.X * -1) - 4, (int)Projectile.Center.Y + ((int)Vel.Y * -1) - 4, 8, 8);
            Rectangle test2 = new Rectangle((int)Projectile.Center.X + (int)(Vel.X * 19) - 4, (int)Projectile.Center.Y + (int)(Vel.Y * 19) - 4, 8, 8);
            Rectangle test3 = new Rectangle((int)Projectile.Center.X + (int)(Vel.X * 39) - 5, (int)Projectile.Center.Y + (int)(Vel.Y * 39) - 5, 10, 10);
            Player Player = Main.player[Projectile.owner];
            if ((((!target.friendly || (target.type == NPCID.Guide && Projectile.owner < 255 && Player.killGuide) || (target.type == NPCID.Clothier && Projectile.owner < 255 && Player.killClothier)))))
            {
                if ((test2.Intersects(target.Hitbox))|| (test3.Intersects(target.Hitbox)) || (test.Intersects(target.Hitbox))|| (test1.Intersects(target.Hitbox)))
                {
                    return target.immune[Main.myPlayer] <= 0;
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

            Player owner = Main.player[Projectile.owner];

            Projectile.velocity *= 1.25f;

            if (first)
            {

                Vector2 PlayerPos = owner.Center;
                //float angle;

                Vector2 AimFor = 150 * Vector2.Normalize(Projectile.velocity) + PlayerPos;

                Vector2 Diff2 = AimFor - Projectile.Center;

                //if (Diff2.X >= 0)
                //{
                //    angle = (float)Math.Atan(Diff2.Y / Diff2.X);
                //}
                //else
                //{
                //    angle = (float)Math.Atan(Diff2.Y / Diff2.X);

                //}
                //Projectile.velocity;

                Projectile.velocity.X = Diff2.X;
                Projectile.velocity.Y = Diff2.Y;
                Projectile.velocity.Normalize();
                Projectile.velocity *= 3.1f;

                Projectile.light = 0.4f;
                lastplpos = owner.Center;
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(-45f);

                if (owner.whoAmI == Main.myPlayer)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(),Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X, Projectile.velocity.Y, ModContent.ProjectileType<SkyRapier2>(), 0, 0, owner.whoAmI);
                }

                first = false;
            }
            else
            {
                Projectile.netUpdate = true;

            }


            Projectile.position += owner.Center - lastplpos;

            lastplpos = owner.Center;

            int DDustID = Dust.NewDust(Projectile.Center-new Vector2(8,4), 0, 0, 226, Projectile.velocity.X * 0.4f, Projectile.velocity.Y * 0.4f, 100, default, 0.2f); //Spawns dust
            Main.dust[DDustID].noGravity = true;
            Main.dust[DDustID].velocity = 1.1f*Main.dust[DDustID].velocity.RotatedByRandom(MathHelper.ToRadians(10));

            if (Projectile.timeLeft < 4)
            {
                Projectile.alpha += 60;
                if (Projectile.timeLeft ==3)
                {
                    Projectile.velocity *= -0.3f;
                };

            }

            //if (Projectile.velocity.Y > 16f)
            //{
            //    Projectile.velocity.Y = 16f;
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
        //    Vector2 Vel = Projectile.velocity;
        //    Vel.Normalize();
        //    Rectangle test = new Rectangle((int)Projectile.Center.X + (int)Vel.X * -16, (int)Projectile.Center.Y + (int)Vel.Y * -16, 1,1);
        //    Rectangle test2 = new Rectangle((int)Projectile.Center.X+ (int)Vel.X*14, (int)Projectile.Center.Y + (int)Vel.Y * 14,1,1);
        //    Rectangle test3 = new Rectangle((int)Projectile.Center.X + (int)Vel.X * 46, (int)Projectile.Center.Y + (int)Vel.Y * 46, 1, 1);
        //    if (Projectile.owner == Main.myPlayer && Projectile.damage > 0)
        //    {
        //        Player Player = Main.player[Projectile.owner];
        //        for (int k = 0; k < 200; k++)
        //        {
        //            NPC curNPC = Main.npc[k];
        //            if ((((!curNPC.friendly || (curNPC.type == 22 && Projectile.owner < 255 && Player.killGuide) || (curNPC.type == 54 && Projectile.owner < 255 && Player.killClothier)))))
        //            {
        //                if ((test3.Intersects(curNPC.Hitbox)) || (test.Intersects(curNPC.Hitbox)) || (test2.Intersects(curNPC.Hitbox)))
        //                {
        //                    vasya = Projectile.damage - 17 + rnd.Next(34);
        //                    curNPC.StrikeNPC(vasya, Projectile.knockBack, (int)Projectile.velocity.ToRotation());
        //                    Player.addDPS(vasya);
        //            } }
        //        }
        //        if (Main.LocalPlayer.hostile)
        //        {
        //            for (int l = 0; l < 255; l++)
        //            {
        //                Player subPlayer = Main.player[l];
        //                if (l != Projectile.owner && subPlayer.active && !subPlayer.dead && !subPlayer.immune && subPlayer.hostile && Projectile.playerImmune[l] <= 0 && (Main.LocalPlayer.team == 0 || Main.LocalPlayer.team != subPlayer.team))
        //                {
        //                    if ((test3.Intersects(subPlayer.Hitbox)) || (test.Intersects(subPlayer.Hitbox)) || (test2.Intersects(subPlayer.Hitbox)))
        //                    {
        //                        subPlayer.HurtOld(Projectile.damage, (int)Projectile.knockBack);
        //                    }
        //                }
        //            }
        //        }
        //    }


        //    base.PostAI();
        //}







    }
}
