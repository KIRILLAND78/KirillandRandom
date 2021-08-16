using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using KirillandRandom;
namespace KirillandRandom.Items
{
	public class EyeOfDeath : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Eye Of Death");
			Tooltip.SetDefault("Your hp is limited to half of maximum hp.\r\nIncreases your damage by 30%\r\nIgnore instances of damage that would deal less than 30 hp.\r\nCan't use during bossfights.");
		}
		public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.width = 30;
			Item.height = 30;
			Item.value = Item.sellPrice(gold: 1);
			Item.rare = ItemRarityID.Red;
		}


        public override bool? UseItem(Player player)
        {

            for (int k = 0; k < 200; k++)
            {
                NPC curNPC = Main.npc[k];
                if ((curNPC.boss == true)&&(curNPC.active==true))
                {
                    return false;
                }
            }

            player.GetModPlayer<MPlayer>().eyeofdeath = !player.GetModPlayer<MPlayer>().eyeofdeath;
			return true;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.WallOfFleshBossBag, 1)
				.AddTile(TileID.Anvils)
				.Register();
		}

	}
}