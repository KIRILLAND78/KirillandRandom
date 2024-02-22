using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;



namespace KirillandRandom.Projectiles
{
    public class UndeadSlash : ModProjectile
    {
        public Player owner;
        public int timer_d = 0;
        public int slashes = 0;
        public NPC targetd = null;
        public Vector2 PlayerPos;
        public Vector2 lastplpos;
        bool first = true;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.Name = "Soul Slash";
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.timeLeft = 40;
            Projectile.penetrate = 9999;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Melee;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value.Height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Color.White * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, Projectile.oldRot[k], drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            }
            return true;

        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target == targetd)
            {
                slashes++;
                timer_d = 9;
            }

            if (targetd == null)
            {
                targetd = target;
            }

            base.OnHitNPC(target, hit, damageDone);
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
                if ((test2.Intersects(target.Hitbox)) || (test3.Intersects(target.Hitbox)) || (test.Intersects(target.Hitbox)) || (test1.Intersects(target.Hitbox)))
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
        public override void OnKill(int timeLeft)
        {

            owner.GetModPlayer<MPlayer>().targetd = null;
            Main.player[Projectile.owner].teleporting = true;
            Main.player[Projectile.owner].teleportTime = 2;
            Main.player[Projectile.owner].Teleport(PlayerPos, 6, 1);
            base.OnKill(timeLeft);
        }


        public override void AI()
        {

            if (Projectile.position.X < 40) Projectile.position.X = 40;
            if (Projectile.position.Y < 40) Projectile.position.Y = 40;

            if (Projectile.position.X > Main.maxTilesX * 16 - 40) Projectile.position.X = Main.maxTilesX * 16 - 40;
            if (Projectile.position.Y > Main.maxTilesY * 16 - 40) Projectile.position.Y = Main.maxTilesY * 16 - 40;
            timer_d--;
            owner = Main.player[Projectile.owner];
            owner.direction = ((MathHelper.ToDegrees(Projectile.rotation) + 45) < 180 && ((MathHelper.ToDegrees(Projectile.rotation) + 45)) > 0) ? 1 : -1;
            if (owner.dead)
            {
                Projectile.Kill();
            }
            if (first)
            {
                PlayerPos = owner.position;

                Projectile.light = 0.4f;
                first = false;
            }
            else
            {
                Projectile.netUpdate = true;
            }
            owner.SetImmuneTimeForAllTypes(5);
            owner.Center = Projectile.Center - Projectile.velocity * 0.7f;

            if (targetd != null)
            {
                owner.GetModPlayer<MPlayer>().targetd = targetd;
                Projectile.timeLeft = 4;
                owner.itemAnimation = 4;
                owner.itemTime = 4;

                //slashes
                if ((timer_d == 0) || ((owner.Center - targetd.Center).Length() >= 200))
                {
                    int GreenFl = Dust.NewDust(owner.Center, 2, 2, DustID.Clentaminator_Green, 0, 0, 0, default, 1f);

                    GreenFl = Dust.NewDust(owner.Center, 2, 2, DustID.Clentaminator_Green, 0, 0, 0, default, 1f);

                    if (Main.myPlayer == Projectile.owner)
                    {
                        if (slashes == 6)
                        {
                            if (!Main.mouseLeft || owner.GetManaCost(owner.HeldItem) > owner.statMana)
                            {
                                Projectile.Kill();
                                return;
                            }
                            else
                            {
                                slashes = 0;
                                owner.statMana -= owner.GetManaCost(owner.HeldItem);
                            }
                        }

                        if (!targetd.active)
                        {
                            Projectile.Kill();
                            return;
                        }
                    }
                    if ((owner.Center - targetd.Center).Length() >= 200)
                    {
                        timer_d = -1;
                    }
                    Projectile.Center = targetd.Center + new Vector2(175, 0).RotatedByRandom(MathHelper.ToRadians(360));
                    Projectile.velocity = targetd.Center - Projectile.Center;
                    Projectile.velocity.Normalize();
                    Projectile.velocity *= 27;
                    Main.player[Projectile.owner].teleporting = true;
                    Main.player[Projectile.owner].teleportTime = 2;
                    Main.player[Projectile.owner].Teleport(Projectile.Center - Projectile.velocity * 0.5f, 6, 1);

                    GreenFl = Dust.NewDust(owner.Center, 2, 2, DustID.Clentaminator_Green, 0, 0, 0, default, 1f);
                    GreenFl = Dust.NewDust(owner.Center, 2, 2, DustID.Clentaminator_Green, 0, 0, 0, default, 1f);
                }


            }

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);

            for (int k = Projectile.oldRot.Length - 1; k > 0; k--)
            {
                Projectile.oldRot[k] = Projectile.oldRot[k - 1];
            }
            Projectile.oldRot[0] = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
        }


        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            int size = 500;
            hitbox.X -= size;
            hitbox.Y -= size;
            hitbox.Width += size * 2;
            hitbox.Height += size * 2;
        }
    }
}
