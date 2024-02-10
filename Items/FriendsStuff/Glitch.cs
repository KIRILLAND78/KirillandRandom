using KirillandRandom.Dusts;
using KirillandRandom.Projectiles.FriendsStuff;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace KirillandRandom.Items.FriendsStuff
{
    internal class Glitch : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Item.DefaultToMagicWeapon(ModContent.ProjectileType<GlitchProjectile>(), 10, 0, true);
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.damage = 67;
            Item.mana = 7;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            base.SetDefaults();
        }
        public override void AddRecipes()
        {

            CreateRecipe()
                .AddCondition(Condition.InAether)
                .AddIngredient(ItemID.SoulofFright, 5)
                .AddIngredient(ItemID.SoulofMight, 5)
                .AddIngredient(ItemID.SoulofSight, 5)
                .AddIngredient(ItemID.SoulofNight, 5)
                .AddIngredient(ItemID.SoulofLight, 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();
            base.AddRecipes();
        }
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            if (player.itemTime % 4 == 0)
                Dust.NewDust((Vector2)player.HandPosition - new Vector2(5, 5), 0, 0, ModContent.DustType<BinaryDust>(), Scale: 0.7f);
            base.UseStyle(player, heldItemFrame);
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position = Main.MouseWorld;
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return base.Shoot(player, source, Main.MouseWorld, velocity, type, damage, knockback);
        }
    }
}
