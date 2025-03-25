using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace KirillandRandom
{
    public class KirillandRandom : Mod
    {
        
        public override void Load()
        {
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
