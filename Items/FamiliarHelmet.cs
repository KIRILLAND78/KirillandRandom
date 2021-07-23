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
            item.rare = ItemRarityID.Green;
            item.width = 12;
            item.value= Item.sellPrice(platinum:1);
            item.height = 12;
            item.maxStack = 1;
            item.vanity = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.IronBar, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
