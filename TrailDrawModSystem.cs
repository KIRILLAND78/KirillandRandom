using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace KirillandRandom
{
    class TrailDrawModSystem : ModPlayer
    {
        public List<Trail> trails = [];
        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            foreach (var trail in trails)
            {
                trail.Draw();
                trail.counter++;
            }
            trails.RemoveAll(x => x.counter > 60);
            base.DrawEffects(drawInfo, ref r, ref g, ref b, ref a, ref fullBright);
        }
    }
    class Trail
    {
        public List<Vector2> positions = [];
        public List<float> rotations = [];
        public Vector2 entitySize;
        public int counter = 0;
        public void Draw()
        {

            var miscShaderData = GameShaders.Misc["FlameLash"];
            miscShaderData.Apply();
            Main.graphics.GraphicsDevice.Textures[0] = ModContent.Request<Texture2D>("KirillandRandom/Visuals/white").Value;

            var vertexStr = new VertexStrip();
            vertexStr.PrepareStrip(positions.ToArray(), rotations.ToArray(),
                ((float progress) =>
                {
                    return Color.Gold * Math.Min(((float)(60 - counter)) / 60, 1);// * 0.95f;
                }),
                ((float progress) =>
                {
                    return 8f;
                }),
                -Main.screenPosition + entitySize / 2f, 20, includeBacksides: true);
            vertexStr.DrawTrail();

        }
    }
}

