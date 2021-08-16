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

namespace KirillandRandom.Items
{
	public class GemRing : ModItem
	{

		public override void SetStaticDefaults()
		{

			DisplayName.SetDefault("Link Gem Ring");
			Tooltip.SetDefault("Link fades if distance between players is too big.\r\nCan be used only in multiplayer.\r\nUnobtainable. Disabled.");

		}


		//public override bool CanUseItem(Player Player)
		//{
		//	return true;
		//}
		public override void SetDefaults()
		{
			Item.mana = 0;
			Item.DamageType= DamageClass.Magic;
			Item.width = 60;
			Item.height = 60;
			Item.value = 10000;
			Item.shoot = ModContent.ProjectileType<GemRingLaser>();
			Item.damage = 100;
			Item.shootSpeed = 20;
			Item.useTime = 71;
			Item.useAnimation = 71;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 1;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.rare = ItemRarityID.Expert;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
			Item.channel = true;
		}

		//public override bool Shoot(Player Player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
  //      {
		//	for (int i = 0; i < 256; i++)
		//	{
		//		if ((Main.player[i].team == Player.team) && Main.player[i].active && Player.whoAmI != i && !Main.player[i].dead)//check if Player exist Kiri: This is bad method. Reimplement later.
		//		{
		//			int dd = Projectile.NewProjectile(Player.position, new Vector2(speedX, speedY), type, damage, knockBack, Player.whoAmI, i, i);
		//			Main.projectile[dd].ai[1] = i;
		//		}

		//	}

  //          return false;
		//}

	}
}