using EpicBattleFantasyUltimate.Items.Consumables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace EpicBattleFantasyUltimate.NPCs
{
	public class EpicGlobalNPC : GlobalNPC
	{
		#region Rampant Bleed Variables

		public bool RBleed = true;
		public int RBleedStacks;

		public override bool InstancePerEntity => true;

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
		public int CursedStacks;
		public int CursedAlpha = 0;
		private double CursedDefense;
		private double CursedMult;

		#endregion Cursed Variables

		public int bossesDefeated = 0;

		#region Electrified Variables

		public bool Electrified = false;

		#endregion Electrified Variables

		#region ResetEffects

		public override void ResetEffects(NPC npc)
		{
			#region Feral Bleed Reset

			if (RBleed == false)
			{
				RBleedStacks = 0;
			}
			RBleed = false;

			#endregion Feral Bleed Reset

			#region Weakened Reset

			if (Weakened == false)
			{
				WeakenedStacks = 0;
			}

			Weakened = false;

			#endregion Weakened Reset

			#region Cursed Reset

			if (Cursed == false)
			{
				CursedStacks = 0;
			}

			Cursed = false;

			#endregion Cursed Reset

			Electrified = false;
		}

		#endregion ResetEffects

		#region ModifyHitPlayer

		public override void ModifyHitPlayer(NPC npc, Player target, ref int damage, ref bool crit)
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
				damage = (int)(damage * 0.50f);
			}

			#endregion Weakened Weakening
		}

		#endregion ModifyHitPlayer

		#region PostAI

		public override void PostAI(NPC npc)
		{
			#region Cursed Effects

			if (Cursed)
			{
				if (CursedStacks <= 5)
				{
					CursedMult = 0.1 * CursedStacks;
					CursedDefense = 1 - CursedMult;
					npc.defense = (int)(npc.defense * CursedDefense);
				}
				else
				{
					npc.defense = (int)(npc.defense * 0.5);
				}
			}
			else
			{
				npc.defense = npc.defDefense;
			}

			if (CursedStacks == 1)
			{
				CursedAlpha = 25;
			}
			else if (CursedStacks == 2)
			{
				CursedAlpha = 75;
			}
			else if (CursedStacks == 3)
			{
				CursedAlpha = 150;
			}
			else if (CursedStacks == 4)
			{
				CursedAlpha = 255;
			}
			else if (CursedStacks >= 5)
			{
				if (CursedAlphaCheck == true)
				{
					CursedAlpha -= 10;
				}
				else if (CursedAlphaCheck == false)
				{
					CursedAlpha += 10;
				}

				if (CursedAlpha >= 255)
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

		#endregion PostAI

		#region UpdateLifeRegen

		public override void UpdateLifeRegen(NPC npc, ref int damage)
		{
			#region Feral Bleed Effects

			if (RBleed == true)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}

				npc.lifeRegen -= RBleedStacks * 10;
			}

			#endregion Feral Bleed Effects

			#region Electrified Effects

			if (Electrified)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}

				npc.lifeRegen -= 8;
				if (npc.velocity.X != 0f)
				{
					npc.lifeRegen -= 32;
				}
			}

			#endregion Electrified Effects
		}

		#endregion UpdateLifeRegen

		#region SetupShop

		public override void SetupShop(int type, Chest shop, ref int nextSlot)
		{
			if (type == NPCID.PartyGirl)
			{
				shop.item[nextSlot].SetDefaults(ItemType<Cake>());
				shop.item[nextSlot].shopCustomPrice = 1000000;
				nextSlot++;
			}
		}

		#endregion SetupShop

		#region NPCLoot

		public override void NPCLoot(NPC npc)
		{
			#region if boss

			if (npc.boss)
			{
				Item.NewItem(npc.getRect(), mod.ItemType("DarkMatter"), Main.rand.Next(2) + 1);

				#region unique boss count

				//unique boss count

				if (NPC.killCount[npc.type] <= 0)
				{
					EpicWorld.bossesDefeated++;
				}

				/*for (int i = 0; i < NPCLoader.NPCCount; ++i)
				{
					npc.SetDefaults(i);

					if (npc.boss && NPC.killCount[i] > 0)
					{
						EpicWorld.bossesDefeated++;
					}
				}*/

				#endregion unique boss count
			}

			#endregion if boss
		}

		#endregion NPCLoot

		public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
		{
			if (EpicWorld.OreEvent)
			{
				maxSpawns *= 2;
				spawnRate *= (int)0.7f;
			}
		}

		#region DrawEffects

		public override void DrawEffects(NPC npc, ref Color drawColor)
		{
			#region Rampant Bleeding Dust

			if (RBleed)
			{
				if (RBleedStacks <= 5)
				{
					if (Main.rand.NextFloat() <= .1f)
					{
						Dust.NewDustDirect(npc.position - new Vector2(2f, 2f), npc.width, npc.height, DustID.Blood, 0f, 0f, 0, new Color(255, 255, 255), 1f);
					}
				}
				else if (RBleedStacks > 5 && RBleedStacks <= 10)
				{
					if (Main.rand.NextFloat() <= .2f)
					{
						Dust.NewDustDirect(npc.position - new Vector2(2f, 2f), npc.width, npc.height, DustID.Blood, 0f, 0f, 0, new Color(255, 255, 255), 1f);
					}
				}
				else if (RBleedStacks > 10 && RBleedStacks <= 20)
				{
					if (Main.rand.NextFloat() <= .4f)
					{
						Dust.NewDustDirect(npc.position - new Vector2(2f, 2f), npc.width, npc.height, DustID.Blood, 0f, 0f, 0, new Color(255, 255, 255), 1f);
					}
				}
				else if (RBleedStacks > 20)
				{
					Dust.NewDustDirect(npc.position - new Vector2(2f, 2f), npc.width, npc.height, DustID.Blood, 0f, 0f, 0, new Color(255, 255, 255), 1f);
				}
			}

			#endregion Rampant Bleeding Dust

			#region Electrified Dust

			if (Electrified)
			{
				Dust dust = Dust.NewDustDirect(npc.position - new Vector2(2f, 2f), npc.width, npc.height, DustID.Electric, 0f, 0f, 0, new Color(255, 255, 255), 1f);
				dust.noGravity = true;
			}

			#endregion Electrified Dust
		}

		#endregion DrawEffects

		#region PostDraw

		public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
		{
			if (Cursed)
			{
				Texture2D tex = mod.GetTexture("Buffs/Debuffs/CursedEffect");
				Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, tex.Height * 0.5f);

				Vector2 drawPos = npc.Center - new Vector2(0, 15 + npc.height / 2) - Main.screenPosition;

				spriteBatch.Draw(tex, drawPos, new Rectangle(0, 0, tex.Width, tex.Height), new Color(255, 255, 255, CursedAlpha), 0, drawOrigin, 1, SpriteEffects.None, 0f);
			}
		}

		#endregion PostDraw
	}
}