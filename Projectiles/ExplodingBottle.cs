using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using KirillandRandom.NPCs;
using Terraria.ID;
using KirillandRandom;
using Terraria.DataStructures;

namespace KirillandRandom.Projectiles
{
    public class ExplodingBottle : ModProjectile
    {
        public bool first = true;
        public override void SetDefaults()
        {

            Projectile.Name = "ExplodingBottle";
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.timeLeft = 120;
            Projectile.penetrate = 1;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.aiStyle = 8;
        }

        public override void Kill(int timeLeft)
        {
            //Projectile.NewProjectile(new EntitySource_ByProjectileSourceId(Projectile.whoAmI), Projectile.position - new Vector2(-10f, 10f), new Vector2(0f, 0f), ProjectileID.DD2ExplosiveTrapT1Explosion, 60, 0);
        }
        public override void AI()
        {

            //Player owner = Main.player[Projectile.owner];
            //if (first) { 
            //int id = NPC.NewNPC(new EntitySource_ByProjectileSourceId(Projectile.whoAmI), (int)Projectile.position.X, (int)Projectile.position.Y, ModContent.NPCType<NPC_Dog>());
            //NPC summonedNPC = Main.npc[id];
            //MNPC modSummonedNPC = summonedNPC.GetGlobalNPC<MNPC>();
            //modSummonedNPC.barrel = Projectile;
            //    first = false;
            //        }







            //int DDustID = Dust.NewDust(Projectile.position - new Vector2(2f, 2f), Projectile.width + 4, Projectile.height + 4, 17, Projectile.velocity.X * 0.4f, Projectile.velocity.Y * 0.4f, 100, default(Color), 1.1f); //Spawns dust
            //Main.dust[DDustID].noGravity = true;



        }
    }
}
