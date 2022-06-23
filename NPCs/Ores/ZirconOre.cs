﻿using EpicBattleFantasyUltimate.ClassTypes;
using EpicBattleFantasyUltimate.Items.Materials;
using EpicBattleFantasyUltimate.Projectiles.NPCProj.OreExplosions;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.NPCs.Ores
{
    public class ZirconOre : OreNPC
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zircon Ore");
            Main.npcFrameCount[NPC.type] = 6;
        }

        public override void SetSafeDefaults()
        {
            NPC.width = 40;
            NPC.height = 40;

            NPC.lifeMax = 90;
            NPC.damage = 30;
            NPC.defense = 5;
            NPC.lifeRegen = 4;
            NPC.knockBackResist = -0.2f;

            NPC.noGravity = true;

            DrawOffsetY = -5;

            NPC.noTileCollide = true;
            NPC.aiStyle = -1;

            Explosion = ModContent.ProjectileType<TopazExplosion>();

            MoveSpeedMultval = 10f;
            MoveSpeedBalval = 50;
            SpeedBalance = 100f;

            DashCooldown = 5;

            DashDistance = 10f;
            DashCharge = 120;
            DashVelocity = 12f;
            DashDuration = 180;//Should be higher than the Dash Charge

            StunDuration = 5;//In seconds
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
				// Sets the spawning conditions of this NPC that is listed in the bestiary.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundSnow,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("Concentrated frosty magic in a rocky shell, bursting into magic to help Sprites naturally develop.")
            });
        }

        #region FindFrame

        public override void FindFrame(int frameHeight)
        {
            if (++NPC.frameCounter >= 7)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y = (NPC.frame.Y + frameHeight) % (frameHeight * Main.npcFrameCount[NPC.type]);
            }
        }

        #endregion FindFrame

        public override bool CheckDead()
        {
            int goreIndex = Gore.NewGore(NPC.GetSource_Death(), NPC.position, (NPC.velocity * NPC.direction) * -1, Mod.Find<ModGore>("ZirconOre_Gore1").Type, 1f);
            int goreIndex2 = Gore.NewGore(NPC.GetSource_Death(), NPC.position, (NPC.velocity * NPC.direction), Mod.Find<ModGore>("ZirconOre_Gore2").Type, 1f);
            int goreIndex3 = Gore.NewGore(NPC.GetSource_Death(), NPC.position, (NPC.velocity * NPC.direction) * -1, Mod.Find<ModGore>("ZirconOre_Gore3").Type, 1f);
            int goreIndex4 = Gore.NewGore(NPC.GetSource_Death(), NPC.position, (NPC.velocity * NPC.direction), Mod.Find<ModGore>("ZirconOre_Gore4").Type, 1f);

            for (int i = 0; i <= 15; i++)
            {
                Dust.NewDustDirect(NPC.Center, NPC.width, NPC.height, DustID.Stone, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), Scale: 1);
            }
            for (int j = 0; j <= 5; j++)
            {
                Dust.NewDustDirect(NPC.Center, NPC.width, NPC.height, DustID.Ice, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), Scale: 1);
            }

            return true;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SolidWater>(), 1 , 2,2));
        }
    }
}