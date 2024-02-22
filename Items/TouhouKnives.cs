using KirillandRandom.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace KirillandRandom.Items
{
    internal class TouhouKnives : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;
            Item.DamageType = DamageClass.Magic;
            Item.damage = 30;
            Item.knockBack = 5;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.autoReuse = true;
            Item.mana = 0;
            Item.shoot = ModContent.ProjectileType<TouhouKnives_proj>();
            Item.shootSpeed = 12;
            Item.mana = 10;
            Item.noMelee = true;
            Item.noUseGraphic = true;
        }
        public override bool? CanAutoReuseItem(Player player)
        {
            return true;
        }
        public override bool AltFunctionUse(Player Player)
        {
            return true;
        }
        public override bool CanUseItem(Player Player)
        {
            if (Player.altFunctionUse != 2)//left click
            {

                Item.shoot = ModContent.ProjectileType<TouhouKnives_proj>();
                Item.useTime = 10;
                Item.useAnimation = 10;
                Item.UseSound = SoundID.Item1;
                Item.mana = 10;
            }
            else
            {//right click
                Item.mana = 25;
                if (Player.statMana >= Player.GetManaCost(Item))
                {
                    Item.useTime = 10;
                    Item.useAnimation = 10;
                    Item.shoot = ProjectileID.None;
                    Item.UseSound = SoundID.Item1;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {

            Projectile.NewProjectile(source, position, velocity.RotateRandom(MathHelper.PiOver2 / 6), type, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position, velocity.RotateRandom(MathHelper.PiOver2 / 6), type, damage, knockback, player.whoAmI);

            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
        public override void AddRecipes()
        {

            CreateRecipe()
                .AddIngredient(ItemID.SilverBar, 5)
                .AddIngredient(ItemID.SoulofLight, 6)
                .AddTile(TileID.AdamantiteForge)
                .Register();
            base.AddRecipes();
        }

    }
}
