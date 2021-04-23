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
	public class SkyRapier_item : ModItem
	{

		public override void SetStaticDefaults()
		{

			// DisplayName.SetDefault("Something"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("Pierce with speed of heaven!\r\nWIP!");
		}

		public override bool AltFunctionUse(Player player)
		{
			return false;
		}
		public override bool CanUseItem(Player player)
		{
				item.noUseGraphic = true;
				item.shoot = mod.ProjectileType("SkyRapier");
				item.melee = true;
				item.width = 0;
				item.height = 0;
				item.useTime = 7;
				item.useAnimation = 7;
				item.useStyle = ItemUseStyleID.HoldingOut;
				item.knockBack = 6;
				item.UseSound = SoundID.Item1;
				item.autoReuse = true;
			
			return true;
		}
		public override void SetDefaults()
		{
			//todo
			item.shootSpeed = 18;
			item.damage = 55;
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


	}
}
