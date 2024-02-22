using KirillandRandom.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KirillandRandom.Items
{
    public class ScytheOfVyse : ModItem
    {
        public override void SetDefaults()
        {
            Item.mana = 200;
            Item.DamageType = DamageClass.Magic;
            Item.width = 40;
            Item.height = 40;
            Item.value = 10000;
            Item.shoot = ModContent.ProjectileType<Hex>();
            Item.damage = 10;
            Item.shootSpeed = 4;
            Item.useTime = 50;
            Item.useAnimation = 50;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 0;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.rare = ItemRarityID.Cyan;
            Item.UseSound = SoundID.Item117;
            Item.autoReuse = false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.GravityGlobe, 1)
                .AddIngredient(ItemID.LunarBar, 10)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }

    }
}