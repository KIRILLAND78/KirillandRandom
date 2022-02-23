using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Text;
using KirillandRandom.Projectiles;
using Terraria.DataStructures;

namespace KirillandRandom.Items
{
	public class WatersoulGuardianStaff : ModItem
	{
		public int first = 1;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("WatersoulGuardianStaff");
			Tooltip.SetDefault("WIP");
		}
		public override bool AltFunctionUse(Player Player)
		{
			return true;
		}

        public override bool CanUseItem(Player Player)
		{
			Item.DamageType = DamageClass.Magic;
			
			if (Player.altFunctionUse == 2)
			{
				Item.mana = 0;
				Item.shoot = ProjectileID.None;//SPINNING SCYTHE
				Item.useTime = 1;
				Item.autoReuse = false;


				Item.noUseGraphic = true;
				Item.noMelee = true;
				Item.channel = true;
				Item.shootSpeed = 0;

				Item.useAnimation = 1;
				Item.useStyle = ItemUseStyleID.HoldUp;
				Item.UseSound = SoundID.DD2_SkyDragonsFurySwing;
				Player.GetModPlayer<MPlayer>().WatSoulMode++;
				if (Player.GetModPlayer<MPlayer>().WatSoulMode == 4) Player.GetModPlayer<MPlayer>().WatSoulMode = 0;
				if (Player.GetModPlayer<MPlayer>().WatSoulMode == 0) CombatText.NewText(Player.getRect(), Color.CadetBlue, "Player: Attack", true, true);
				if (Player.GetModPlayer<MPlayer>().WatSoulMode == 1) CombatText.NewText(Player.getRect(), Color.CadetBlue, "Player: Defense", true, true);
				if (Player.GetModPlayer<MPlayer>().WatSoulMode == 2) CombatText.NewText(Player.getRect(), Color.CadetBlue, "Staff: Attack", true, true);
				if (Player.GetModPlayer<MPlayer>().WatSoulMode == 3) CombatText.NewText(Player.getRect(), Color.CadetBlue, "Staff: Mass Attack", true, true);
				if (Player.GetModPlayer<MPlayer>().WatSoulMode >= 2)
				{
					if (Player.GetModPlayer<MPlayer>().WatSoulHelper == null)
					{
						int a = Projectile.NewProjectile(Player.GetProjectileSource_Item(Item), Player.Center, Vector2.Zero, ModContent.ProjectileType<WatersoulGuardianStaffP>(), 0, 0, Player.whoAmI);
						Player.GetModPlayer<MPlayer>().WatSoulHelper = Main.projectile[a];
						//Item.shoot = ModContent.ProjectileType<WatersoulGuardianStaffP>();

					}
				}
				return true;
            }
            else
			{
				if (Player.GetModPlayer<MPlayer>().WatSoulMode >= 2)
				{
					Item.mana = 0;
					Item.channel = false;
					Item.shootSpeed = 1;
					Item.useTime = 1;
					Item.useStyle = ItemUseStyleID.Swing;
					Item.useAnimation = 1;
					Item.autoReuse = true;
					Item.shoot = ProjectileID.None;
					Item.UseSound = SoundID.DD2_SkyDragonsFuryShot.WithVolume(0);
					if (Player.GetModPlayer<MPlayer>().WatSoulMode == 2)
					{//MATTACK
						Player.GetModPlayer<MPlayer>().WatSoulHelper.ai[0] = 1;
						Player.GetModPlayer<MPlayer>().WatSoulHelper.ai[1] = 5;
						//Player.GetModPlayer<MPlayer>().WatSoulHelper.ai[2] = 5;
					}
					if (Player.GetModPlayer<MPlayer>().WatSoulMode == 3)
					{//SHOOT
						Player.GetModPlayer<MPlayer>().WatSoulHelper.ai[0] = 1;
						Player.GetModPlayer<MPlayer>().WatSoulHelper.ai[1] = 5;
						//Player.GetModPlayer<MPlayer>().WatSoulHelper.ai[2] = 5;
					}

                }
                else
				{
					Item.UseSound = SoundID.Item65.WithVolume(0);
					Item.noUseGraphic = false;
					if (Player.GetModPlayer<MPlayer>().WatSoulMode == 0)
					{
						Item.mana = 15;
						Item.channel = false;
						Item.shootSpeed = 3;
						Item.useTime = 10;
						Item.useStyle = ItemUseStyleID.Shoot;
						Item.useAnimation = 10;
						Item.autoReuse = true;
						Item.shoot = ModContent.ProjectileType<Aquabolt>();
					}
					if (Player.GetModPlayer<MPlayer>().WatSoulMode == 1)
					{
						Item.mana = 10;
						Item.channel = false;
						Item.shootSpeed = 3;
						Item.useTime = 16;
						Item.useStyle = ItemUseStyleID.Shoot;
						Item.useAnimation = 16;
						Item.autoReuse = true;
						Item.shoot = ModContent.ProjectileType<Aquabolt>();
					}
				}
					
				
			}

			return true;
		}
        public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 newpos = position + velocity * 20;
			if (player.GetModPlayer<MPlayer>().WatSoulMode == 1)
            {
				int id = Projectile.NewProjectile(new ProjectileSource_Item(player, Item), newpos, velocity.RotatedBy(MathHelper.PiOver2/3), type, damage, knockback, player.whoAmI);
				Main.projectile[id].timeLeft = 15;
				id = Projectile.NewProjectile(new ProjectileSource_Item(player, Item), newpos, velocity.RotatedBy(MathHelper.PiOver2/6), type, damage, knockback, player.whoAmI);
				Main.projectile[id].timeLeft = 25;
				id = Projectile.NewProjectile(new ProjectileSource_Item(player, Item), newpos, velocity.RotatedBy(-MathHelper.PiOver2/3), type, damage, knockback, player.whoAmI);
				Main.projectile[id].timeLeft = 15;
				id = Projectile.NewProjectile(new ProjectileSource_Item(player, Item), newpos, velocity.RotatedBy(-MathHelper.PiOver2/6), type, damage, knockback, player.whoAmI);
				Main.projectile[id].timeLeft = 25;
				id = Projectile.NewProjectile(new ProjectileSource_Item(player, Item), newpos, velocity, type, damage, knockback, player.whoAmI);
				Main.projectile[id].timeLeft = 40;
				return false;
			}
			Projectile.NewProjectile(new ProjectileSource_Item(player, Item), newpos, velocity, type, damage, knockback, player.whoAmI);
			return false;
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
