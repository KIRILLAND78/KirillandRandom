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
        {//Свойства вещей. статичные.
            DisplayName.SetDefault("Familiar Helmet");
            Tooltip.SetDefault("Hmm... It reminds me of somebody...\r\nIt definetely belongs to someone...");
        }
        public override void SetDefaults()
        {
            //Свойства вещей, только... как дропнутая?
            item.rare = ItemRarityID.Green;
            item.width = 12;
            item.value= Item.sellPrice(platinum:1);
            item.height = 12;
            item.maxStack = 1;
            item.defense = 500;//ОЧЕНЬ ИМБАЛАНСНАЯ, ТОЛЬКО ДЛЯ ТЕСТОВ
        }
    }
}
