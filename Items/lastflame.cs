using KirillandRandom.Projectiles;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KirillandRandom.Items
{
    public class LastFlame : ModItem
    {
        public int first = 1;
        public override bool? CanAutoReuseItem(Player player)
        {
            return true;
        }
        public override bool AltFunctionUse(Player Player)
        {
            return true;
        }
        public override void HoldItem(Player player)
        {

            player.GetModPlayer<MPlayer>().angle += 1.2f;
            if (player.GetModPlayer<ItemDrawPlayer>().flyingItemDraw == false)
            {
                player.GetModPlayer<ItemDrawPlayer>().flyingItemDraw = true;
                player.GetModPlayer<ItemDrawPlayer>().flyingItemAsset = ModContent.Request<Texture2D>("KirillandRandom/Items/LastFlame");
                player.GetModPlayer<ItemDrawPlayer>().itemLookForForDrawing = Item;
            }
            base.HoldItem(player);
        }
        public override void ModifyManaCost(Player player, ref float reduce, ref float mult)
        {
            if (player.altFunctionUse == 2)
            {
                mult = 0;
                return;
            }
            else
            {
                reduce-= (player.GetModPlayer<MPlayer>().flames_summoned-1) * 0.12f;
            }
            base.ModifyManaCost(player, ref reduce, ref mult);
        }
        public override bool CanUseItem(Player Player)
        {
            if ((Player.altFunctionUse != 2) && (Player.GetModPlayer<MPlayer>().flames_summoned < 4))
            {
                //if ((Player.statMana >= (30 - Player.GetModPlayer<MPlayer>().flames_summoned * 5)))
                //if ((Player.statMana >= (30 - Player.GetModPlayer<MPlayer>().flames_summoned * 5)))
                {
                    Item.shoot = ProjectileID.None;
                    if (Player.GetModPlayer<MPlayer>().flames_summoned < 4)
                    {
                        Item.shoot = ModContent.ProjectileType<LastFlameBolt>();
                        Player.GetModPlayer<MPlayer>().flames_summoned += 1;
                    }
                    //Item.mana = 30 - Player.GetModPlayer<MPlayer>().flames_summoned * 5;
                    Item.useTime = 30;
                    Item.useAnimation = 30;
                    Item.useStyle = ItemUseStyleID.HoldUp;
                    Item.UseSound = SoundID.DD2_ExplosiveTrapExplode;

                    return true;
                }
                return false;
            }
            else
            {
                Player.altFunctionUse = 2;
                //Item.mana = 0;
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
            Item.damage = 60;
            Item.mana = 25;
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

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.DemoniteBar, 5)
                .AddIngredient(ItemID.DemonScythe, 1)
                .AddIngredient(ItemID.SoulofNight, 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
