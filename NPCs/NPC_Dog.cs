using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using KirillandRandom.NPCs;

namespace KirillandRandom.NPCs
{
	public class NPC_Dog : ModNPC
	{
		int fran = 0;
		int timer = 115;
		public override void SetStaticDefaults()
		{
			NPC.friendly = true;
			NPC.frame.Height = 34;

			NPC.frame.Width = 50;
			NPC.scale = 1;
			DisplayName.SetDefault("Dog");
			Main.npcFrameCount[NPC.type] = 4;
		}
        public override void FindFrame(int frameHeight)
		{
			NPC.frame.Y = (int)3 * frameHeight;
			if (timer > 0)
			{
				fran += 1;
				if (fran >= 30) { fran -= 30; };
				NPC.frame.Y = (int)fran / 10 * frameHeight;
			}

			base.FindFrame(frameHeight);
        }
        public override void SetDefaults()
		{
			NPC.scale = 1;
			NPC.knockBackResist = 0f;
			NPC.width = 50;
			NPC.height = 34;
			NPC.aiStyle = -1; // This NPC has a completely unique AI, so we set this to -1. The default aiStyle 0 will face the Player, which might conflict with custom AI code.
			NPC.damage = 0;
			NPC.defense = 0;
			NPC.lifeMax = 80;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			//NPC.alpha = 175;
			//NPC.color = new Color(0, 80, 255, 100);
			NPC.value = 0;
			NPC.buffImmune[BuffID.Confused] = true; // NPC default to being immune to the Confused debuff. Allowing confused could be a little more work depending on the AI. NPC.confused is true while the NPC is confused.
		}
		// Allows hitting the NPC with melee type weapons, even if it's friendly.
		public override bool? CanBeHitByItem(Player Player, Item Item)
		{
			return true;
		}

		// Same as the above but with projectiles.
		public override bool? CanBeHitByProjectile(Projectile Projectile)
		{
			return Projectile.friendly && Projectile.owner < 255;
		}


		
		public override void AI()
		{
			if (timer > 0){
				if (NPC.GetGlobalNPC<MNPC>().barrel.position.X >= NPC.position.X)
                {
					NPC.velocity.X +=0.2f;

					NPC.spriteDirection = 1;
				}
                else
                {
					NPC.velocity.X -= 0.2f;
					NPC.spriteDirection = -1;
				}
				timer -= 1;
            }
            else
            {
				if (timer == 0)
				{
					NPC.StrikeNPC(75, 0, 0);

					timer -= 1;
				}
				if (timer < 0)
				{
					NPC.velocity.X = NPC.velocity.X/1.1f;
					if (timer <= -120)
                    {
						NPC.StrikeNPCNoInteraction(5, 0, 0);


                    }
					timer -= 1;
				}

			}

		}
		
	}
}