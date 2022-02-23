using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KirillandRandom.Projectiles
{
	public class WatersoulGuardianStaffP : ModProjectile
	{

		public int recharge = 0;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Something");
			Main.projFrames[Projectile.type] = 1;
			//Main.projPet[Projectile.type] = true;
		}

		public void MassAttack(int damage, int knockback)
		{

			if (Main.myPlayer == Projectile.owner)
			{
				if (recharge <= 0 && Main.player[Projectile.owner].CheckMana((int)(120 * Main.player[Projectile.owner].manaCost)))
				{
					Main.player[Projectile.owner].statMana -= (int)(120 * Main.player[Projectile.owner].manaCost);
					Main.player[Projectile.owner].manaRegenDelay = 100;
					for (int a = 0; a < 8; a++)
					{
						Vector2 vel = (Vector2.UnitX).RotatedBy(MathHelper.TwoPi / 8 * a);
						vel = vel * 6;
						int f =Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center + Vector2.UnitX.RotatedBy(MathHelper.TwoPi / 8 * a) * 10, vel, ModContent.ProjectileType<Aquabolt>(), Main.player[Projectile.owner].GetWeaponDamage(Main.player[Projectile.owner].HeldItem), Main.player[Projectile.owner].GetWeaponKnockback(Main.player[Projectile.owner].HeldItem, Main.player[Projectile.owner].HeldItem.knockBack), Main.myPlayer);
						Main.projectile[f].tileCollide = false;
						Main.projectile[f].ai[0] = 5;
					}
					recharge = 100;
				}
			}
		}
		public void Shoot(int damage, int knockback)
		{
			if (recharge <= 0&& Main.player[Projectile.owner].CheckMana((int)(30* Main.player[Projectile.owner].manaCost)))
			{
				if (Main.myPlayer == Projectile.owner)
				{
					Main.player[Projectile.owner].statMana -= (int)(30 * Main.player[Projectile.owner].manaCost);
					Main.player[Projectile.owner].manaRegenDelay = 100;
					Vector2 vel = (Main.MouseWorld - Projectile.Center);
					vel.Normalize();
					vel = vel * 8;
					for (int a = 0; a < 3; a++)
					{
						Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center + Vector2.UnitX.RotatedBy(MathHelper.TwoPi / 3 * a) * 10, vel, ModContent.ProjectileType<Aquabolt>(), (int)(Main.player[Projectile.owner].GetWeaponDamage(Main.player[Projectile.owner].HeldItem)*0.8f), Main.player[Projectile.owner].GetWeaponKnockback(Main.player[Projectile.owner].HeldItem, Main.player[Projectile.owner].HeldItem.knockBack), Main.myPlayer);
					}
					recharge = 15;
				}
			}

		}



		public override void SetDefaults()
		{
			Projectile.light = 0.1f;
			Projectile.CloneDefaults(ProjectileID.ZephyrFish);
			Main.projFrames[Projectile.type] = 1;
			AIType = ProjectileID.ZephyrFish;
		}
        public override void Kill(int timeLeft)
        {
			Main.player[Projectile.owner].GetModPlayer<MPlayer>().WatSoulMode = 0;
			Main.player[Projectile.owner].GetModPlayer<MPlayer>().WatSoulHelper = null;

			base.Kill(timeLeft);
        }

        public override bool PreAI()
		{
			if (Main.myPlayer == Projectile.owner)
			{
				if (recharge > 0)
				{
					recharge--;
				}
			}
			
			Player Player = Main.player[Projectile.owner];

			if ((Player.Center - Projectile.Center).Length() > 800)
            {
				Projectile.Center = Player.Center;
            }

			if (Projectile.ai[0] == 1)
			{
				Projectile.ai[0] = 0;
				if (Player.GetModPlayer<MPlayer>().WatSoulMode == 2)
				{
					Shoot((int)Projectile.ai[1], 5);
                }
                else
                {
					MassAttack((int)Projectile.ai[1], 5);
                }
			}

			if (Player.GetModPlayer<MPlayer>().WatSoulMode<=1)
            {
				Projectile.Kill();
            }

			Projectile.light = 0.1f;
			return true;
			
		}

        public override bool PreDraw(ref Color lightColor)
		{
			Projectile.frame = 0;
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

			Projectile.timeLeft = 2;
			
			if (Player.dead)
			{
				Projectile.Kill();
			}
		}
	}
}