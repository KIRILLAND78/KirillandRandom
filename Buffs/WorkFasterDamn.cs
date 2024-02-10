using Terraria;
using Terraria.ModLoader;

namespace KirillandRandom.Buffs
{
    class WorkFasterDamn : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.lightPet[Type] = false;
            Main.debuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
            base.SetStaticDefaults();
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.pickSpeed *= 0.75f;
        }
    }
}