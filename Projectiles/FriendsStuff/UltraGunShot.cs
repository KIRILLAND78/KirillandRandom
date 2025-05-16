using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace KirillandRandom.Projectiles.FriendsStuff
{
    internal class UltraGunShot : ModProjectile
    {
        List<Vector2> rPos = new();
        List<float> rDir = new();
        public override bool PreDraw(ref Color lightColor)
        {
            return false;
        }
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
        }
        public override void SetDefaults()
        {
            Projectile.penetrate = 10;
            Projectile.timeLeft = 100;
            Projectile.friendly = true;
            Projectile.extraUpdates = 12;

            base.SetDefaults();
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[Projectile.owner] = 2;
            base.OnHitNPC(target, hit, damageDone);
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
        }
        public override void AI()
        {
            //Main.NewText(Main.netMode);

            if ((Main.netMode == NetmodeID.SinglePlayer) || (Projectile.owner == Main.myPlayer)){
            }
            if ((Main.netMode == NetmodeID.SinglePlayer) || (Projectile.owner == Main.myPlayer))
                for (var i = 0; i < Main.maxProjectiles; i++)
                {

                    if (Main.projectile[i].active && (Main.projectile[i].owner == Projectile.owner) && (Main.projectile[i].type == ModContent.ProjectileType<UltraGunCoin>()))
                    {
                        //Main.NewText(Main.projectile[i].Center);
                        //Main.NewText(Projectile.Center);
                        //Main.NewText("===");
                        //Main.NewText("Dsas!");
                        if (((Main.projectile[i].Center - Projectile.Center).Length() < 30) || Collision.CheckAABBvLineCollision2(Main.projectile[i].Center - Main.projectile[i].Size * 2, Main.projectile[i].Size * 4, Projectile.Center, Projectile.Center - Projectile.velocity))
                        {
                            //string[] f = { "1", "2" };
                            //ChatHelper.BroadcastChatMessage(NetworkText.FromKey((Main.projectile[i].active).ToString(), f), Color.Wheat);
                            //Main.NewText("RecursionHit!");
                            Main.projectile[i].Kill();
                            //Main.projectile[i].netUpdate = true;
                            //Projectile.netUpdate = true;
                            //ChatHelper.BroadcastChatMessage(NetworkText.FromKey((Main.projectile[i].active).ToString(), f), Color.Wheat);
                            Projectile.damage = (int)(Projectile.damage * 1.2f);
                            int closest = -1;
                            int closestD = 99999;
                            for (var j = 0; j < Main.maxProjectiles; j++)
                            {
                                if (Main.projectile[j].active && (i != j) && (closestD > (Main.projectile[j].Center - Main.projectile[i].Center).Length()) && ((Main.projectile[j].owner == Projectile.owner) || (Main.netMode == NetmodeID.SinglePlayer)) && (Main.projectile[j].type == ModContent.ProjectileType<UltraGunCoin>()))
                                {
                                    closest = j;
                                    closestD = (int)(Main.projectile[j].Center - Main.projectile[i].Center).Length();
                                }
                            }
                            Projectile.Center = Main.projectile[i].Center;

                            int DDustID = Dust.NewDust(Projectile.Center, 2, 2, DustID.GoldCoin, Projectile.velocity.X * 0.4f, Projectile.velocity.Y * 0.4f, 100, default, 0.8f); //Spawns dust
                            Main.dust[DDustID].noGravity = true;
                            Main.dust[DDustID].velocity = 0.8f * Main.dust[DDustID].velocity.RotatedByRandom(MathHelper.ToRadians(2));
                            if (closest < 0)
                            {
                                //aim at nearest enemy
                                for (var j = 0; j < Main.maxNPCs; j++)
                                {
                                    if (Main.npc[j].active && (!Main.npc[j].dontTakeDamage) && (!Main.npc[j].friendly) && (closestD > (Main.npc[j].Center - Main.projectile[i].Center).Length()))
                                    {
                                        
                                        closest = j;
                                        closestD = (int)(Main.npc[j].Center - Main.projectile[i].Center).Length();
                                    }
                                }
                                if (closest >= 0)
                                {
                                    //launch at enemy
                                    var vec = Main.npc[closest].Center - Main.projectile[i].Center;

                                rDir.Add(Projectile.position.AngleFrom(rPos.Last()));
                                    rPos.Add(Projectile.position);
                                    vec.Normalize();
                                    vec *= 24;
                                    Projectile.velocity = vec;
                                }
                                //do nothing

                            }
                            else
                            {
                                //launch at coin
                                var vec = Main.projectile[closest].Center - Main.projectile[i].Center;
                                vec.Normalize();
                                vec *= 24;
                                Projectile.velocity = vec;

                                rDir.Add(Projectile.position.AngleFrom(rPos.Last()));
                                rPos.Add(Projectile.position);
                            }
                            break;
                        }

                    }
                }
            Projectile.rotation = Projectile.velocity.AngleFrom(Vector2.Zero);
            base.AI();
        }
        public override void OnSpawn(IEntitySource source)
        {
                                rDir.Add(Projectile.direction);
                                rPos.Add(Projectile.position);
            base.OnSpawn(source);
        }
        public override void OnKill(int timeLeft)
        {
                                rDir.Add(Projectile.position.AngleFrom(rPos.Last()));
            rPos.Add(Projectile.position);
                            Main.player[Projectile.owner].GetModPlayer<TrailDrawModSystem>().trails.Add(new Trail(){entitySize= Projectile.Size, positions = rPos, rotations = rDir});
            base.OnKill(timeLeft);
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return base.Colliding(projHitbox, targetHitbox);
        }
    }
}
