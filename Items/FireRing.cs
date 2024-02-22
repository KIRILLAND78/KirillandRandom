using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace KirillandRandom.Items
{
    public class firering : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;
            Item.accessory = true;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.LightRed;
        }
        public override void UpdateAccessory(Player Player, bool hideVisual)
        {
            Player.GetModPlayer<MPlayer>().flamingdedication = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Ruby, 2)
                .AddIngredient(ItemID.HellstoneBar, 2)
                .AddIngredient(ItemID.GoldBar, 4)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}