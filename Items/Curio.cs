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
	public class Curiosity : ModItem
	{

		public override void SetStaticDefaults()
		{
			
			// DisplayName.SetDefault("Something"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("This weapon has alt attack and 'overuse' mechanic\r\nContinuous use of only one fire mode improves damage of other fire mode and\r\n weakens current.\r\nWIP!");
		}

        public override bool AltFunctionUse(Player player)
        {
			return true;
		}
		public override void HoldItem(Player player)
		{
			if (player.GetModPlayer<MPlayer>().OveruseMeterCreated == false)
			{
				Projectile.NewProjectile(player.position.X, player.position.Y, 0, 0, mod.ProjectileType("OveruseMeter"), 0, 0, player.whoAmI);
				Projectile.NewProjectile(player.position.X, player.position.Y, 0, 0, mod.ProjectileType("OveruseMeter1"), 0, 0, player.whoAmI);
				player.GetModPlayer<MPlayer>().OveruseMeterCreated = true;
			}
			base.HoldItem(player);
		}


		public override bool CanUseItem(Player player)
        {
			if (player.altFunctionUse == 2)
			{
				item.noUseGraphic = true;
				item.useStyle = ItemUseStyleID.SwingThrow;
				item.shoot = ProjectileID.ChargedBlasterOrb;
				item.useTime = 18;
				item.useAnimation = 18;
				item.shootSpeed = 18;
				item.knockBack = 1;
				item.useStyle = ItemUseStyleID.HoldingOut;
				item.damage = 60;
				//item.damage = 60;
				if (player.GetModPlayer<MPlayer>().overuse > 40)
                {
					item.damage = 90;
				}
				if (player.GetModPlayer<MPlayer>().overuse < -40)
				{
					item.damage = 45;
				}
				player.GetModPlayer<MPlayer>().overuse -= 2;
				item.UseSound = SoundID.DD2_SkyDragonsFuryShot;
			}
			else
			{
				item.noUseGraphic = false;
				item.shoot = ProjectileID.None;
				item.damage = 80;
				if (player.GetModPlayer<MPlayer>().overuse > 40)
				{
					item.damage = 60;
				}
				if (player.GetModPlayer<MPlayer>().overuse < -40)
				{
					item.damage = 120;
				}
				item.melee = true;
				item.width = 40;
				item.height = 90;
				item.useTime = 15;
				item.useAnimation = 15;
				item.useStyle = ItemUseStyleID.SwingThrow;
				item.knockBack = 6;
				player.GetModPlayer<MPlayer>().overuse += 3;
				item.UseSound = SoundID.Item1;
				item.autoReuse = true;
			}
			item.damage = (int)(item.damage*1.1);
			return true;
        }
        public override void SetDefaults()
		{

			item.shootSpeed = 18;
			item.damage = 80;
			item.melee = true;
			item.width = 40;
			item.height = 90;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = ItemRarityID.Expert; 
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}


		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
		}



		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 speed = new Vector2(speedX, speedY);
			speed = speed.RotatedByRandom(MathHelper.ToRadians(2));
			speedX = speed.X;
			speedY = speed.Y;
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("Curiosity_alt"), 0, 0, Main.myPlayer);
			

			return true;//Коррекция поведения при выстреле.
		}

	}
}
