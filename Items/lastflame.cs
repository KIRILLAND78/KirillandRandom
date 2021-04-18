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
			Tooltip.SetDefault("Damage boost (+5 for EACH projectile) and reduced mana usage (-5) for each summoned flame.");
		}
        public override bool AltFunctionUse(Player player)
		{
			if ((player.statMana >= (30 - player.GetModPlayer<MPlayer>().flames_summoned * 5))&&(player.GetModPlayer<MPlayer>().flames_summoned<4))
			{
				return true;
			}
			else { return false; }
		}
		public override void HoldItem(Player player)
        {

			player.GetModPlayer<MPlayer>().angle += 3f;
			base.HoldItem(player);
            if (player.GetModPlayer<MPlayer>().BookCreated==false)
            {
				player.GetModPlayer<MPlayer>().BookCreated = true;

				Projectile.NewProjectile(new Vector2(player.position.X, player.position.Y), new Vector2(0, 0), mod.ProjectileType("lastflameBook"), 0, 0, player.whoAmI); //owner.rangedDamage is basically the damage multiplier for ranged weapons
            }
        }

        public override bool CanUseItem(Player player)
        {
			if (player.altFunctionUse == 2)
			{
					item.shoot = ProjectileID.None;
					if (player.GetModPlayer<MPlayer>().flames_summoned < 4) {
						item.shoot = mod.ProjectileType("lastflame");
						player.GetModPlayer<MPlayer>().flames_summoned += 1;
					}
					item.mana = 30- player.GetModPlayer<MPlayer>().flames_summoned*5;
					item.useTime = 30;
					item.useAnimation = 30;
					item.useStyle = ItemUseStyleID.HoldingUp;
					item.damage = 60;
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
				item.value = 10000;
				item.UseSound = SoundID.DD2_FlameburstTowerShot;
			}
			return true;
        }
        public override void SetDefaults()
		{

			item.noUseGraphic = true;
			item.damage = 60;
			item.mana = 25;
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

			recipe.AddIngredient(ItemID.DemoniteBar, 5);
			recipe.AddIngredient(ItemID.DemonScythe, 1);
			recipe.AddIngredient(ItemID.SoulofNight, 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}
}
