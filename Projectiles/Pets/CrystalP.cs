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
			Main.projFrames[projectile.type] = 4;
			Main.projPet[projectile.type] = true;
		}

		public override void SetDefaults()
		{
			projectile.light = 0.6f;
			projectile.CloneDefaults(ProjectileID.ZephyrFish);
			aiType = ProjectileID.ZephyrFish;
		}

		public override bool PreAI()
		{
			Player player = Main.player[projectile.owner];
			player.zephyrfish = false;

			projectile.light = 0.6f;
			return true;
			
		}
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			SpriteEffects spriteEffects = SpriteEffects.None;
			Texture2D texture = Main.projectileTexture[projectile.type];
			int frameHeight = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
			int startY = frameHeight * projectile.frame;
			Rectangle sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);
			Vector2 origin = sourceRectangle.Size() / 2f;

			Color drawColor = projectile.GetAlpha(lightColor);
			Main.spriteBatch.Draw(texture,
			projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY),
			sourceRectangle, drawColor, projectile.rotation, origin, projectile.scale, spriteEffects, 0f);

			return false;
		}
        public override void AI()
		{
			
			Player player = Main.player[projectile.owner];
			MPlayer modPlayer = player.GetModPlayer<MPlayer>();
			if (player.dead)
			{
				modPlayer.Something = false;
			}
			if (modPlayer.Something)
			{
				projectile.timeLeft = 2;
			}
		}
	}
}