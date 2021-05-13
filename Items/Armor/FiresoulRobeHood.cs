using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KirillandRandom.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class FiresoulRobeHood : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			//DisplayName.SetDefault("Somethinngg");
			Tooltip.SetDefault("'On Fire!' grants 5% increased damage.\r\n+40 max mana.\r\n+10% magic damage and crit chance.");
		}

        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
			drawHair = true;
            base.DrawHair(ref drawHair, ref drawAltHair);
        }
        public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 10000;
			item.rare = ItemRarityID.Green;
			item.defense = 6;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetModPlayer<MPlayer>().fireHead = true;
			player.GetModPlayer<MPlayer>().fireamplification += 0.05f;
			player.statManaMax2 += 40;
			player.magicCrit += 10;
			player.magicDamage += 0.10f;
		}

		//public override void AddRecipes()
		//{
		//	ModRecipe recipe = new ModRecipe(mod);
		//	recipe.SetResult(this);
		//	recipe.AddRecipe();
		//}
	}
}
