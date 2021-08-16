
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace KirillandRandom.Dusts
{
	class Umbra_smoke : ModDust
	{



        public override void OnSpawn(Dust dust)
		{
			dust.noGravity = true;
			dust.alpha = 180;

			dust.velocity = Vector2.Zero;
		}

		// This Update method shows off some interesting movement. Using customData assigned to a Player, we spiral around the Player while slowly getting closer. In practice, it looks like a vortex.
		public override bool Update(Dust dust)
		{
			dust.alpha+=3;

			// Here we make sure to kill any dust that get really small.
			if (dust.alpha > 240)
			{
				dust.active = false;
			}
			return false;
		}
	}
}
