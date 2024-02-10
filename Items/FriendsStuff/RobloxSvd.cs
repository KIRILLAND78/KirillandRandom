using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace KirillandRandom.Items.FriendsStuff
{
    internal class RobloxSvd : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Item.DefaultToRangedWeapon(ProjectileID.Bullet, AmmoID.Bullet, 17, 30, false);
            Item.damage = 106;
            Item.knockBack = 8;
            Item.crit = 12;
            var f = SoundID.Item40;
            f.Pitch = -0.6f;
            Item.UseSound = f;
            base.SetDefaults();
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-8, 0);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.SniperRifle, 1)
                .AddIngredient(ItemID.ShroomiteBar, 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();
            base.AddRecipes();
        }

    }
}
