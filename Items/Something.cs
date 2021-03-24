using Terraria.ID;
using Terraria.ModLoader;
using Terraria;


namespace KirillandRandom.Items
{
	public class Curiosity : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Something"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("UGA-CHAGA!!!");
		}

		public override void SetDefaults()
		{
			item.damage = 50;
			item.melee = true;
			item.width = 40;
			item.height = 90;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = ItemRarityID.Expert;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = ProjectileID.ChargedBlasterOrb;
			item.shootSpeed = 20;
		}
		//public override void CanEquipAccessory(Player player, int slot){		}






		//public override void AltFunctionUse(Player player){		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		public override void UpdateInventory(Player player)
		{
			//item.useTime = 32+Convert.ToInt32(item.TotalUseTime);

		}

	}
}