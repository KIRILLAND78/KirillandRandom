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
    public class TestHead:ModItem
    {
        public override void SetStaticDefaults()
        {//Свойства вещей. статичные.
            DisplayName.SetDefault("Test Head");
            Tooltip.SetDefault("Beep-bap bop?");
        }
        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Gray;
            Item.width = 12;
            Item.value= Item.sellPrice(platinum:1);
            Item.height = 12;
            Item.maxStack = 1;
            Item.defense = 500;
        }
    }
}
