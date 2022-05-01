using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using KirillandRandom.Dusts;
using KirillandRandom.Primitives;
using Terraria.DataStructures;
using KirillandRandom.Items;


namespace KirillandRandom.Projectiles
{
    internal class TouhouKnives_proj: ModProjectile
    {
        bool first = true;
        public override void SetDefaults()
        {
            Projectile.friendly=true;
            Projectile.hostile = false;
            Projectile.width = 100;
            Projectile.height = 100;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 90;
            //Projectile.damage = 20;
            Projectile.width = 30;
            Projectile.penetrate = 1;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.height = 30;
            base.SetDefaults();
        }
        public override void Kill(int timeLeft)
        {
            int DDustID = Dust.NewDust(Projectile.Center-new Vector2(2,2), 4 , 4 , DustID.PurificationPowder, 0, 0, 100, default, 1f); //Spawns dust
            Main.dust[DDustID].noGravity = true;
            Main.dust[DDustID].noLight = true;
            base.Kill(timeLeft);
        }
        public override void AI()
        {
            if (first)
            {
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45);
            }
            if (Projectile.ai[0] > 0)
            {
                Projectile.timeLeft++;
                Projectile.position -= Projectile.velocity;
                Projectile.ai[0]--;
            }
            
            first = false;
            Player owner = Main.player[Projectile.owner];
            
            if ((owner.itemAnimation>owner.itemAnimationMax-1)&&(owner.controlUseItem) && (Projectile.ai[1]==0) && (owner.HeldItem.type==ModContent.ItemType<TouhouKnives>()) && (owner.CheckMana(owner.GetManaCost(owner.HeldItem))) && (Projectile.timeLeft < 89)&& (owner.altFunctionUse == 2))
            {
                Projectile.ai[1] = 1;
                Projectile.ai[0] = 15;
                for (int i = 0; i < 6; i++)
                {
                    int DDustID = Dust.NewDust(Projectile.Center - new Vector2(2, 2)+new Vector2(20,0).RotatedBy(MathHelper.TwoPi/6*i), 4, 4, DustID.PurificationPowder, 0, 0, 100, default, 1f); //Spawns dust
                    Main.dust[DDustID].noGravity = true;
                    Main.dust[DDustID].noLight = true;
                }
                for (int i = 0; i <= 3; i++)
                {
                    Vector2 Pos = Projectile.position+new Vector2(20,0).RotateRandom(MathHelper.TwoPi);
                    int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Pos, Projectile.velocity.RotateRandom(MathHelper.PiOver2 / 6), Projectile.type, Projectile.damage, Projectile.knockBack, Projectile.owner);
                    Main.projectile[proj].timeLeft = Projectile.timeLeft;
                    Main.projectile[proj].ai[0]=15;
                    Main.projectile[proj].ai[1] = 1;
                }
            }
        }

    }
}
