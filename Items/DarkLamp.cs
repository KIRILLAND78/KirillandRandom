using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Text;
using KirillandRandom.Projectiles;
using Terraria.DataStructures;

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
			Item.width = 10;
			Item.height = 12;
			Item.holdStyle = 1;
			//Item.noWet = true;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 120;
			Item.useTime = 120;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.flame = true;
			Item.value = 50;


			Item.noMelee = true;
			Item.shoot = ModContent.ProjectileType<DarkLampLight>();
			Item.shootSpeed = 0;
			Item.damage = 101;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 120;
			Item.knockBack = 10;
			Item.value = 10000;
			Item.rare = ItemRarityID.LightRed;
			Item.UseSound = SoundID.Item34;
		}
		public override void HoldItem(Player Player)
		{
			if (Main.rand.Next(Player.itemAnimation > 0 ? 40 : 80) == 0)
			{
				Dust.NewDust(new Vector2(Player.itemLocation.X + 12f * Player.direction, Player.itemLocation.Y - 12f * Player.gravDir), 4, 4, 6);
			}
			Vector2 position = Player.RotatedRelativePoint(new Vector2(Player.itemLocation.X + 12f * Player.direction + Player.velocity.X, Player.itemLocation.Y - 14f + Player.velocity.Y), true);
			Lighting.AddLight(position, 1f, 1f, 1f);
		}
        public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			player.AddBuff(BuffID.OnFire, 340);
			return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
		public override void AddRecipes()
		{
            CreateRecipe()
			.AddIngredient(ItemID.AdamantiteBar, 8)

            .AddIngredient(ItemID.LivingFireBlock, 50)

            .AddIngredient(ItemID.SoulofNight, 10)

            .AddIngredient(ItemID.SoulofLight, 10)
                .AddTile(TileID.MythrilAnvil)
				.Register();

			CreateRecipe()

			.AddIngredient(ItemID.TitaniumBar, 8)

			.AddIngredient(ItemID.LivingFireBlock, 50)

			.AddIngredient(ItemID.SoulofNight, 10)

			.AddIngredient(ItemID.SoulofLight, 10)
				.AddTile(TileID.MythrilAnvil)
				.Register();


		}


	}
}
