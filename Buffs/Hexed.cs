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
    class Hexed : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.lightPet[Type] = false;
            Main.debuff[Type] = false;
            DisplayName.SetDefault("Hexed");
            Description.SetDefault("-60 defense\r\nYou are pig now\r\nYou can't attack and your wings disabled.");
            Main.buffNoTimeDisplay[Type] = false;
            base.SetStaticDefaults();
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense -= 60;
            player.GetModPlayer<MPlayer>().Hexed = true;
        }
    }
}