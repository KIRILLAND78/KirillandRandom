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
	public class DarkLamp : ModItem
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cursed Candle");
			Tooltip.SetDefault("Burns self on use.");
		}




		public override void SetDefaults()
		{
			item.width = 10;
			item.height = 12;
			item.holdStyle = 1;
			//item.noWet = true;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 120;
			item.useTime = 120;
			item.useStyle = ItemUseStyleID.HoldingUp;
			item.flame = true;
			item.value = 50;


			item.noMelee = true;
			item.shoot = mod.ProjectileType("DarkLampLight");
			item.shootSpeed = 0;
			item.damage = 95;
			item.magic = true;
			item.mana = 120;
			item.knockBack = 10;
			item.value = 10000;
			item.rare = ItemRarityID.LightRed;
			item.UseSound = SoundID.Item34;
		}
		public override void HoldItem(Player player)
		{
			if (Main.rand.Next(player.itemAnimation > 0 ? 40 : 80) == 0)
			{
				Dust.NewDust(new Vector2(player.itemLocation.X + 12f * player.direction, player.itemLocation.Y - 12f * player.gravDir), 4, 4, 6);
			}
			Vector2 position = player.RotatedRelativePoint(new Vector2(player.itemLocation.X + 12f * player.direction + player.velocity.X, player.itemLocation.Y - 14f + player.velocity.Y), true);
			Lighting.AddLight(position, 1f, 1f, 1f);
		}
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			player.AddBuff(BuffID.OnFire, 340);

			return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.AdamantiteBar, 8);
			recipe.AddIngredient(ItemID.LivingFireBlock, 50);
			recipe.AddIngredient(ItemID.SoulofNight, 10);
			recipe.AddIngredient(ItemID.SoulofLight, 10);
			recipe.SetResult(this);
			recipe.AddRecipe();
			ModRecipe recipe2 = new ModRecipe(mod);
			recipe2.AddIngredient(ItemID.TitaniumBar, 8);
			recipe2.AddIngredient(ItemID.LivingFireBlock, 50);
			recipe2.AddIngredient(ItemID.SoulofNight, 10);
			recipe2.AddIngredient(ItemID.SoulofLight, 10);
			recipe2.SetResult(this);
			recipe2.AddRecipe();
		}

	}
}
