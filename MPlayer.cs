
using KirillandRandom.Buffs;
using KirillandRandom.Items;
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
        public override void ResetEffects()
        {
            flamingdedication = false;
            eyeofdeath = false;
                Something = false;
            fireregen = false;

            fireamplification = 0;
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
            if (flamingdedication)
            {
                player.AddBuff(BuffID.OnFire, 120);
            }

            int fdamage = (int)((damage - (0.5 * player.statDefense)) * (1 - player.endurance));
            if (Main.expertMode)
            {
                fdamage = (int)((damage - (0.75 * player.statDefense)) * (1 - player.endurance));
                player.immune = true;
            }
            if ((eyeofdeath==true) && (fdamage < 25))
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
            // Here we would make a backup clone of values that are only correct on the local players Player instance.
            // Some examples would be RPG stats from a GUI, Hotkey states, and Extra Item Slots
        }
    }
}
