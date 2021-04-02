using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace KirillandRandom.Buffs
{
    class CrystalPB : ModBuff
    {
        public override void SetDefaults()
        {
            Main.lightPet[Type] = true;
            Main.debuff[Type] = false;
            DisplayName.SetDefault("Corrupted royal crystal");
            Description.SetDefault("");
            Main.buffNoTimeDisplay[Type] = true;
            base.SetDefaults();
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.manaRegenBonus += 50;
            player.buffTime[buffIndex] = 18000;
            player.GetModPlayer<MPlayer>().Something = true;
            bool petProjectileNotSpawned = player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Pets.CrystalP>()] <= 0;
            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, ModContent.ProjectileType<Projectiles.Pets.CrystalP>(), 0, 0f, player.whoAmI, 0f, 0f);
            }
        }
    }
}
