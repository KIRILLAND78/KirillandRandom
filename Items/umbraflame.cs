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
	public class UmbraFlame : ModItem
	{
		public int first = 1;
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("+16 damage boost for each summoned circle of flames.");
		}
        public override bool AltFunctionUse(Player player)
		{
			if ((player.statMana >= (30))&&(player.GetModPlayer<MPlayer>().flames_summoned<16))
			{
				return true;
			}
			else { return false; }
		}
		public override void HoldItem(Player player)
        {
			player.GetModPlayer<MPlayer>().angle += 4f;
			base.HoldItem(player);
            if (player.GetModPlayer<MPlayer>().BookCreated==false)
            {
				player.GetModPlayer<MPlayer>().BookCreated = true;

				Projectile.NewProjectile(new Vector2(player.position.X, player.position.Y), new Vector2(0, 0), mod.ProjectileType("UmbraFlameBook"), 0, 0, player.whoAmI); //owner.rangedDamage is basically the damage multiplier for ranged weapons
            }
        }

        public override bool CanUseItem(Player player)
        {
			if (player.altFunctionUse == 2)
			{
					item.shoot = ProjectileID.None;
					if (player.GetModPlayer<MPlayer>().flames_summoned < 16) {
						item.shoot = mod.ProjectileType("UmbraFlameBolt");

					}

						item.mana = 30;
				item.useTime = 40;
					item.useAnimation = 40;
					item.useStyle = ItemUseStyleID.HoldingUp;
					item.UseSound = SoundID.DD2_ExplosiveTrapExplode;
				} 
			else
			{

				item.mana = 0;
				player.GetModPlayer<MPlayer>().flames_summoned = 0;
				item.shoot = ProjectileID.None;
				item.useTime = 5;
				item.useStyle = ItemUseStyleID.HoldingOut;
				item.useAnimation = 5;
				item.UseSound = SoundID.DD2_FlameburstTowerShot;
			}
			return true;
        }
        public override void SetDefaults()
		{

			item.noUseGraphic = true;
			item.damage = 90;
			item.mana = 25;
			item.noMelee = true;
			item.magic = true;
			item.useTime = 5;
			item.shootSpeed = 0;
			item.useAnimation = 30;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 0;
			item.value = 10000;
			item.rare = ItemRarityID.Purple; 
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (player.GetModPlayer<MPlayer>().flames_summoned == 8)
			{
				for (int i = 9; i <= 16; i++)
				{
					Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI, i);
				}
				player.GetModPlayer<MPlayer>().flames_summoned += 8;
			}
			if (player.GetModPlayer<MPlayer>().flames_summoned == 3)
			{
				for (int i = 4; i <= 8; i++)
				{
					Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI, i);
				}
				player.GetModPlayer<MPlayer>().flames_summoned += 5;
			}
			if (player.GetModPlayer<MPlayer>().flames_summoned == 0)
			{
				for (int i = 0; i <= 3; i++)
				{
					Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI, i);
				}
				player.GetModPlayer<MPlayer>().flames_summoned += 3;
			}

			return false;
        }



    }
}
