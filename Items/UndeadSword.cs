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
			DisplayName.SetDefault("Soul Guardian");
			Tooltip.SetDefault("Cannot be used while Mana Sickness is active.");
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
			Item.damage = 196;
			Item.value = 10000;
			Item.rare = ItemRarityID.Yellow;
        }
        public override bool CanUseItem(Player player)
        {if (!player.manaSick && player.statMana>=player.GetManaCost(Item))
			{
				return base.CanUseItem(player);
			}
			else return false;
        }


        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Ectoplasm, 4)
                .AddIngredient(ItemID.TitaniumBar, 8)
                .AddTile(TileID.MythrilAnvil)
                .Register();
            CreateRecipe()
                .AddIngredient(ItemID.Ectoplasm, 4)
                .AddIngredient(ItemID.AdamantiteBar, 8)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
