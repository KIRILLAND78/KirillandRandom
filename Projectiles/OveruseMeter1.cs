using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;



namespace KirillandRandom.Projectiles
{
    public class OveruseMeter1 : ModProjectile
    {
        public Item Sword;
        public bool first = true;
        public override void SetDefaults()
        {
            projectile.Name = "OVMETER(how? just how?)";
            projectile.width = 12;
            projectile.height = 12;
            projectile.timeLeft = 720000;
            projectile.penetrate = 1;
            projectile.friendly = false;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.ranged = true;
            projectile.aiStyle = 0;
        }
        public override void AI()
        {
            Player owner = Main.player[projectile.owner];
            if (owner.GetModPlayer<MPlayer>().overuse > 100) { owner.GetModPlayer<MPlayer>().overuse = 100; }

            if (owner.GetModPlayer<MPlayer>().overuse < -100) { owner.GetModPlayer<MPlayer>().overuse = -100; }
            projectile.position.X = owner.Center.X - 2 + (owner.GetModPlayer<MPlayer>().overuse / 2.6f);

            projectile.position.Y = owner.Center.Y+40;


            if (first)
            {
                Sword = owner.HeldItem;
                first = false;
            }

            if (owner.HeldItem != Sword)
            {
                projectile.Kill();
                Main.player[projectile.owner].GetModPlayer<MPlayer>().OveruseMeterCreated = false;
            }



        }
    }
}

