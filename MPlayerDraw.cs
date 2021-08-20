﻿
using KirillandRandom.Buffs;
using KirillandRandom.Items;
using KirillandRandom.Items.Armor;
using KirillandRandom.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using ReLogic.Content;
using KirillandRandom;
namespace KirillandRandom
{
	public class MPlayerDrawHands : PlayerDrawLayer
	{
		private Asset<Texture2D> HandArmorTexture;
		public override bool IsHeadLayer => false;
		public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.ArmOverItem);
		protected override void Draw(ref PlayerDrawSet drawInfo)
		{
			if (drawInfo.drawPlayer.GetModPlayer<MPlayer>().fireBody)
			{
				if (drawInfo.drawPlayer.mount.Active) return;
					HandArmorTexture = ModContent.Request<Texture2D>("KirillandRandom/Items/Armor/FiresoulRobe_Arms_Glow");
				
				var position = drawInfo.Position + new Vector2(-10f, -10f) - Main.screenPosition;
				position = new Vector2((int)position.X, (int)position.Y + drawInfo.drawPlayer.mount.PlayerHeadOffset);
				if (drawInfo.drawPlayer.mount.Active)
				{
					position += new Vector2(0, -2);
				}
				DrawData drdt = new DrawData(
					HandArmorTexture.Value, //The texture to render.
					position, //Position to render at.
					drawInfo.drawPlayer.bodyFrame, //Source rectangle.
					drawInfo.colorArmorBody == Color.Transparent ? Color.Transparent : new Color(100, 100, 100, 100), //Color.
					0f, //Rotation.
					Vector2.Zero,//exampleItemTexture.Size() * 0.5f, //Origin. Uses the texture's center.
					1f, //Scale.
					drawInfo.drawPlayer.direction == 1f ? SpriteEffects.None : SpriteEffects.FlipHorizontally, //SpriteEffects.
					0 //'Layer'. This is always 0 in Terraria.
				);
				drdt.shader = drawInfo.cBody;
				drawInfo.DrawDataCache.Add(drdt);

			}

		}
	}
	public class MPlayerDrawHead : PlayerDrawLayer
	{
		private Asset<Texture2D> HeadArmorTexture;
		public override bool IsHeadLayer => true;
		public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.Head);
		protected override void Draw(ref PlayerDrawSet drawInfo)
		{
			if (drawInfo.drawPlayer.mount.Active) return;

			//Main.NewText(Convert.ToString(drawInfo.drawPlayer.mount.PlayerHeadOffset),Color.Yellow);
			if (drawInfo.drawPlayer.GetModPlayer<MPlayer>().fireHead)
			{
				HeadArmorTexture = ModContent.Request<Texture2D>("KirillandRandom/Items/Armor/FiresoulRobeHood_Head_Glow");
				var position = drawInfo.Position + new Vector2(-10f, -10f) - Main.screenPosition;
				position = new Vector2((int)position.X, (int)position.Y + drawInfo.mountOffSet);
				//if (drawInfo.drawPlayer.mount.Active)
    //            {
				//	position += new Vector2(0,-2);
    //            }
				DrawData drdt = new DrawData(
					HeadArmorTexture.Value, //The texture to render.
					position, //Position to render at.
					drawInfo.drawPlayer.legFrame, //Source rectangle. //for some reason headFrame doesn't work correctly? Investigate.
					drawInfo.colorArmorHead == Color.Transparent ? Color.Transparent : new Color(100, 100, 100, 100), //Color. 
					0f, //Rotation.
					Vector2.Zero,//exampleItemTexture.Size() * 0.5f, //Origin. Uses the texture's center.
					1f, //Scale.
					drawInfo.drawPlayer.direction == 1f ? SpriteEffects.None : SpriteEffects.FlipHorizontally, //SpriteEffects.
					0 //'Layer'. This is always 0 in Terraria.
				);
				drdt.shader = drawInfo.cHead;
				drawInfo.DrawDataCache.Add(drdt);

			}

		}
	}
	public class MPlayerDrawBody : PlayerDrawLayer
	{
		private Asset<Texture2D> BodyArmorTexture;
		public override bool IsHeadLayer => false;
		public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.Torso);
		protected override void Draw(ref PlayerDrawSet drawInfo)
		{
			if (drawInfo.drawPlayer.mount.Active) return;

			if (drawInfo.drawPlayer.GetModPlayer<MPlayer>().fireBody)
			{
				BodyArmorTexture = ModContent.Request<Texture2D>("KirillandRandom/Items/Armor/FiresoulRobe_Body_Glow");

				var position = drawInfo.Position + new Vector2(-10f, -10f) - Main.screenPosition;
				position = new Vector2((int)position.X, (int)position.Y + drawInfo.drawPlayer.mount.PlayerHeadOffset);
				if (drawInfo.drawPlayer.mount.Active)
				{
					position += new Vector2(0, -2);
				}
				DrawData drdt = new DrawData(
					BodyArmorTexture.Value, //The texture to render.
					position, //Position to render at.
					drawInfo.drawPlayer.bodyFrame, //Source rectangle.
					drawInfo.colorArmorBody == Color.Transparent ? Color.Transparent : new Color(100, 100, 100, 100), //Color.
					0f, //Rotation.
					Vector2.Zero,//exampleItemTexture.Size() * 0.5f, //Origin. Uses the texture's center.
					1f, //Scale.
					drawInfo.drawPlayer.direction == 1f ? SpriteEffects.None : SpriteEffects.FlipHorizontally, //SpriteEffects.
					0 //'Layer'. This is always 0 in Terraria.
				);
				drdt.shader = drawInfo.cBody;
				drawInfo.DrawDataCache.Add(drdt);

			}

		}
	}


		public class MPlayerDrawLegs : PlayerDrawLayer
	{
		private Asset<Texture2D> LegArmorTexture;
		public override bool IsHeadLayer => false;
		public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.Leggings);
		protected override void Draw(ref PlayerDrawSet drawInfo)
		{
			if (drawInfo.drawPlayer.mount.Active) return;
			if (drawInfo.drawPlayer.GetModPlayer<MPlayer>().Hexed)
			{
				LegArmorTexture = ModContent.Request<Texture2D>("KirillandRandom/Items/Armor/Pig_Legs");

				var position = drawInfo.Position + new Vector2(-10f, -10f) - Main.screenPosition;
				position = new Vector2((int)position.X, (int)position.Y + drawInfo.drawPlayer.mount.PlayerHeadOffset);
				if (drawInfo.drawPlayer.mount.Active)
				{
					position += new Vector2(0, -2);
				}
				drawInfo.DrawDataCache.Add(new DrawData(
					LegArmorTexture.Value, //The texture to render.
					position, //Position to render at.
					drawInfo.drawPlayer.legFrame, //Source rectangle.
					Lighting.GetColor((int)drawInfo.Center.X / 16, (int)drawInfo.Center.Y / 16, Color.White), //Color.
					0f, //Rotation.
					Vector2.Zero,//exampleItemTexture.Size() * 0.5f, //Origin. Uses the texture's center.
					1f, //Scale.
					drawInfo.drawPlayer.direction == 1f ? SpriteEffects.None : SpriteEffects.FlipHorizontally, //SpriteEffects.
					0 //'Layer'. This is always 0 in Terraria.
				));


				return;
            }

				if (drawInfo.drawPlayer.GetModPlayer<MPlayer>().fireLeggings)
			{
				
					  LegArmorTexture = ModContent.Request<Texture2D>("KirillandRandom/Items/Armor/FiresoulRobeLeggings_Legs_Glow");

				var position = drawInfo.Position + new Vector2(-10f, -10f) - Main.screenPosition;
				if (drawInfo.isSitting)
                {
					position += new Vector2(drawInfo.drawPlayer.direction == 1 ? 6 : -6, -7);
				}
				
				position = new Vector2((int)position.X, (int)position.Y + drawInfo.drawPlayer.mount.PlayerHeadOffset);
				if (drawInfo.drawPlayer.mount.Active)
				{
					position += new Vector2(0, -2);
				}
				DrawData drdt = new DrawData(
					LegArmorTexture.Value, //The texture to render.
					position, //Position to render at.
					drawInfo.drawPlayer.legFrame, //Source rectangle.
					drawInfo.colorArmorLegs == Color.Transparent ? Color.Transparent : new Color(100, 100, 100, 100), //Color.
					0f, //Rotation.
					Vector2.Zero,//exampleItemTexture.Size() * 0.5f, //Origin. Uses the texture's center.
					1f, //Scale.
					drawInfo.drawPlayer.direction == 1f ? SpriteEffects.None : SpriteEffects.FlipHorizontally, //SpriteEffects.
					0 //'Layer'. This is always 0 in Terraria.
				);
				drdt.shader = drawInfo.cLegs;
				drawInfo.DrawDataCache.Add(drdt);

			}

		}
	}
}