using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Text;
using KirillandRandom.NPCs;

namespace KirillandRandom.Items
{
	public class ScytheOfVyse : ModItem
	{

		public override void SetStaticDefaults()
		{

			DisplayName.SetDefault("Scythe Of Vyse"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("Turns people (players) into pigs.");
		}


		public override void SetDefaults()
		{
			item.mana = 200;
			item.magic= true;
			item.width = 40;
			item.height = 40;
			item.value = 10000;
			item.shoot = mod.ProjectileType("Hex");
			item.damage = 10;
			item.shootSpeed = 4;
			item.useTime = 50;
			item.useAnimation = 50;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 0;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.rare = ItemRarityID.Cyan;
			item.UseSound = SoundID.Item117;
			item.autoReuse = false;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);

			recipe.AddIngredient(ItemID.GravityGlobe, 1);
			recipe.AddIngredient(ItemID.LunarBar, 10);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();

		}

	}
}