using KirillandRandom.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace KirillandRandom.Items
{
    public class ChScythe : ModItem
    {
        public int first = 1;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Spark");
            // Tooltip.SetDefault("Charge enemies with right click.\r\n+50 bonus damage (for left click attack) with each stored charge on enemy.");
        }
        public override bool AltFunctionUse(Player Player)
        {
            return true;
        }

        public override bool CanUseItem(Player Player)
        {
            if (Player.altFunctionUse != 2)
            {
                Item.channel = false;
                Item.shoot = ModContent.ProjectileType<Projectiles.ChScythe>();//BIG SCYTHE
                Item.shootSpeed = 17;
                Item.useTime = 600;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.useAnimation = 60;
                Item.autoReuse = true;
                Item.UseSound = SoundID.DD2_SkyDragonsFuryShot;
            }
            else
            {
                Item.shoot = ModContent.ProjectileType<ChScytheSpin>();//SPINNING SCYTHE
                Item.useTime = 21;
                Item.autoReuse = false;

                Item.channel = true;
                Item.shootSpeed = 0;
                Item.useAnimation = 21;
                Item.useStyle = ItemUseStyleID.DrinkLong;
                Item.UseSound = SoundID.DD2_SkyDragonsFurySwing;
            }
            return true;
        }
        public override void SetDefaults()
        {

            Item.noUseGraphic = true;
            Item.damage = 80;
            Item.noMelee = true;
            Item.useTime = 10;
            Item.shootSpeed = 0;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 0;
            Item.value = 10000;
            Item.rare = ItemRarityID.Cyan;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;

            Item.DamageType = DamageClass.Melee;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.MartianConduitPlating, 50)
                .AddIngredient(ItemID.InfluxWaver, 1)
                .AddIngredient(ItemID.ChargedBlasterCannon, 1)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

    }
}
