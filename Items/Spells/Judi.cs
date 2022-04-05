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
			Item.damage = 40;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Magic;
			Item.channel = true; //Channel so that you can hold the weapon [Important]
			Item.mana = 5;
			Item.rare = ItemRarityID.Pink;
			Item.width = 28;
			Item.height = 30;
			Item.useTime = 20;
			Item.UseSound = SoundID.Item13;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.shootSpeed = 14f;
			Item.useAnimation = 20;
			Item.shoot = ModContent.ProjectileType<JudiBeam>();
			Item.value = Item.sellPrice(silver: 3);
		}
    }
}
