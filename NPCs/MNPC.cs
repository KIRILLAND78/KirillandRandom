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

        public Projectile barrel { get; internal set; }

        public int charge_e = 0;
		public bool charge;

        public override void ResetEffects(NPC NPC)
		{
			charge = false;
		}

		public override void UpdateLifeRegen(NPC NPC, ref int damage)
		{
			if (charge == false) { charge_e = 0; };
			if (charge)
			{
				if (NPC.lifeRegen > 0)
				{
					NPC.lifeRegen = 0;
				}
				NPC.lifeRegen -= 2*charge_e;
				if (damage < charge_e)
				{
					damage = charge_e;
				}
			}
		}

		public override void DrawEffects(NPC NPC, ref Color drawColor)
		{
			if (charge)
			{
				if (Main.rand.Next(4) < 3)
				{
					int dust = Dust.NewDust(NPC.position - new Vector2(2f, 2f), NPC.width + 4, NPC.height + 4, DustID.Electric, NPC.velocity.X * 0.4f, NPC.velocity.Y * 0.4f, 100, default(Color), 0.13f*charge_e);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 0.15f*charge_e;
					Main.dust[dust].velocity.Y -= 0.075f * charge_e;
					if (Main.rand.NextBool(4))
					{
						Main.dust[dust].noGravity = false;
						Main.dust[dust].scale = 0.02f * charge_e;
					}
				}
				Lighting.AddLight(NPC.position, 0.1f, 0.2f, 0.5f);
			}
		}
	}
}