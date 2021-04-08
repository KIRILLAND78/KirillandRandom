using KirillandRandom.Items;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KirillandRandom.NPCs
{
	public class MNPC : GlobalNPC
	{
		public override bool InstancePerEntity => true;
		public int charge_e = 0;
		public bool charge;

		public override void ResetEffects(NPC npc)
		{
			charge = false;
		}

		public override void UpdateLifeRegen(NPC npc, ref int damage)
		{
			if (charge == false) { charge_e = 0; };
			if (charge)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= 2*charge_e;
				if (damage < charge_e)
				{
					damage = charge_e;
				}
			}
		}

		public override void DrawEffects(NPC npc, ref Color drawColor)
		{
			if (charge)
			{
				if (Main.rand.Next(4) < 3)
				{
					int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, DustID.Electric, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 0.13f*charge_e);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 0.15f*charge_e;
					Main.dust[dust].velocity.Y -= 0.075f * charge_e;
					if (Main.rand.NextBool(4))
					{
						Main.dust[dust].noGravity = false;
						Main.dust[dust].scale = 0.02f * charge_e;
					}
				}
				Lighting.AddLight(npc.position, 0.1f, 0.2f, 0.5f);
			}
		}
	}
}