using EpicBattleFantasyUltimate.Buffs.Buffs;
using EpicBattleFantasyUltimate.Buffs.Debuffs;
using EpicBattleFantasyUltimate.Items.Revolvers;
using EpicBattleFantasyUltimate.Items.Launchers;
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
using EpicBattleFantasyUltimate.Items.Swords;
using EpicBattleFantasyUltimate.Items.Accessories;

namespace EpicBattleFantasyUltimate
{
	public class EpicPlayer : ModPlayer
	{
		#region Attack Speed Multiplier Vars

		private float multiplier = 0f;
		private float Haste = 0f;
		private float Infuriated = 0f;

		#endregion Attack Speed Multiplier Vars

		#region Limit Break

		public const int DefaultMaxLimit = 100;

		public static readonly Color GetLimit = Color.OrangeRed;

		public static EpicPlayer ModPlayer(Player Player)
		{
			return Player.GetModPlayer<EpicPlayer>();
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

		public override void OnEnterWorld(Player Player)
		{
		}

		#endregion Limit Break

		#region Overhead Buff Drawing

		public int numberOfDrawableBuffs;
		private const int drawableBuffOffset = 42;

		#endregion Overhead Buff Drawing

		#region Shadow

		public bool shadow = false;

		#endregion Shadow

		#region Doomed Variables

		public bool Doom = false;
		int DoomBuff;

		#endregion

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

		public override void OnHitByNPC(NPC npc, int damage, bool crit)
		{
			#region OreImmunity

			if (npc.type == ModContent.NPCType<PeridotOre>() || npc.type == ModContent.NPCType<QuartzOre>() || npc.type == ModContent.NPCType<ZirconOre>() || npc.type == ModContent.NPCType<SapphireOre>() || npc.type == ModContent.NPCType<AmethystOre>() || npc.type == ModContent.NPCType<AmethystOre_Dark>() || npc.type == ModContent.NPCType<RubyOre>() || npc.type == ModContent.NPCType<TopazOre>())
			{
				Player.immune = false;
			}

			#endregion OreImmunity

			#region Limit Generation

			LimitGenerationNPC(npc, damage, crit);

			TimeDiff = 0;

			#endregion Limit Generation
		}

		public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
		{
			if (damageSource.SourceProjectileIndex > -1)
			{
				#region Sapphire Explosion Knockback

				if (damageSource.SourceProjectileType == ModContent.ProjectileType<SapphireExplosion>())
				{
					Vector2 hitDir = Vector2.Normalize(Player.position - Main.projectile[damageSource.SourceProjectileIndex].Center);

					Player.velocity = hitDir * 16f; // Strong knockback.
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
			float HpLost = ((float)damage / (float)Player.statLifeMax2) * 100f;

			// Generates a value between 0.75-1.25 based on current life.
			// Lower value if more life left.
			float HpModifier = 1.25f - (float)Player.statLife / (float)Player.statLifeMax * 0.5f;

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
			float HpLost = ((float)damage / (float)Player.statLifeMax2) * 100f;

			// Generates a value between 0.75-1.25 based on current life.
			// Lower value if more life left.
			float HpModifier = 1.25f - (float)Player.statLife / (float)Player.statLifeMax * 0.5f;

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
			CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y, Player.width, Player.height), Color.OrangeRed, healAmount);
			if (broadcast && Main.netMode == NetmodeID.MultiplayerClient && Player.whoAmI == Main.myPlayer)
			{
				NetMessage.SendData(MessageID.PlayerHeal, -1, -1, null, Player.whoAmI, healAmount);
			}
		}

		public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
		{
			ModPacket packet = Mod.GetPacket();
			packet.Write((byte)EpicMessageType.EpicPlayerSyncPlayer);
			packet.Write((byte)Player.whoAmI);
			packet.Write(LimitCurrent);
			packet.Send(toWho, fromWho);
		}

		#endregion Limit Hooks

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

			if (Player.HeldItem.type != ModContent.ItemType<ShadowBlasterGun>())
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

			#region Doomed Reset
			if (!Doom)
			{
				DoomBuff = 0;
			}
			Doom = false;
			#endregion
		}

		public override void UpdateBadLifeRegen()
		{
			#region Rampant Bleed Effects

			if (RBleed)
			{
				if (Player.lifeRegen > 0)
				{
					Player.lifeRegen = 0;
				}

				Player.lifeRegenTime = 0;
				// lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 8 life lost per second.
				Player.lifeRegen -= RBleedStacks;
			}

			#endregion Rampant Bleed Effects
		}

		public override void PostUpdateBuffs()
		{
			#region Sugar Rush Jump Height

			if (Player.HasBuff(ModContent.BuffType<SugarRush>()))
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
					Player.statDefense = (int)(Player.statDefense * CursedDefense);
				}
				else
				{
					Player.statDefense = (int)(Player.statDefense * 0.5);
				}
			}
			else
			{
				Player.statDefense = Player.statDefense;
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
		}

		public override float UseTimeMultiplier(Item item)
		{
			#region Haste and Infuriated speed effects

			if (Player.HasBuff(ModContent.BuffType<HasteBuff>()))
			{
				Haste = 1f;
			}
			else
			{
				Haste = 0f;
			}
			if (Player.HasBuff(ModContent.BuffType<Infuriated>()))
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

		public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
		{
			#region Crystal Revolver Healing

			if (Player.HeldItem.type == ModContent.ItemType<CrystalRevolver>() && Player.statLife < Player.statLifeMax)
			{
				if (Player.HasItem(ModContent.ItemType<CrystalRevolver>()) && Player.HasItem(ModContent.ItemType<CrystalWing>()))
				{
					Player.statLife += 6;
					Player.HealEffect(6);
				}
				else
				{
					Player.statLife += 3;
					Player.HealEffect(3);
				}
			}

			#endregion Crystal Revolver Healing

			#region Crystal Wing Healing

			dps += damage;

			if (Player.HeldItem.type == ModContent.ItemType<CrystalWing>() && Player.statLife < Player.statLifeMax && dps > 100 && heal && Player.HasItem(ModContent.ItemType<CrystalRevolver>()))
			{
				Player.statLife += dps / 2;
				Player.HealEffect(dps / 2);

				heal = false;
			}
			else if (Player.HeldItem.type == ModContent.ItemType<CrystalWing>() && Player.statLife < Player.statLifeMax && dps > 100 && heal)
			{
				Player.statLife += dps / 4;
				Player.HealEffect(dps / 4);

				heal = false;
			}

			#endregion Crystal Wing Healing

			#region Vortex Implosion

			#region Vortex Cannon implosion mechanic

			if (Player.HeldItem.type == ModContent.ItemType<VortexCannon>())
			{
				if (Player.HasItem(ModContent.ItemType<VortexCannon>()) && Player.HasItem(ModContent.ItemType<VortexRevolver>()))
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

			if (Player.HeldItem.type == ModContent.ItemType<VortexRevolver>())
			{
				if (Player.HasItem(ModContent.ItemType<VortexCannon>()) && Player.HasItem(ModContent.ItemType<VortexRevolver>()))
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

			if (Player.HeldItem.type == ModContent.ItemType<HellfireRevolver>() || Player.HeldItem.type == ModContent.ItemType<HellfireShotgun>())
			{
				if (Player.HasItem(ModContent.ItemType<HellfireRevolver>()) && Player.HasItem(ModContent.ItemType<HellfireShotgun>()))
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

			#region Doom Damage
			if (Doom)
			{
				
				for (int j = 0; j < Player.MaxBuffs; ++j)
				{
					if(Player.buffType[j] == ModContent.BuffType<Doomed>())
					{
						DoomBuff = j;
					}
				}
				if(Player.buffTime[DoomBuff] <= 10)
				{
					Player.Hurt(PlayerDeathReason.ByCustomReason("DEATH!!!!!!!!!!!!!!!!!!!!!!!!!!!"), Player.statLifeMax2 * 999, 0, true, false, true);
				}
			}

			#endregion

		}

		public override void PreUpdate()
		{

			#region Shadow Blaster Effect

			if (Player.HasItem(ModContent.ItemType<ShadowBlaster>()) && Player.HeldItem.type == ModContent.ItemType<ShadowBlasterGun>())
			{
				shadow = true;
			}

			#endregion Shadow Blaster Effect

			#region Limit Stuff

			TimeDiff++;

			#endregion Limit Stuff
		}

		public override void PostUpdateRunSpeeds()
		{
			#region Thunder Core Speed

			if (Player.HeldItem.type == ModContent.ItemType<ThunderCore>() && Player.HasItem(ModContent.ItemType<ThunderCoreGun>()))
			{
				Player.maxRunSpeed += 1.2f;
				Player.moveSpeed += 1.2f;
				Player.accRunSpeed += 1.2f;
			}

			#endregion Thunder Core Speed

			#region Dragon's Feather Speed

			if (Player.HeldItem.type == ModContent.ItemType<DragonsFeather>())
			{
				Player.maxRunSpeed += 1.5f;
				Player.moveSpeed += 1.5f;
				Player.accRunSpeed += 1.5f;
			}

			#endregion Dragon's Feather Speed

			#region Haste Speed

			if (Player.HasBuff(ModContent.BuffType<HasteBuff>()))
			{
				Player.maxRunSpeed += 2f;
				Player.accRunSpeed += 2f;
				Player.moveSpeed += 2f;
			}

			#endregion Haste Speed

			#region Sugar Rush Speed

			if (Player.HasBuff(ModContent.BuffType<SugarRush>()))
			{
				Player.maxRunSpeed += 2f;
				Player.moveSpeed += 1f;
			}

			#endregion Sugar Rush Speed

			#region King's Guard Shield Speed

			for (int i = 3; i < 8 + Player.extraAccessorySlots; i++)
			{
				if (Player.armor[i].type == ModContent.ItemType<KingsGuardShield>())
				{
					Player.maxRunSpeed *= 0.70f;
					Player.accRunSpeed *= 0.70f;
				}
			}

			#endregion King's Guard Shield Speed

			#region Tired Speed

			if (Tired)
			{
				TiredPower = TiredStacks * 0.1f;

				if (TiredStacks < 5)
				{
					Player.maxRunSpeed *= (1f - TiredPower);
					Player.accRunSpeed *= (1f - TiredPower);
					Player.moveSpeed *= (1f - TiredPower);
				}
				else if (TiredStacks >= 5)
				{
					Player.maxRunSpeed *= 0.5f;
					Player.accRunSpeed *= 0.5f;
					Player.moveSpeed *= 0.5f;
				}
			}

			#endregion Tired Speed

			#region Heaven's speed

			if (Player.HasBuff(ModContent.BuffType<Kyun>()))
			{
				Player.maxRunSpeed += 1.5f;
				Player.accRunSpeed += 1.5f;
				Player.moveSpeed += 1.5f;
			}

			#endregion Heaven's speed
		}

		public override void ModifyWeaponCrit(Item item, ref int crit)
		{
			#region Gungnir Crit

			if (Player.HasItem(ModContent.ItemType<GungnirRifle>()) && Player.HasItem(ModContent.ItemType<GungnirRevolver>()) && (Player.HeldItem.type == ModContent.ItemType<GungnirRifle>() || Player.HeldItem.type == ModContent.ItemType<GungnirRevolver>()))
			{
				crit += 15;
			}

			#endregion Gungnir Crit
		}

		public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
		{
			#region Rampant Bleeding Dust

			if (RBleed)
			{
				if (RBleedStacks <= 5)
				{
					if (Main.rand.NextFloat() <= .1f)
					{
						Dust.NewDustDirect(Player.position - new Vector2(2f, 2f), Player.width, Player.height, DustID.Blood, 0f, 0f, 0, new Color(255, 255, 255), 1f);
					}
				}
				else if (RBleedStacks > 5 && RBleedStacks <= 10)
				{
					if (Main.rand.NextFloat() <= .2f)
					{
						Dust.NewDustDirect(Player.position - new Vector2(2f, 2f), Player.width, Player.height, DustID.Blood, 0f, 0f, 0, new Color(255, 255, 255), 1f);
					}
				}
				else if (RBleedStacks > 10 && RBleedStacks <= 20)
				{
					if (Main.rand.NextFloat() <= .4f)
					{
						Dust.NewDustDirect(Player.position - new Vector2(2f, 2f), Player.width, Player.height, DustID.Blood, 0f, 0f, 0, new Color(255, 255, 255), 1f);
					}
				}
				else if (RBleedStacks > 20)
				{
					Dust.NewDustDirect(Player.position - new Vector2(2f, 2f), Player.width, Player.height, DustID.Blood, 0f, 0f, 0, new Color(255, 255, 255), 1f);
				}
			}

			#endregion Rampant Bleeding Dust
		}


		#region ModifyDrawLayers

		/*public static readonly PlayerLayer MiscEffects = new PlayerLayer("EpicBattleFantasyUltimate", "MiscEffects", PlayerLayer.MiscEffectsFront, delegate (PlayerDrawInfo drawInfo)
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
				Main.PlayerDrawData.Add(data);

				drawX += drawableBuffOffset;
			}

			if (modPlayer.Blessed)
			{
				// Do drawing.
				Texture2D texture = mod.GetTexture("Buffs/Buffs/BlessedEffect");

				Color alpha2 = Lighting.GetColor((int)((drawInfo.position.X + drawPlayer.width / 2f) / 16f), (int)((drawInfo.position.Y - 4f - texture.Height / 2f) / 16f));
				DrawData data = new DrawData(texture, new Vector2(drawX, drawY - 5), null, alpha2 * 1f, 0f, new Vector2(texture.Width / 2, texture.Height), 1f, SpriteEffects.None, 0);
				Main.PlayerDrawData.Add(data);

				drawX += drawableBuffOffset;
			}

			if (modPlayer.Doom)
			{
				Texture2D texture = mod.GetTexture("Buffs/Debuffs/DoomEffect");

				Color alpha2 = Lighting.GetColor((int)((drawInfo.position.X + drawPlayer.width / 2f) / 16f), (int)((drawInfo.position.Y - 4f - texture.Height / 2f) / 16f));
				DrawData data = new DrawData(texture, new Vector2(drawX, drawY - 5), null, alpha2 * 1f, 0f, new Vector2(texture.Width / 2, texture.Height), 1f, SpriteEffects.None, 0);
				Main.PlayerDrawData.Add(data);

				drawX += drawableBuffOffset;
			}
		});
		public override void ModifyDrawLayerOrdering(IDictionary<PlayerDrawLayer, PlayerDrawLayer.Position> positions)
		{
			if (Player.statLife > 0)
			{
				MiscEffects.visible = true;
				layers.Add(MiscEffects);
			}
		}*/
		#endregion ModifyDrawLayers
	}
}