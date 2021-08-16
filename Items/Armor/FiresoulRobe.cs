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
			Item.width = 34;
			Item.height = 18;
			Item.value = 10000;
			Item.rare = ItemRarityID.LightRed;
			Item.flame = true;
			Item.defense = 10;
		}
        public override void DrawHands(ref bool drawHands, ref bool drawArms)
        {
			drawHands = true;
			drawArms = true;

            base.DrawHands(ref drawHands, ref drawArms);
        }
        public override void UpdateEquip(Player Player)
		{
			Player.GetModPlayer<MPlayer>().fireBody = true;
			Player.GetModPlayer<MPlayer>().fireamplification += 0.10f;
			Player.statManaMax2 += 40;
			Player.GetDamage(DamageClass.Magic) += 0.02f;
			Player.GetCritChance(DamageClass.Magic) += 4;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<FierySilk>(), 7)
				.Register();
		}
	}
}
