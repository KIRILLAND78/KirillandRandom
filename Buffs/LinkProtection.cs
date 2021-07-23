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
    class LinkProtection : ModBuff
    {
        public override void SetDefaults()
        {
            Main.lightPet[Type] = false;
            Main.debuff[Type] = false;
            DisplayName.SetDefault("Protected");
            Description.SetDefault("+20 defense\r\nYou feel much safer.");
            Main.buffNoTimeDisplay[Type] = false;
            base.SetDefaults();
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 20;
        }
    }
}