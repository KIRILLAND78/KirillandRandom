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
	public class SkyRapier_item : ModItem
	{

		public override void SetStaticDefaults()
		{

			DisplayName.SetDefault("Rapier of Sky"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("Pierce with speed of heaven!\r\nProbably buggy! Please report any found bugs!");
		}

		public override void SetDefaults()
		{
			item.noUseGraphic = true;
			item.shoot = mod.ProjectileType("SkyRapier");
			item.melee = true;
			item.width = 0;
			item.noMelee = true;
			item.height = 0;
			item.useTime = 6;
			item.useAnimation = 6;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 6;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			//todo
			item.shootSpeed = 18;
			item.damage = 88;
			item.value = 10000;
			item.rare = ItemRarityID.Cyan;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Nanites, 50);
			recipe.AddIngredient(ItemID.TitaniumBar, 4);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();

			ModRecipe recipe2 = new ModRecipe(mod);
			recipe2.AddIngredient(ItemID.Nanites, 50);
			recipe2.AddIngredient(ItemID.AdamantiteBar, 4);
			recipe2.AddTile(TileID.MythrilAnvil);
			recipe2.SetResult(this);
			recipe2.AddRecipe();
		}

	}
}
