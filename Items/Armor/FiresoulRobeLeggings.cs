using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KirillandRandom.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class FiresoulRobeLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<FiresoulRobe>() && head.type == ModContent.ItemType<FiresoulRobeHood>() && legs.type == ModContent.ItemType<FiresoulRobeLeggings>();
        }


        public override void UpdateArmorSet(Player player)
        {
            player.GetModPlayer<MPlayer>().fireregen = true;
            player.GetDamage(DamageClass.Generic) += 0.2f;
            player.setBonus = "'On Fire!' grants health regeneration. +20% damage";
        }
        public override void SetDefaults()
        {
            Item.flame = true;
            Item.width = 26;
            Item.height = 14;
            Item.value = 10000;
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 8;
        }

        public override void UpdateEquip(Player Player)
        {
            Player.GetModPlayer<MPlayer>().fireLeggings = true;
            Player.GetModPlayer<MPlayer>().fireamplification += 0.08f;
            Player.statManaMax2 += 20;
            Player.GetCritChance(DamageClass.Magic) += 4;
            Player.GetDamage(DamageClass.Magic) += 0.04f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<FierySilk>(), 6)
                .Register();
        }
    }
}
