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
	public class ElderStaff : ModItem
	{

		public override void SetStaticDefaults()
		{

			DisplayName.SetDefault("Crystal staff");
		}


		public override bool CanUseItem(Player player)
		{
			return true;
		}
		public override void SetDefaults()
		{
			item.mana = 100;
			item.magic= true;
			item.width = 60;
			item.height = 60;
			item.value = 10000;
			item.shoot = mod.ProjectileType("ElderStaffProjectile"); ;
			item.damage = 100;
			item.shootSpeed = 20;
			item.useTime = 71;
			item.useAnimation = 71;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 20;
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