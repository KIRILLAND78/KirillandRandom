using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace KirillandRandom.Projectiles
{
    public class GemRingLaser : ModProjectile
	{

			private const float MAX_CHARGE = 50f;
			//The distance charge particle from the player center
			private const float MOVE_DISTANCE = 60f;

			// The actual distance is stored in the ai0 field
			// By making a property to handle this it makes our life easier, and the accessibility more readable
			public float Distance
			{
				get => projectile.ai[0];
				set => projectile.ai[0] = value;
			}

			// The actual charge value is stored in the localAI0 field
			public float Charge
			{
				get => projectile.localAI[0];
				set => projectile.localAI[0] = value;
			}

			// Are we at max charge? With c#6 you can simply use => which indicates this is a get only property
			public bool IsAtMaxCharge => Charge == MAX_CHARGE;
			public override void SetDefaults()
			{
				
				projectile.width = 10;
				projectile.height = 10;
				projectile.friendly = true;
				projectile.penetrate = -1;
				projectile.tileCollide = false;
				projectile.magic = true;
				projectile.hide = true;
			}
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
			return false;
        }
		 

			// Change the way of collision check of the projectile
			public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
			{
				// We can only collide if we are at max charge, which is when the laser is actually fired
				if (!IsAtMaxCharge) return false;

				Player player = Main.player[projectile.owner];
				Vector2 unit = projectile.velocity;
				float point = 0f;
				// Run an AABB versus Line check to look for collisions, look up AABB collision first to see how it works
				// It will look for collisions on the given line using AABB
				return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), player.Center,
					player.Center + unit * Distance, 22, ref point);
			}

			// Set custom immunity time on hitting an NPC
			public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
			{
				target.immune[projectile.owner] = 5;
			}

			// The AI of the projectile
			public override void AI()
			{
				Player player = Main.player[projectile.owner];
				projectile.position = player.Center + projectile.velocity * MOVE_DISTANCE;
				projectile.timeLeft = 2;

				if ((player.Center - Main.player[(int)projectile.ai[1]].Center).Length() >= 1500)
            {
				projectile.Kill();
				return;
            }
				// By separating large AI into methods it becomes very easy to see the flow of the AI in a broader sense
				// First we update player variables that are needed to channel the laser
				// Then we run our charging laser logic
				// If we are fully charged, we proceed to update the laser's position
				// Finally we spawn some effects like dusts and light

				UpdatePlayer(player);
				ChargeLaser(player);

				// If laser is not charged yet, stop the AI here.
				if (Charge < MAX_CHARGE) return;

				SetLaserPosition(player);
				SpawnDusts(player);
				Main.player[(int)projectile.ai[1]].AddBuff(ModContent.BuffType<Buffs.LinkProtection>(), 100);
				player.AddBuff(ModContent.BuffType<Buffs.LinkProtection>(), 100);
		}

			private void SpawnDusts(Player player)
			{

				Vector2 unit = projectile.velocity * -1;
				Vector2 dustPos = player.Center + projectile.velocity * Distance;
			if (Main.time % 5 < 1)
			{
				for (int i = 0; i < 2; ++i)
				{
					float num1 = projectile.velocity.ToRotation() + (Main.rand.Next(2) == 1 ? -1.0f : 1.0f) * 1.57f;
					float num2 = (float)(Main.rand.NextDouble() * 0.8f + 1.0f);
					Vector2 dustVel = new Vector2((float)Math.Cos(num1) * num2, (float)Math.Sin(num1) * num2);
				}
			}

			if (IsAtMaxCharge)
            {int abc= (int)((player.Center - Main.player[(int)projectile.ai[1]].Center).Length() / 15);
				for (int i = 1; i<abc; i++)
                {
					int bb=Dust.NewDust(player.Center - ((player.Center - Main.player[(int)projectile.ai[1]].Center) / abc * i), 1,1,63, default,default, 50, Color.LightSkyBlue, 0.8f);
					Main.dust[bb].noGravity = true;
				}

            }


			}

			/*
			 * Sets the end of the laser position based on where it collides with something
			 */
			private void SetLaserPosition(Player player)
		{
			for (Distance = MOVE_DISTANCE; Distance <= (float)5 * ((int)((projectile.Center - Main.player[(int)projectile.ai[1]].Center) / 5).Length()) - MOVE_DISTANCE; Distance += 5f)
			{
				}
			}

			private void ChargeLaser(Player player)
			{
				// Kill the projectile if the player stops channeling
				if (!player.channel)
				{
					projectile.Kill();
				}
				else
				{
					// Do we still have enough mana? If not, we kill the projectile because we cannot use it anymore
					if (Main.time % 20 < 1 && !player.CheckMana(player.inventory[player.selectedItem].mana, false))
					{
						projectile.Kill();
					}
					Vector2 offset = projectile.velocity;
					offset *= MOVE_DISTANCE - 20;
					Vector2 pos = player.Center + offset - new Vector2(10, 10);
					if (Charge < MAX_CHARGE)
					{
						Charge++;
					}
					int chargeFact = (int)(Charge / 20f);
					Vector2 dustVelocity = Vector2.UnitX * 18f;
					dustVelocity = dustVelocity.RotatedBy(projectile.rotation - 1.57f);
					Vector2 spawnPos = projectile.Center + dustVelocity;
				}
			}

			private void UpdatePlayer(Player player)
			{
				if (projectile.owner == Main.myPlayer)
				{
				
					Vector2 diff = Main.player[(int)projectile.ai[1]].Center - player.Center;
					diff.Normalize();
					projectile.velocity = diff;
					projectile.direction = Main.player[(int)projectile.ai[1]].Center.X > player.position.X ? 1 : -1;
					projectile.netUpdate = true;
				
				}
				int dir = projectile.direction;
				//player.ChangeDir(dir); // Set player direction to where we are shooting
				player.heldProj = projectile.whoAmI; // Update player's held projectile
				player.itemTime = 2; // Set item time to 2 frames while we are used
				player.itemAnimation = 2; // Set item animation time to 2 frames while we are used
				player.itemRotation = (float)Math.Atan2(projectile.velocity.Y * dir, projectile.velocity.X * dir); // Set the item rotation to where we are shooting
			}
			public override bool ShouldUpdatePosition() => true;

		}
	}