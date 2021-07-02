using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.NPCs.Monoliths
{
	class TestMonolith : ModNPC
	{

		int num2;


		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Test Monolith");
		}

		public override void SetDefaults()
		{
			npc.height = 100;
			npc.width = 30;

			npc.lifeMax = 100;
			npc.defense = 1000;

			npc.knockBackResist = 1;
			npc.aiStyle = -1;
		}

		public override bool PreAI()
		{
			return false;
		}
	}  
}
