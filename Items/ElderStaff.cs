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
	public class ElderStaff : ModItem
	{

		public override void SetStaticDefaults()
		{

			DisplayName.SetDefault("Crystal staff");
			Tooltip.SetDefault("Holding same energy as Rod of Discord.");
		}


		public override bool CanUseItem(Player Player)
		{
			return true;
		}
		public override void SetDefaults()
		{
			Item.mana = 70;
			Item.DamageType= DamageClass.Magic;
			Item.width = 60;
			Item.height = 60;
			Item.value = 10000;
			Item.shoot = ModContent.ProjectileType<ElderStaffProjectile>(); ;
			Item.damage = 100;
			Item.shootSpeed = 20;
			Item.useTime = 71;
			Item.useAnimation = 71;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 20;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
		}


		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.CrystalShard, 20)
			.AddIngredient(ItemID.SoulofFright, 5)
			.AddIngredient(ItemID.SoulofMight, 5)
			.AddIngredient(ItemID.SoulofSight, 5)

				.AddTile(TileID.LunarCraftingStation)
				.Register();
		}

        //public override void AddRecipes()
        //{
        //	ModRecipe recipe = new ModRecipe(mod);

        
        //	recipe.AddRecipe();

        //}


    }
}