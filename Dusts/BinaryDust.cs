using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace KirillandRandom.Dusts
{
    internal class BinaryDust : ModDust
    {
        public override string Texture => "KirillandRandom/Visuals/1";
        Asset<Texture2D> texture;
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            var t = Main._rand.Next(2);
            if (t == 0) dust.customData = ModContent.Request<Texture2D>("KirillandRandom/Visuals/0");
            else dust.customData = ModContent.Request<Texture2D>("KirillandRandom/Visuals/1");
            base.OnSpawn(dust);
        }
        public override bool PreDraw(Dust dust)
        {
            Main.EntitySpriteDraw(((Asset<Texture2D>)dust.customData).Value, dust.position - Main.screenPosition, null, Color.White, 0, Vector2.Zero, dust.scale * 0.8f, SpriteEffects.None);
            return false;
        }
    }
}
