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
	public class Firegun : ModItem
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fire Zone");
			Tooltip.SetDefault("Spreads fire in a cone.\r\nUses gel for ammo\r\nWIP!");
		}




        public override void SetDefaults()
		{
			item.useAmmo = AmmoID.Gel;
			item.noMelee = true;
			item.shoot = mod.ProjectileType("Firegun_damage_zone");
			item.shootSpeed = 9;
			item.damage = 40;
			item.ranged = true;
			item.width = 30;
			item.height = 90;
			item.useTime = 5;
			item.useAnimation = 9;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 0;
			item.value = 10000;
			item.rare = ItemRarityID.Expert; 
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}
		public override Vector2? HoldoutOffset()// HoldoutOffset has to return a Vector2 because it needs two values (an X and Y value) to move your flamethrower sprite. Think of it as moving a point on a cartesian plane.
		{
			return new Vector2(0, -2); // If your own flamethrower is being held wrong, edit these values. You can test out holdout offsets using Modder's Toolkit.
		}
		public override bool ConsumeAmmo(Player player)
		{
			return Main.rand.NextFloat() >= .4f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {

			Vector2 speed2 = new Vector2(speedX, speedY);
			speed2 = speed2.RotatedByRandom(MathHelper.ToRadians(15))*3f;
			Projectile.NewProjectile(position.X, position.Y, speed2.X, speed2.Y, mod.ProjectileType("Firegun_damage_zone"), damage, knockBack, Main.myPlayer);

			speed2 = new Vector2(speedX, speedY);
			speed2 = speed2.RotatedByRandom(MathHelper.ToRadians(15)) * 3f;
			Projectile.NewProjectile(position.X, position.Y, speed2.X, speed2.Y, mod.ProjectileType("Firegun_damage_zone"), damage, knockBack, Main.myPlayer);
			speed2 = new Vector2(speedX, speedY);
			speed2 = speed2.RotatedByRandom(MathHelper.ToRadians(15)) * 3f;
			Projectile.NewProjectile(position.X, position.Y, speed2.X, speed2.Y, mod.ProjectileType("Firegun_damage_zone"), damage, knockBack, Main.myPlayer);
			speed2 = new Vector2(speedX, speedY);
			speed2 = speed2.RotatedByRandom(MathHelper.ToRadians(15)) * 3f;
			Projectile.NewProjectile(position.X, position.Y, speed2.X, speed2.Y, mod.ProjectileType("Firegun_damage_zone"), damage, knockBack, Main.myPlayer);










			return false;
        }

    }
}
