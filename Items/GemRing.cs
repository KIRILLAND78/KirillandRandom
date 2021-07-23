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
	public class GemRing : ModItem
	{

		public override void SetStaticDefaults()
		{

			DisplayName.SetDefault("Crystal staff");
			Tooltip.SetDefault("Link fades if distance between players is too big.\r\nCan be used only in multiplayer.\r\nWIP very buggy");

		}


		public override bool CanUseItem(Player player)
		{
			return true;
		}
		public override void SetDefaults()
		{
			item.mana = 0;
			item.magic= true;
			item.width = 60;
			item.height = 60;
			item.value = 10000;
			item.shoot = mod.ProjectileType("GemRingLaser");
			item.damage = 100;
			item.shootSpeed = 20;
			item.useTime = 71;
			item.useAnimation = 71;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 1;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.rare = ItemRarityID.Expert;
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
			item.channel = true;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			for (int i = 0; i < 256; i++)
			{
				if ((Main.player[i].team == player.team) && Main.player[i].active && player.whoAmI != i && !Main.player[i].dead)//check if player exist Kiri: This is bad method. Reimplement later.
				{
					int dd = Projectile.NewProjectile(player.position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI, i, i);
					Main.projectile[dd].ai[1] = i;
				}

			}

            return false;
		}

	}
}