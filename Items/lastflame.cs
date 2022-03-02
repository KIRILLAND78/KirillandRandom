using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Text;
using KirillandRandom.Projectiles;
using Terraria.DataStructures;

namespace KirillandRandom.Items
{
	public class LastFlame : ModItem
	{
		public int first = 1;
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Right click to release flames. +5 damage boost and reduced mana usage (-5) for each summoned flame.");
		}
		public override bool? CanAutoReuseItem(Player player)
		{
			return true;
		}
		public override bool AltFunctionUse(Player Player)
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

				Projectile.NewProjectile(new EntitySource_ItemUse(player, Item),new Vector2(player.position.X, player.position.Y), new Vector2(0, 0), ModContent.ProjectileType<LastFlameBook>(), 0, 0, player.whoAmI); //owner.rangedDamage is basically the damage multiplier for ranged weapons
            }
        }

        public override bool CanUseItem(Player Player)
        {
			if (Player.altFunctionUse != 2)
			{
				if ((Player.statMana >= (30 - Player.GetModPlayer<MPlayer>().flames_summoned * 5)) && (Player.GetModPlayer<MPlayer>().flames_summoned < 4))
				{
				Item.shoot = ProjectileID.None;
					if (Player.GetModPlayer<MPlayer>().flames_summoned < 4) {
						Item.shoot = ModContent.ProjectileType<LastFlameBolt>();
						Player.GetModPlayer<MPlayer>().flames_summoned += 1;
					}
					Item.mana = 30- Player.GetModPlayer<MPlayer>().flames_summoned*5;
					Item.useTime = 30;
					Item.useAnimation = 30;
					Item.useStyle = ItemUseStyleID.HoldUp;
					Item.UseSound = SoundID.DD2_ExplosiveTrapExplode;

					return true;
				}
				return false;
			} 
			else
			{

				Item.mana = 0;
				Player.GetModPlayer<MPlayer>().flames_summoned = 0;
				Item.shoot = ProjectileID.None;
				Item.useTime = 5;
				Item.useStyle = ItemUseStyleID.Shoot;
				Item.useAnimation = 5;
				Item.UseSound = SoundID.DD2_FlameburstTowerShot;
			}
			return true;
        }
        public override void SetDefaults()
		{

			Item.noUseGraphic = true;
			Item.damage = 60;
			Item.mana = 25;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Magic;
			Item.useTime = 5;
			Item.shootSpeed = 0;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 0;
			Item.value = 10000;
			Item.rare = ItemRarityID.Purple; 
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}



		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.DemoniteBar, 5)
				.AddIngredient(ItemID.DemonScythe, 1)
				.AddIngredient(ItemID.SoulofNight, 10)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}


	}
}
