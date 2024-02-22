using KirillandRandom.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace KirillandRandom.Items
{
    public class SkyRapier_item : ModItem
    {

        public override void SetDefaults()
        {
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<SkyRapier>();
            Item.DamageType = DamageClass.Melee;
            Item.width = 0;
            Item.noMelee = true;
            Item.height = 0;
            Item.useTime = 3;
            Item.useAnimation = 3;
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.knockBack = 4;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shootSpeed = 18;
            Item.damage = 60;
            Item.crit = 4;
            Item.value = 10000;
            Item.rare = ItemRarityID.Cyan;
            Item.channel = true;
        }


        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Nanites, 50)
                .AddIngredient(ItemID.TitaniumBar, 4)
                .AddTile(TileID.MythrilAnvil)
                .Register();
            CreateRecipe()
                .AddIngredient(ItemID.Nanites, 50)
                .AddIngredient(ItemID.AdamantiteBar, 4)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 MousePos = new Vector2(Main.MouseWorld.X, Main.MouseWorld.Y);
            Vector2 PlayerPos = player.Center;
            Vector2 Diff = MousePos - PlayerPos;
            Vector2 DiffRand = Diff.RotatedByRandom(MathHelper.ToRadians(45));
            Vector2 nposition = player.Center + (30 * Vector2.Normalize(DiffRand));
            Projectile.NewProjectile(source, new Vector2(nposition.X, nposition.Y), velocity.RotateRandom(0.1), type, damage, knockback, player.whoAmI);
            return false;
        }

    }
}
