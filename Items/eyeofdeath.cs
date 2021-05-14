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
			item.useStyle = ItemUseStyleID.HoldingUp;
			item.useTime = 20;
			item.useAnimation = 20;
			item.width = 30;
			item.height = 30;
			item.value = Item.sellPrice(gold: 1);
			item.rare = ItemRarityID.Red;
		}

        public override bool UseItem(Player player)
        {
            for (int k = 0; k < 200; k++)
            {
                NPC curNPC = Main.npc[k];
                if ((curNPC.boss == true)&&(curNPC.active==true))
                {
                    return false;
                }
            }

            player.GetModPlayer<MPlayer>().eyeofdeath = player.GetModPlayer<MPlayer>().eyeofdeath ? false : true;
			return true;
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