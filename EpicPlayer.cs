using EpicBattleFantasyUltimate.Buffs.Buffs;
using EpicBattleFantasyUltimate.Items.Weapons.Guns.Revolvers;
using EpicBattleFantasyUltimate.Items.Weapons.Launchers;
using EpicBattleFantasyUltimate.NPCs.Ores;
using EpicBattleFantasyUltimate.Projectiles.NPCProj.OreExplosions;
using EpicBattleFantasyUltimate.Projectiles.NPCProj.Wraith;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using static EpicBattleFantasyUltimate.EpicBattleFantasyUltimate;

namespace EpicBattleFantasyUltimate
{
    public class EpicPlayer : ModPlayer
    {
        #region Flair Slot Items

        Item item1 => EpicBattleFantasyUltimate.instance?.SlotUI.FlairSlots[0].Item;
        Item item2 => EpicBattleFantasyUltimate.instance?.SlotUI.FlairSlots[1].Item;
        Item item3 => EpicBattleFantasyUltimate.instance?.SlotUI.FlairSlots[2].Item;

        #endregion Flair Slot Items

        #region Attack Speed Multiplier Vars

        private float multiplier = 0f;
        private float Haste = 0f;
        private float Infuriated = 0f;

        #endregion Attack Speed Multiplier Vars

        #region Limit Break

        public const int DefaultMaxLimit = 100;

        public static readonly Color GetLimit = Color.OrangeRed;

        public static EpicPlayer ModPlayer(Player player)
        {
            return player.GetModPlayer<EpicPlayer>();
        }

        //Limit

        public int LimitCurrent;
        public int MaxLimit;
        public int MaxLimit2;
        public int LimitGen;
        public bool Tryforce;
        public int TimeDiff;
        public int TimePassed;
        public float HpLost;
        public float HpModifier;

        public override void Initialize()
        {
            MaxLimit = DefaultMaxLimit;
            MaxLimit2 = MaxLimit;
        }

        public override void OnEnterWorld(Player player)
        {
        }

        #endregion Limit Break

        #region Save/Load

        public override TagCompound Save()
        {
            TagCompound tc = new TagCompound()
            {
            {"LimitPoints", LimitCurrent },
            };

            for (int i = 0; i < EpicBattleFantasyUltimate.instance?.SlotUI.FlairSlots.Length; ++i)
            {
                if (EpicBattleFantasyUltimate.instance.SlotUI.FlairSlots[i].Item == null)
                {
                    tc.Add("flairSlot_" + i, string.Empty);
                }
                else
                {
                    tc.Add("flairSlot_" + i, ItemIO.ToBase64(EpicBattleFantasyUltimate.instance.SlotUI.FlairSlots[i].Item));
                }
            }

            return (tc);
        }

        public override void Load(TagCompound tc)
        {
            LimitCurrent = tc.GetInt("LimitPoints");

            if (EpicBattleFantasyUltimate.instance == null)
            {
                return;
            }

            for (int i = 0; i < EpicBattleFantasyUltimate.instance?.SlotUI.FlairSlots.Length; ++i)
            {
                string flairSlotData = tc.GetString("flairSlot_" + i);

                if (!string.IsNullOrWhiteSpace(flairSlotData))
                {
                    EpicBattleFantasyUltimate.instance.SlotUI.FlairSlots[i].Item = ItemIO.FromBase64(flairSlotData);
                }
                else
                {
                    EpicBattleFantasyUltimate.instance.SlotUI.FlairSlots[i].Item = new Item();
                }
            }
        }

        #endregion Save/Load

        #region Overhead Buff Drawing

        public int numberOfDrawableBuffs;
        private const int drawableBuffOffset = 42;

        #endregion Overhead Buff Drawing

        #region Shadow

        public bool shadow = false;

        #endregion Shadow

        #region Tired Variables

        public bool Tired = true;
        public int TiredStacks = 0;
        private float TiredPower = 1f;
        private float TiredMult = 1f;

        #endregion Tired Variables

        #region Rampant Bleed Variables

        public bool RBleed;
        public int RBleedStacks;

        #endregion Rampant Bleed Variables

        #region Weakened Variables

        public bool Weakened = true;
        public int WeakenedStacks;
        private double WeakenedPower;
        private double WeakenedMult;

        #endregion Weakened Variables

        #region Cursed Variables

        public bool Cursed = false;
        public bool CursedAlphaCheck = true;
        public int CursedStacks = 1;
        public float CursedAlpha = 0;
        private double CursedDefense;
        private double CursedMult;
        public float CurseRotation;

        #endregion Cursed Variables

        #region Crystal Wing Healing variables

        private int dps = 0;
        private int timer = 60;
        private bool heal = true;
        private int timer2 = 60;

        #endregion Crystal Wing Healing variables

        #region Blessed Variables

        public bool Blessed = false;

        #endregion Blessed Variables

        #region OnHitByNPC

        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
            #region OreImmunity

            if (npc.type == ModContent.NPCType<PeridotOre>() || npc.type == ModContent.NPCType<QuartzOre>() || npc.type == ModContent.NPCType<ZirconOre>() || npc.type == ModContent.NPCType<SapphireOre>() || npc.type == ModContent.NPCType<AmethystOre>() || npc.type == ModContent.NPCType<AmethystOre_Dark>() || npc.type == ModContent.NPCType<RubyOre>() || npc.type == ModContent.NPCType<TopazOre>())
            {
                player.immune = false;
            }

            #endregion OreImmunity

            #region Limit Generation

            LimitGenerationNPC(npc, damage, crit);

            TimeDiff = 0;

            #endregion Limit Generation
        }

        #endregion OnHitByNPC

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (damageSource.SourceProjectileIndex > -1)
            {
                if (damageSource.SourceProjectileType == ModContent.ProjectileType<CursingRune>())
                {
                    player.immune = false;

                    return true;
                }

                #region Sapphire Explosion Knockback

                if (damageSource.SourceProjectileType == ModContent.ProjectileType<SapphireExplosion>())
                {
                    Vector2 hitDir = Vector2.Normalize(player.position - Main.projectile[damageSource.SourceProjectileIndex].Center);

                    player.velocity = hitDir * 16f; // Strong knockback.
                }

                #endregion Sapphire Explosion Knockback
            }

            return true;
        }

        #region Limit Hooks

        public override void OnHitByProjectile(Projectile proj, int damage, bool crit)
        {
            LimitGenerationProj(proj, damage, crit);

            TimeDiff = 0;
        }

        private void LimitGenerationNPC(NPC npc, int damage, bool crit)
        {
            float HpLost = ((float)damage / (float)player.statLifeMax2) * 100f;

            // Generates a value between 0.75-1.25 based on current life.
            // Lower value if more life left.
            float HpModifier = 1.25f - (float)player.statLife / (float)player.statLifeMax * 0.5f;

            // Generates:
            // 0.0:1.0 + 0.0:0.25
            // Result: 0.0:1.25
            float TimePassed = ((float)MathHelper.Clamp(TimeDiff, 0, 2) / 2f + (float)MathHelper.Clamp(TimeDiff, 0, 60) / 240f);

            // Generates:
            // 0:50 * 0.75:1.25 * 0.0:1.25
            // Result: 0:78
            int LimitGen = (int)(HpLost * 0.5f * HpModifier * TimePassed);

            if (Tryforce)
            {
                LimitCurrent += (int)(LimitGen * 1.20f);
                LimitCurrent = (int)MathHelper.Clamp(LimitCurrent, 0, MaxLimit2);
            }
            else
            {
                LimitCurrent += LimitGen;
                LimitCurrent = (int)MathHelper.Clamp(LimitCurrent, 0, MaxLimit2);
            }
        }

        private void LimitGenerationProj(Projectile proj, int damage, bool crit)
        {
            float HpLost = ((float)damage / (float)player.statLifeMax2) * 100f;

            // Generates a value between 0.75-1.25 based on current life.
            // Lower value if more life left.
            float HpModifier = 1.25f - (float)player.statLife / (float)player.statLifeMax * 0.5f;

            // Generates:
            // 0.0:1.0 + 0.0:0.25
            // Result: 0.0:1.25
            float TimePassed = ((float)MathHelper.Clamp(TimeDiff, 0, 2) / 2f + (float)MathHelper.Clamp(TimeDiff, 0, 60) / 240f);

            // Generates:
            // 0:50 * 0.75:1.25 * 0.0:1.25
            // Result: 0:78
            int LimitGen = (int)(HpLost * 0.5f * HpModifier * TimePassed);

            if (Tryforce)
            {
                LimitCurrent += (int)(LimitGen * 1.20f);
                LimitCurrent = (int)MathHelper.Clamp(LimitCurrent, 0, MaxLimit2);
            }
            else
            {
                LimitCurrent += LimitGen;
                LimitCurrent = (int)MathHelper.Clamp(LimitCurrent, 0, MaxLimit2);
            }
        }

        public void LimitEffect(int healAmount, bool broadcast = true)
        {
            CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), Color.OrangeRed, healAmount);
            if (broadcast && Main.netMode == NetmodeID.MultiplayerClient && player.whoAmI == Main.myPlayer)
            {
                NetMessage.SendData(MessageID.HealEffect, -1, -1, null, player.whoAmI, healAmount);
            }
        }

        public override void LoadLegacy(BinaryReader reader)
        {
            int _ = reader.ReadInt32();
            LimitCurrent = reader.ReadInt32();
        }

        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = mod.GetPacket();
            packet.Write((byte)EpicMessageType.EpicPlayerSyncPlayer);
            packet.Write((byte)player.whoAmI);
            packet.Write(LimitCurrent);
            packet.Send(toWho, fromWho);
        }

        #endregion Limit Hooks

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

            #endregion Weakened Weakening
        }

        #endregion ModifyHitNPC

        #region ResetEffects

        public override void ResetEffects()
        {
            #region Rampant Bleed Reset

            if (RBleed == false)
            {
                RBleedStacks = 0;
            }

            RBleed = false;

            #endregion Rampant Bleed Reset

            #region Shadow Blaster Effect

            if (player.HeldItem.type != mod.ItemType("ShadowBlasterGun"))
            {
                shadow = false;
            }

            #endregion Shadow Blaster Effect

            #region Weakened Reset

            if (Weakened == false)
            {
                WeakenedStacks = 0;
            }

            Weakened = false;

            #endregion Weakened Reset

            #region Tired Reset

            if (Tired == false)
            {
                TiredStacks = 0;
                TiredPower = 1f;
                TiredMult = 1f;
            }

            Tired = false;

            #endregion Tired Reset

            #region Cursed Reset

            if (Cursed == false)
            {
                CursedStacks = 1;
            }

            Cursed = false;

            #endregion Cursed Reset

            #region Number Of Drawable Buffs

            numberOfDrawableBuffs = -1;

            Blessed = false;

            #endregion Number Of Drawable Buffs

            #region Tryforce

            Tryforce = false;

            #endregion Tryforce
        }

        #endregion ResetEffects

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

            #endregion Rampant Bleed Effects
        }

        #endregion UpdateBadLifeRegen

        #region PostUpdateBuffs

        public override void PostUpdateBuffs()
        {
            #region Sugar Rush Jump Height

            if (player.HasBuff(mod.BuffType("SugarRush")))
            {
                Player.jumpHeight += 7;
                Player.jumpSpeed += 0.3f;
            }

            #endregion Sugar Rush Jump Height

            #region Cursed Effects

            if (Cursed)
            {
                if (CursedStacks <= 5)
                {
                    CursedMult = 0.1 * CursedStacks;
                    CursedDefense = 1 - CursedMult;
                    player.statDefense = (int)(player.statDefense * CursedDefense);
                }
                else
                {
                    player.statDefense = (int)(player.statDefense * 0.5);
                }
            }
            else
            {
                player.statDefense = player.statDefense;
            }

            if (CursedStacks == 1)
            {
                CursedAlpha = .25f;
            }
            else if (CursedStacks == 2)
            {
                CursedAlpha = .50f;
            }
            else if (CursedStacks == 3)
            {
                CursedAlpha = .75f;
            }
            else if (CursedStacks == 4)
            {
                CursedAlpha = 1;
            }
            else if (CursedStacks >= 5)
            {
                if (CursedAlphaCheck)
                {
                    CursedAlpha -= .05f;
                }
                else if (CursedAlphaCheck == false)
                {
                    CursedAlpha += .05f;
                }

                if (CursedAlpha >= 1f)
                {
                    CursedAlphaCheck = true;
                }
                else if (CursedAlpha <= 0)
                {
                    CursedAlphaCheck = false;
                }
            }

            #endregion Cursed Effects

            #region Flair Slots

            // Check to see if the item EXISTS.
            if (item1 != null && !item1.IsAir)
            {
                item1.modItem.UpdateAccessory(player, false);
            }

            // Check to see if the item EXISTS.
            if (item2 != null && !item2.IsAir)
            {
                item2.modItem.UpdateAccessory(player, false);
            }

            // Check to see if the item EXISTS.
            if (item3 != null && !item3.IsAir)
            {
                item3.modItem.UpdateAccessory(player, false);
            }

            #endregion Flair Slots
        }

        #endregion PostUpdateBuffs

        #region UseTimeMultiplier

        public override float UseTimeMultiplier(Item item)
        {
            #region Haste and Infuriated speed effects

            if (player.HasBuff(ModContent.BuffType<HasteBuff>()))
            {
                Haste = 1f;
            }
            else
            {
                Haste = 0f;
            }
            if (player.HasBuff(ModContent.BuffType<Infuriated>()))
            {
                Infuriated = 2f;
            }
            else
            {
                Infuriated = 0f;
            }

            #endregion Haste and Infuriated speed effects

            if (TiredStacks < 5)
            {
                TiredMult = 1f - TiredStacks * 0.1f;
            }
            else
            {
                TiredMult = 0.5f;
            }

            multiplier = (1f + (Haste + Infuriated)) * TiredMult;

            return multiplier;
        }

        #endregion UseTimeMultiplier

        #region OnHitNPCWithProj

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            #region Crystal Healing

            #region Crystal Revolver Healing

            if (player.HeldItem.type == ModContent.ItemType<CrystalRevolver>() && player.statLife < player.statLifeMax)
            {
                if (player.HasItem(ModContent.ItemType<CrystalRevolver>()) && player.HasItem(ModContent.ItemType<CrystalWing>()))
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

            #endregion Crystal Revolver Healing

            #region Crystal Wing Healing

            dps += damage;

            if (player.HeldItem.type == mod.ItemType("CrystalWing") && player.statLife < player.statLifeMax && dps > 100 && heal && player.HasItem(mod.ItemType("CrystalRevolver")))
            {
                player.statLife += dps / 2;
                player.HealEffect(dps / 2);

                heal = false;
            }
            else if (player.HeldItem.type == mod.ItemType("CrystalWing") && player.statLife < player.statLifeMax && dps > 100 && heal)
            {
                player.statLife += dps / 4;
                player.HealEffect(dps / 4);

                heal = false;
            }

            #endregion Crystal Wing Healing

            #endregion Crystal Healing

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

            #endregion Vortex Cannon implosion mechanic

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

            #endregion Vortex Revolver Inverted Knockback

            #endregion Vortex Implosion

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

            #endregion Hellfire AddBuff
        }

        #endregion OnHitNPCWithProj

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

            #endregion Crystal Wing Healing Logic

            if (LimitCurrent < 0)
            {
                LimitCurrent = 0;
            }
        }

        #endregion PostUpdate

        #region PreUpdate

        public override void PreUpdate()
        {

            #region Shadow Blaster Effect

            if (player.HasItem(mod.ItemType("ShadowBlaster")) && player.HeldItem.type == mod.ItemType("ShadowBlasterGun"))
            {
                shadow = true;
            }

            #endregion Shadow Blaster Effect

            #region Limit Stuff

            TimeDiff++;

            #endregion Limit Stuff
        }

        #endregion PreUpdate

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

            #endregion Thunder Core Speed

            #region Dragon's Feather Speed

            if (player.HeldItem.type == mod.ItemType("DragonsFeather"))
            {
                player.maxRunSpeed += 1.5f;
                player.moveSpeed += 1.5f;
                player.accRunSpeed += 1.5f;
            }

            #endregion Dragon's Feather Speed

            #region Haste Speed

            if (player.HasBuff(mod.BuffType("HasteBuff")))
            {
                player.maxRunSpeed += 2f;
                player.accRunSpeed += 2f;
                player.moveSpeed += 2f;
            }

            #endregion Haste Speed

            #region Sugar Rush Speed

            if (player.HasBuff(mod.BuffType("SugarRush")))
            {
                player.maxRunSpeed += 2f;
                player.moveSpeed += 1f;
            }

            #endregion Sugar Rush Speed

            #region King's Guard Shield Speed

            for (int i = 3; i < 8 + player.extraAccessorySlots; i++)
            {
                if (player.armor[i].type == mod.ItemType("KingsGuardShield"))
                {
                    player.maxRunSpeed *= 0.70f;
                    player.accRunSpeed *= 0.70f;
                }
            }

            #endregion King's Guard Shield Speed

            #region Tired Speed

            if (Tired)
            {
                TiredPower = TiredStacks * 0.1f;

                if (TiredStacks < 5)
                {
                    player.maxRunSpeed *= (1f - TiredPower);
                    player.accRunSpeed *= (1f - TiredPower);
                    player.moveSpeed *= (1f - TiredPower);
                }
                else if (TiredStacks >= 5)
                {
                    player.maxRunSpeed *= 0.5f;
                    player.accRunSpeed *= 0.5f;
                    player.moveSpeed *= 0.5f;
                }
            }

            #endregion Tired Speed

            #region Heaven's speed

            if (player.HasBuff(ModContent.BuffType<Kyun>()))
            {
                player.maxRunSpeed += 1.5f;
                player.accRunSpeed += 1.5f;
                player.moveSpeed += 1.5f;
            }

            #endregion Heaven's speed
        }

        #endregion PostUpdateRunSpeeds

        #region GetWeaponCrit

        public override void GetWeaponCrit(Item item, ref int crit)
        {
            #region Gungnir Crit

            if (player.HasItem(mod.ItemType("GungnirRifle")) && player.HasItem(mod.ItemType("GungnirRevolver")) && (player.HeldItem.type == mod.ItemType("GungnirRifle") || player.HeldItem.type == mod.ItemType("GungnirRevolver")))
            {
                crit += 15;
            }

            #endregion Gungnir Crit
        }

        #endregion GetWeaponCrit

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
                        Dust.NewDustDirect(player.position - new Vector2(2f, 2f), player.width, player.height, DustID.Blood, 0f, 0f, 0, new Color(255, 255, 255), 1f);
                    }
                }
                else if (RBleedStacks > 5 && RBleedStacks <= 10)
                {
                    if (Main.rand.NextFloat() <= .2f)
                    {
                        Dust.NewDustDirect(player.position - new Vector2(2f, 2f), player.width, player.height, DustID.Blood, 0f, 0f, 0, new Color(255, 255, 255), 1f);
                    }
                }
                else if (RBleedStacks > 10 && RBleedStacks <= 20)
                {
                    if (Main.rand.NextFloat() <= .4f)
                    {
                        Dust.NewDustDirect(player.position - new Vector2(2f, 2f), player.width, player.height, DustID.Blood, 0f, 0f, 0, new Color(255, 255, 255), 1f);
                    }
                }
                else if (RBleedStacks > 20)
                {
                    Dust.NewDustDirect(player.position - new Vector2(2f, 2f), player.width, player.height, DustID.Blood, 0f, 0f, 0, new Color(255, 255, 255), 1f);
                }
            }

            #endregion Rampant Bleeding Dust

        }

        #endregion DrawEffects

        #region ModifyDrawLayers

        public static readonly PlayerLayer MiscEffects = new PlayerLayer("EpicBattleFantasyUltimate", "MiscEffects", PlayerLayer.MiscEffectsFront, delegate (PlayerDrawInfo drawInfo)
        {
            if (drawInfo.shadow != 0f)
            {
                return;
            }

            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("EpicBattleFantasyUltimate");
            EpicPlayer modPlayer = drawPlayer.GetModPlayer<EpicPlayer>();

            int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X) - (drawableBuffOffset / 2) * modPlayer.numberOfDrawableBuffs;
            int drawY = (int)(drawInfo.position.Y - 4f - Main.screenPosition.Y);

            if (modPlayer.Cursed)
            {
                // Do drawing.
                Texture2D texture = mod.GetTexture("Buffs/Debuffs/CursedEffect2");

                Color alpha = Lighting.GetColor((int)((drawInfo.position.X + drawPlayer.width / 2f) / 16f), (int)((drawInfo.position.Y - 4f - texture.Height / 2f) / 16f));
                DrawData data = new DrawData(texture, new Vector2(drawX, drawY - 5), null, alpha * modPlayer.CursedAlpha, 0f, new Vector2(texture.Width / 2, texture.Height), 1f, SpriteEffects.None, 0);
                Main.playerDrawData.Add(data);

                drawX += drawableBuffOffset;
            }

            if (modPlayer.Blessed)
            {
                // Do drawing.
                Texture2D texture = mod.GetTexture("Buffs/Buffs/BlessedEffect");

                Color alpha2 = Lighting.GetColor((int)((drawInfo.position.X + drawPlayer.width / 2f) / 16f), (int)((drawInfo.position.Y - 4f - texture.Height / 2f) / 16f));
                DrawData data = new DrawData(texture, new Vector2(drawX, drawY - 5), null, alpha2 * 1f, 0f, new Vector2(texture.Width / 2, texture.Height), 1f, SpriteEffects.None, 0);
                Main.playerDrawData.Add(data);

                drawX += drawableBuffOffset;
            }
        });

        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {
            if (player.HeldItem.modItem is Items.SignatureItems.PaintSplatteredBrush) PlayerLayer.HeldItem.visible = false;

            Action<PlayerDrawInfo> layerTarget = DrawGlowmasks; //the Action<T> of our layer. This is the delegate which will actually do the drawing of the layer.
            PlayerLayer layer = new PlayerLayer("ExampleSwordLayer", "Sword Glowmask", layerTarget); //Instantiate a new instance of PlayerLayer to insert into the list
            layers.Insert(layers.IndexOf(layers.FirstOrDefault(n => n.Name == "Arms")), layer); //Insert the layer at the appropriate index.

            if (player.statLife > 0)
            {
                MiscEffects.visible = true;
                layers.Add(MiscEffects);
            }

            void DrawGlowmasks(PlayerDrawInfo info)
            {
                if (info.drawPlayer.HeldItem.modItem is Items.IPlayerLayerDrawable) (info.drawPlayer.HeldItem.modItem as Items.IPlayerLayerDrawable).DrawGlowmask(info);
            }
        }

        #endregion ModifyDrawLayers
    }
}