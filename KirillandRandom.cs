using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using Terraria;

using System.Collections.Generic;
using System.Reflection;

namespace KirillandRandom
{
	public class KirillandRandom : Mod
	{//Reimplement shaders later
	 //    public override void Load()
	 //    {

		//        // All of this loading needs to be client-side.

		//        if (Main.netMode != NetmodeID.Server)
		//        {

		//            Ref<Effect> nihilRef = new Ref<Effect>(GetEffect("Effects/Nihil"));
		//            Filters.Scene["nihil"] = new Filter(new ScreenShaderData(nihilRef, "Nihil"), EffectPriority.VeryHigh);
		//            Filters.Scene["nihil"].Load();


		//        }
		//    }

		public override void Load()
		{
		}

		public override void Unload()
		{
		}

	} 
}
