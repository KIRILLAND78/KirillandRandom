using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

//Now needed for proje sources
using Terraria.DataStructures;

namespace KirillandRandom.Buffs
{
    class CrystalPB : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.lightPet[Type] = true;
            Main.debuff[Type] = false;
            DisplayName.SetDefault("Crystal");
            Description.SetDefault("WIP. INF MANA");
            Main.buffNoTimeDisplay[Type] = true;
            base.SetStaticDefaults();
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.statMana+= 100;
            player.buffTime[buffIndex] = 18000;
            player.GetModPlayer<MPlayer>().Something = true;
            bool petProjectileNotSpawned = player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Pets.CrystalP>()] <= 0;
            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
            {
                
                Projectile.NewProjectile(new EntitySource_Buff(player, default, buffIndex), player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, ModContent.ProjectileType<Projectiles.Pets.CrystalP>(), 0, 0f, player.whoAmI, 0f, 0f);
            }
        }
    }
}
