﻿using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KirillandRandom.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class FiresoulRobeLeggings : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			//DisplayName.SetDefault("Somethinngg");
			Tooltip.SetDefault("'On Fire!' grants 10% increased damage.\r\n+20 max mana.\r\n+4% magic damage and crit chance.");
			
		}
		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<FiresoulRobe>() && legs.type == ModContent.ItemType<FiresoulRobeLeggings>();
		}


        public override void UpdateArmorSet(Player player)
		{
			player.GetModPlayer<MPlayer>().fireregen = true;
			player.allDamage += 0.2f;
			player.setBonus = "When 'On Fire!' regenerate health.";
		}
        public override void SetDefaults()
		{
			item.flame = true;
			item.width = 18;
			item.height = 18;
			item.value = 10000;
			item.rare = ItemRarityID.Green;
			item.defense = 10;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetModPlayer<MPlayer>().fireLeggings = true;
			player.GetModPlayer<MPlayer>().fireamplification += 0.10f;
			player.statManaMax2 += 20;
			player.magicCrit += 4;
			player.magicDamage += 0.04f;
		}

		//public override void AddRecipes()
		//{
		//	ModRecipe recipe = new ModRecipe(mod);
		//	recipe.SetResult(this);
		//	recipe.AddRecipe();
		//}
	}
}
