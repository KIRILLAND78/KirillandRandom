using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria;

namespace KirillandRandom.Items
{
    [AutoloadEquip(EquipType.Head)]
    public class FamiliarHelmet:ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Familiar Helmet");
        }
        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Green;
            Item.width = 12;
            Item.value= Item.sellPrice(platinum:1);
            Item.height = 12;
            Item.maxStack = 1;
            Item.vanity = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.IronBar, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
