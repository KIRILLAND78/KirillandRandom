using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Text;
using KirillandRandom.Projectiles;

namespace KirillandRandom.Items
{
	public class DogBottle : ModItem
	{

		public override void SetStaticDefaults()
		{

			DisplayName.SetDefault("Bottle."); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("Relic of the past. Unobtainable. Disabled.");
		}


		//public override bool CanUseItem(Player Player)
		//{
		//	return true;
		//}
		//public override void SetDefaults()
		//{
		//	Item.DamageType= DamageClass.Generic;
		//	Item.width = 40;
		//	Item.height = 40;
		//	Item.value = 10000;
		//	Item.shoot = ModContent.ProjectileType<ExplodingBottle>(); ;
		//	Item.damage = 80;
		//	Item.shootSpeed = 4;
		//	Item.useTime = 25;
		//	Item.useAnimation = 25;
		//	Item.useStyle = ItemUseStyleID.Swing;
		//	Item.knockBack = 6;
		//	Item.noMelee = true;
		//	Item.noUseGraphic = true;
		//	Item.rare = ItemRarityID.Expert;
		//	Item.UseSound = SoundID.Item1;
		//	Item.autoReuse = false;
		//}

	}
}