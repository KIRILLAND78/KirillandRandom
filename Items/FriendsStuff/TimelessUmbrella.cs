using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace KirillandRandom.Items.FriendsStuff
{
    public class TimelessUmbrella : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;
            Item.width = 22;
            Item.height = 20;
            Item.value = 0;
            Item.rare = ItemRarityID.Green;
            Item.DamageType = DamageClass.Melee;
            Item.damage = 10;
            Item.useTime = 17;
            Item.useAnimation = 17;
            Item.knockBack = 2;
            Item.useStyle = ItemUseStyleID.Swing;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Umbrella)
                .AddIngredient(ItemID.IronBar, 4)
                .AddIngredient(ItemID.Silk, 8)
                .Register();
        }
    }
}
