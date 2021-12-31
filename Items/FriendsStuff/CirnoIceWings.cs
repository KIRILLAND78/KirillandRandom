using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ModLoader;

namespace KirillandRandom.Items.FriendsStuff

{
	[AutoloadEquip(EquipType.Wings)]
	public class CirnoIceWings : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Baka!");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			// These wings use the same values as the solar wings
			// Fly time: 180 ticks = 3 seconds
			// Fly speed: 9
			// Acceleration multiplier: 2.5
			ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(110, 7f, 2f);
		}

		public override void SetDefaults()
		{
			
			Item.width = 22;
			Item.height = 20;
			Item.value = 10000;
			Item.rare = ItemRarityID.Green;
			Item.accessory = true;
		}
		public override bool WingUpdate(Player player, bool inUse)
		{
			if (inUse)
			{if (player.wingTime + 1 == player.wingTimeMax)
                {
					player.wingFrame = 1;
                }
				if (player.wingTime > 0)
				{
					player.wingFrameCounter++;
				}
				if (player.wingFrameCounter > 6)//Как часто меняется кадр
				{
					player.wingFrame++;
					if (player.wingFrame == 2)
                    {
						//Звук
                    }
					player.wingFrameCounter = 0;
					if (player.wingFrame >3)//Переключение кадров
					{
						player.wingFrame = 0;
					}
				}
            }
            else
			{
				player.wingFrame = 1;//падение
				if (player.wingTime <= 0 && player.controlJump)
				{
					player.wingFrame = 2;//парение
				}
				if (player.wingTime == player.wingTimeMax)
				{
					player.wingFrame = 0;
				}
            }
return true;
		}


		public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
			ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
		{
			ascentWhenFalling = 2.1f; // Falling glide speed
			ascentWhenRising = 0.15f; // Rising speed
			maxCanAscendMultiplier = 1f;
			maxAscentMultiplier = 3f;
			constantAscend = 0.215f;
		}

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.IceFeather, 1)
				.AddIngredient(ItemID.SoulofFlight,20)
				.Register();
		}
	}
}