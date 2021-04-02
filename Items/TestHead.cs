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
            Tooltip.SetDefault("Beep-beep-beep-bop-bap-beepbeepbeepbeep-beep-boop-bap");
        }
        public override void SetDefaults()
        {
            item.rare = ItemRarityID.Gray;
            item.width = 12;
            item.value= Item.sellPrice(platinum:1);
            item.height = 12;
            item.maxStack = 1;
            item.defense = 500;
        }
    }
}
