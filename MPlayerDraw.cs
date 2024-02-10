using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
namespace KirillandRandom
{
    public class MPlayerDrawHands : PlayerDrawLayer
    {
        private Asset<Texture2D> HandArmorTexture;
        public override bool IsHeadLayer => false;
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.ArmOverItem);
        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            if (drawInfo.drawPlayer.GetModPlayer<MPlayer>().fireBody)
            {
                //if (drawInfo.drawPlayer.mount.Active) return;
                HandArmorTexture = ModContent.Request<Texture2D>("KirillandRandom/Items/Armor/FiresoulRobe_Arms_Glow");

                Player drawPlayer = drawInfo.drawPlayer;
                var position = drawInfo.Position + drawInfo.bodyVect + new Vector2(drawPlayer.width / 2 - drawPlayer.bodyFrame.Width / 2, drawPlayer.height - drawPlayer.bodyFrame.Height + 4f) - Main.screenPosition;
                position = new Vector2((int)position.X, (int)position.Y);

                DrawData drdt = new DrawData(
                    HandArmorTexture.Value, //The texture to render.
                    position, //Position to render at.
                    drawInfo.drawPlayer.bodyFrame, //Source rectangle.
                    drawInfo.colorArmorBody == Color.Transparent ? Color.Transparent : new Color(100, 100, 100, 100), //Color.
                    0f, //Rotation.
                    drawInfo.bodyVect,//exampleItemTexture.Size() * 0.5f, //Origin. Uses the texture's center.
                    1f, //Scale.
                    drawInfo.playerEffect, //SpriteEffects.
                    0 //'Layer'. This is always 0 in Terraria.
                );
                drdt.shader = drawInfo.cBody;
                drawInfo.DrawDataCache.Add(drdt);

            }

        }
    }
    public class MPlayerDrawHead : PlayerDrawLayer
    {
        private Asset<Texture2D> HeadArmorTexture;
        public override bool IsHeadLayer => true;
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.Head);
        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            //if (drawInfo.drawPlayer.mount.Active) return;

            //Main.NewText(Convert.ToString(drawInfo.drawPlayer.mount.PlayerHeadOffset),Color.Yellow);
            if (drawInfo.drawPlayer.GetModPlayer<MPlayer>().fireHead)
            {
                Player drawPlayer = drawInfo.drawPlayer;
                HeadArmorTexture = ModContent.Request<Texture2D>("KirillandRandom/Items/Armor/FiresoulRobeHood_Head_Glow");
                //var position = drawInfo.headVect + drawInfo.Position + new Vector2(-10f, -10f) - Main.screenPosition+ drawPlayer.headPosition;
                var position = drawInfo.headVect + drawInfo.Position - Main.screenPosition + new Vector2(drawPlayer.width / 2 - drawPlayer.bodyFrame.Width / 2, drawPlayer.height - drawPlayer.bodyFrame.Height + 4f) + drawPlayer.headPosition;
                position = new Vector2((int)position.X, (int)position.Y);
                DrawData drdt = new DrawData(
                    HeadArmorTexture.Value, //The texture to render.
                    position, //Position to render at.
                    drawInfo.drawPlayer.bodyFrame, //Source rectangle. //for some reason headFrame doesn't work correctly? Investigate.
                    drawInfo.colorArmorHead == Color.Transparent ? Color.Transparent : new Color(100, 100, 100, 100), //Color. 
                    0f, //Rotation.
                    drawInfo.headVect,//exampleItemTexture.Size() * 0.5f, //Origin. Uses the texture's center.
                    1f, //Scale.
                    drawInfo.playerEffect, //SpriteEffects.
                    0 //'Layer'. This is always 0 in Terraria.
                );
                drdt.shader = drawInfo.cHead;
                drawInfo.DrawDataCache.Add(drdt);

            }

        }
    }
    public class MPlayerDrawBody : PlayerDrawLayer
    {
        private Asset<Texture2D> BodyArmorTexture;
        public override bool IsHeadLayer => false;
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.Torso);
        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;

            if (drawInfo.drawPlayer.GetModPlayer<MPlayer>().fireBody)
            {
                BodyArmorTexture = ModContent.Request<Texture2D>("KirillandRandom/Items/Armor/FiresoulRobe_Body_Glow");

                var position = drawInfo.Position + new Vector2(drawPlayer.width / 2 - drawPlayer.bodyFrame.Width / 2, drawPlayer.height - drawPlayer.bodyFrame.Height + 4f) - Main.screenPosition + drawPlayer.bodyPosition;
                position = new Vector2((int)position.X, (int)position.Y);

                DrawData drdt = new DrawData(
                    BodyArmorTexture.Value, //The texture to render.
                    position, //Position to render at.
                    drawInfo.drawPlayer.bodyFrame, //Source rectangle.
                    drawInfo.colorArmorBody == Color.Transparent ? Color.Transparent : new Color(100, 100, 100, 100), //Color.
                    0f, //Rotation.
                    Vector2.Zero,//exampleItemTexture.Size() * 0.5f, //Origin. Uses the texture's center.
                    1f, //Scale.
                    drawInfo.playerEffect, //SpriteEffects.
                    0 //'Layer'. This is always 0 in Terraria.
                );
                drdt.shader = drawInfo.cBody;
                drawInfo.DrawDataCache.Add(drdt);

            }

        }
    }


    public class MPlayerDrawLegs : PlayerDrawLayer
    {
        private Asset<Texture2D> LegArmorTexture;
        public override bool IsHeadLayer => false;
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.Leggings);

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            Main.instance.LoadNPC(NPCID.Frog);
            LegArmorTexture = ModContent.Request<Texture2D>("KirillandRandom/Items/Armor/Critter_Legs");//Terraria.GameContent.TextureAssets.Npc[NPCID.Frog];
            if (drawInfo.drawPlayer.GetModPlayer<MPlayer>().Hexed)
            {
                var position = drawInfo.Position + new Vector2(drawPlayer.width / 2 - drawPlayer.bodyFrame.Width / 2, drawPlayer.height - drawPlayer.bodyFrame.Height + 4f) + drawPlayer.legPosition - Main.screenPosition;
                position = new Vector2((int)position.X, (int)position.Y);
                if (drawInfo.isSitting) position.Y -= 14;
                    drawInfo.DrawDataCache.Add(new DrawData(
                    LegArmorTexture.Value, //The texture to render.
                    position, //Position to render at.
                    //new Rectangle(0, 0, 100, 100),
                    drawPlayer.legFrame,//new Rectangle(0, (((int)drawInfo.drawPlayer.legFrameCounter)==0)?0:((((int)(drawInfo.drawPlayer.legFrameCounter))%8)+5)*22, 26, 22), //Source rectangle.
                    Lighting.GetColor((int)drawInfo.Center.X / 16, (int)drawInfo.Center.Y / 16, Color.White), //Color.
                    0f, //Rotation.
                    Vector2.Zero,//exampleItemTexture.Size() * 0.5f, //Origin. Uses the texture's center.
                    1f, //Scale.
                    (drawInfo.playerEffect == SpriteEffects.FlipHorizontally)? SpriteEffects.None: SpriteEffects.FlipHorizontally, //SpriteEffects.
                    0 //'Layer'. This is always 0 in Terraria.
                ));


                return;
            }

            if (drawInfo.drawPlayer.GetModPlayer<MPlayer>().fireLeggings)
            {

                LegArmorTexture = ModContent.Request<Texture2D>("KirillandRandom/Items/Armor/FiresoulRobeLeggings_Legs_Glow");

                var position = drawInfo.Position + new Vector2(drawPlayer.width / 2 - drawPlayer.bodyFrame.Width / 2, drawPlayer.height - drawPlayer.bodyFrame.Height + 4f) + drawPlayer.legPosition - Main.screenPosition;
                if (drawInfo.isSitting)
                {
                    position += new Vector2(drawInfo.drawPlayer.direction == 1 ? 6 : -6, -7);
                    if (drawPlayer.mount.Active)
                    {
                        position += new Vector2(0, 3);
                    }
                }

                position = new Vector2((int)position.X, (int)position.Y);

                DrawData drdt = new DrawData(
                    LegArmorTexture.Value, //The texture to render.
                    position, //Position to render at.
                    drawInfo.drawPlayer.legFrame, //Source rectangle.
                    drawInfo.colorArmorLegs == Color.Transparent ? Color.Transparent : new Color(100, 100, 100, 100), //Color.
                    0f, //Rotation.
                    Vector2.Zero,//exampleItemTexture.Size() * 0.5f, //Origin. Uses the texture's center.
                    1f, //Scale.
                    drawInfo.playerEffect, //SpriteEffects.
                    0 //'Layer'. This is always 0 in Terraria.
                );
                drdt.shader = drawInfo.cLegs;
                drawInfo.DrawDataCache.Add(drdt);

            }

        }
    }
}