using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace KirillandRandom.Items
{
    public class Firegun : ModItem
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Nethersong");
            // Tooltip.SetDefault("Spreads fire in a cone.\r\nUses gel for ammo\r\nWIP!\r\n20% chance to not consume ammo");
        }




        public override void SetDefaults()
        {
            Item.useAmmo = AmmoID.Gel;
            Item.noMelee = true;
            Item.shoot = ProjectileID.Flames;
            Item.shootSpeed = 6.8f;
            Item.damage = 22;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 30;
            Item.height = 90;
            Item.useTime = 6;
            Item.useAnimation = 18;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 0;
            Item.value = 10000;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item34;
            Item.autoReuse = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Flamethrower, 1)
                .AddIngredient(ItemID.ShroomiteBar, 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, -2);
        }
        public override bool CanConsumeAmmo(Item ammo, Player Player)
        {
            return ((Player.itemAnimation >= Player.itemAnimationMax - 2) && (Main.rand.NextFloat() >= .2f));
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 pos, Vector2 velocity, int type, int damage, float knockBack)
        {
            int fire = ProjectileID.Flames;


            for (int i = 0; i <= 2; i++)
            {
                Vector2 speed2 = velocity.RotatedByRandom(MathHelper.ToRadians(22));
                Projectile.NewProjectile(source, pos, speed2, fire, damage, knockBack, Main.myPlayer);

            }

            return false;
        }

    }
}
