using KirillandRandom.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace KirillandRandom.Buffs
{
    class stacking_charge : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
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
