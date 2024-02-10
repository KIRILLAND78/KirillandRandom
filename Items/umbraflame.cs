using KirillandRandom.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace KirillandRandom.Items
{
    public class UmbraFlame : ModItem
    {
        public int first = 1;
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Right click to release flames. +16 damage boost for each summoned circle of flames.");
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override void HoldItem(Player player)
        {
            player.GetModPlayer<MPlayer>().angle += 4f;
            if (player.GetModPlayer<ItemDrawPlayer>().flyingItemDraw == false)
            {
                player.GetModPlayer<ItemDrawPlayer>().flyingItemDraw = true;
                player.GetModPlayer<ItemDrawPlayer>().flyingItemAsset = ModContent.Request<Texture2D>("KirillandRandom/Items/UmbraFlame");
                player.GetModPlayer<ItemDrawPlayer>().itemLookForForDrawing = Item;
            }
            base.HoldItem(player);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.FragmentNebula, 8)
                .AddIngredient(ItemID.FragmentSolar, 8)
                .AddIngredient(ItemID.FragmentStardust, 8)
                .AddIngredient(ItemID.FragmentVortex, 8)

                .AddIngredient(ModContent.ItemType<LastFlame>(), 1)

                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }

        public override bool CanUseItem(Player Player)
        {
            if (Player.altFunctionUse != 2)
            {
                if ((Player.statMana >= (30)) && (Player.GetModPlayer<MPlayer>().flames_summoned < 16))
                {
                    Item.shoot = ProjectileID.None;
                    if (Player.GetModPlayer<MPlayer>().flames_summoned < 16)
                    {
                        Item.shoot = ModContent.ProjectileType<UmbraFlameBolt>();

                    }

                    Item.mana = 30;
                    Item.useTime = 40;
                    Item.useAnimation = 40;
                    Item.useStyle = ItemUseStyleID.Shoot;
                    Item.UseSound = SoundID.DD2_ExplosiveTrapExplode;
                    return true;
                }
                return false;
            }
            else
            {

                Item.mana = 0;
                Player.GetModPlayer<MPlayer>().flames_summoned = 0;
                Item.shoot = ProjectileID.None;
                Item.useTime = 5;
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.useAnimation = 5;
                Item.UseSound = SoundID.DD2_FlameburstTowerShot;
            }
            return true;
        }
        public override void SetDefaults()
        {

            Item.noUseGraphic = true;
            Item.damage = 90;
            Item.mana = 30;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Magic;
            Item.useTime = 5;
            Item.shootSpeed = 0;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 0;
            Item.value = 10000;
            Item.rare = ItemRarityID.Purple;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }
        public override bool Shoot(Player Player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (Player.GetModPlayer<MPlayer>().flames_summoned == 8)
            {
                for (int i = 9; i <= 16; i++)
                {
                    Projectile.NewProjectile(source, position, Vector2.Zero, type, damage, knockback, Player.whoAmI, i);
                }
                Player.GetModPlayer<MPlayer>().flames_summoned += 8;
            }
            if (Player.GetModPlayer<MPlayer>().flames_summoned == 3)
            {
                for (int i = 4; i <= 8; i++)
                {
                    Projectile.NewProjectile(source, position, Vector2.Zero, type, damage, knockback, Player.whoAmI, i);
                }
                Player.GetModPlayer<MPlayer>().flames_summoned += 5;
            }
            if (Player.GetModPlayer<MPlayer>().flames_summoned == 0)
            {
                for (int i = 1; i <= 3; i++)
                {
                    Projectile.NewProjectile(source, position, Vector2.Zero, type, damage, knockback, Player.whoAmI, i);
                }
                Player.GetModPlayer<MPlayer>().flames_summoned += 3;
            }

            return false;
        }


    }
}
