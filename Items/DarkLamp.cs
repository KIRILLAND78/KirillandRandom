using KirillandRandom.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace KirillandRandom.Items
{
    public class DarkLamp : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 12;
            Item.holdStyle = 1;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 120;
            Item.useTime = 120;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.flame = true;
            Item.value = 50;


            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<DarkLampLight>();
            Item.shootSpeed = 0;
            Item.damage = 101;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 120;
            Item.knockBack = 10;
            Item.value = 10000;
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item34;
        }
        public override void HoldItem(Player Player)
        {
            if (Main.rand.NextBool(Player.itemAnimation > 0 ? 40 : 80))
            {
                Dust.NewDust(new Vector2(Player.itemLocation.X + 12f * Player.direction, Player.itemLocation.Y - 12f * Player.gravDir), 4, 4, DustID.Torch);
            }
            Vector2 position = Player.RotatedRelativePoint(new Vector2(Player.itemLocation.X + 12f * Player.direction + Player.velocity.X, Player.itemLocation.Y - 14f + Player.velocity.Y), true);
            Lighting.AddLight(position, 1f, 1f, 1f);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.AddBuff(BuffID.OnFire, 340);
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.AdamantiteBar, 8)

            .AddIngredient(ItemID.LivingFireBlock, 50)

            .AddIngredient(ItemID.SoulofNight, 10)

            .AddIngredient(ItemID.SoulofLight, 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();

            CreateRecipe()

            .AddIngredient(ItemID.TitaniumBar, 8)

            .AddIngredient(ItemID.LivingFireBlock, 50)

            .AddIngredient(ItemID.SoulofNight, 10)

            .AddIngredient(ItemID.SoulofLight, 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();


        }


    }
}
