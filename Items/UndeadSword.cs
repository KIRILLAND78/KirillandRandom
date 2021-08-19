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
	public class UndeadSword : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("UndeadSword");
			Tooltip.SetDefault("Cannot be used while Mana Sickness is active. Work in progress.");
		}

		public override void SetDefaults()
		{
			Item.mana = 100;
			Item.noUseGraphic = true;
			Item.shoot = ModContent.ProjectileType<UndeadSlash>();
			Item.DamageType = DamageClass.Melee;
			Item.width = 0;
			Item.noMelee = true;
			Item.height = 0;
			Item.useTime = 60;
			Item.useAnimation = 60;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 4;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
			Item.shootSpeed = 20;
			Item.damage = 88;
			Item.value = 10000;
			Item.rare = ItemRarityID.Expert;
        }
        public override bool CanUseItem(Player player)
        {if (!player.manaSick && player.statMana>=player.GetManaCost(Item))
			{
				return base.CanUseItem(player);
			}
			else return false;
        }


        //public override void AddRecipes()
        //{
        //	CreateRecipe()
        //		.AddIngredient(ItemID.Nanites, 50)
        //		.AddIngredient(ItemID.TitaniumBar, 4)
        //		.AddTile(TileID.MythrilAnvil)
        //		.Register();
        //	CreateRecipe()
        //		.AddIngredient(ItemID.Nanites, 50)
        //		.AddIngredient(ItemID.AdamantiteBar, 4)
        //		.AddTile(TileID.MythrilAnvil)
        //		.Register();
        //}
    }
}
