using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using KirillandRandom.NPCs;

namespace KirillandRandom.Buffs
{
    class stacking_charge : ModBuff
    {
        public override void SetDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            DisplayName.SetDefault("Stacking charge");
            Description.SetDefault("Charge!");
            base.SetDefaults();
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<MPlayer>().charge = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<MNPC>().charge = true;
        }
    }
}
