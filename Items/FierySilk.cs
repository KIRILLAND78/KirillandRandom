using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KirillandRandom.Items
{
    class FierySilk: ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Fiery Silk");
			Tooltip.SetDefault("Magic-infused silk. Seems to have some curious properties.");
		}

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
				.AddIngredient(ItemID.LivingFireBlock, 10)
				.AddIngredient(ItemID.SoulofLight, 2)
				.AddIngredient(ItemID.SoulofNight, 2)
				.AddIngredient(ItemID.Silk, 1)
				.AddTile(TileID.Anvils)
				.Register();
		}


	}
}
