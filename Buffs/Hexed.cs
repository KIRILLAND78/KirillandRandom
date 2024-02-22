using Terraria;
using Terraria.ModLoader;

namespace KirillandRandom.Buffs
{
    class Hexed : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.lightPet[Type] = false;
            Main.debuff[Type] = false;
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