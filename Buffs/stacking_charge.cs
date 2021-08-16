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
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            DisplayName.SetDefault("Stacking charge");
            Description.SetDefault("Charged!");
            base.SetStaticDefaults();
        }

        public override void Update(Player Player, ref int buffIndex)
        {
            Player.GetModPlayer<MPlayer>().charge = true;
        }
        public override void Update(NPC NPC, ref int buffIndex)
        {
            NPC.GetGlobalNPC<MNPC>().charge = true;
        }
    }
}
