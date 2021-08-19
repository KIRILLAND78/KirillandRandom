
using Terraria.Graphics.Shaders;
using KirillandRandom.Buffs;
using KirillandRandom.Items;
using KirillandRandom.Items.Armor;
using KirillandRandom.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.DataStructures;
namespace KirillandRandom
{
    class MPlayer : ModPlayer
    {
        public int overuse;
        public int charge_e = 0;
        public bool charge;
        public bool OveruseMeterCreated;
        public bool BookCreated;
        public bool Something;
        public int flames_summoned;
        public float angle;
        public bool eyeofdeath;
        public bool fireregen;
        public float fireamplification;
        public bool flamingdedication;
        public bool fireLeggings;
        public bool fireBody;
        public bool fireHead;
        public bool Hexed;
        public NPC targetd = null;
        public override void ResetEffects()
        {
            flamingdedication = false;
            Something = false;
            fireregen = false;
            fireBody = false;
            fireHead = false;
            fireLeggings = false;
            Hexed = false;
            fireamplification = 0;
        }
        public override TagCompound Save()
        {
            return new TagCompound {
				{"eyeofdeath", eyeofdeath}
            };
        }
        public override void Load(TagCompound tag)
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
                drawInfo.colorArmorLegs=Color.Transparent;
                drawInfo.colorShoes= Color.Transparent;
                drawInfo.colorPants= Color.Transparent;
                drawInfo.colorArmorHead= Color.Transparent;

            }
            base.ModifyDrawInfo(ref drawInfo);
        }
        public override bool Shoot(Item item, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
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
        public override bool? CanHitNPC(Item Item, NPC target)
        {
            if (Hexed)
            {
                return false;
            }
            return base.CanHitNPC(Item, target);
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
        {if (Player.name == "KIRILLAND-Modder"&& !Player.dead) {
                if (Main.rand.NextBool(4) && drawInfo.shadow == 0f)
                {
                    int TestDust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, 63, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, Color.White, 0.8f);
                    Main.dust[TestDust].noGravity = true;
                    Main.dust[TestDust].noLight = true;
                }
            }
        if (fireamplification == 0.24f&&!Player.dead)
            {
                if (Main.rand.NextBool(10) && drawInfo.shadow == 0f)
                {
                    int TestDust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, 6, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, Color.White, 0.5f);
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
            if (Player.forceMerman||Player.forceWerewolf || Player.wereWolf || Player.merman)
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
                { fireHead = true;
                }
                }
            if (Player.armor[11].type > ItemID.None)
            {

                if (Player.armor[11].type != ModContent.ItemType<FiresoulRobe>())
                {
                    fireBody = false;
                }
                else
                { fireBody = true;
                } }
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






        public override void OnHitNPC(Item Item, NPC target, int damage, float knockback, bool crit)
        {if (flamingdedication) {
                target.AddBuff(BuffID.OnFire, 60);
            }
            base.OnHitNPC(Item, target, damage, knockback, crit);
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (flamingdedication)
            {
                target.AddBuff(BuffID.OnFire, 60);
            }
            base.OnHitNPCWithProj(proj, target, damage, knockback, crit);
        }


        public override void OnHitByNPC(NPC NPC, int damage, bool crit)
        {
            if (flamingdedication)
            {
                NPC.AddBuff(BuffID.OnFire, 120);
            }
            base.OnHitByNPC(NPC, damage, crit);
        }

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit,
            ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            float custom_endurance= Player.endurance;

                if (flamingdedication)
            {
                Player.AddBuff(BuffID.OnFire, 120);
            }
            int fdamage = (int)((damage - (0.5 * Player.statDefense)) * (1 - custom_endurance));
            if (Main.expertMode)
            {fdamage = (int)((damage - (0.75 * Player.statDefense)) * (1 - custom_endurance));
            }
            if ((eyeofdeath==true) && (fdamage < 30) && (damageSource.SourceOtherIndex!=3))
            {
                Player.immune = true;
                Player.immuneTime = Player.longInvince ? 80 : 40;

                damage = 0;

                customDamage = true;
            }

            return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
        }
        public override void ModifyWeaponDamage(Item item, ref StatModifier damage, ref float flat)
        {
            if (eyeofdeath == true)
            {
                damage *= 1.3f;
            }
            if (fireamplification > 0)
            {
                if (Player.onFire == true)
                {
                    damage *= 1+fireamplification;
                }
            }
            base.ModifyWeaponDamage(item, ref damage, ref flat);
        }

        public override void UpdateLifeRegen()
        {   if (fireregen)
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
            if (eyeofdeath)
            {
                if (Main.netMode != NetmodeID.Server)
                {
                    if (Player.statLife >= (Player.statLifeMax / 2))
                    {
                        Player.statLife = (Player.statLifeMax / 2);
                    }



                }
            }

            base.PreUpdate();
        }

        public override void Initialize()
        {
            OveruseMeterCreated = false;
            BookCreated = false;
            flames_summoned = 0;
            angle = 0;
            overuse = 0;
            base.Initialize();
        }
        public override void clientClone(ModPlayer clientClone)
        {
            MPlayer clone = clientClone as MPlayer;
            clone.flames_summoned = flames_summoned;
        }

        //public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        //{
        //    ModPacket packet = mod.GetPacket();
        //    packet.Write((byte)ExampleModMessageType.ExamplePlayerSyncPlayer);
        //    packet.Write((byte)Player.whoAmI);
        //    packet.Write(flames_summoned);
        //    packet.Send(toWho, fromWho);
        //}

        //public override void SendClientChanges(ModPlayer clientPlayer)
        //{
        //    // Here we would sync something like an RPG stat whenever the Player changes it.
        //    ExamplePlayer clone = clientPlayer as ExamplePlayer;
        //    if (clone.nonStopParty != nonStopParty)
        //    {
        //        // Send a Mod Packet with the changes.
        //        var packet = mod.GetPacket();
        //        packet.Write((byte)ExampleModMessageType.NonStopPartyChanged);
        //        packet.Write((byte)Player.whoAmI);
        //        packet.Write(nonStopParty);
        //        packet.Send();
        //    }
        //}
    }
}
