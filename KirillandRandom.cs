using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace KirillandRandom
{
    public class KirillandRandom : Mod
    {

        public override void Load()
        {
            if (Main.netMode != NetmodeID.Server)
            {

                Asset<Effect> filterShader = this.Assets.Request<Effect>("Shaders/rain");
                Asset<Effect> filterShader2 = this.Assets.Request<Effect>("Shaders/Shockwave");


                //Filters.Scene["MyMod:FilterName"] = new Filter(new ScreenShaderData(filterShader, "PixelShaderFunction"), EffectPriority.Medium);
                //Filters.Scene["MyMod:FilterName"].Load();
                //Filters.Scene["MyMod:Shockwave"] = new Filter(new ScreenShaderData(filterShader2, "Shockwave"), EffectPriority.Medium);
                //Filters.Scene["MyMod:Shockwave"].Load();
            }
            base.Load();
        }


        public override void Unload()
        {
            base.Unload();
        }

    }
    public class KlndMenu : ModMenu
    {
        public override string DisplayName => "Random Additions";
        public override bool IsLoadingEnabled(Mod mod)
        {
            return false;
        }
        //public override Asset<Texture2D> Logo => ModContent.Request<Texture2D>("KirillandRandom/Menu/Hexed");
        //public override Asset<Texture2D> SunTexture => ModContent.Request<Texture2D>("KirillandRandom/Buffs/Hexed");
        //public override Asset<Texture2D> MoonTexture => ModContent.Request<Texture2D>("KirillandRandom/Buffs/Hexed");
    }
}
