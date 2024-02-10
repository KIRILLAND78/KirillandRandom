using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace KirillandRandom.NPCs
{
    public class MNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public Projectile barrel { get; internal set; }

        private int _charge_e;
        public int charge_e { get { return _charge_e; } set { _charge_e = value; if (_charge_e > 5) _charge_e = 5; if (_charge_e <= 0) { _charge_e = 0; charge = false; } } }
        public bool charge;

        public override void ResetEffects(NPC NPC)
        {
            charge = false;
        }

        public override void UpdateLifeRegen(NPC NPC, ref int damage)
        {
            if (charge == false) { charge_e = 0; };
            if (charge)
            {
                if (NPC.lifeRegen > 0)
                {
                    NPC.lifeRegen = 0;
                }
                NPC.lifeRegen -= 2 * charge_e;
                if (damage < charge_e)
                {
                    damage = charge_e;
                }
            }
        }

        public override void DrawEffects(NPC NPC, ref Color drawColor)
        {
            if (charge)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(NPC.position - new Vector2(2f, 2f), NPC.width + 4, NPC.height + 4, DustID.Electric, NPC.velocity.X * 0.4f, NPC.velocity.Y * 0.4f, 100, default(Color), 0.13f * charge_e);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 0.15f * charge_e;
                    Main.dust[dust].velocity.Y -= 0.075f * charge_e;
                    if (Main.rand.NextBool(4))
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale = 0.02f * charge_e;
                    }
                }
                Lighting.AddLight(NPC.position, 0.1f, 0.2f, 0.5f);
            }
            base.DrawEffects(NPC, ref drawColor);
            if ((charge) && (charge_e > 0))
            {
                ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, FontAssets.MouseText.Value, $"{charge_e}", NPC.Top - Main.screenPosition + new Vector2(0, -20), Color.AliceBlue, 0f, new Vector2(0, 0), new Vector2(1, 1));
            }
        }
    }
}