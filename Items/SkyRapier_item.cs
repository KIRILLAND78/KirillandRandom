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
	public class SkyRapier_item : ModItem
	{

		public override void SetStaticDefaults()
		{

			DisplayName.SetDefault("Brilliancy");
		}

		public override void SetDefaults()
		{
			Item.noUseGraphic = true;
			Item.shoot = ModContent.ProjectileType<SkyRapier>();
			Item.DamageType = DamageClass.Melee;
			Item.width = 0;
			Item.noMelee = true;
			Item.height = 0;
			Item.useTime = 4;
			Item.useAnimation = 4;
			Item.useStyle = ItemUseStyleID.Rapier;//Testing out new weapon anims
			Item.knockBack = 4;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.shootSpeed = 18;
			Item.damage = 88;
			Item.value = 10000;
			Item.rare = ItemRarityID.Cyan;
			Item.channel = true;
        }


		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.Nanites, 50)
				.AddIngredient(ItemID.TitaniumBar, 4)
				.AddTile(TileID.MythrilAnvil)
				.Register();
			CreateRecipe()
				.AddIngredient(ItemID.Nanites, 50)
				.AddIngredient(ItemID.AdamantiteBar, 4)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			Vector2 MousePos = new Vector2(Main.MouseWorld.X, Main.MouseWorld.Y);
			Vector2 PlayerPos = player.Center;
			Vector2 Diff = MousePos - PlayerPos;
			Vector2 DiffRand = Diff.RotatedByRandom(MathHelper.ToRadians(45));
			Vector2 nposition = player.Center+(30 * Vector2.Normalize(DiffRand));
			Projectile.NewProjectile(source,new Vector2(nposition.X, nposition.Y), velocity.RotateRandom(0.1), type, damage, knockback, player.whoAmI);
			return false;
		}

	}
}
