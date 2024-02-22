using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace KirillandRandom.Items
{
    public class EyeOfDeath : ModItem
    {
        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.width = 30;
            Item.height = 30;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Red;
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
            Main.NewText("aaa");
            player.GetModPlayer<MPlayer>().eyeofdeath = !player.GetModPlayer<MPlayer>().eyeofdeath;
            Main.NewText(player.GetModPlayer<MPlayer>().eyeofdeath);
            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.WallOfFleshBossBag, 1)
                .AddTile(TileID.Anvils)
                .Register();
        }

    }
}