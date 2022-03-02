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
using Terraria.DataStructures;

namespace KirillandRandom.Items
{
	public class Firegun : ModItem
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nethersong");
			Tooltip.SetDefault("Spreads fire in a cone.\r\nUses gel for ammo\r\nWIP!\r\n20% chance to not consume ammo");
		}




        public override void SetDefaults()
		{
			Item.useAmmo = AmmoID.Gel;
			Item.noMelee = true;
			Item.shoot = ModContent.ProjectileType<Firegun_damage_zone>();
			Item.shootSpeed = 9;
			Item.damage = 37;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 30;
			Item.height = 90;
			Item.useTime = 5;
			Item.useAnimation = 15;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 0;
			Item.value = 10000;
			Item.rare = ItemRarityID.Orange; 
			Item.UseSound = SoundID.Item34;
			Item.autoReuse = true;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.Flamethrower, 1)
				.AddIngredient(ItemID.ShroomiteBar, 5)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
		public override Vector2? HoldoutOffset()// HoldoutOffset has to return a Vector2 because it needs two values (an X and Y value) to move your flamethrower sprite. Think of it as moving a point on a cartesian plane.
		{
			return new Vector2(0, -2); // If your own flamethrower is being held wrong, edit these values. You can test out holdout offsets using Modder's Toolkit.
		}
		public override bool CanConsumeAmmo(Player Player)
		{
			// To make this Item only consume ammo during the first jet, we check to make sure the animation just started. ConsumeAmmo is called 5 times because of Item.useTime and Item.useAnimation values in SetDefaults above.
			return ((Player.itemAnimation >= Player.itemAnimationMax - 2)&& (Main.rand.NextFloat() >= .2f));
		}
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 pos, Vector2 velocity, int type, int damage, float knockBack)
        {
			int fire = ModContent.ProjectileType<Firegun_damage_zone>();


			for (int i = 0; i <= 4; i++) {
				Vector2 speed2 = velocity.RotatedByRandom(MathHelper.ToRadians(15)) * 3f;
				Projectile.NewProjectile(source, pos, speed2, fire, damage, knockBack, Main.myPlayer);

			}

			return false;
        }

    }
}
