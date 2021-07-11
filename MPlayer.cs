
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

        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {
            int index = layers.IndexOf(PlayerLayer.Legs);
            if (index != -1)
            {
                layers.Insert(index + 1, LegsGlow);
            }
            index = layers.IndexOf(PlayerLayer.Head);
            if (index != -1)
            {
                layers.Insert(index + 1, HeadGlow);
            }
            index = layers.IndexOf(PlayerLayer.Body);
            if (index != -1)
            {
                layers.Insert(index + 1, BodyGlow);
            }
            index = layers.IndexOf(PlayerLayer.Arms);
            if (index != -1)
            {
                layers.Insert(index + 1, ArmsGlow);
            }
            index = layers.IndexOf(PlayerLayer.MiscEffectsFront);
            if (index != -1)
            {
                layers.Insert(index + 1, MiscEffect);
            }
            index = layers.IndexOf(PlayerLayer.Legs);
            if (index != -1)
            {
                layers.Insert(index + 1, Animal);
            }

            HeadGlow.visible = true;

            BodyGlow.visible = true;
            ArmsGlow.visible = true;
            MiscEffect.visible = true;
            LegsGlow.visible = true;
            Animal.visible = false;
            if (Hexed)
            {
                layers.ForEach(delegate (PlayerLayer lay) {
                    lay.visible = false;
                }
                );
                PlayerLayer.Legs.visible = true;
                Animal.visible = true;
                PlayerLayer.MountBack.visible = true;
                PlayerLayer.MountBack.visible = true;

            }
            else{
                layers.ForEach(delegate (PlayerLayer lay) {
                    lay.visible = true;
                }
                );

                Animal.visible = false;

            }
        }
        public override void ModifyDrawInfo(ref PlayerDrawInfo drawInfo)
        {
            if (Hexed){
                drawInfo.legColor = Color.Transparent;
                drawInfo.lowerArmorColor = Color.Transparent;
                drawInfo.shoeColor = Color.Transparent;
                drawInfo.pantsColor = Color.Transparent;

            }




                base.ModifyDrawInfo(ref drawInfo);
        }

        public override bool Shoot(Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {if (Hexed)
            {
                return false;
            }
            return base.Shoot(item, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
        public override bool CanHitPvp(Item item, Player target)
        {
            if (Hexed)
            {
                return false;
            }
            return base.CanHitPvp(item, target);
        }
        public override bool? CanHitNPC(Item item, NPC target)
        {
            if (Hexed)
            {
                return false;
            }
            return base.CanHitNPC(item, target);
        }


        public override void PostUpdateEquips()
        {


            if (player.forceMerman||player.forceWerewolf || player.wereWolf || player.merman)
            {
                fireBody = false;
                fireHead = false;
                fireLeggings = false;
            }



            if (player.armor[10].type > ItemID.None)
            {

                if (player.armor[10].type != ModContent.ItemType<FiresoulRobeHood>())
                {
                    fireHead = false;
                }
                else
                { fireHead = true;
                }
                }
            if (player.armor[11].type > ItemID.None)
            {

                if (player.armor[11].type != ModContent.ItemType<FiresoulRobe>())
                {
                    fireBody = false;
                }
                else
                { fireBody = true;
                } }
            if (player.armor[12].type > ItemID.None)
            {

                if (player.armor[12].type != ModContent.ItemType<FiresoulRobeLeggings>())
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




        public static readonly PlayerLayer Animal = new PlayerLayer("KirillandRandom", "Animal", PlayerLayer.Legs, delegate (PlayerDrawInfo drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            Color color = drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow);
            MPlayer modPlayer = drawPlayer.GetModPlayer<MPlayer>();
            Texture2D texture = null;

            if (drawInfo.shadow != 0f || drawInfo.drawPlayer.invis)
            {
                return;
            }
            Mod mod = ModLoader.GetMod("KirillandRandom");

                texture = mod.GetTexture("Items/Armor/Pig_Legs");

            if (texture == null)
            {
                return;
            }
            //I am a bit confused.
            //just a little bit.
            Vector2 drawPos = drawInfo.position - Main.screenPosition + new Vector2(drawPlayer.width / 2 - drawPlayer.bodyFrame.Width / 2, drawPlayer.height - drawPlayer.bodyFrame.Height + 4f) + drawPlayer.legPosition;
            DrawData drawData = new DrawData(texture, drawPos.Floor() + drawInfo.legOrigin, drawPlayer.bodyFrame, color, drawPlayer.legRotation, drawInfo.legOrigin, 1f, drawInfo.spriteEffects, 0)
            {
                shader = drawInfo.legArmorShader
            };
            Main.playerDrawData.Add(drawData);
        });



        public static readonly PlayerLayer HeadGlow = new PlayerLayer("KirillandRandom", "HeadGlow", PlayerLayer.Head, delegate (PlayerDrawInfo drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            MPlayer modPlayer = drawPlayer.GetModPlayer<MPlayer>();
            Color color = drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow);
            Texture2D texture = null;

            if (drawInfo.shadow != 0f || drawInfo.drawPlayer.invis)
            {
                return;
            }
            Mod mod = ModLoader.GetMod("KirillandRandom");

            if (modPlayer.fireHead)
            {
                texture = mod.GetTexture("Items/Armor/FiresoulRobeHood_Head_Glow");
                color *= 0.1f;
            }

            if (texture == null)
            {
                return;
            }
            //I am a bit confused.
            //just a little bit.
            Vector2 drawPos = drawInfo.position - Main.screenPosition + new Vector2(drawPlayer.width / 2 - drawPlayer.bodyFrame.Width / 2, drawPlayer.height - drawPlayer.bodyFrame.Height + 4f) + drawPlayer.headPosition;
            DrawData drawData = new DrawData(texture, drawPos.Floor() + drawInfo.headOrigin, drawPlayer.bodyFrame, color, drawPlayer.headRotation, drawInfo.headOrigin, 1f, drawInfo.spriteEffects, 0)
            {
                shader = drawInfo.headArmorShader
            };
            Main.playerDrawData.Add(drawData);
        });
        public static readonly PlayerLayer BodyGlow = new PlayerLayer("KirillandRandom", "BodyGlow", PlayerLayer.Body, delegate (PlayerDrawInfo drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            MPlayer modPlayer = drawPlayer.GetModPlayer<MPlayer>();
            Color color = drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow);
            Texture2D texture = null;

            if (drawInfo.shadow != 0f || drawInfo.drawPlayer.invis)
            {
                return;
            }
            Mod mod = ModLoader.GetMod("KirillandRandom");

            if (modPlayer.fireBody)
            {
                texture = mod.GetTexture("Items/Armor/FiresoulRobe_Body_Glow");
                color *= 0.1f;
            }

            if (texture == null)
            {
                return;
            }
            //I am a bit confused.
            //just a little bit.
            Vector2 drawPos = drawInfo.position - Main.screenPosition + new Vector2(drawPlayer.width / 2 - drawPlayer.bodyFrame.Width / 2, drawPlayer.height - drawPlayer.bodyFrame.Height + 4f) + drawPlayer.bodyPosition;
            DrawData drawData = new DrawData(texture, drawPos.Floor() + drawInfo.bodyOrigin, drawPlayer.bodyFrame, color, drawPlayer.bodyRotation, drawInfo.bodyOrigin, 1f, drawInfo.spriteEffects, 0)
            {
                shader = drawInfo.bodyArmorShader
            };
            Main.playerDrawData.Add(drawData);
        });
        public static readonly PlayerLayer LegsGlow = new PlayerLayer("KirillandRandom", "LegsGlow", PlayerLayer.Legs, delegate (PlayerDrawInfo drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            MPlayer modPlayer = drawPlayer.GetModPlayer<MPlayer>();
            Color color = drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow);
            Texture2D texture = null;

            if (drawInfo.shadow != 0f || drawInfo.drawPlayer.invis)
            {
                return;
            }
            Mod mod = ModLoader.GetMod("KirillandRandom");

            if (modPlayer.fireLeggings)
            {
                texture = mod.GetTexture("Items/Armor/FiresoulRobeLeggings_Legs_Glow");
                color *= 0.1f;
            }

            if (texture == null)
            {
                return;
            }
            //I am a bit confused.
            //just a little bit.
            Vector2 drawPos = drawInfo.position - Main.screenPosition + new Vector2(drawPlayer.width / 2 - drawPlayer.bodyFrame.Width / 2, drawPlayer.height - drawPlayer.bodyFrame.Height + 4f) + drawPlayer.legPosition;
            DrawData drawData = new DrawData(texture, drawPos.Floor() + drawInfo.legOrigin, drawPlayer.bodyFrame, color, drawPlayer.legRotation, drawInfo.legOrigin, 1f, drawInfo.spriteEffects, 0)
            {
                shader = drawInfo.legArmorShader
            };
            Main.playerDrawData.Add(drawData);
        });

        public static readonly PlayerLayer ArmsGlow = new PlayerLayer("KirillandRandom", "ArmsGlow", PlayerLayer.Arms, delegate (PlayerDrawInfo drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            MPlayer modPlayer = drawPlayer.GetModPlayer<MPlayer>();
            Color color = drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow);
            Texture2D texture = null;

            if (drawInfo.shadow != 0f || drawInfo.drawPlayer.invis)
            {
                return;
            }
            Mod mod = ModLoader.GetMod("KirillandRandom");

            if (modPlayer.fireBody)
            {
                texture = mod.GetTexture("Items/Armor/FiresoulRobe_Arms_Glow");
                color *= 0.08f;
            }

            if (texture == null)
            {
                return;
            }
            Vector2 drawPos = drawInfo.position - Main.screenPosition + new Vector2(drawPlayer.width / 2 - drawPlayer.bodyFrame.Width / 2, drawPlayer.height - drawPlayer.bodyFrame.Height + 4f) + drawPlayer.bodyPosition;
            DrawData drawData = new DrawData(texture, drawPos.Floor() + drawPlayer.bodyFrame.Size() / 2, drawPlayer.bodyFrame, color, drawPlayer.bodyRotation, drawInfo.bodyOrigin, 1f, drawInfo.spriteEffects, 0)
            {
                shader = drawInfo.bodyArmorShader
            };
            Main.playerDrawData.Add(drawData);
        });
        public static readonly PlayerLayer MiscEffect = new PlayerLayer("KirillandRandom", "MiscEffect", PlayerLayer.MiscEffectsFront, delegate (PlayerDrawInfo drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            MPlayer modPlayer = drawPlayer.GetModPlayer<MPlayer>();

            if (drawInfo.shadow != 0f || drawPlayer.dead) return;

            if (Main.gameMenu) return;

        });









        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (eyeofdeath == true)
            {
                damage = (int)(damage*1.3);
            }
            

            base.ModifyHitNPC(item, target, ref damage, ref knockback, ref crit);
        }


        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {if (flamingdedication) {
                target.AddBuff(BuffID.OnFire, 60);
            }
            base.OnHitNPC(item, target, damage, knockback, crit);
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (flamingdedication)
            {
                target.AddBuff(BuffID.OnFire, 60);
            }
            base.OnHitNPCWithProj(proj, target, damage, knockback, crit);
        }


        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
            if (flamingdedication)
            {
                npc.AddBuff(BuffID.OnFire, 120);
            }
            base.OnHitByNPC(npc, damage, crit);
        }

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit,
            ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            float custom_endurance= player.endurance;
            if (eyeofdeath == true)
            {
                custom_endurance += 0.1f;
            }

                if (flamingdedication)
            {
                player.AddBuff(BuffID.OnFire, 120);
            }

            int fdamage = (int)((damage - (0.5 * player.statDefense)) * (1 - custom_endurance));
            player.immune = true;

            player.immuneTime = player.longInvince? 80: 40;
            if (Main.expertMode)
            {fdamage = (int)((damage - (0.75 * player.statDefense)) * (1 - custom_endurance));
            }
            if ((eyeofdeath==true) && (fdamage < 30))
            {
                damage = 0;

                customDamage = true;
            }

            return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
        }

        public override void ModifyWeaponDamage(Item item, ref float add, ref float mult, ref float flat)
        {
            if (fireamplification>0)
            {
                if (player.onFire == true)
                {
                    mult += fireamplification;
                }
            }
            base.ModifyWeaponDamage(item, ref add, ref mult, ref flat);
        }

        public override void UpdateLifeRegen()
        {   if (fireregen)
            {
                
                if (player.onFire == true)
                {
                    player.lifeRegen = +16;


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
                    if (player.statLife >= (player.statLifeMax / 2))
                    {
                        player.statLife = (player.statLifeMax / 2);
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
        //    packet.Write((byte)player.whoAmI);
        //    packet.Write(flames_summoned);
        //    packet.Send(toWho, fromWho);
        //}

        //public override void SendClientChanges(ModPlayer clientPlayer)
        //{
        //    // Here we would sync something like an RPG stat whenever the player changes it.
        //    ExamplePlayer clone = clientPlayer as ExamplePlayer;
        //    if (clone.nonStopParty != nonStopParty)
        //    {
        //        // Send a Mod Packet with the changes.
        //        var packet = mod.GetPacket();
        //        packet.Write((byte)ExampleModMessageType.NonStopPartyChanged);
        //        packet.Write((byte)player.whoAmI);
        //        packet.Write(nonStopParty);
        //        packet.Send();
        //    }
        //}
    }
}
