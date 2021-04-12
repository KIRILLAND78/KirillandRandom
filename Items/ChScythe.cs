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
			Tooltip.SetDefault("Charge enemies with right click.\r\nDischarge enemies with left click.");
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
				item.shoot = mod.ProjectileType("ChScytheSpin");//SPINNING SCYTHE
				item.useTime = 30;

				item.shootSpeed = 0;
				item.useAnimation = 30;
				item.useStyle = ItemUseStyleID.SwingThrow;
				item.damage = 60;
				item.UseSound = SoundID.DD2_ExplosiveTrapExplode;
			}
			else
			{
				player.GetModPlayer<MPlayer>().flames_summoned = 0;
				item.shoot = mod.ProjectileType("ChScythe");//BIG SCYTHE
				item.shootSpeed = 50;
				item.useTime = 90;
				item.useStyle = ItemUseStyleID.SwingThrow;
				item.useAnimation = 90;
				item.value = 10000;
				item.UseSound = SoundID.DD2_FlameburstTowerShot;
			}
			return true;
		}
		public override void SetDefaults()
		{


			item.noUseGraphic = true;
			item.damage = 60;
			item.noMelee = true;
			item.magic = true;
			item.useTime = 10;
			item.shootSpeed = 0;
			item.useAnimation = 10;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 0;
			item.value = 10000;
			item.rare = ItemRarityID.Purple;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;

			item.melee = true;
		}
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (player.altFunctionUse == 2)
			{
				Projectile.NewProjectile(position.X, position.Y, 0, 0, mod.ProjectileType("ChScytheSpin"), 60, 0, Main.myPlayer, 1f, 0);
			}
			return true;
        }



        public override void AddRecipes()
		{
			/**
			ModRecipe recipe = new ModRecipe(mod);

			recipe.AddIngredient(ItemID.DemoniteBar, 5);
			recipe.AddIngredient(ItemID.DemonScythe, 1);
			recipe.AddIngredient(ItemID.SoulofNight, 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
			**/
		}

	}
}
