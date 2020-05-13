using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Utilities;
using Microsoft.Xna.Framework;
using EpicBattleFantasyUltimate.Items.Weapons.Guns.Revolvers;
using EpicBattleFantasyUltimate.Items.Weapons.Launchers;
using EpicBattleFantasyUltimate.Projectiles.StaffProjectiles;
using System.Data.OleDb;

namespace EpicBattleFantasyUltimate
{
    public class EpicPlayer : ModPlayer
    {

        #region shadow

        public bool shadow = false;

        #endregion

        #region Tired Variables

        public bool Tired = true;
        public float TiredStacks;
        float TiredPower;

        #endregion

        #region Rampant Bleed Variables

        public bool RBleed;
        public int RBleedStacks;

        #endregion

        #region Weakened Variables

        public bool Weakened = true;
        public int WeakenedStacks;
        double WeakenedPower;
        double WeakenedMult;

        #endregion

        #region ModifyHitNPC

        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            #region Weakened Weakening

            if (Weakened && WeakenedStacks <= 5)
            {
                WeakenedMult = 0.1 * WeakenedStacks;
                WeakenedPower = 1 - WeakenedMult;
                damage = (int)(damage * WeakenedPower);
            }
            else if (Weakened && WeakenedStacks > 5)
            {
                damage = (int)(damage * 0.75f);
            }

            #endregion

        }

        #endregion

        #region ResetEffects

        public override void ResetEffects()
        {

            #region Rampant Bleed Reset

            if (RBleed == false)
            {
                RBleedStacks = 0;
            }
            
            RBleed = false;

            #endregion

            #region Shadow Blaster Effect

            if (player.HeldItem.type != mod.ItemType("ShadowBlasterGun"))
            {
                shadow = false;
            }

            #endregion

            #region Weakened Reset

            if (Weakened == false)
            {
                WeakenedStacks = 0;
            }

            Weakened = false;

            #endregion

            #region Tired Reset

            if (Tired == false)
            {
                TiredStacks = 0;
            }

            Tired = false;

            #endregion

        }

        #endregion

        #region UpdateBadLifeRegen

        public override void UpdateBadLifeRegen()
        {

            #region Rampant Bleed Effects

            if (RBleed)
            {
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }

                player.lifeRegenTime = 0;
                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 8 life lost per second.
                player.lifeRegen -= RBleedStacks;

            }

            #endregion

        }

        #endregion

        #region Crystal Wing Healing variables

        int dps = 0;
        int timer = 60;
        bool heal = true;
        int timer2 = 60;

        #endregion

        #region PostUpdateBuffs

        public override void PostUpdateBuffs()
        {

            #region Sugar Rush Jump Height

            if (player.HasBuff(mod.BuffType("SugarRush")))
            {
                Player.jumpHeight += 7;
                Player.jumpSpeed += 0.3f;
            }

            #endregion

        }

        #endregion

        #region UseTimeMultiplier

        public override float UseTimeMultiplier(Item item)
        {


            #region Haste and Infuriated speed effects

            if (player.HasBuff(mod.BuffType("HasteBuff")) && player.HasBuff(mod.BuffType("Infuriated")))
            {
                return 4f;
            }
            if (!item.IsAir && (item.ranged || item.magic || item.thrown || item.melee) && player.HasBuff(mod.BuffType("HasteBuff")))
            {
                
                return 2f;
                
            }
            if (!item.IsAir && (item.ranged || item.magic || item.thrown || item.melee) && player.HasBuff(mod.BuffType("Infuriated")))
            {
                
                return 2f;
                
            }

            #endregion

            #region Tired

            if (!item.IsAir && (item.ranged || item.magic || item.thrown || item.melee) && player.HasBuff(mod.BuffType("Tired")) && TiredStacks <= 0.5f)
            {

                return 1f - TiredStacks;

            }
            else if (!item.IsAir && (item.ranged || item.magic || item.thrown || item.melee) && player.HasBuff(mod.BuffType("Tired")) && TiredStacks > 0.5f)
            {
                return 0.5f;
            }

            #endregion

            return 1f;



            
        }

        #endregion

        #region OnHitNPCWithProj

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {

            #region Crystal Healing

            #region Crystal Revolver Healing

            if (player.HeldItem.type == mod.ItemType("CrystalRevolver") && player.statLife < player.statLifeMax)
            {
                if (player.HasItem(mod.ItemType("CrystalRevolver")) && player.HasItem(mod.ItemType("CrystalWing")))
                {
                    player.statLife += 6;
                    player.HealEffect(6);
                }
                else
                {
                    player.statLife += 3;
                    player.HealEffect(3);
                }

            }

            #endregion

            #region Crystal Wing Healing

            dps += damage;


            if (player.HeldItem.type == mod.ItemType("CrystalWing") && player.statLife < player.statLifeMax && dps > 100 && heal == true && player.HasItem(mod.ItemType("CrystalRevolver")))
            {
                player.statLife += dps / 2;
                player.HealEffect(dps / 2);

                heal = false;
            }
            else if (player.HeldItem.type == mod.ItemType("CrystalWing") && player.statLife < player.statLifeMax && dps > 100 && heal == true)
            {
                player.statLife += dps / 4;
                player.HealEffect(dps / 4);

                heal = false;
            }

            #endregion

            #endregion

            #region Vortex Implosion

            #region Vortex Cannon implosion mechanic

            if (player.HeldItem.type == mod.ItemType("VortexCannon"))
            {
                if (player.HasItem(mod.ItemType("VortexCannon")) && player.HasItem(mod.ItemType("VortexRevolver")))
                {
                    target.velocity = target.DirectionTo(proj.Center) * 40;
                }
                else
                {
                    target.velocity = target.DirectionTo(proj.Center) * 20;
                }

            }

            #endregion

            #region Vortex Revolver Inverted Knockback

            if (player.HeldItem.type == mod.ItemType("VortexRevolver"))
            {
                if (player.HasItem(mod.ItemType("VortexCannon")) && player.HasItem(mod.ItemType("VortexRevolver")))
                {
                    target.velocity = target.DirectionTo(proj.Center) * 10;
                }
                else
                {
                    target.velocity = target.DirectionTo(proj.Center) * 5;
                }
            }

            #endregion

            #endregion

            #region Hellfire AddBuff

            if (player.HeldItem.type == mod.ItemType("HellfireRevolver") || player.HeldItem.type == mod.ItemType("HellfireShotgun"))
            {
                if (player.HasItem(mod.ItemType("HellfireRevolver")) && player.HasItem(mod.ItemType("HellfireShotgun")))
                {
                    target.AddBuff(BuffID.OnFire, 300);
                }
                else
                {
                    target.AddBuff(BuffID.OnFire, 60);
                }

            }

            #endregion

        }

        #endregion

        #region PostUpdate

        public override void PostUpdate()
        {

            #region Crystal Wing Healing Logic

            if (heal == false)
            {
                timer--;
            }

            timer2--;

            if (timer <= 0)
            {
                dps = 0;
                heal = true;
                timer = 60;
            }

            if (timer2 <= 0 && dps > 0)
            {
                dps = 0;

                timer2 = 60;
            }

            #endregion



        }

        #endregion

        #region PreUpdate
        public override void PreUpdate()
        {
            #region Coconut Health

            if (player.HasItem(mod.ItemType("CoconutGun")) && player.HasItem(mod.ItemType("CoconutShooter")) && player.statLife > 0)
            {
                player.statLifeMax2 += 50;

            }

            #endregion

            #region Shadow Blaster Effect

            if (player.HasItem(mod.ItemType("ShadowBlaster")) && player.HeldItem.type == mod.ItemType("ShadowBlasterGun"))
            {
                shadow = true;
            }

            #endregion

        }

        #endregion

        #region PostUpdateRunSpeeds

        public override void PostUpdateRunSpeeds()
        {

            #region Thunder Core Speed

            if (player.HeldItem.type == mod.ItemType("ThunderCore") && player.HasItem(mod.ItemType("ThunderCoreGun")))
            {
                player.maxRunSpeed += 1.2f;
                player.moveSpeed += 1.2f;
                player.accRunSpeed += 1.2f;
            }

            #endregion

            #region Dragon's Feather Speed

            if (player.HeldItem.type == mod.ItemType("DragonsFeather"))
            {
                player.maxRunSpeed += 1.5f;
                player.moveSpeed += 1.5f;
                player.accRunSpeed += 1.5f;
            }

            #endregion

            #region Haste Speed

            if (player.HasBuff(mod.BuffType("HasteBuff")))
            {
                player.maxRunSpeed += 2f;
                player.accRunSpeed += 2f;
                player.moveSpeed += 2f;
            }

            #endregion

            #region Sugar Rush Speed

            if (player.HasBuff(mod.BuffType("SugarRush")))
            {
                player.maxRunSpeed += 2f;
                player.moveSpeed += 1f;

            }

            #endregion

            #region King's Guard Shield Speed

            for (int i = 3; i < 8 + player.extraAccessorySlots; i++)
            {
                if (player.armor[i].type == mod.ItemType("KingsGuardShield"))
                {
                    player.maxRunSpeed *= 0.70f;
                    player.accRunSpeed *= 0.70f;
                }
            }

            #endregion

            #region Tired Speed

            if (Tired && TiredStacks <= 0.5f)
            {
                
                TiredPower = 1f - TiredStacks;
                player.maxRunSpeed *= TiredPower;
                player.accRunSpeed *= TiredPower;
                player.moveSpeed *= TiredPower;

            }
            else if (Tired && TiredStacks > 0.5f)
            {
                player.maxRunSpeed *= 0.5f;
                player.accRunSpeed *= 0.5f;
                player.moveSpeed *= 0.5f;
            }

            #endregion

        }

        #endregion

        #region GetWeaponCrit

        public override void GetWeaponCrit(Item item, ref int crit)
        {

            #region Gungnir Crit

            if (player.HasItem(mod.ItemType("GungnirRifle")) && player.HasItem(mod.ItemType("GungnirRevolver")) && (player.HeldItem.type == mod.ItemType("GungnirRifle") || player.HeldItem.type == mod.ItemType("GungnirRevolver")))
            {
                crit += 15;
            }

            #endregion

        }

        #endregion

        #region DrawEffects

        public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            #region Rampant Bleeding Dust

            if (RBleed)
            {
                if (RBleedStacks <= 5)
                {
                    if (Main.rand.NextFloat() <= .1f)
                    {
                        Dust.NewDustDirect(player.position - new Vector2(2f, 2f), player.width, player.height, 5, 0f, 0f, 0, new Color(255, 255, 255), 1f);
                    }
                }
                else if (RBleedStacks > 5 && RBleedStacks <= 10)
                {
                    if (Main.rand.NextFloat() <= .2f)
                    {
                        Dust.NewDustDirect(player.position - new Vector2(2f, 2f), player.width, player.height, 5, 0f, 0f, 0, new Color(255, 255, 255), 1f);
                    }
                }
                else if (RBleedStacks > 10 && RBleedStacks <= 20)
                {
                    if (Main.rand.NextFloat() <= .4f)
                    {
                        Dust.NewDustDirect(player.position - new Vector2(2f, 2f), player.width, player.height, 5, 0f, 0f, 0, new Color(255, 255, 255), 1f);
                    }
                }
                else if (RBleedStacks > 20)
                {
                    Dust.NewDustDirect(player.position - new Vector2(2f, 2f), player.width, player.height, 5, 0f, 0f, 0, new Color(255, 255, 255), 1f);
                }

               



            }
            #endregion
        }

        #endregion

    }
}



