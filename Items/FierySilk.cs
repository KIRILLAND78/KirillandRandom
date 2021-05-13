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
			item.flame = true;
			item.width = 26;
			item.height = 28;
			item.value = 0;
			item.rare = ItemRarityID.Orange;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LivingFireBlock, 10);
			recipe.AddIngredient(ItemID.SoulofLight, 1);
			recipe.AddIngredient(ItemID.SoulofNight, 1);
			recipe.AddIngredient(ItemID.Silk, 1);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}


	}
}
