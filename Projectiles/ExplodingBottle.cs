using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using KirillandRandom.NPCs;
using Terraria.ID;
using KirillandRandom;

namespace KirillandRandom.Projectiles
{
    public class ExplodingBottle : ModProjectile
    {
        public bool first = true;
        public override void SetDefaults()
        {

            projectile.Name = "ExplodingBottle";
            projectile.width = 20;
            projectile.height = 20;
            projectile.timeLeft = 120;
            projectile.penetrate = 1;
            projectile.friendly = false;
            projectile.hostile = false;
            projectile.tileCollide = true;
            projectile.ignoreWater = false;
            projectile.ranged = true;
            projectile.aiStyle = 8;
        }

        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(projectile.position - new Vector2(-10f, 10f), new Vector2(0f, 0f), ProjectileID.DD2ExplosiveTrapT1Explosion, 60, 0);
        }
        public override void AI()
        {

            Player owner = Main.player[projectile.owner];
            if (first) { 
            int id = NPC.NewNPC((int)projectile.position.X, (int)projectile.position.Y, ModContent.NPCType<NPC_Dog>());
            NPC summonedNPC = Main.npc[id];
            MNPC modSummonedNPC = summonedNPC.GetGlobalNPC<MNPC>();
            modSummonedNPC.barrel = projectile;
                first = false;
                    }







            //int DDustID = Dust.NewDust(projectile.position - new Vector2(2f, 2f), projectile.width + 4, projectile.height + 4, 17, projectile.velocity.X * 0.4f, projectile.velocity.Y * 0.4f, 100, default(Color), 1.1f); //Spawns dust
            //Main.dust[DDustID].noGravity = true;



        }
    }
}
