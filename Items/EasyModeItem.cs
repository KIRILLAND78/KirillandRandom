using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace KirillandRandom.Items
{
    public class EasyModeItem : ModItem
    {
        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.width = 30;
            Item.height = 30;
            Item.value = Item.sellPrice(0);
            Item.rare = ItemRarityID.Blue;
        }


        public override bool? UseItem(Player player)
        {

            for (int k = 0; k < Main.maxNPCs; k++)
            {
                NPC curNPC = Main.npc[k];
                if ((curNPC.boss == true) && (curNPC.active == true))
                {
                    return false;
                }
            }
            player.GetModPlayer<MPlayer>().easyMode = !player.GetModPlayer<MPlayer>().easyMode;
            Main.NewText(player.GetModPlayer<MPlayer>().easyMode?"Easy mode activated":"Easy mode deactivated");
            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.IronBar, 1)
                .AddTile(TileID.Anvils)
                .Register();
            CreateRecipe()
                .AddIngredient(ItemID.LeadBar, 1)
                .AddTile(TileID.Anvils)
                .Register();
        }

    }
}