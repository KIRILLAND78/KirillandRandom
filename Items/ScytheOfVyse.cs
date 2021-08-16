using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Text;
using KirillandRandom.NPCs;
using KirillandRandom.Projectiles;

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
			Item.mana = 200;
			Item.DamageType = DamageClass.Magic;
			Item.width = 40;
			Item.height = 40;
			Item.value = 10000;
			Item.shoot = ModContent.ProjectileType<Hex>();
			Item.damage = 10;
			Item.shootSpeed = 4;
			Item.useTime = 50;
			Item.useAnimation = 50;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 0;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.rare = ItemRarityID.Cyan;
			Item.UseSound = SoundID.Item117;
			Item.autoReuse = false;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.GravityGlobe, 1)
				.AddIngredient(ItemID.LunarBar, 10)
				.AddTile(TileID.LunarCraftingStation)
				.Register();
		}

	}
}