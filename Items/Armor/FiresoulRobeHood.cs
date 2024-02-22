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
            ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = 10000;
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 6;
        }

        public override void UpdateEquip(Player Player)
        {
            Player.GetModPlayer<MPlayer>().fireHead = true;
            Player.GetModPlayer<MPlayer>().fireamplification += 0.06f;
            Player.statManaMax2 += 40;
            Player.GetCritChance(DamageClass.Magic) += 10;
            Player.GetDamage(DamageClass.Magic) += 0.06f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<FierySilk>(), 5)
                .Register();
        }
    }
}
