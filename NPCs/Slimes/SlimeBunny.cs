/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;


namespace EpicBattleFantasyUltimate.NPCs.Slimes
{
    public class SlimeBunny: ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Slime Bunny");
        }

        public override void SetDefaults()
        {
            NPC.width = 36;
            NPC.height = 48;

            NPC.lifeMax = 220;
            NPC.damage = 10;
            NPC.defense = 6;
            NPC.lifeRegen = 4;
            NPC.value = 50;

            NPC.aiStyle = -1;
            NPC.noGravity = false;

            if (Main.hardMode)
            {
                NPC.lifeMax *= 2;
                NPC.defense *= 2;
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
				// Sets the spawning conditions of this NPC that is listed in the bestiary.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Marble,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("A Slime with Bunny ears and a carrot. HOW CUTE.")
            });
        }
    }
}*/
