using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KirillandRandom.Projectiles.Pets
{
	public class CrystalP : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Something"); 
			Main.projFrames[Projectile.type] = 4;
			Main.projPet[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.light = 0.6f;
			Projectile.CloneDefaults(ProjectileID.ZephyrFish);
			AIType = ProjectileID.ZephyrFish;
		}

		public override bool PreAI()
		{
			Player Player = Main.player[Projectile.owner];
			Player.zephyrfish = false;

			Projectile.light = 0.6f;
			return true;
			
		}

        public override bool PreDraw(ref Color lightColor)
        {
			SpriteEffects spriteEffects = SpriteEffects.None;
			Texture2D texture = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;
			int frameHeight = texture.Height / Main.projFrames[Projectile.type];
			int startY = frameHeight * Projectile.frame;
			Rectangle sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);
			Vector2 origin = sourceRectangle.Size() / 2f;

			Color drawColor = Projectile.GetAlpha(lightColor);
			Main.spriteBatch.Draw(texture,
			Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
			sourceRectangle, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);

			return false;
		}
        public override void AI()
		{
			
			Player Player = Main.player[Projectile.owner];
			MPlayer modPlayer = Player.GetModPlayer<MPlayer>();
			if (Player.dead)
			{
				modPlayer.Something = false;
			}
			if (modPlayer.Something)
			{
				Projectile.timeLeft = 2;
			}
		}
	}
}