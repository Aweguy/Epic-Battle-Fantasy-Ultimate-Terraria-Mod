using EpicBattleFantasyUltimate.Projectiles;
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


namespace EpicBattleFantasyUltimate.Items.Spells
{
    public abstract class Judi : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Judi");
            Tooltip.SetDefault("A charging beam that its damage and range increase while it's charging");
        }

        public override void SetDefaults()
        {
			item.damage = 40;
			item.noMelee = true;
			item.magic = true;
			item.channel = true; //Channel so that you can hold the weapon [Important]
			item.mana = 5;
			item.rare = ItemRarityID.Pink;
			item.width = 28;
			item.height = 30;
			item.useTime = 20;
			item.UseSound = SoundID.Item13;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.shootSpeed = 14f;
			item.useAnimation = 20;
			item.shoot = ModContent.ProjectileType<JudiBeam>();
			item.value = Item.sellPrice(silver: 3);
		}
    }
}
