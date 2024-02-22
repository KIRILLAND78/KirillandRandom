using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KirillandRandom.Items
{
    class FierySilk : ModItem
    {
        public override void SetDefaults()
        {
            Item.flame = true;
            Item.width = 26;
            Item.height = 28;
            Item.value = 0;
            Item.rare = ItemRarityID.Orange;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.LivingFireBlock, 5)
                .AddIngredient(ItemID.SoulofLight, 1)
                .AddIngredient(ItemID.SoulofNight, 1)
                .AddIngredient(ItemID.Silk, 1)
                .AddTile(TileID.Anvils)
                .Register();
        }


    }
}
