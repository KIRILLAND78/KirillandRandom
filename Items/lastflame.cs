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
	public class LastFlame : ModItem
	{
		public int first = 1;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Something"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("UGA-CHAGA!!!");
		}
        public override bool AltFunctionUse(Player player)
        {
			return true;
		}
		public override void HoldItem(Player player)
        {

			player.GetModPlayer<MPlayer>().angle += 3f;
			base.HoldItem(player);
            if (player.GetModPlayer<MPlayer>().BookCreated==false)
            {
				player.GetModPlayer<MPlayer>().BookCreated = true;
				Projectile.NewProjectile(player.position.X, player.position.Y, 0, 0, mod.ProjectileType("lastflameBook"), 0, 0, Main.myPlayer); //owner.rangedDamage is basically the damage multiplier for ranged weapons
            }
        }

        public override bool CanUseItem(Player player)
        {
			if (player.altFunctionUse == 2)
			{
				item.shoot = ProjectileID.None;
				if (player.GetModPlayer<MPlayer>().flames_summoned < 3) {
					item.shoot = mod.ProjectileType("lastflame");
				}
				
				item.useTime = 40;
				item.useAnimation = 40;
				item.useStyle = ItemUseStyleID.HoldingUp;
				item.damage = 60;
				item.UseSound = SoundID.DD2_ExplosiveTrapExplode;
				player.GetModPlayer<MPlayer>().flames_summoned += 1;
			}
			else
			{
				player.GetModPlayer<MPlayer>().flames_summoned =0;
				item.shoot = ProjectileID.None;
				item.useTime = 5;
				item.useStyle = ItemUseStyleID.HoldingOut;
				item.useAnimation = 5;
				item.value = 10000;
				item.UseSound = SoundID.DD2_FlameburstTowerShot;
			}
			return true;
        }
        public override void SetDefaults()
		{

			item.noUseGraphic = true;
			item.damage = 60;
			item.noMelee = true;
			item.magic = true;
			item.useTime = 10;
			item.shootSpeed = 0;
			item.useAnimation = 10;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 0;
			item.value = 10000;
			item.rare = ItemRarityID.Purple; 
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}





		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}
}
