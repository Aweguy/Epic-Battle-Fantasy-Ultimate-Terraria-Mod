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


namespace EpicBattleFantasyUltimate.ClassTypes
{
	public abstract class EpicPiercingBow : ModItem
	{
		public override bool CloneNewInstances => true;

		public virtual void SetSafeDefaults()
		{
		}

		public virtual void SetSafeStaticDefaults()
		{
		}

		public override void SetStaticDefaults()
		{
			SetSafeStaticDefaults();
		}

		public override void SetDefaults()
		{
			SetSafeDefaults();
		}



	}
}
