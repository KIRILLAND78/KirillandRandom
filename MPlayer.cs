using KirillandRandom.Items.Armor;
using KirillandRandom.Items.FriendsStuff;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace KirillandRandom
{
    class MPlayer : ModPlayer
    {
        public float thisIsKindOfDumbToBeHonest;
        public int WatSoulMode = 0;
        public Projectile WatSoulHelper = null;
        public bool ThankYouThoriumDev = true;
        public int overuse;
        public int charge_e = 0;
        public bool charge;
        public bool OveruseMeterCreated;
        public bool Something;
        public int flames_summoned;
        public float angle;
        public bool EyeOfDeath { get { return eyeofdeath; } set { eyeofdeath = value; if (value == true) { Player.Hurt(PlayerDeathReason.LegacyEmpty(), Player.statLife / 2, 0); } } }
        public bool eyeofdeath;
        public bool fireregen;
        public float fireamplification;
        public bool flamingdedication;
        public bool fireLeggings;
        public bool fireBody;
        public bool fireHead;
        public bool Hexed;
        private int coin = 0;
        public int Coin { get { return coin; } set { if (value > coin) coinTimer = 0; coin = value; if (coin < 0) coin = 0; if (coin > 3) { coin = 3; coinTimer = 0; } } }
        public int coinTimer;
        public NPC targetd = null;
        public override void ResetEffects()
        {
            targetd = null;
            flamingdedication = false;
            Something = false;
            fireregen = false;
            fireBody = false;
            fireHead = false;
            fireLeggings = false;
            Hexed = false;
            fireamplification = 0;
        }
        public override void SaveData(TagCompound saveCompound)
        {
            saveCompound.Set("eyeofdeath", eyeofdeath);
        }
        public override void LoadData(TagCompound tag)
        {
            eyeofdeath = tag.GetBool("eyeofdeath");
        }
        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
            if (Hexed)
            {
                //actually should hide them entirely (somehow), but it works for now.
                drawInfo.colorShirt = Color.Transparent;
                drawInfo.colorUnderShirt = Color.Transparent;
                drawInfo.colorEyeWhites = Color.Transparent;
                drawInfo.colorHead = Color.Transparent;
                drawInfo.colorHair = Color.Transparent;
                drawInfo.colorEyes = Color.Transparent;
                drawInfo.colorBodySkin = Color.Transparent;
                drawInfo.colorLegs = Color.Transparent;
                drawInfo.colorArmorBody = Color.Transparent;
                drawInfo.colorArmorLegs = Color.Transparent;
                drawInfo.colorShoes = Color.Transparent;
                drawInfo.colorPants = Color.Transparent;
                drawInfo.colorArmorHead = Color.Transparent;

            }
            base.ModifyDrawInfo(ref drawInfo);
        }
        public override bool Shoot(Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (Hexed)
            {
                return false;
            }
            return base.Shoot(item, source, position, velocity, type, damage, knockback);
        }
        public override bool CanHitPvp(Item Item, Player target)
        {
            if (Hexed)
            {
                return false;
            }
            return base.CanHitPvp(Item, target);
        }
        public override bool CanHitNPC(NPC target)
        {
            if (Hexed)
            {
                return false;
            }
            return base.CanHitNPC(target);
        }

        public override void ModifyScreenPosition()
        {
            if (targetd != null)
            {
                //Main.screenLastPosition = targetd.position - new Vector2(Main.ScreenSize.X / 2, Main.ScreenSize.Y / 2);
                Main.screenPosition = targetd.Center - new Vector2(Main.ScreenSize.X / 2, Main.ScreenSize.Y / 2);
            }
            base.ModifyScreenPosition();
        }



        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if ((drawInfo.shadow == 0f) && (Player.HeldItem.type == ModContent.ItemType<UltraGun>()))
            {
                if (coin == 3)
                {
                    Main.EntitySpriteDraw(Terraria.GameContent.TextureAssets.Coin[2].Value, Player.Top - Main.screenPosition - Vector2.UnitY * 30 - Vector2.UnitX * 6, new Rectangle(0, 0, 12, 16), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None);
                    Main.EntitySpriteDraw(Terraria.GameContent.TextureAssets.Coin[2].Value, Player.Top - Main.screenPosition - Vector2.UnitY * 30 + Vector2.UnitX * 18 - Vector2.UnitX * 6, new Rectangle(0, 0, 12, 16), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None);
                    Main.EntitySpriteDraw(Terraria.GameContent.TextureAssets.Coin[2].Value, Player.Top - Main.screenPosition - Vector2.UnitY * 30 - Vector2.UnitX * 18 - Vector2.UnitX * 6, new Rectangle(0, 0, 12, 16), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None);
                }
                if (coin == 2)
                {

                    Main.EntitySpriteDraw(Terraria.GameContent.TextureAssets.Coin[2].Value, Player.Top - Main.screenPosition - Vector2.UnitY * 30 + Vector2.UnitX * 8 - Vector2.UnitX * 6, new Rectangle(0, 0, 12, 16), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None);
                    Main.EntitySpriteDraw(Terraria.GameContent.TextureAssets.Coin[2].Value, Player.Top - Main.screenPosition - Vector2.UnitY * 30 - Vector2.UnitX * 8 - Vector2.UnitX * 6, new Rectangle(0, 0, 12, 16), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None);
                }
                if (coin == 1)
                {
                    Main.EntitySpriteDraw(Terraria.GameContent.TextureAssets.Coin[2].Value, Player.Top - Main.screenPosition - Vector2.UnitY * 30 - Vector2.UnitX * 6, new Rectangle(0, 0, 12, 16), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None);
                }
            }
            if (Player.name == "KIRILLAND-Modder" && !Player.dead)
            {
                if (Main.rand.NextBool(4) && drawInfo.shadow == 0f)
                {
                    int TestDust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, DustID.WhiteTorch, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, Color.White, 0.8f);
                    Main.dust[TestDust].noGravity = true;
                    Main.dust[TestDust].noLight = true;
                }
            }
            if (fireamplification == 0.24f && !Player.dead)
            {
                if (Main.rand.NextBool(10) && drawInfo.shadow == 0f)
                {
                    int TestDust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, DustID.Torch, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, Color.White, 0.5f);
                    Main.dust[TestDust].shader = GameShaders.Armor.GetSecondaryShader(drawInfo.cBody, drawInfo.drawPlayer);


                    Main.dust[TestDust].noGravity = true;
                    Main.dust[TestDust].noLight = true;
                }

            }

            base.DrawEffects(drawInfo, ref r, ref g, ref b, ref a, ref fullBright);
        }
        public override void PostUpdateEquips()
        {

            if (Hexed)
            {
                Player.wingTime = 0;
            }
            if (Player.forceMerman || Player.forceWerewolf || Player.wereWolf || Player.merman)
            {
                fireBody = false;
                fireHead = false;
                fireLeggings = false;
            }


            if (Player.armor[10].type > ItemID.None)
            {

                if (Player.armor[10].type != ModContent.ItemType<FiresoulRobeHood>())
                {
                    fireHead = false;
                }
                else
                {
                    fireHead = true;
                }
            }
            if (Player.armor[11].type > ItemID.None)
            {

                if (Player.armor[11].type != ModContent.ItemType<FiresoulRobe>())
                {
                    fireBody = false;
                }
                else
                {
                    fireBody = true;
                }
            }
            if (Player.armor[12].type > ItemID.None)
            {

                if (Player.armor[12].type != ModContent.ItemType<FiresoulRobeLeggings>())
                {
                    fireLeggings = false;
                }
                else
                {
                    fireLeggings = true;
                }
            }
            base.PostUpdateEquips();
        }

        public override void PostUpdate()
        {
            base.PostUpdate();
            if (eyeofdeath)
                Player.statLifeMax2 = (Player.statLifeMax2 / 2);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (flamingdedication)
            {
                target.AddBuff(BuffID.OnFire, 60);
            }
            base.OnHitNPC(target, hit, damageDone);
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (flamingdedication)
            {
                target.AddBuff(BuffID.OnFire, 60);
            }
            base.OnHitNPCWithProj(proj, target, hit, damageDone);
        }
        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            if (flamingdedication)
            {
                npc.AddBuff(BuffID.OnFire, 120);
            }
            base.OnHitByNPC(npc, hurtInfo);
        }
        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {

            if ((eyeofdeath == true) && (modifiers.FinalDamage.Flat < 30) && (modifiers.DamageSource.SourceOtherIndex != 3))
            {
                Player.immune = true;
                Player.immuneTime = Player.longInvince ? 80 : 40;

                modifiers.FinalDamage.Base = 0;
            }
        }

        public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
        {
            if (eyeofdeath == true)
            {
                damage *= 1.3f;
            }
            if (fireamplification > 0)
            {
                if (Player.onFire == true)
                {
                    damage *= 1 + fireamplification;
                }
            }
            base.ModifyWeaponDamage(item, ref damage);
        }

        public override void UpdateLifeRegen()
        {
            if (fireregen)
            {

                if (Player.onFire == true)
                {
                    Player.lifeRegen = +16;


                }



            }

            base.UpdateLifeRegen();
        }



        public override void PreUpdate()
        {
            thisIsKindOfDumbToBeHonest++;
            if (coin < 3)
                coinTimer++;
            if (coinTimer > 100)
            {
                coin++;
                coinTimer = 0;
            }

            base.PreUpdate();
        }

        public override void Initialize()
        {
            OveruseMeterCreated = false;
            flames_summoned = 0;
            angle = 0;
            overuse = 0;
            base.Initialize();
        }
        public override ModPlayer Clone(Player newEntity)
        {
            return base.Clone(newEntity);
        }
        public override void CopyClientState(ModPlayer clientClone)/* tModPorter Suggestion: Replace Item.Clone usages with Item.CopyNetStateTo */
        {
            MPlayer clone = clientClone as MPlayer;
            clone.flames_summoned = flames_summoned;
            clone.Hexed = Hexed;
            clone.fireregen = fireregen;
        }
        public override void SendClientChanges(ModPlayer clientPlayer)
        {
            base.SendClientChanges(clientPlayer);
        }
    }

}

