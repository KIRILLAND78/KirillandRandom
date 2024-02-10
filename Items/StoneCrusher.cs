using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace KirillandRandom.Items
{
    internal class StoneCrusher : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 80;
            Item.knockBack = 20;
            Item.DamageType = DamageClass.Melee;

            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            base.SetDefaults();
        }
    }
}
