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
	public class DogBottle : ModItem
	{

		public override void SetStaticDefaults()
		{

			DisplayName.SetDefault("Dog and bottle."); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("OH NO! OH NO! OH NOOOO!!!");
		}


		public override bool CanUseItem(Player player)
		{
			return true;
		}
		public override void SetDefaults()
		{
			item.ranged= true;
			item.width = 40;
			item.height = 40;
			item.value = 10000;
			item.shoot = mod.ProjectileType("ExplodingBottle"); ;
			item.damage = 80;
			item.shootSpeed = 4;
			item.useTime = 25;
			item.useAnimation = 25;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 6;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.rare = ItemRarityID.Expert;
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			return true;//это что-то типа коррекции поведения снарядов... наверное? По типу изменения урона, угла стрельбы и т.д.
		}

	}
}