using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KirillandRandom.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class FiresoulRobe : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			//DisplayName.SetDefault("Somethinngg");
			Tooltip.SetDefault("'On Fire!' grants 10% increased damage.\r\n+40 max mana.\r\n+2% magic damage and crit chance.");
		}
		public override void SetDefaults()
		{
			item.width = 34;
			item.height = 18;
			item.value = 10000;
			item.rare = ItemRarityID.LightRed;
			item.flame = true;
			item.defense = 10;
		}
        public override void DrawHands(ref bool drawHands, ref bool drawArms)
        {
			drawHands = true;
			drawArms = false;

            base.DrawHands(ref drawHands, ref drawArms);
        }
        public override void UpdateEquip(Player player)
		{
			player.GetModPlayer<MPlayer>().fireBody = true;
			player.GetModPlayer<MPlayer>().fireamplification += 0.10f;
			player.statManaMax2 += 40;
			player.magicCrit += 4;
			player.magicDamage += 0.02f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<FierySilk>(), 6);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
