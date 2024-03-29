﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KirillandRandom.Items.FriendsStuff
{
    [AutoloadEquip(EquipType.Head)]
    public class TimelessHat : ModItem
    {
        public override void SetStaticDefaults()
        {
            ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true;
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Green;
            Item.width = 12;
            Item.value = Item.sellPrice(platinum: 1);
            Item.height = 12;
            Item.maxStack = 1;
            Item.vanity = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Silk, 10)
                .Register();
        }
    }
}
