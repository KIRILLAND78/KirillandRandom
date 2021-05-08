using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using KirillandRandom;
namespace KirillandRandom.Items
{
	public class firering : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ring of Flaming Dedication");
			Tooltip.SetDefault("Every attack inflicts 'On Fire!' for a second.\r\nTaking damage sets melee attacker 'On Fire!' for two seconds.\r\nTaking damage also inflict 'On Fire!' on you for two second.");
		}
		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 30;
			item.accessory = true;
			item.value = Item.sellPrice(gold: 1);
			item.rare = ItemRarityID.Orange;
		}



        public override void UpdateAccessory(Player player, bool hideVisual)
		{
				player.GetModPlayer<MPlayer>().flamingdedication = true; 
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