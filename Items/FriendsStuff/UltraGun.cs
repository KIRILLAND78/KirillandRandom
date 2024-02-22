using KirillandRandom.Projectiles.FriendsStuff;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace KirillandRandom.Items.FriendsStuff
{
    internal class UltraGun : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Item.DefaultToRangedWeapon(ModContent.ProjectileType<UltraGunShot>(), AmmoID.None, 30, 10, false);
            Item.damage = 90;
            Item.knockBack = 2;

            Item.crit = 10;
            var f = SoundID.Item40;
            f.Pitch = 0.3f;
            Item.UseSound = f;
            base.SetDefaults();
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(8, 0);
        }
        public override bool AltFunctionUse(Player Player)
        {
            if (Player.GetModPlayer<MPlayer>().Coin > 0)
            {
                Player.GetModPlayer<MPlayer>().Coin--;
                return true;
            }
            return false;
        }
        public override bool CanUseItem(Player player)
        {

            if (player.altFunctionUse == 2)
            {
                var f = SoundID.CoinPickup;
                f.Pitch = 0.3f;
                Item.UseSound = f;
                return true;
            }
            else
            {

                var f = SoundID.Item40;
                f.Pitch = 0.3f;
                Item.UseSound = f;
                return base.CanUseItem(player);
            }
        }
        public override float UseSpeedMultiplier(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                return 100f;
            }
            return base.UseSpeedMultiplier(player);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.HallowedBar, 5)
                .AddIngredient(ItemID.IllegalGunParts, 1)
                .AddIngredient(ItemID.SoulofFright, 20)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                Projectile.NewProjectile(source, position, velocity * 1.2f, ModContent.ProjectileType<UltraGunCoin>(), damage, knockback, Main.myPlayer);
                return false;
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
    }
}
