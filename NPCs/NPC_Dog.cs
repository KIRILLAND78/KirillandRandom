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
			npc.friendly = true;
			npc.frame.Height = 34;

			npc.frame.Width = 50;
			npc.scale = 1;
			DisplayName.SetDefault("The Dog");
			Main.npcFrameCount[npc.type] = 4;
		}
        public override void FindFrame(int frameHeight)
		{
			npc.frame.Y = (int)3 * frameHeight;
			if (timer > 0)
			{
				fran += 1;
				if (fran >= 30) { fran -= 30; };
				npc.frame.Y = (int)fran / 10 * frameHeight;
			}

			base.FindFrame(frameHeight);
        }
        public override void SetDefaults()
		{
			npc.scale = 1;
			npc.knockBackResist = 0f;
			npc.width = 50;
			npc.height = 34;
			npc.aiStyle = -1; // This npc has a completely unique AI, so we set this to -1. The default aiStyle 0 will face the player, which might conflict with custom AI code.
			npc.damage = 0;
			npc.defense = 0;
			npc.lifeMax = 80;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			//npc.alpha = 175;
			//npc.color = new Color(0, 80, 255, 100);
			npc.value = 25f;
			npc.buffImmune[BuffID.Confused] = true; // npc default to being immune to the Confused debuff. Allowing confused could be a little more work depending on the AI. npc.confused is true while the npc is confused.
		}
		// Allows hitting the NPC with melee type weapons, even if it's friendly.
		public override bool? CanBeHitByItem(Player player, Item item)
		{
			return true;
		}

		// Same as the above but with projectiles.
		public override bool? CanBeHitByProjectile(Projectile projectile)
		{
			return projectile.friendly && projectile.owner < 255;
		}


		
		public override void AI()
		{
			if (timer > 0){
				if (npc.GetGlobalNPC<MNPC>().barrel.position.X >= npc.position.X)
                {
					npc.velocity.X +=0.2f;

					npc.spriteDirection = 1;
				}
                else
                {
					npc.velocity.X -= 0.2f;
					npc.spriteDirection = -1;
				}
				timer -= 1;
            }
            else
            {
				if (timer == 0)
				{
					npc.StrikeNPC(75, 0, 0);

					timer -= 1;
				}
				if (timer < 0)
				{
					npc.velocity.X = npc.velocity.X/1.1f;
					if (timer <= -120)
                    {
						npc.StrikeNPCNoInteraction(5, 0, 0);


                    }
					timer -= 1;
				}

			}

		}
		
	}
}