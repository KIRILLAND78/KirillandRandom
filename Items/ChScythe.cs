using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Text;


namespace KirillandRandom.Items
{
	public class ChScythe : ModItem
	{
		public int first = 1;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Charged Scythe");
			Tooltip.SetDefault("Charge enemies with right click.\r\nDischarge enemies with left click for bonus damage.\r\n+50 bonus damage for each stored charge.");
		}
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		public override void HoldItem(Player player)
		{
			base.HoldItem(player);
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				item.channel = false;
				item.shoot = mod.ProjectileType("ChScythe");//BIG SCYTHE
				item.shootSpeed = 17;
				item.useTime = 60;
				item.useStyle = ItemUseStyleID.SwingThrow;
				item.useAnimation = 60;
				item.autoReuse = true;
				item.damage = 90;
				item.UseSound = SoundID.DD2_SkyDragonsFuryShot;
			}
			else
			{
				item.shoot = mod.ProjectileType("ChScytheSpin");//SPINNING SCYTHE
				item.useTime = 20;
				item.autoReuse = false;

				item.channel = true;
				item.shootSpeed = 0;
				item.useAnimation = (int)(20 / player.meleeSpeed);
				item.useStyle = ItemUseStyleID.SwingThrow;
				item.damage = 80;
				item.UseSound = SoundID.DD2_SkyDragonsFurySwing;
			}
			return true;
		}
        public override void SetDefaults()
		{
			
			item.noUseGraphic = true;
			item.damage = 90;
			item.noMelee = true;
			item.useTime = 10;
			item.shootSpeed = 0;
			item.useAnimation = 10;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 0;
			item.value = 10000;
			item.rare = ItemRarityID.Cyan;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;

			item.melee = true;
		}


        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);

			recipe.AddIngredient(ItemID.MartianConduitPlating, 50);
			recipe.AddIngredient(ItemID.InfluxWaver, 1);
			recipe.AddIngredient(ItemID.ChargedBlasterCannon, 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
			
		}

	}
}
