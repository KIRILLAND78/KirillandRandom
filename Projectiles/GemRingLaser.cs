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
			//The distance charge particle from the Player center
			private const float MOVE_DISTANCE = 60f;

			// The actual distance is stored in the ai0 field
			// By making a property to handle this it makes our life easier, and the accessibility more readable
			public float Distance
			{
				get => Projectile.ai[0];
				set => Projectile.ai[0] = value;
			}

			// The actual charge value is stored in the localAI0 field
			public float Charge
			{
				get => Projectile.localAI[0];
				set => Projectile.localAI[0] = value;
			}

			// Are we at max charge? With c#6 you can simply use => which indicates this is a get only property
			public bool IsAtMaxCharge => Charge == MAX_CHARGE;
			public override void SetDefaults()
			{
				
				Projectile.width = 10;
				Projectile.height = 10;
				Projectile.friendly = true;
				Projectile.penetrate = -1;
				Projectile.tileCollide = false;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.hide = true;
			}
		 

			// Change the way of collision check of the Projectile
			public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
			{
				// We can only collide if we are at max charge, which is when the laser is actually fired
				if (!IsAtMaxCharge) return false;

				Player Player = Main.player[Projectile.owner];
				Vector2 unit = Projectile.velocity;
				float point = 0f;
				// Run an AABB versus Line check to look for collisions, look up AABB collision first to see how it works
				// It will look for collisions on the given line using AABB
				return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Player.Center,
					Player.Center + unit * Distance, 22, ref point);
			}

			// Set custom immunity time on hitting an NPC
			public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
			{
				target.immune[Projectile.owner] = 5;
			}

			// The AI of the Projectile
			public override void AI()
			{
				Player Player = Main.player[Projectile.owner];
				Projectile.position = Player.Center + Projectile.velocity * MOVE_DISTANCE;
				Projectile.timeLeft = 2;

				if ((Player.Center - Main.player[(int)Projectile.ai[1]].Center).Length() >= 1500)
            {
				Projectile.Kill();
				return;
            }
				// By separating large AI into methods it becomes very easy to see the flow of the AI in a broader sense
				// First we update Player variables that are needed to channel the laser
				// Then we run our charging laser logic
				// If we are fully charged, we proceed to update the laser's position
				// Finally we spawn some effects like dusts and light

				UpdatePlayer(Player);
				ChargeLaser(Player);

				// If laser is not charged yet, stop the AI here.
				if (Charge < MAX_CHARGE) return;

				SetLaserPosition(Player);
				SpawnDusts(Player);
				Main.player[(int)Projectile.ai[1]].AddBuff(ModContent.BuffType<Buffs.LinkProtection>(), 100);
				Player.AddBuff(ModContent.BuffType<Buffs.LinkProtection>(), 100);
		}

			private void SpawnDusts(Player Player)
			{

				Vector2 unit = Projectile.velocity * -1;
				Vector2 dustPos = Player.Center + Projectile.velocity * Distance;
			if (Main.time % 5 < 1)
			{
				for (int i = 0; i < 2; ++i)
				{
					float num1 = Projectile.velocity.ToRotation() + (Main.rand.Next(2) == 1 ? -1.0f : 1.0f) * 1.57f;
					float num2 = (float)(Main.rand.NextDouble() * 0.8f + 1.0f);
					Vector2 dustVel = new Vector2((float)Math.Cos(num1) * num2, (float)Math.Sin(num1) * num2);
				}
			}

			if (IsAtMaxCharge)
            {int abc= (int)((Player.Center - Main.player[(int)Projectile.ai[1]].Center).Length() / 15);
				for (int i = 1; i<abc; i++)
                {
					int bb=Dust.NewDust(Player.Center - ((Player.Center - Main.player[(int)Projectile.ai[1]].Center) / abc * i), 1,1,63, default,default, 50, Color.LightSkyBlue, 0.8f);
					Main.dust[bb].noGravity = true;
				}

            }


			}

			/*
			 * Sets the end of the laser position based on where it collides with something
			 */
			private void SetLaserPosition(Player Player)
		{
			for (Distance = MOVE_DISTANCE; Distance <= (float)5 * ((int)((Projectile.Center - Main.player[(int)Projectile.ai[1]].Center) / 5).Length()) - MOVE_DISTANCE; Distance += 5f)
			{
				}
			}

			private void ChargeLaser(Player Player)
			{
				// Kill the Projectile if the Player stops channeling
				if (!Player.channel)
				{
					Projectile.Kill();
				}
				else
				{
					// Do we still have enough mana? If not, we kill the Projectile because we cannot use it anymore
					if (Main.time % 20 < 1 && !Player.CheckMana(Player.inventory[Player.selectedItem].mana, false))
					{
						Projectile.Kill();
					}
					Vector2 offset = Projectile.velocity;
					offset *= MOVE_DISTANCE - 20;
					Vector2 pos = Player.Center + offset - new Vector2(10, 10);
					if (Charge < MAX_CHARGE)
					{
						Charge++;
					}
					int chargeFact = (int)(Charge / 20f);
					Vector2 dustVelocity = Vector2.UnitX * 18f;
					dustVelocity = dustVelocity.RotatedBy(Projectile.rotation - 1.57f);
					Vector2 spawnPos = Projectile.Center + dustVelocity;
				}
			}

			private void UpdatePlayer(Player Player)
			{
				if (Projectile.owner == Main.myPlayer)
				{
				
					Vector2 diff = Main.player[(int)Projectile.ai[1]].Center - Player.Center;
					diff.Normalize();
					Projectile.velocity = diff;
					Projectile.direction = Main.player[(int)Projectile.ai[1]].Center.X > Player.position.X ? 1 : -1;
					Projectile.netUpdate = true;
				
				}
				int dir = Projectile.direction;
				//Player.ChangeDir(dir); // Set Player direction to where we are shooting
				Player.heldProj = Projectile.whoAmI; // Update Player's held Projectile
				Player.itemTime = 2; // Set Item time to 2 frames while we are used
				Player.itemAnimation = 2; // Set Item animation time to 2 frames while we are used
				Player.itemRotation = (float)Math.Atan2(Projectile.velocity.Y * dir, Projectile.velocity.X * dir); // Set the Item rotation to where we are shooting
			}
			public override bool ShouldUpdatePosition() => true;

		}
	}