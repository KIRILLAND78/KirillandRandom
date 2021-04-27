using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using KirillandRandom;
namespace KirillandRandom.Items
{
	public class eyeofdeath : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Your hp is limited to half of maximum hp.\r\nReduces incoming damage by 10%, increases your damage by 30%\r\nIgnore instances of damage that deal less than 15 hp.//TODO\r\nEquiping this during bossfight will get you punished.//TODO");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.accessory = true;
			item.value = Item.sellPrice(gold: 1);
			item.rare = ItemRarityID.Red;
		}



		public override void UpdateAccessory(Player player, bool hideVisual)
		{

			player.GetModPlayer<MPlayer>().eyeofdeath = true; 
			player.endurance += 0.1f;
			player.allDamageMult += 0.3f; // The acceleration multiplier of the player's movement speed
		}

		//public override void AddRecipes()
		//{
		//	ModRecipe recipe = new ModRecipe(mod);
		//	recipe.AddIngredient(ItemID.DirtBlock, 2);
		//	recipe.AddTile(TileID.Anvils);
		//	recipe.SetResult(this);
		//	recipe.AddRecipe();
		//}
	}
}