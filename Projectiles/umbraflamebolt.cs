using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using KirillandRandom.Dusts;
using KirillandRandom.Primitives;


namespace KirillandRandom.Projectiles
{
    public class UmbraFlameBolt : ModProjectile
    {
        NPC target;
        public int bonusDamage = 0;
        Item Book;
        public int mode = 2;
        private int first=1;
        private bool backup_update=false;
        private int orig_dmg = 0;

        //drawing

        bool drawingfirst = true;
        VertexBuffer vertexBuffer;
        IndexBuffer indexBuffer;

        BasicEffect basicEffect;

        Vector2[] positions = new Vector2[20];
        Vector2[] velocities = new Vector2[20];


        VertexPositionColorTexture[] vertices = new VertexPositionColorTexture[20];


        public override void SetDefaults()
        {
            
            Projectile.light = 0.7f;
            Projectile.Name = "Umbra Flame";
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.timeLeft = 99999999;
            Projectile.penetrate = 99999;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.aiStyle = 0;

        }
        public override void Kill(int timeLeft)
        {
            if (mode != 1)
            {
                Main.player[Projectile.owner].GetModPlayer<MPlayer>().flames_summoned -= 1;
            }
            base.Kill(timeLeft);
        }





        public override void PostDraw(Color lightColor)
        {
            vertices[0] = new VertexPositionColorTexture((positions[0] - Main.screenPosition).ScreenCoord(), Color.Cyan, new Vector2(0, 1));
            vertices[2] = new VertexPositionColorTexture((positions[1] - Main.screenPosition).ScreenCoord(), Color.Cyan, new Vector2(0.1f, 0.5f));
            vertices[5] = new VertexPositionColorTexture((positions[3] - Main.screenPosition).ScreenCoord(), Color.Cyan, new Vector2(0.2f, 0.6f));
            vertices[8] = new VertexPositionColorTexture((positions[4] - Main.screenPosition).ScreenCoord(), Color.Cyan, new Vector2(0.3f, 0.7f));
            vertices[11] = new VertexPositionColorTexture((positions[5] - Main.screenPosition).ScreenCoord(), Color.Cyan, new Vector2(0.4f, 0.78f));
            vertices[14] = new VertexPositionColorTexture((positions[6] - Main.screenPosition).ScreenCoord(), Color.Cyan, new Vector2(0.5f, 0.86f));
            vertices[17] = new VertexPositionColorTexture((positions[7] - Main.screenPosition).ScreenCoord(), Color.Cyan, new Vector2(0.6f, 0.92f));
            vertices[19] = new VertexPositionColorTexture((positions[8] - Main.screenPosition).ScreenCoord(), Color.Cyan, new Vector2(1, 1));

            vertices[1] = new VertexPositionColorTexture((positions[1] - Main.screenPosition + velocities[1].RotatedBy(MathHelper.PiOver2) * -6).ScreenCoord(), Color.Cyan, new Vector2(0.1f, 1));
            vertices[4] = new VertexPositionColorTexture((positions[3] - Main.screenPosition + velocities[3].RotatedBy(MathHelper.PiOver2) * -5).ScreenCoord(), Color.Cyan, new Vector2(0.2f, 1));
            vertices[7] = new VertexPositionColorTexture((positions[4] - Main.screenPosition + velocities[4].RotatedBy(MathHelper.PiOver2) * -4).ScreenCoord(), Color.Cyan, new Vector2(0.3f, 1));
            vertices[10] = new VertexPositionColorTexture((positions[5] - Main.screenPosition + velocities[5].RotatedBy(MathHelper.PiOver2) * -3).ScreenCoord(), Color.Cyan, new Vector2(0.4f, 1));
            vertices[13] = new VertexPositionColorTexture((positions[6] - Main.screenPosition + velocities[6].RotatedBy(MathHelper.PiOver2) * -2).ScreenCoord(), Color.Cyan, new Vector2(0.5f, 1));
            vertices[16] = new VertexPositionColorTexture((positions[7] - Main.screenPosition + velocities[7].RotatedBy(MathHelper.PiOver2) * -1).ScreenCoord(), Color.Cyan, new Vector2(0.6f, 1));

            vertices[3] = new VertexPositionColorTexture((positions[1] - Main.screenPosition + velocities[1].RotatedBy(MathHelper.PiOver2) * 6).ScreenCoord(), Color.Cyan, new Vector2(0.1f, 1));
            vertices[6] = new VertexPositionColorTexture((positions[3] - Main.screenPosition + velocities[3].RotatedBy(MathHelper.PiOver2) * 5).ScreenCoord(), Color.Cyan, new Vector2(0.2f,1));
            vertices[9] = new VertexPositionColorTexture((positions[4] - Main.screenPosition + velocities[4].RotatedBy(MathHelper.PiOver2) * 4).ScreenCoord(), Color.Cyan, new Vector2(0.3f, 1));
            vertices[12] = new VertexPositionColorTexture((positions[5] - Main.screenPosition + velocities[5].RotatedBy(MathHelper.PiOver2) * 3).ScreenCoord(), Color.Cyan, new Vector2(0.4f, 1));
            vertices[15] = new VertexPositionColorTexture((positions[6] - Main.screenPosition + velocities[6].RotatedBy(MathHelper.PiOver2) * 2).ScreenCoord(), Color.Cyan, new Vector2(0.5f, 1));
            vertices[18] = new VertexPositionColorTexture((positions[7] - Main.screenPosition + velocities[7].RotatedBy(MathHelper.PiOver2) * 1).ScreenCoord(), Color.Cyan, new Vector2(0.6f,1));
            //vertices[1] = new VertexPositionColorTexture((positions[1] - Main.screenPosition + velocities[0].RotatedBy(MathHelper.PiOver2) * -8).ScreenCoord(), Color.Cyan, new Vector2(0.5f, 1));
            //vertices[2] = new VertexPositionColorTexture((positions[1] - Main.screenPosition).ScreenCoord(), Color.Cyan, new Vector2(0.5f, 0.5f));
            //vertices[3] = new VertexPositionColorTexture((positions[1] - Main.screenPosition + velocities[1].RotatedBy(MathHelper.PiOver2) * 8).ScreenCoord(), Color.Cyan, new Vector2(0.5f, 1));
            //vertices[4] = new VertexPositionColorTexture((positions[8] - Main.screenPosition ).ScreenCoord(), Color.Cyan, new Vector2(1, 1));
            //vertexBuffer = new VertexBuffer(Main.graphics.GraphicsDevice, typeof(VertexPositionColorTexture), 12, BufferUsage.WriteOnly);
            vertexBuffer.SetData(vertices);
            //basicEffect.View = view;
            Main.graphics.GraphicsDevice.SetVertexBuffer(vertexBuffer);
            Main.graphics.GraphicsDevice.RasterizerState = RasterizerState.CullNone;
            Main.graphics.GraphicsDevice.BlendState = BlendState.Additive;
            Main.graphics.GraphicsDevice.Indices = indexBuffer;
            //Main.graphics.GraphicsDevice.VertexTextures[0]= basicEffect.Texture;
            //Main.graphics.GraphicsDevice.VertexTextures[1] = basicEffect.Texture;
            //Main.graphics.GraphicsDevice.VertexTextures[2] = basicEffect.Texture;
            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                Main.graphics.GraphicsDevice.Textures[0] = basicEffect.Texture;
                //Main.graphics.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, 5);
                Main.graphics.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 6, 0, 20);
            }

             
            //if (first!=1){ 
            //g.Reset();

            //g.ApplyTranslation(Projectile.position - Main.screenPosition - g.mesh[0].Position.XY());
            //g.mesh[0].Position = new Vector3((Projectile.position - Projectile.velocity * 2).X, (Projectile.position - Projectile.velocity * 2).Y, g.mesh[0].Position.Z);
            //g.Draw(); }
            //return true;
        }
        public override void PostAI()
        {
            for (int k = velocities.Length - 1; k > 0; k--)
            {
                velocities[k] = velocities[k - 1];
                positions[k] = positions[k - 1];
            }
            positions[0] = Projectile.Center + Projectile.velocity * 1.5f;
            velocities[0] = Projectile.velocity;
            velocities[0].Normalize();
            base.PostAI();
        }

        public override void AI()
        {

            if (drawingfirst)
            {

                //world = Matrix.CreateTranslation(0, 0, 0);
                //view = Matrix.CreateLookAt(new Vector3(0, 0, 400), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
                //Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 800f / 480f, 0.01f, 100f);

                //projection = Matrix.CreateOrthographic(Main.ScreenSize.X, Main.ScreenSize.Y, 0.2f, 800);



                //Vector3 pos = (worldPos - Main.screenPosition).ScreenCoord();///СМОТРИ СЮДА!!!!!!!!!


                basicEffect = new BasicEffect(Main.graphics.GraphicsDevice);
                vertices[0] = new VertexPositionColorTexture((Projectile.position - Main.screenPosition).ScreenCoord() , Color.Red, new Vector2(0, 1));
                vertices[1] = new VertexPositionColorTexture((Projectile.position - Main.screenPosition).ScreenCoord(), Color.Orange, new Vector2(0.5f, 1));
                vertices[2] = new VertexPositionColorTexture((Projectile.position - Main.screenPosition).ScreenCoord(), Color.Yellow, new Vector2(0.5f, 0.5f));
                vertices[3] = new VertexPositionColorTexture((Projectile.position - Main.screenPosition).ScreenCoord(), Color.Green, new Vector2(0.5f, 1));
                vertices[4] = new VertexPositionColorTexture((Projectile.position - Main.screenPosition).ScreenCoord(), Color.Blue, new Vector2(1, 1));
                vertices[5] = new VertexPositionColorTexture((Projectile.position - Main.screenPosition).ScreenCoord(), Color.Green, new Vector2(0.5f, 1));
                vertices[6] = new VertexPositionColorTexture((Projectile.position - Main.screenPosition).ScreenCoord(), Color.Blue, new Vector2(1, 1));
                vertices[7] = new VertexPositionColorTexture((Projectile.position - Main.screenPosition).ScreenCoord(), Color.Green, new Vector2(0.5f, 1));
                vertices[8] = new VertexPositionColorTexture((Projectile.position - Main.screenPosition).ScreenCoord(), Color.Blue, new Vector2(1, 1));
                vertices[9] = new VertexPositionColorTexture((Projectile.position - Main.screenPosition).ScreenCoord(), Color.Green, new Vector2(0.5f, 1));
                vertices[10] = new VertexPositionColorTexture((Projectile.position - Main.screenPosition).ScreenCoord(), Color.Blue, new Vector2(1, 1));
                vertices[11] = new VertexPositionColorTexture((Projectile.position - Main.screenPosition).ScreenCoord(), Color.Green, new Vector2(0.5f, 1));
                vertices[12] = new VertexPositionColorTexture((Projectile.position - Main.screenPosition).ScreenCoord(), Color.Blue, new Vector2(1, 1));
                vertices[13] = new VertexPositionColorTexture((Projectile.position - Main.screenPosition).ScreenCoord(), Color.Green, new Vector2(0.5f, 1));
                vertices[14] = new VertexPositionColorTexture((Projectile.position - Main.screenPosition).ScreenCoord(), Color.Blue, new Vector2(1, 1));
                vertices[15] = new VertexPositionColorTexture((Projectile.position - Main.screenPosition).ScreenCoord(), Color.Green, new Vector2(0.5f, 1));
                vertices[16] = new VertexPositionColorTexture((Projectile.position - Main.screenPosition).ScreenCoord(), Color.Blue, new Vector2(1, 1));
                vertices[17] = new VertexPositionColorTexture((Projectile.position - Main.screenPosition).ScreenCoord(), Color.Green, new Vector2(0.5f, 1));
                vertices[18] = new VertexPositionColorTexture((Projectile.position - Main.screenPosition).ScreenCoord(), Color.Blue, new Vector2(1, 1));
                vertices[19] = new VertexPositionColorTexture((Projectile.position - Main.screenPosition).ScreenCoord(), Color.Blue, new Vector2(1, 1));
                vertexBuffer = new VertexBuffer(Main.graphics.GraphicsDevice, typeof(VertexPositionColorTexture), 20, BufferUsage.WriteOnly);
                vertexBuffer.SetData(vertices);
                for (int a = 0; a < positions.Length; a++)
                {
                    positions[a] = (Projectile.position - Main.screenPosition);
                    velocities[a] = new Vector2(0,0);
                }
                short[] indices = new short[75];
                indices[0] = 0; indices[1] = 1; indices[2] = 2;//
                indices[3] = 0; indices[4] = 2; indices[5] = 3;//
                indices[6] = 1; indices[7] = 2; indices[8] = 5;//
                //indices[9] = 1; indices[10] = 5; indices[11] = 3;//err
                indices[9] = 7; indices[10] = 8; indices[11] = 10;
                indices[12] = 1; indices[13] = 4; indices[14] = 5;//
                indices[15] = 3; indices[16] = 5; indices[17] = 6;//
                indices[18] = 3; indices[19] = 5; indices[20] = 2;//
                indices[21] = 5; indices[22] = 7; indices[23] = 8;//
                indices[24] = 5; indices[25] = 6; indices[26] = 9;//
                indices[27] = 5; indices[28] = 8; indices[29] = 9;//
                indices[30] = 7; indices[31] = 5; indices[32] = 4;//
                indices[33] = 10; indices[34] = 11; indices[35] = 8;//
                indices[36] = 9; indices[37] = 8; indices[38] = 11;//
                indices[39] = 9; indices[40] = 11; indices[41] = 12;//
                indices[42] = 10; indices[43] = 11; indices[44] = 13;//

                indices[45] = 11; indices[46] = 14; indices[47] = 13;//
                indices[48] = 12; indices[49] = 14; indices[50] = 15;//
                indices[51] = 11; indices[52] = 14; indices[53] = 12;//

                indices[57] = 13; indices[58] = 14; indices[59] = 16;//
                indices[60] = 14; indices[61] = 16; indices[62] = 17;//
                indices[63] = 14; indices[64] = 17; indices[65] = 18;//
                indices[66] = 14; indices[67] = 15; indices[68] = 18;//
                indices[69] = 16; indices[70] = 17; indices[71] = 19;//
                indices[72] = 18; indices[73] = 17; indices[74] = 19;//

                indices[54] = 18; indices[55] = 17; indices[56] = 19;
                indexBuffer = new IndexBuffer(Main.graphics.GraphicsDevice, typeof(short), indices.Length, BufferUsage.WriteOnly);

                indexBuffer.SetData(indices);
                //basicEffect.World = world;
                //basicEffect.Projection = projection;
                basicEffect.VertexColorEnabled = true;
                basicEffect.TextureEnabled = true;
                basicEffect.Texture = ModContent.Request<Texture2D>("KirillandRandom/Visuals/testtrail", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;

            }
            drawingfirst = false;
            Player owner = Main.player[Projectile.owner];

            
            if (owner.dead == true)
            {
                Projectile.Kill();
            }
            if (first==1)
            {
            }
            //int DDustID = Dust.NewDust(Projectile.Center-new Vector2(2,2), 4 , 4 , ModContent.DustType<Umbra_smoke>(), 0, 0, 100, default, 1f); //Spawns dust
            //Main.dust[DDustID].noGravity = true;


            if (mode == 1)
            {
                Projectile.netUpdate = true;
                if (first == 0)
                {
                    Projectile.penetrate = 1;
                    Projectile.friendly = true;
                    Projectile.tileCollide = true;



                    Projectile.timeLeft = 800;
                    Projectile.light = 0.6f;
                    if (Main.myPlayer == Projectile.owner)
                    {
                        var shootToX = Main.MouseWorld.X - Projectile.Center.X;//обоже.
                        var shootToY = Main.MouseWorld.Y - Projectile.Center.Y;//обоже.
                        float distance = (float)Math.Sqrt((shootToX * shootToX + shootToY * shootToY));
                        shootToX *= 15.0f / distance;
                        shootToY *= 15.0f / distance;
                        Projectile.velocity.X = shootToX;//обоже.
                        Projectile.velocity.Y = shootToY;//обоже.

                    }
                    first = 2;
                }
                if ((Main.netMode == NetmodeID.Server) || (Main.netMode == NetmodeID.SinglePlayer))
                {
                    if (target != null && !target.active)
                    {
                        target = null;
                    }
                    if (target != null)
                    {
                        float angle = ((-Projectile.Center + target.Center).ToRotation() + ((-Projectile.Center + target.Center).ToRotation() < 0 ? MathHelper.TwoPi : 0) - Projectile.velocity.ToRotation() - (Projectile.velocity.ToRotation() < 0 ? MathHelper.TwoPi : 0) * -1) % MathHelper.TwoPi;
                        angle = angle > MathHelper.Pi ? -(MathHelper.TwoPi - angle) : angle;
                        Projectile.velocity = Projectile.velocity.RotatedBy(Math.Clamp(angle, (-MathHelper.Pi / 40), (MathHelper.Pi / 40)));

                    }
                    else
                    {
                        NPC buffer = null;
                        float g = 0;
                        for (int i = 0; i < Main.maxNPCs; i++)
                        {
                            if (Vector2.Distance(Main.npc[i].Center, Projectile.Center) <= 200 && Main.npc[i].active && !Main.npc[i].friendly && Main.npc[i].type!=NPCID.TargetDummy)
                                if (g < Vector2.Distance(Main.npc[i].Center, Projectile.Center)) buffer = Main.npc[i];

                        }
                        target = buffer;
                    }
                }




            }
            else{

                Player p = Main.player[Projectile.owner];
                if ((first != 1)&&(backup_update))
                {

                    Projectile.netUpdate = true;


                }
                if (first == 1)
                {
                    
                    orig_dmg = Projectile.damage;
                    Book = p.HeldItem;
                    if (Main.myPlayer == Projectile.owner)
                    {if (Projectile.ai[0] < 4)
                        {
                            Projectile.ai[1] = p.GetModPlayer<MPlayer>().angle + 120 * Projectile.ai[0];
                        }
                        else
                        {
                            Projectile.ai[1] = -p.GetModPlayer<MPlayer>().angle + 72 * Projectile.ai[0];
                            if (Projectile.ai[0] >8)
                            {

                                Projectile.ai[1] = p.GetModPlayer<MPlayer>().angle + 45 * Projectile.ai[0];

                            }
                        }

                        Projectile.netUpdate = true;
                    }
                    first = 0;
                }
                    Projectile.alpha = 64;
                

                double deg = (double)Projectile.ai[1];
                double rad = deg * (Math.PI / 180);
                double dist = 32;
                if (Projectile.ai[0] >3){
                    dist = 60;
                }
                if (Projectile.ai[0] > 8){
                    dist = 88;
                }
                bonusDamage = owner.GetModPlayer<MPlayer>().flames_summoned == 8 ? 32 : 16;

                bonusDamage = owner.GetModPlayer<MPlayer>().flames_summoned == 16 ? 48 : bonusDamage;

                Projectile.damage =orig_dmg+bonusDamage;

                Projectile.position.X = p.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
                Projectile.position.Y = p.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;
                if (Projectile.ai[0] < 9 && Projectile.ai[0] > 3)
                {
                    Projectile.ai[1] -= 4f;
                }
                else
                {
                    Projectile.ai[1] += 4f;
                }

                

                if (owner.HeldItem != Book)
                {
                    Projectile.Kill();
                }
                if ((p.controlUseItem)&&(p.altFunctionUse==2))
                {
                    mode = 1;
                }
            }
        }
    }
}
