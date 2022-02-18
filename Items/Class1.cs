using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using KirillandRandom.Dusts;
using KirillandRandom.Primitives;
using Terraria.DataStructures;
using KirillandRandom.Items;
namespace KirillandRandom.Items
{
    internal class StoneCrusher : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 80;
            Item.knockBack = 20;
            Item.DamageType = DamageClass.Melee;

            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            base.SetDefaults();
        }
    }
}
