using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace KirillandRandom.Items
{
	public class Curiosity : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Something"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("UGA-CHAGA!!!");
		}
        public override bool AltFunctionUse(Player player)
        {
			return true;
		}
        public override bool CanUseItem(Player player)
        {
			if (player.altFunctionUse == 2)
			{

				item.useStyle = ItemUseStyleID.SwingThrow;
				item.shoot = ProjectileID.ChargedBlasterOrb;
				item.useTime = 30;
				item.useAnimation = 30;

				item.shootSpeed = 30;
				item.knockBack = 1;
				item.useStyle = ItemUseStyleID.Stabbing;
				item.damage = 30;

				item.UseSound = SoundID.DD2_SkyDragonsFuryShot;
			}
			else
			{
				
				item.shoot = ProjectileID.None;
				item.damage = 50;
				item.melee = true;
				item.width = 40;
				item.height = 90;
				item.useTime = 30;
				item.useAnimation = 30;
				item.useStyle = ItemUseStyleID.SwingThrow;
				item.knockBack = 6;
				item.value = 10000;
				item.rare = ItemRarityID.Expert;
				item.UseSound = SoundID.Item1;
				item.autoReuse = true;
			}
			return true;
        }
        public override void SetDefaults()
		{
			item.damage = 50;
			item.melee = true;
			item.width = 40;
			item.height = 90;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = ItemRarityID.Expert; 
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}


		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			{
				target.AddBuff(BuffID.CursedInferno, 30);//СИНИЙ ОГОНЬ СЮДА. ИЛИ ВООБЩЕ ЧТО-ТО ДРУГОЕ.
			}
		}



		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}



		//КОД ДАЛЬШЕ Я ТУПО СПИЗД... позаимствовал с документации. и изменил немного. да, вот такой я гадкий.
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 speed = new Vector2(speedX, speedY);
			speed = speed.RotatedByRandom(MathHelper.ToRadians(5));
			speedX = speed.X;
			speedY = speed.Y;
			return true;//это что-то типа коррекции поведения снарядов... наверное? По типу изменения урона, угла стрельбы и т.д.
		}

	}
}
