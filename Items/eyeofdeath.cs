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
			DisplayName.SetDefault("Eye Of Death");
			Tooltip.SetDefault("Your hp is limited to half of maximum hp.\r\nReduces incoming damage by 10%, increases your damage by 30%\r\nIgnore instances of damage that would deal less than 25 hp.\r\nCan't equip during bossfights.");
		}
		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 30;
			item.accessory = true;
			item.value = Item.sellPrice(gold: 1);
			item.rare = ItemRarityID.Red;
		}
        public override bool CanEquipAccessory(Player player, int slot)
        {
			for (int k = 0; k < 200; k++)
			{
				NPC curNPC = Main.npc[k];
				if (curNPC.boss == true)
				{
					return false;
				}
			}
			return base.CanEquipAccessory(player, slot);
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